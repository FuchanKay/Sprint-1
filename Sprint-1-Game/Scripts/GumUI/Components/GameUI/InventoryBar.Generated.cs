//Code for GameUI/InventoryBar (Container)
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using GUMProject.Components.Controls;
using GUMProject.Components.GameUI;
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using RenderingLibrary.Graphics;
using System.Linq;
namespace GUMProject.Components.GameUI;
partial class InventoryBar : global::Gum.Forms.Controls.FrameworkElement
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
            var visual = new global::MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("GameUI/InventoryBar") ?? throw new System.InvalidOperationException("Could not find an element named GameUI/InventoryBar - did you forget to load a Gum project?");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new InventoryBar(visual);
            return visual;
        });
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(InventoryBar)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("GameUI/InventoryBar", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public Panel InventoryBarPanel { get; protected set; }
    public InventorySlot InventorySlotInstance1 { get; protected set; }
    public InventorySlot InventorySlotInstance2 { get; protected set; }
    public InventorySlot InventorySlotInstance3 { get; protected set; }

    public InventoryBar(InteractiveGue visual) : base(visual)
    {
    }
    public InventoryBar()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        InventoryBarPanel = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<Panel>(this.Visual,"InventoryBarPanel");
        InventorySlotInstance1 = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<InventorySlot>(this.Visual,"InventorySlotInstance1");
        InventorySlotInstance2 = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<InventorySlot>(this.Visual,"InventorySlotInstance2");
        InventorySlotInstance3 = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<InventorySlot>(this.Visual,"InventorySlotInstance3");
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
