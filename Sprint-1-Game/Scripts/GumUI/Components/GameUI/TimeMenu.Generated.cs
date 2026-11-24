//Code for GameUI/TimeMenu (Container)
using Gum;
using Gum.Converters;
using Gum.DataTypes;
using Gum.GueDeriving;
using Gum.Managers;
using Gum.Wireframe;
using GUMProject.Components.GameUI.Pieces;
using GumRuntime;
using RenderingLibrary.Graphics;
using System.Linq;
namespace GUMProject.Components.GameUI;
[global::Gum.DataTypes.GumElement("GameUI/TimeMenu")]
partial class TimeMenu : global::Gum.Forms.Controls.FrameworkElement
{
    #if UNITY_5_3_OR_NEWER && !GUM_NO_UNITY_ENGINE
    [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
    #else
    [System.Runtime.CompilerServices.ModuleInitializer]
    #endif
    public static void RegisterRuntimeType()
    {
        var template = new global::Gum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new global::Gum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetRequiredElementSave("GameUI/TimeMenu");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new TimeMenu(visual);
            return visual;
        });
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(TimeMenu)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("GameUI/TimeMenu", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public TitleBar TitleBarInstance { get; protected set; }
    public ContainerRuntime MainContainer { get; protected set; }
    public NineSliceRuntime Background { get; protected set; }
    public TextRuntime Time { get; protected set; }

    public string TimeText
    {
        get => Time.Text;
        set => Time.Text = value;
    }

    public TimeMenu(InteractiveGue visual) : base(visual)
    {
    }
    public TimeMenu()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        TitleBarInstance = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<TitleBar>(this.Visual,"TitleBarInstance");
        MainContainer = this.Visual?.GetGraphicalUiElementByName("MainContainer") as global::Gum.GueDeriving.ContainerRuntime;
        Background = this.Visual?.GetGraphicalUiElementByName("Background") as global::Gum.GueDeriving.NineSliceRuntime;
        Time = this.Visual?.GetGraphicalUiElementByName("Time") as global::Gum.GueDeriving.TextRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
