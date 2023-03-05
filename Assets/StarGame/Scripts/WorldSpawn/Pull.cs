using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class Pull<T> where T : MonoBehaviour
{
    private List<T> _prefabs;
    private Transform _container;
    private Queue<T> _pull = new(100);
    private int _count;
    private bool _autoExpand;

    public Pull(Transform container, bool autoExpand, List<T> prefabs, int count = 0)
    {
        _container = container;
        _prefabs = prefabs;
        _autoExpand = autoExpand;
        _count = count;
        CreatePool();
    }

    public void CreatePool()
    {
        _pull = new Queue<T>();

        for (int i = 0; i < _count; i++)
            CreateOne();
    }

    private T GetRandomPrefab()
    {
        int index = Random.Range(0, _prefabs.Count);

        var go = _prefabs[index];
        return go;
    }


    private void CreateOne()
    {
        var cell = Object.Instantiate(GetRandomPrefab(), _container);
        cell.gameObject.SetActive(false);
        _pull.Enqueue(cell);
    }

    public void Reset()
    {
        for (int i = 0; i < _pull.Count; i++)
        {
            Object.Destroy(_pull.Dequeue());
        }

        _pull.Clear();
    }

    public void ReturnToPoll(T element)
    {
        _pull.Enqueue(element);
        element.gameObject.SetActive(false);
    }

    public bool CanProvide()
    {
        foreach (var prefab in _prefabs)
        {
            if (_pull.Contains(prefab) == false)
                return false;
        }

        return true;
    }
    public T GetElement()
    {
        T element = null;

        if (_pull.Count != 0)
            element = _pull.Dequeue();

        else if (_autoExpand)
        {
            CreateOne();
            element = _pull.Dequeue();
        }
        else
            throw new Exception("CantProvideItem");

        element.gameObject.SetActive(true);
        return element;
    }
}