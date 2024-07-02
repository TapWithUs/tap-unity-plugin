using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deque<T>
{
    private List<T> _items;
    private int _maxLength;

    Deque(int maxLength)
    {
        _items = new List<T>();
        _maxLength = Mathf.Max(0,maxLength);
    }

    void put(T value)
    {
        _items.Add(value);
        _items.RemoveRange(0, Mathf.Max(0,_items.Count - _maxLength));
    }
}
