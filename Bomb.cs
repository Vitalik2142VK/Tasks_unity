using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Bomb : MonoBehaviour
{
    private const float MaxAlpha = 1.0f;
    private const float MinAlpha = 0.1f;

    [SerializeField] private float _timeExplosion;
    [SerializeField] private float _radiusExplosion;
    [SerializeField] private float _forceExplosion;

    private Renderer _renderer;

    public event Action<Bomb> Removed;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        Color color = _renderer.material.color;
        color.a = MaxAlpha;
        _renderer.material.color = color;
    }

    private void Update()
    {
        Color color = _renderer.material.color;
        float alpha = color.a;

        alpha = Mathf.Lerp(alpha, 0.0f, _timeExplosion * Time.deltaTime);
        color.a = alpha;
        _renderer.material.color = color;

        if (alpha < MinAlpha)
            Explode();
    }

    private void Explode()
    {
        Vector3 position = transform.position;
        List<Rigidbody> explodableObjects = Physics.OverlapSphere(position, _radiusExplosion)
                .Select(c => c.attachedRigidbody)
                .Where(r => r != null)
                .ToList();

        foreach (var rigidbody in explodableObjects)
        {
            rigidbody.AddExplosionForce(_forceExplosion, position, _radiusExplosion);
        }

        Removed?.Invoke(this);

    }
}
