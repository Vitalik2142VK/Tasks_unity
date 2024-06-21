using UnityEngine;

public class RandomPositionSpawner : MonoBehaviour
{
    [SerializeField] private Platform _platform;
    [SerializeField] private float _height;

    private Vector2 _maxValues;
    private Vector2 _minValues;

    private void Awake()
    {
        float divider = 2.0f;
        float negativeValue = -1.0f;
        float valueX = _platform.transform.localScale.x / divider;
        float valueZ = _platform.transform.localScale.z / divider;

        _maxValues = new Vector2(valueX, valueZ);
        _minValues = new Vector2(valueX * negativeValue, valueZ * negativeValue);
    }

    public Vector3 GetPosition()
    {
        float x = Random.Range(_minValues.x, _maxValues.x);
        float z = Random.Range(_minValues.y, _maxValues.x);

        transform.localPosition = new Vector3(x, _height, z);

        return transform.position;
    }
}
