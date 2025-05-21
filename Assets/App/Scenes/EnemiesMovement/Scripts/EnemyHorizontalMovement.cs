using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontalMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float waitTime = 0.5f;

    [Header("Local Patrol Points")]
    [SerializeField] private Vector2 localPoint1;
    [SerializeField] private Vector2 localPoint2;

    [Header("Start Direction")]
    [SerializeField] private bool startGoingToPoint1 = true;
    
    [Header("Debug")]
    [SerializeField] private bool drawGizmos = true;

    private Rigidbody2D _rb;
    private Vector2[] _worldPoints = new Vector2[2];
    private int _currentTargetIndex;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.isKinematic = true;

        _worldPoints[0] = transform.TransformPoint(localPoint1);
        _worldPoints[1] = transform.TransformPoint(localPoint2);

        _currentTargetIndex = startGoingToPoint1 ? 0 : 1;

        StartCoroutine(PatrolRoutine());
    }

    private IEnumerator PatrolRoutine()
    {
        while (true)
        {
            yield return MoveTo(_worldPoints[_currentTargetIndex]);
            yield return new WaitForSeconds(waitTime);

            _currentTargetIndex = 1 - _currentTargetIndex;
        }
    }

private IEnumerator MoveTo(Vector2 target)
{
    float rayLength = 0.1f; // distancia mínima para detectar colisión
    LayerMask obstacleMask = LayerMask.GetMask("Default"); // ajusta si usas otros layers

    while (Vector2.Distance(_rb.position, target) > 0.05f)
    {
        Vector2 direction = (target - _rb.position).normalized;

        // Detectar si hay un obstáculo justo delante
        RaycastHit2D hit = Physics2D.Raycast(_rb.position, direction, rayLength, obstacleMask);

        if (hit.collider != null && hit.collider.isTrigger==false)
        {
            // Se detecta un obstáculo delante, salir del bucle para girar
            yield break;
        }

        _rb.MovePosition(_rb.position + direction * (speed * Time.fixedDeltaTime));
        yield return new WaitForFixedUpdate();
    }

    _rb.MovePosition(target);
}

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Vector2 worldP1 = transform.TransformPoint(localPoint1);
            Vector2 worldP2 = transform.TransformPoint(localPoint2);

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(worldP1, 0.1f);
            Gizmos.DrawSphere(worldP2, 0.1f);
            Gizmos.DrawLine(worldP1, worldP2);
        }
        
    }
}
