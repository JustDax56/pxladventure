using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cure : MonoBehaviour
{
    [SerializeField] float CureHealth;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out HealthSystem targetHealth))
        {
            if (!targetHealth.IsMaxHealth())
            {
                targetHealth.TakeCure(CureHealth);
                Destroy(gameObject);
            }
        }
    }
}
