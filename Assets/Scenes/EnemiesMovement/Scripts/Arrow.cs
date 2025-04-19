using UnityEngine;

public class Arrow : MonoBehaviour,IThrowable
{
    private Rigidbody2D _rb;
    private Vector2 _direction;
    private float _speed;
    private System.Action<IThrowable> _onHitCallback;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float speed, System.Action<IThrowable> onHitCallback)
    {
        _direction = direction.normalized;
        _speed = speed;
        _onHitCallback = onHitCallback;

        gameObject.SetActive(true);
        _rb.velocity = _direction * _speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _rb.velocity = Vector2.zero;
        _onHitCallback?.Invoke(this);
    }
}