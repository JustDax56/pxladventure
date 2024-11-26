using UnityEngine;

public class PlayerFootCollision : MonoBehaviour
{
    [Header("Jump Attack Settings")]
    [SerializeField] private float bounceForce = 10f;
    [SerializeField] private float damageAmount = 1f;
    [SerializeField] private LayerMask enemyLayer; 

    private PlayerController playerController;
    private Rigidbody2D playerRb;
    private bool canDamage = true;
    private float damageResetTime = 0.5f;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        playerRb = GetComponentInParent<Rigidbody2D>();

        if (playerController == null || playerRb == null)
        {
            Debug.LogError($"PlayerFootCollision: falta la wea de referencia  {gameObject.name}");
            enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canDamage) return;
        if (other == null) return;
        if (((1 << other.gameObject.layer) & enemyLayer) == 0) return;


        HealthSystem enemyHealth = other.GetComponent<HealthSystem>();
        if (enemyHealth == null && other.transform.parent != null)
        {
            enemyHealth = other.transform.parent.GetComponent<HealthSystem>();
        }

        if (enemyHealth != null && playerRb.linearVelocity.y < 0) 
        {

            enemyHealth.TakeDamage(damageAmount);


            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, bounceForce);


            playerController.HasUsedDoubleJump = false;


            canDamage = false;
            Invoke(nameof(ResetDamage), damageResetTime);
        }
    }

    private void ResetDamage()
    {
        canDamage = true;
    }
}
