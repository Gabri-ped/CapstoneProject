using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class GroundChecker : MonoBehaviour
{
    [SerializeField] private float _groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask _groundLayer;

    public bool IsGrounded { get; private set; }

    private void Update()
    {
        // Controlla se sotto il player c'è il terreno
        IsGrounded = Physics.Raycast(transform.position + Vector3.up * 0.05f, Vector3.down, _groundCheckDistance, _groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.position - Vector3.up * _groundCheckDistance);
    }
}
