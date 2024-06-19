using System;
using UnityEngine;

public abstract class ObjectSpawner : MonoBehaviour
{
    public event Action CountUpdated;

    public int CountActiveObjects { get; protected set; }
    public int CountSpawnedObjects { get; protected set; }

    protected void Spawn()
    {
        CountActiveObjects++;
        CountSpawnedObjects++;

        CountUpdated?.Invoke();
    }

    protected void Put()
    {
        CountActiveObjects--;

        CountUpdated?.Invoke();
    }
}
