//Code for PuzzleEditorUI
using Gum;
using Gum.Converters;
using Gum.DataTypes;
using Gum.GueDeriving;
using Gum.Managers;
using Gum.Wireframe;
using GUMProject.Components.Controls;
using GUMProject.Components.GameUI;
using GumRuntime;
using RenderingLibrary.Graphics;
using System.Linq;
namespace GUMProject.Screens;
[global::Gum.DataTypes.GumElement("PuzzleEditorUI")]
partial class PuzzleEditorUI : global::Gum.Forms.Controls.FrameworkElement
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
            var element = ObjectFinder.Self.GetRequiredElementSave("PuzzleEditorUI");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new PuzzleEditorUI(visual);
            visual.Width = 0;
            visual.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
            visual.Height = 0;
            visual.HeightUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
            return visual;
        });
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(PuzzleEditorUI)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("PuzzleEditorUI", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public ButtonStandard ButtonStandardInstance1 { get; protected set; }
    public DirectionPanelComponent DirectionPanelComponentInstance { get; protected set; }
    public ButtonStandard ButtonStandardInstance { get; protected set; }

    public PuzzleEditorUI(InteractiveGue visual) : base(visual)
    {
    }
    public PuzzleEditorUI()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        ButtonStandardInstance1 = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<ButtonStandard>(this.Visual,"ButtonStandardInstance1");
        DirectionPanelComponentInstance = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<DirectionPanelComponent>(this.Visual,"DirectionPanelComponentInstance");
        ButtonStandardInstance = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<ButtonStandard>(this.Visual,"ButtonStandardInstance");
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
