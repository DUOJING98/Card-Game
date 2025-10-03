using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarController : MonoBehaviour
{
    private CharacterBse currentCharacter;

    [Header("Elements")]
    public Transform healthBarTransform;

    private UIDocument healthBarDocument;
    private ProgressBar healthBar;


    private void Awake()
    {
        currentCharacter = GetComponent<CharacterBse>();
        
    }
    private void Start()
    {
        InitHealthBar();
    }
    private void Update()
    {
        UpdateHealthBar();
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
        if (healthBar!=null)
        {
            healthBar.title = $"{currentCharacter.CurrentHP}/{currentCharacter.MaxHP}";
            healthBar.value = currentCharacter.CurrentHP;
            //–¬§∑§§•≤©`•‡§«HP•–©`§Œ•Í•π•»§Úœ˜≥˝§π§Î
            healthBar.RemoveFromClassList("heightHealth");
            healthBar.RemoveFromClassList("mediumHealth");
            healthBar.RemoveFromClassList("lowHealth");

            var percentage = (float)currentCharacter.CurrentHP / (float)currentCharacter.MaxHP;
            //≤–§ÍHP§ÀèÍ§∏§∆HP•–©`§Œ…´§Úâ‰∏¸§π§Î
            if (percentage < 0.3f)
            {
                healthBar.AddToClassList("lowHealth");
            }
            else if(percentage < 0.6f)
            {
                healthBar.AddToClassList("mediumHealth");
            }
            else
            {
                healthBar.AddToClassList("heightHealth");
            }
        }
    }
    
    [ContextMenu("UITest")]
    public void InitHealthBar()
    {

        healthBarDocument = GetComponent<UIDocument>();
        healthBar = healthBarDocument.rootVisualElement.Q<ProgressBar>("HealthBar");
        healthBar.highValue =currentCharacter.MaxHP;
        MoveToWorldPosition(healthBar, healthBarTransform.position, Vector2.zero);
    }
}