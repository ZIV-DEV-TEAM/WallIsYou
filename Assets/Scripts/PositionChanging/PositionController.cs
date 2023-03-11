using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionController : MonoBehaviour
{
    [SerializeField] private Position[] positions;
    private Dictionary<int, Position> _positions;

    private void Start()
    {
        _positions = new Dictionary<int, Position>();
        for (int i = 0; i < positions.Length; i++)
        {
            var key = positions[i].ID;
            var value = positions[i];
            _positions.Add(key, value);
        }
    }

    public Position GetPosition(int key)
    {
        return _positions[key];
    }
}
