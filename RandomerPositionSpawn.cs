using UnityEngine;

public class RandomerPositionSpawn : MonoBehaviour
{
    [SerializeField] private Platform _platform;
    [SerializeField] private float _height;

    private float _maxValueX;
    private float _minValueX;
    private float _maxValueZ;
    private float _minValueZ;

    private void Awake()
    {
        float divider = 2.0f;
        float negativeValue = -1.0f;

        _maxValueX = _platform.transform.localScale.x / divider;
        _minValueX = _maxValueX * negativeValue;
        _maxValueZ = _platform.transform.localScale.z / divider;
        _minValueZ = _maxValueZ * negativeValue;
    }

    public Vector3 GetPosition()
    {
        float x = Random.Range(_minValueX, _maxValueX);
        float z = Random.Range(_minValueZ, _maxValueZ);

        transform.localPosition = new Vector3(x, _height, z);

        return transform.position;
    }
}
