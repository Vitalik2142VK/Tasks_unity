using TMPro;
using UnityEngine;

public class CountSpawnObjectsViewer : MonoBehaviour
{
    [SerializeField] private ObjectSpawner _spawner;
    [SerializeField] private TextMeshProUGUI _countActive;
    [SerializeField] private TextMeshProUGUI _countSpawned;

    private void OnEnable()
    {
        _spawner.CountUpdated += OnUpdateView;
    }

    private void OnDisable()
    {
        _spawner.CountUpdated -= OnUpdateView;
    }

    private void OnUpdateView()
    {
        _countActive.text = _spawner.CountActiveObjects.ToString();
        _countSpawned.text = _spawner.CountSpawnedObjects.ToString();
    }
}
