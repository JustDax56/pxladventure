using UnityEngine;
using System.Collections.Generic;

public class MovementChamaleon : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public Collider2D objetivo;
    [SerializeField] private GroundCheck groundCheck;
    public float velocidad;
    public Transform centroVision;
    public Vector2 tamañoVision;
    public LayerMask capasVision;
    public float Distancia;
    [SerializeField] private List<DamageSystem> Daños;
    [SerializeField] private string hitTriggerParameter = "Hit";
    private HealthSystem healthSystem;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        healthSystem = GetComponent<HealthSystem>();

        if (groundCheck == null)
        {
            groundCheck = GetComponent<GroundCheck>();
            if (groundCheck == null)
            {
                Debug.LogError("GroundCheck no está asignado ni encontrado en el objeto.");
            }
        }
        if (healthSystem != null)
        {
            healthSystem.OnDamageTaken.AddListener(OnDamageReceived);
        }
    }

    private void FixedUpdate()
    {
        if (groundCheck != null)
        {
            bool isGrounded = groundCheck.IsGrounded;
            animator.SetBool("IsGrounded", isGrounded);
        }

        objetivo = Physics2D.OverlapBox(centroVision.position, tamañoVision, 0, capasVision);
        animator.SetBool("JugadorDetectado", objetivo != null);
        if (objetivo != null)
        {
            Distancia = Vector2.Distance(transform.position, objetivo.transform.position);
            animator.SetFloat("Distancia", Distancia);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(centroVision.position, tamañoVision);
    }

    private void OnDamageReceived()
    {

        animator.SetTrigger(hitTriggerParameter);
    }

    private void OnDestroy()
    {
        if (healthSystem != null)
        {
            healthSystem.OnDamageTaken.RemoveListener(OnDamageReceived);
        }
    }

    private void ActivarDaños()
    {
        foreach (var item in Daños)
        {
            item.ActivateCollision();
        }
    }

    private void DesactivarDaños()
    {
        foreach (var item in Daños)
        {
            item.DeactivateCollision();
        }
    }
}
