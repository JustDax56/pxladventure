using UnityEngine.Events;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float maxHealth = 5f;
    [SerializeField] private float damageCooldown = 0.5f;
    public UnityEvent<float> OnHealthChanged;
    public UnityEvent OnDeath;
    public UnityEvent OnDamageTaken;
    private float currentHealth;
    private float lastDamageTime;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public bool IsMaxHealth() => currentHealth >= maxHealth;

    public void TakeDamage(float damage)
    {
        if (Time.time - lastDamageTime >= damageCooldown)
        {
            currentHealth = Mathf.Max(0, currentHealth - damage);
            lastDamageTime = Time.time;
            OnHealthChanged?.Invoke(currentHealth);
             
            if (damage > 0)
            {
                OnDamageTaken?.Invoke();
            }

            if (currentHealth <= 0)
            {
                OnDeath?.Invoke();
            }
        }
    }

    public void TakeCure(float cure)
    {
        if (currentHealth > 0 && !IsMaxHealth())
        {
            currentHealth = Mathf.Min(maxHealth, currentHealth + cure);
            OnHealthChanged?.Invoke(currentHealth);
        }
    }
}