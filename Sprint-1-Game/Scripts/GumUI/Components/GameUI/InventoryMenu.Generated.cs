//Code for GameUI/InventoryMenu (Container)
using Gum;
using Gum.Converters;
using Gum.DataTypes;
using Gum.GueDeriving;
using Gum.Managers;
using Gum.Wireframe;
using GUMProject.Components.Controls;
using GUMProject.Components.GameUI;
using GUMProject.Components.GameUI.Pieces;
using GumRuntime;
using RenderingLibrary.Graphics;
using System.Linq;
namespace GUMProject.Components.GameUI;
[global::Gum.DataTypes.GumElement("GameUI/InventoryMenu")]
partial class InventoryMenu : global::Gum.Forms.Controls.FrameworkElement
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
            var element = ObjectFinder.Self.GetRequiredElementSave("GameUI/InventoryMenu");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new InventoryMenu(visual);
            return visual;
        });
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(InventoryMenu)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("GameUI/InventoryMenu", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public TitleBar TitleBarInstance { get; protected set; }
    public ContainerRuntime MainContainer { get; protected set; }
    public NineSliceRuntime Background { get; protected set; }
    public Panel InventoryPanel { get; protected set; }
    public InventorySlot InventorySlotInstance1 { get; protected set; }
    public InventorySlot InventorySlotInstance2 { get; protected set; }
    public InventorySlot InventorySlotInstance3 { get; protected set; }

    public InventoryMenu(InteractiveGue visual) : base(visual)
    {
    }
    public InventoryMenu()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        TitleBarInstance = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<TitleBar>(this.Visual,"TitleBarInstance");
        MainContainer = this.Visual?.GetGraphicalUiElementByName("MainContainer") as global::Gum.GueDeriving.ContainerRuntime;
        Background = this.Visual?.GetGraphicalUiElementByName("Background") as global::Gum.GueDeriving.NineSliceRuntime;
        InventoryPanel = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<Panel>(this.Visual,"InventoryPanel");
        InventorySlotInstance1 = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<InventorySlot>(this.Visual,"InventorySlotInstance1");
        InventorySlotInstance2 = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<InventorySlot>(this.Visual,"InventorySlotInstance2");
        InventorySlotInstance3 = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<InventorySlot>(this.Visual,"InventorySlotInstance3");
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
