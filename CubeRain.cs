using System.Collections;
using UnityEngine;

public class CubeRain : MonoBehaviour
{
    [SerializeField] private RandomerPositionSpawn _randomPosition;
    [SerializeField] private Transform _container;
    [SerializeField] private Cube _prefab;
    [SerializeField, Min(0.1f)] private float _timeSpawn;

    private PoolObjcts<Cube> _pool;
    private WaitForSeconds _wait;

    private void Awake()
    {
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

            Cube cube = _pool.GetGameObject();
            cube.transform.position = _randomPosition.GetPosition();
            cube.gameObject.SetActive(true);

            cube.Removal += OnPutCube;
        }
    }

    private void OnPutCube(Cube cube)
    {
        cube.Removal -= OnPutCube;

        _pool.PutGameObject(cube);
    }
}
