﻿using System.Collections.Generic;
using UnityEngine;

public class PoolObjcts<T> where T : MonoBehaviour
{
    private Stack<T> _pool;
    private Transform _conteiner;
    private T _prefab;

    public PoolObjcts(Transform conteiner, T prefab)
    {
        _conteiner = conteiner;
        _prefab = prefab;

        _pool = new Stack<T>();
    }

    public T GetGameObject()
    {
        T obj;

        if (_pool.Count == 0)
        {
            obj = Object.Instantiate(_prefab);
            obj.transform.parent = _conteiner;
        }
        else
        {
            obj = _pool.Pop();
        }

        obj.gameObject.SetActive(true);

        return obj;
    }

    public void PutGameObject(T obj)
    {
        obj.transform.parent = _conteiner;
        obj.gameObject.SetActive(false);
        _pool.Push(obj);
    }

    public void RemoveAll()
    {
        for (int i = 0; i < _conteiner.childCount; i++)
        {
            if (_conteiner.GetChild(i).gameObject.TryGetComponent(out T obj)) 
            {
                Object.Destroy(obj.gameObject);
            }
        }

        _pool.Clear();
    }
}
