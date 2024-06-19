using System;
using System.Collections;
using UnityEngine;

public class CubeRain : ObjectSpawner
{
    [SerializeField] private RandomerPositionSpawn _randomPosition;
    [SerializeField] private Transform _container;
    [SerializeField] private Cube _prefab;
    [SerializeField, Min(0.1f)] private float _timeSpawn;
    [SerializeField] private bool _isStart;

    private PoolObjcts<Cube> _pool;
    private WaitForSeconds _wait;

    public event Action<Vector3> CubeDeleted;

    private void Awake()
    {
        _isStart = true;

        _pool = new PoolObjcts<Cube>(_container, _prefab);
        _wait = new WaitForSeconds(_timeSpawn);
    }

    private void Start()
    {
        StartCoroutine(SpawnCube());
    }

    private IEnumerator SpawnCube()
    {
        while (enabled)
        {
            yield return _wait;

            if (_isStart == false)
                continue;

            Cube cube = _pool.GetGameObject();
            cube.transform.position = _randomPosition.GetPosition();

            cube.Removed += OnPutCube;

            Spawn();
        }
    }

    private void OnPutCube(Cube cube)
    {
        cube.Removed -= OnPutCube;

        _pool.PutGameObject(cube);

        CubeDeleted?.Invoke(cube.transform.position);

        Put();
    }
}
