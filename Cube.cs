using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private const float MaxTimeWait = 5.0f;
    private const float MinTimeWait = 2.0f;

    private float _waitTime;
    private bool _isBeenCollision = false;

    public event Action<Cube> Removal;

    public bool IsBeenCollision => _isBeenCollision;

    private void OnEnable()
    {
        _isBeenCollision = false;
        _waitTime = UnityEngine.Random.Range(MinTimeWait, MaxTimeWait);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isBeenCollision)
            return;

        if (collision.gameObject.TryGetComponent(out Platform _))
        {
            _isBeenCollision = true;

            StartCoroutine(Remove());
        }
    }

    private IEnumerator Remove()
    {
        yield return new WaitForSeconds(_waitTime);

        Removal?.Invoke(this);
    }
}
