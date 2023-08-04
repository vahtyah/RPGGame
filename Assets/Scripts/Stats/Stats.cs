﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[System.Serializable]
public class Stats
{
    [SerializeField] private int _baseValue;
    public List<int> _modifiers;

    public int Value
    {
        get => _baseValue + _modifiers.Sum();
        set => _baseValue = value;
    }

    public void AddModifier(int modifier) => _modifiers.Add(modifier);
    public void RemoveModifier(int modifier) => _modifiers.Remove(modifier);

}