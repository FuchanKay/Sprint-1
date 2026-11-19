//Code for GameUI/Pieces/MotherPanelBackground (Container)
using Gum;
using Gum.Converters;
using Gum.DataTypes;
using Gum.GueDeriving;
using Gum.Managers;
using Gum.Wireframe;
using GumRuntime;
using RenderingLibrary.Graphics;
using System.Linq;
namespace GUMProject.Components.GameUI.Pieces;
[global::Gum.DataTypes.GumElement("GameUI/Pieces/MotherPanelBackground")]
partial class MotherPanelBackground : global::Gum.Forms.Controls.FrameworkElement
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
            var element = ObjectFinder.Self.GetRequiredElementSave("GameUI/Pieces/MotherPanelBackground");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new MotherPanelBackground(visual);
            return visual;
        });
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(MotherPanelBackground)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("GameUI/Pieces/MotherPanelBackground", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public NineSliceRuntime Background { get; protected set; }

    public int BackgroundBlue
    {
        get => Background.Blue;
        set => Background.Blue = value;
    }

    public int BackgroundGreen
    {
        get => Background.Green;
        set => Background.Green = value;
    }

    public int BackgroundRed
    {
        get => Background.Red;
        set => Background.Red = value;
    }

    public MotherPanelBackground(InteractiveGue visual) : base(visual)
    {
    }
    public MotherPanelBackground()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        Background = this.Visual?.GetGraphicalUiElementByName("Background") as global::Gum.GueDeriving.NineSliceRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
