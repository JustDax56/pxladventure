using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    [Header("Damage Settings")]
    [SerializeField] private float damage = 1f;
    [SerializeField] private bool canDamagePlayer = false;
    [SerializeField] private bool canDamageEnemy = false;
    [SerializeField] private bool destroyOnHit = false;

    [Header("Special Settings for Player Interactions")]
    [SerializeField] private bool causesBounce = false;
    [SerializeField] private float bounceForce = 5f;

    private Collider2D collisionDamage;

    private void Start()
    {
        collisionDamage = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleDamage(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleDamage(other.gameObject);
    }

    private void HandleDamage(GameObject target)
    {
        if (target.TryGetComponent(out HealthSystem health))
        {
            if ((target.CompareTag("Player") && canDamagePlayer) ||
                (target.CompareTag("Enemy") && canDamageEnemy))
            {

                health.TakeDamage(damage);


                if (causesBounce && target.CompareTag("Player"))
                {
                    Rigidbody2D playerRb = target.GetComponent<Rigidbody2D>();
                    if (playerRb != null)
                    {
                        playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, bounceForce);
                    }
                }


                if (destroyOnHit)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void ActivateCollision()
    {
        if (collisionDamage != null)
        {
            collisionDamage.enabled = true;
        }
    }

    public void DeactivateCollision()
    {
        if (collisionDamage != null)
        {
            collisionDamage.enabled = false;
        }
    }
}
