//Code for GameUI/DirectionPanelComponent (Container)
using Gum;
using Gum.Converters;
using Gum.DataTypes;
using Gum.GueDeriving;
using Gum.Managers;
using Gum.Wireframe;
using GUMProject.Components.Controls;
using GUMProject.Components.GameUI.Pieces;
using GumRuntime;
using RenderingLibrary.Graphics;
using System.Linq;
namespace GUMProject.Components.GameUI;
[global::Gum.DataTypes.GumElement("GameUI/DirectionPanelComponent")]
partial class DirectionPanelComponent : global::Gum.Forms.Controls.FrameworkElement
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
            var element = ObjectFinder.Self.GetRequiredElementSave("GameUI/DirectionPanelComponent");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new DirectionPanelComponent(visual);
            return visual;
        });
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(DirectionPanelComponent)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("GameUI/DirectionPanelComponent", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public MotherPanelBackground MotherPanelBackgroundInstance { get; protected set; }
    public TextRuntime TextInstance { get; protected set; }
    public Panel DirectionPanel { get; protected set; }
    public ArrowButtom DirectionBottomNorth { get; protected set; }
    public ArrowButtom DirectionBottomEast { get; protected set; }
    public ArrowButtom DirectionBottomSouth { get; protected set; }
    public ArrowButtom DirectionBottomWest { get; protected set; }

    public string DirectionText
    {
        get => TextInstance.Text;
        set => TextInstance.Text = value;
    }

    public DirectionPanelComponent(InteractiveGue visual) : base(visual)
    {
    }
    public DirectionPanelComponent()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        MotherPanelBackgroundInstance = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<MotherPanelBackground>(this.Visual,"MotherPanelBackgroundInstance");
        TextInstance = this.Visual?.GetGraphicalUiElementByName("TextInstance") as global::Gum.GueDeriving.TextRuntime;
        DirectionPanel = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<Panel>(this.Visual,"DirectionPanel");
        DirectionBottomNorth = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<ArrowButtom>(this.Visual,"DirectionBottomNorth");
        DirectionBottomEast = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<ArrowButtom>(this.Visual,"DirectionBottomEast");
        DirectionBottomSouth = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<ArrowButtom>(this.Visual,"DirectionBottomSouth");
        DirectionBottomWest = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<ArrowButtom>(this.Visual,"DirectionBottomWest");
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
