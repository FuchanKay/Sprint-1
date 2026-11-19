//Code for GameUI/InventorySlot (Container)
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
[global::Gum.DataTypes.GumElement("GameUI/InventorySlot")]
partial class InventorySlot : global::Gum.Forms.Controls.FrameworkElement
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
            var element = ObjectFinder.Self.GetRequiredElementSave("GameUI/InventorySlot");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new InventorySlot(visual);
            return visual;
        });
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(InventorySlot)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("GameUI/InventorySlot", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public enum HasItem
    {
        True,
        False,
    }

    HasItem? _hasItemState;
    public HasItem? HasItemState
    {
        get => _hasItemState;
        set
        {
            _hasItemState = value;
            if(value != null)
            {
                if(Visual.Categories.ContainsKey("HasItem"))
                {
                    var category = Visual.Categories["HasItem"];
                    var state = category.States.Find(item => item.Name == value.ToString());
                    this.Visual.ApplyState(state);
                }
                else
                {
                    var category = ((global::Gum.DataTypes.ElementSave)this.Visual.Tag).Categories.FirstOrDefault(item => item.Name == "HasItem");
                    var state = category.States.Find(item => item.Name == value.ToString());
                    this.Visual.ApplyState(state);
                }
            }
        }
    }
    public ItemBackground ItemBackgroundInstance { get; protected set; }
    public ItemIcon ItemIconInstance { get; protected set; }
    public SlotNumber SlotNumberInstance { get; protected set; }
    public NineSliceRuntime HighLightIndicator { get; protected set; }

    public bool Selected
    {
        get => HighLightIndicator.Visible;
        set => HighLightIndicator.Visible = value;
    }

    public int IconLeft
    {
        get => ItemIconInstance.IconLeft;
        set => ItemIconInstance.IconLeft = value;
    }

    public int IconTop
    {
        get => ItemIconInstance.IconTop;
        set => ItemIconInstance.IconTop = value;
    }

    public string SlotNumber
    {
        get => SlotNumberInstance.NumberText;
        set => SlotNumberInstance.NumberText = value;
    }

    public InventorySlot(InteractiveGue visual) : base(visual)
    {
    }
    public InventorySlot()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        ItemBackgroundInstance = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<ItemBackground>(this.Visual,"ItemBackgroundInstance");
        ItemIconInstance = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<ItemIcon>(this.Visual,"ItemIconInstance");
        SlotNumberInstance = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<SlotNumber>(this.Visual,"SlotNumberInstance");
        HighLightIndicator = this.Visual?.GetGraphicalUiElementByName("HighLightIndicator") as global::Gum.GueDeriving.NineSliceRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
