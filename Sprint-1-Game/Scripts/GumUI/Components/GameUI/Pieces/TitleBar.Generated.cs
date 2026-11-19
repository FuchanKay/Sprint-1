//Code for GameUI/Pieces/TitleBar (Container)
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
[global::Gum.DataTypes.GumElement("GameUI/Pieces/TitleBar")]
partial class TitleBar : global::Gum.Forms.Controls.FrameworkElement
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
            var element = ObjectFinder.Self.GetRequiredElementSave("GameUI/Pieces/TitleBar");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new TitleBar(visual);
            return visual;
        });
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(TitleBar)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("GameUI/Pieces/TitleBar", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public NineSliceRuntime Background { get; protected set; }
    public TextRuntime TitleText { get; protected set; }

    public int BackgroundStartX
    {
        get => Background.TextureLeft;
        set => Background.TextureLeft = value;
    }

    public int BackgroundStartY
    {
        get => Background.TextureTop;
        set => Background.TextureTop = value;
    }

    public string TitleName
    {
        get => TitleText.Text;
        set => TitleText.Text = value;
    }

    public TitleBar(InteractiveGue visual) : base(visual)
    {
    }
    public TitleBar()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        Background = this.Visual?.GetGraphicalUiElementByName("Background") as global::Gum.GueDeriving.NineSliceRuntime;
        TitleText = this.Visual?.GetGraphicalUiElementByName("TitleText") as global::Gum.GueDeriving.TextRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
