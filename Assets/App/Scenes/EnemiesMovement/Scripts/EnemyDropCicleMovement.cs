using System.Collections;
using UnityEngine;

public class EnemyDropCicleMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float waitTime = 0.5f;
    
    [Header("Local Patrol Points")]
    [SerializeField] private Vector2 localTopPoint;
    [SerializeField] private Vector2 localBottomPoint;

    [Header("Debug")]
    [SerializeField] private bool drawGizmos = true;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    private Vector2 _topWorld;
    private Vector2 _bottomWorld;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb.isKinematic = true;

        _topWorld = transform.TransformPoint(localTopPoint);
        _bottomWorld = transform.TransformPoint(localBottomPoint);

        _rb.position = _topWorld;

        StartCoroutine(DropCycle());
    }

    private IEnumerator DropCycle()
    {
        while (true)
        {
            yield return MoveTo(_bottomWorld);
            _spriteRenderer.enabled = false;
            _rb.position = _topWorld;
            yield return new WaitForSeconds(waitTime);
            _spriteRenderer.enabled = true;
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
            Vector2 top = transform.TransformPoint(localTopPoint);
            Vector2 bottom = transform.TransformPoint(localBottomPoint);

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(top, 0.1f);
            Gizmos.DrawSphere(bottom, 0.1f);
            Gizmos.DrawLine(top, bottom);
        }
    }
}