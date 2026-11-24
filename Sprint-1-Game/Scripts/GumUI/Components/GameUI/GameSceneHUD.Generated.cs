//Code for GameUI/GameSceneHUD (Container)
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
namespace GUMProject.Components.GameUI;
[global::Gum.DataTypes.GumElement("GameUI/GameSceneHUD")]
partial class GameSceneHUD : global::Gum.Forms.Controls.FrameworkElement
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
            var element = ObjectFinder.Self.GetRequiredElementSave("GameUI/GameSceneHUD");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new GameSceneHUD(visual);
            return visual;
        });
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(GameSceneHUD)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("GameUI/GameSceneHUD", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public Panel HUDPanel { get; protected set; }
    public LevelTitleMenu LevelTitleMenuInstance { get; protected set; }
    public EnemyCounterMenu EnemyCounterMenuInstance { get; protected set; }
    public TimeMenu TimeMenuInstance { get; protected set; }
    public InventoryMenu InventoryMenuInstance { get; protected set; }
    public GameInstructionMenu GameInstructionMenuInstance { get; protected set; }

    public string EnemyCounterText
    {
        get => EnemyCounterMenuInstance.EnemyCounterText;
        set => EnemyCounterMenuInstance.EnemyCounterText = value;
    }

    public string GameInstructionText
    {
        get => GameInstructionMenuInstance.GameInstructionText;
        set => GameInstructionMenuInstance.GameInstructionText = value;
    }

    public string LevelTitleText
    {
        get => LevelTitleMenuInstance.LevelTitleText;
        set => LevelTitleMenuInstance.LevelTitleText = value;
    }

    public string TimeText
    {
        get => TimeMenuInstance.TimeText;
        set => TimeMenuInstance.TimeText = value;
    }

    public GameSceneHUD(InteractiveGue visual) : base(visual)
    {
    }
    public GameSceneHUD()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        HUDPanel = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<Panel>(this.Visual,"HUDPanel");
        LevelTitleMenuInstance = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<LevelTitleMenu>(this.Visual,"LevelTitleMenuInstance");
        EnemyCounterMenuInstance = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<EnemyCounterMenu>(this.Visual,"EnemyCounterMenuInstance");
        TimeMenuInstance = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<TimeMenu>(this.Visual,"TimeMenuInstance");
        InventoryMenuInstance = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<InventoryMenu>(this.Visual,"InventoryMenuInstance");
        GameInstructionMenuInstance = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<GameInstructionMenu>(this.Visual,"GameInstructionMenuInstance");
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
