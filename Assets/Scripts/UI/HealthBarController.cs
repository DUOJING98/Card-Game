using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarController : MonoBehaviour
{
    private CharacterBse currentCharacter;
    private Enemy enemy;
    private VisualElement intentSprite;
    private Label intentAmount;

    [Header("Elements")]
    public Transform healthBarTransform;

    private UIDocument healthBarDocument;
    private ProgressBar healthBar;

    //防御
    private VisualElement defenseElement;
    private Label defenseAmountLabel;

    //Buff
    private VisualElement buffElement;
    private Label buffRound;


    [Header("buffSprite")]
    public Sprite buff;
    public Sprite debuff;
    private void Awake()
    {
        currentCharacter = GetComponent<CharacterBse>();
        enemy = GetComponent<Enemy>();

    }
    private void Start()
    {
        InitHealthBar();
    }
    private void Update()
    {
        UpdateHealthBar();
    }

    [ContextMenu("UITest")]
    public void InitHealthBar()
    {
        //UI
        healthBarDocument = GetComponent<UIDocument>();
        healthBar = healthBarDocument.rootVisualElement.Q<ProgressBar>("HealthBar");
        healthBar.highValue = currentCharacter.MaxHP;
        MoveToWorldPosition(healthBar, healthBarTransform.position, Vector2.zero);
        //防御
        defenseElement = healthBar.Q<VisualElement>("Defense");
        defenseAmountLabel = defenseElement.Q<Label>("DefenseAmount");
        defenseElement.style.display = DisplayStyle.None;
        //buff
        buffElement = healthBar.Q<VisualElement>("Buff");
        buffRound = buffElement.Q<Label>("BuffRound");
        buffElement.style.display = DisplayStyle.None;

        //EnemyIntent
        intentSprite = healthBar.Q<VisualElement>("Intent");
        intentAmount = intentSprite.Q<Label>("IntentAmount");
        intentSprite.style.display = DisplayStyle.None;

    }


    private void MoveToWorldPosition(VisualElement element, Vector3 worldPosition, Vector2 size)
    {
        Rect rect = RuntimePanelUtils.CameraTransformWorldToPanelRect(element.panel, worldPosition, size, Camera.main);
        element.transform.position = rect.position;
    }

    public void UpdateHealthBar()
    {
        if (currentCharacter.isDead)
        {
            healthBar.style.display = DisplayStyle.None;
            return;
        }
        if (healthBar != null)
        {
            healthBar.title = $"{currentCharacter.CurrentHP}/{currentCharacter.MaxHP}";
            healthBar.value = currentCharacter.CurrentHP;
            //新しいゲームでHPバーのリストを削除する
            healthBar.RemoveFromClassList("heightHealth");
            healthBar.RemoveFromClassList("mediumHealth");
            healthBar.RemoveFromClassList("lowHealth");

            var percentage = (float)currentCharacter.CurrentHP / (float)currentCharacter.MaxHP;
            //残りHPに応じてHPバーの色を変更する
            if (percentage < 0.3f)
            {
                healthBar.AddToClassList("lowHealth");
            }
            else if (percentage < 0.6f)
            {
                healthBar.AddToClassList("mediumHealth");
            }
            else
            {
                healthBar.AddToClassList("heightHealth");
            }

            //防御UIを表示するかどうか
            defenseElement.style.display = currentCharacter.defense.currentValue > 0 ? DisplayStyle.Flex : DisplayStyle.None;
            defenseAmountLabel.text = currentCharacter.defense.currentValue.ToString();

            //BUFF表示
            buffElement.style.display = currentCharacter.buffRound.currentValue > 0 ? DisplayStyle.Flex : DisplayStyle.None;
            buffRound.text = currentCharacter.buffRound.currentValue.ToString();
            buffElement.style.backgroundImage = currentCharacter.basePower > 1 ? new StyleBackground(buff) : new StyleBackground(debuff);
        }

    }

    /// <summary>
    /// プレイヤーターン開始時に呼び出す
    /// </summary>
    public void SetIntentElement()
    {
        intentSprite.style.display = DisplayStyle.Flex;

        intentSprite.style.backgroundImage = new StyleBackground(enemy.currentAction.intentSprite);

        //攻撃するかどうかを判定する
        var value = enemy.currentAction.effect.value;
        if (enemy.currentAction.effect.GetType() == typeof(DamageEffect))
        {
            value = (int)math.round(enemy.currentAction.effect.value * enemy.basePower);
        }

        intentAmount.text = value.ToString();
    }

    public void HideIntentElement()
    {
        intentSprite.style.display = DisplayStyle.None;
    }
}