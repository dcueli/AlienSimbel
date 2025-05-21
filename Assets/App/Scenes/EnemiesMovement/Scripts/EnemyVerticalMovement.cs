using System.Collections;
using UnityEngine;


public class EnemyVerticalMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float waitTime = 0.5f;

    [Header("Vertical Movement Settings")]
    [SerializeField] private Vector2 localPoint1; 
    [SerializeField] private Vector2 localPoint2; 

    [Header("Debug")]
    [SerializeField] private bool drawGizmos = true;

    
    private Rigidbody2D _rb;
    private Vector2 _worldPoint1;
    private Vector2 _worldPoint2;

    
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.isKinematic = true;
        
        _worldPoint1 = transform.TransformPoint(localPoint1);
        _worldPoint2 = transform.TransformPoint(localPoint2);

        transform.position = _worldPoint1;
        
        StartCoroutine(PatrolRoutine());
    }

    private IEnumerator PatrolRoutine()
    {
        while (true)
        {
            yield return MoveTo(_worldPoint2);
            yield return new WaitForSeconds(waitTime);
            yield return MoveTo(_worldPoint1);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator MoveTo(Vector2 target)
    {
        while (Vector2.Distance(_rb.position, target) > 0.05f)
        {
            Vector2 direction = (target - _rb.position).normalized;
            _rb.MovePosition(_rb.position + direction * (speed * Time.deltaTime));
            yield return new WaitForFixedUpdate();
        }

        _rb.MovePosition(target); 
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Vector2 drawWorldPoint1 = transform.TransformPoint(localPoint1);
            Vector2 drawWorldPoint2 = transform.TransformPoint(localPoint2);

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(drawWorldPoint1, 0.1f);
            Gizmos.DrawSphere(drawWorldPoint2, 0.1f);
            Gizmos.DrawLine(drawWorldPoint1, drawWorldPoint2);
        }
    }
}