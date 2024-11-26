using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GroundCheck : MonoBehaviour
{
    [Header("Ground Check")]
    [SerializeField] private LayerMask _groundLayerMask = 1 << 3;
    [SerializeField] private Vector2 _groundCheckBoxSize = new(0.5f, 0.1f); 
    [SerializeField] private Vector2 _groundCheckBoxOffset = new(0f, -0.5f); 
    [SerializeField] private Color _gizmoColor = Color.red;
    public bool IsGrounded { get; private set; }

    private void Update()
    {
        GroundChecker();
    }

    private void GroundChecker()
    {

        Vector2 position = transform.TransformPoint(_groundCheckBoxOffset);
        IsGrounded = Physics2D.OverlapBox(position, _groundCheckBoxSize, 0f, _groundLayerMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColor;
        Vector2 position = transform.TransformPoint(_groundCheckBoxOffset);
        Gizmos.DrawWireCube(position, _groundCheckBoxSize);
    }
    
}
