using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class Stats
{
    [SerializeField] private int _baseValue;
    public List<int> _modifiers;
    public static event EventHandler onChanged;//TODO: fix event;

    public int Value
    {
        get => _baseValue + _modifiers.Sum();
        set => _baseValue = value;
    }

    public void AddModifier(int modifier)
    {
        if (modifier > 0)
        {
            _modifiers.Add(modifier);
            OnChanged();
        }
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier > 0)
        {
            _modifiers.Remove(modifier);
            OnChanged();
        }
    }

    protected virtual void OnChanged() { onChanged?.Invoke(this,EventArgs.Empty); }
}