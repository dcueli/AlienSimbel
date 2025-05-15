using UnityEngine;

public interface IThrowable
{
    void Launch(Vector2 direction, float speed, System.Action<IThrowable> onHitCallback);
    GameObject gameObject { get; }
    Transform transform { get; }
}