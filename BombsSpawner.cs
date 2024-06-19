using UnityEngine;

public class BombsSpawner : ObjectSpawner
{
    [SerializeField] private CubeRain _cubeRain;
    [SerializeField] private Transform _container;
    [SerializeField] private Bomb _prefab;

    private PoolObjcts<Bomb> _pool;

    private void Awake()
    {
        _pool = new PoolObjcts<Bomb>(_container, _prefab);
    }

    private void OnEnable()
    {
        _cubeRain.CubeDeleted += OnSpawnBomb;
    }

    private void OnDisable()
    {
        _cubeRain.CubeDeleted -= OnSpawnBomb;
    }

    private void OnSpawnBomb(Vector3 position)
    {
        Bomb bomb = _pool.GetGameObject();
        bomb.transform.position = position;

        bomb.Removed += OnPutBomb;

        Spawn();
    }

    private void OnPutBomb(Bomb bomb)
    {
        bomb.Removed -= OnPutBomb;

        _pool.PutGameObject(bomb);

        Put();
    }
}