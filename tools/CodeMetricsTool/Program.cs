// CodeMetricsTool: computes Visual Studio-style code metrics (Maintainability Index,
// Cyclomatic Complexity, Class Coupling, Depth of Inheritance, Source/Executable lines)
// using the same Roslyn library that Microsoft's Metrics.exe uses, but on a modern
// Roslyn (5.9) whose MSBuild loader works with Visual Studio 2026 / MSBuild 18.
//
// Usage: CodeMetricsTool <path-to.csproj> [output-folder] [label]

using System.Globalization;
using System.Text;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeMetrics;
using Microsoft.CodeAnalysis.MSBuild;

if (args.Length < 1)
{
    Console.Error.WriteLine("Usage: CodeMetricsTool <path-to.csproj> [output-folder] [label]");
    return 2;
}

string projectPath = Path.GetFullPath(args[0]);
string outDir = Path.GetFullPath(args.Length > 1 ? args[1] : "metrics");
string label = args.Length > 2 ? args[2] : "";
Directory.CreateDirectory(outDir);

CultureInfo inv = CultureInfo.InvariantCulture;

// Must run before the workspace is created: points Roslyn at the installed .NET SDK's MSBuild
// (the same thing Microsoft's Metrics.exe does at startup).
MSBuildLocator.RegisterDefaults();

using MSBuildWorkspace workspace = MSBuildWorkspace.Create();
workspace.WorkspaceFailed += (_, e) =>
    Console.Error.WriteLine($"[workspace {e.Diagnostic.Kind}] {e.Diagnostic.Message}");

Console.WriteLine($"Loading {projectPath} ...");
Project project = await workspace.OpenProjectAsync(projectPath);
Compilation? compilation = await project.GetCompilationAsync();
if (compilation is null)
{
    Console.Error.WriteLine("Could not create a compilation for the project.");
    return 1;
}

int errorCount = compilation.GetDiagnostics().Count(d => d.Severity == DiagnosticSeverity.Error);
if (errorCount > 0)
{
    Console.Error.WriteLine(
        $"WARNING: the compilation has {errorCount} error(s) (often missing NuGet references). " +
        "Run 'dotnet restore' / 'dotnet build' on the game first, otherwise the metrics may be incomplete.");
}

CodeAnalysisMetricData root = await CodeAnalysisMetricData.ComputeAsync(
    compilation.Assembly, new CodeMetricsAnalysisContext(compilation, CancellationToken.None));

// ---- detail rows (assembly > namespace > type > member) ----
var detail = new List<string>
{
    "Level,Name,MaintainabilityIndex,CyclomaticComplexity,ClassCoupling,DepthOfInheritance,SourceLines,ExecutableLines"
};
int typeCount = 0;
int maxDepth = 0;

void Walk(CodeAnalysisMetricData d)
{
    ISymbol s = d.Symbol;
    if (!IsGenerated(s))
    {
        detail.Add(string.Join(",",
            s.Kind.ToString(),
            Csv(s.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat)),
            d.MaintainabilityIndex.ToString(inv),
            d.CyclomaticComplexity.ToString(inv),
            d.CoupledNamedTypes.Count.ToString(inv),
            d.DepthOfInheritance?.ToString(inv) ?? "",
            d.SourceLines.ToString(inv),
            d.ExecutableLines.ToString(inv)));
    }

    if (s.Kind == SymbolKind.NamedType && !IsGenerated(s))
    {
        typeCount++;
        if (d.DepthOfInheritance is int depth && depth > maxDepth)
        {
            maxDepth = depth;
        }
    }

    foreach (CodeAnalysisMetricData child in d.Children)
    {
        Walk(child);
    }
}

static bool IsGenerated(ISymbol s)
{
    var inSource = s.Locations.Where(l => l.IsInSource).ToList();
    return inSource.Count > 0 && inSource.All(l =>
    {
        string p = l.SourceTree?.FilePath ?? "";
        return p.Contains("/obj/") || p.Contains("\\obj\\");
    });
}

static string Csv(string v) => "\"" + v.Replace("\"", "\"\"") + "\"";

Walk(root);

string stamp = DateTime.Now.ToString("yyyy-MM-dd", inv);
string detailPath = Path.Combine(outDir, $"metrics-detail-{stamp}.csv");
File.WriteAllLines(detailPath, detail, new UTF8Encoding(false));

// ---- one summary row appended per run (for weekly comparison / graphs) ----
string historyPath = Path.Combine(outDir, "history.csv");
if (!File.Exists(historyPath))
{
    File.AppendAllText(historyPath,
        "Date,Label,Assembly,MaintainabilityIndex,CyclomaticComplexity,ClassCoupling,MaxDepthOfInheritance,SourceLines,ExecutableLines,Types\n",
        new UTF8Encoding(false));
}

string summary = string.Join(",",
    stamp,
    Csv(label),
    Csv(compilation.AssemblyName ?? ""),
    root.MaintainabilityIndex.ToString(inv),
    root.CyclomaticComplexity.ToString(inv),
    root.CoupledNamedTypes.Count.ToString(inv),
    maxDepth.ToString(inv),
    root.SourceLines.ToString(inv),
    root.ExecutableLines.ToString(inv),
    typeCount.ToString(inv));
File.AppendAllText(historyPath, summary + "\n", new UTF8Encoding(false));

Console.WriteLine($"Detail : {detailPath}");
Console.WriteLine($"History: {historyPath}");
Console.WriteLine(
    $"Summary: MI={root.MaintainabilityIndex}  CC={root.CyclomaticComplexity}  " +
    $"Coupling={root.CoupledNamedTypes.Count}  MaxDepth={maxDepth}  " +
    $"SLOC={root.SourceLines}  ELOC={root.ExecutableLines}  Types={typeCount}");
return 0;