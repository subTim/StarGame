using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class Pull
{
    private List<GameObject> _prefabs;
    private Transform _container;
    private Queue<GameObject> _pull = new(100);
    private int _count;
    private bool _autoExpand;

    public Pull(Transform container, bool autoExpand, List<GameObject> prefabs, int count = 0)
    {
        _container = container;
        _prefabs = prefabs;
        _autoExpand = autoExpand;
        _count = count;
    }

    public void CreatePool()
    {
        _pull = new Queue<GameObject>();

        for (int i = 0; i < _count; i++)
            CreateOne();
    }

    private GameObject GetRandomPrefab()
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

    public void ReturnToPoll(GameObject element)
    {
        _pull.Enqueue(element);
        element.SetActive(false);
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

    public GameObject GetElementWithPredicate(Predicate<GameObject> predicate)
    {
        GameObject element = null;
        do
        {
            if (element != null)
                ReturnToPoll(element);

            element = GetElement();

        } while (predicate(element) == false);

        element.SetActive(true);
        return element;
    }

    public GameObject GetElement()
    {
        GameObject element = null;

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