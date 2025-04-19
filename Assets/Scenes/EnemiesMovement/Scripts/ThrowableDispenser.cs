using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ThrowableDispenser : MonoBehaviour
{
    [Header("Throwable Settings")]
    [SerializeField] private GameObject throwablePrefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private float throwableSpeed = 10f;
    [SerializeField] private ThrowableDirection shootDirection = ThrowableDirection.Left;
    [SerializeField] private float spawnInterval = 1f;

    [Header("Dispenser Activation")]
    [SerializeField] private bool canBeDeactivated = false; 
    [SerializeField] private bool isActive = true; 

    private List<IThrowable> _throwablePool = new List<IThrowable>();

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject throwableGo = Instantiate(throwablePrefab, transform.position, Quaternion.identity, transform);
            throwableGo.SetActive(false);

            IThrowable throwable = throwableGo.GetComponent<IThrowable>();
            _throwablePool.Add(throwable);
        }

        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (isActive)
            {
                SpawnThrowable();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnThrowable()
    {
        IThrowable throwable = GetThrowableFromPool();
        if (throwable != null)
        {
            throwable.transform.position = transform.position;
            float value = (int)shootDirection;
            Vector2 direction = new Vector2(value, 0);
            throwable.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction); // opcional

            throwable.Launch(direction, throwableSpeed, ReturnThrowableToPool);
        }
    }

    private IThrowable GetThrowableFromPool()
    {
        foreach (IThrowable throwable in _throwablePool)
        {
            if (!throwable.gameObject.activeInHierarchy)
                return throwable;
        }
        return null;
    }

    private void ReturnThrowableToPool(IThrowable throwable)
    {
        throwable.gameObject.SetActive(false);
    }

    public void SetActiveState(bool active)
    {
        if (canBeDeactivated)
        {
            isActive = active;
        }
    }

    enum ThrowableDirection
    {
        Left = -1,
        Right = 1
    }

}
