using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private void Awake()
    {

        if (healthSystem == null)
        {
            Debug.LogError("HealthSystem reference is missing!");
            return;
        }


        healthSystem.OnHealthChanged.AddListener(UpdateHealthBar);
    }

    private void Start()
    {
        UpdateHealthBar(healthSystem.CurrentHealth);
    }

    private void OnDestroy()
    {

        if (healthSystem != null)
        {
            healthSystem.OnHealthChanged.RemoveListener(UpdateHealthBar);
        }
    }

    private void UpdateHealthBar(float newHealth)
    {

        int fullHeartsCount = Mathf.FloorToInt(newHealth);


        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < fullHeartsCount)
            {
                hearts[i].sprite = fullHeart; 
            }
            else
            {
                hearts[i].sprite = emptyHeart; 
            }
        }
    }
}
