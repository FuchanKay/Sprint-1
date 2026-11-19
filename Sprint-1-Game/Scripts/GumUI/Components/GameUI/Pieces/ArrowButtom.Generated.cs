//Code for GameUI/Pieces/ArrowButtom (Container)
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
[global::Gum.DataTypes.GumElement("GameUI/Pieces/ArrowButtom")]
partial class ArrowButtom : global::Gum.Forms.Controls.FrameworkElement
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
            var element = ObjectFinder.Self.GetRequiredElementSave("GameUI/Pieces/ArrowButtom");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new ArrowButtom(visual);
            return visual;
        });
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(ArrowButtom)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("GameUI/Pieces/ArrowButtom", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public NineSliceRuntime Backgroud { get; protected set; }
    public SpriteRuntime ArrowSprite { get; protected set; }
    public NineSliceRuntime FocusIndicator { get; protected set; }

    public int ArrowSpriteLeft
    {
        get => ArrowSprite.TextureLeft;
        set => ArrowSprite.TextureLeft = value;
    }

    public bool Selected
    {
        get => FocusIndicator.Visible;
        set => FocusIndicator.Visible = value;
    }

    public ArrowButtom(InteractiveGue visual) : base(visual)
    {
    }
    public ArrowButtom()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        Backgroud = this.Visual?.GetGraphicalUiElementByName("Backgroud") as global::Gum.GueDeriving.NineSliceRuntime;
        ArrowSprite = this.Visual?.GetGraphicalUiElementByName("ArrowSprite") as global::Gum.GueDeriving.SpriteRuntime;
        FocusIndicator = this.Visual?.GetGraphicalUiElementByName("FocusIndicator") as global::Gum.GueDeriving.NineSliceRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
