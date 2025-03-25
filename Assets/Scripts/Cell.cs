using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Value { get; private set; }
    public int Points => IsEmpty ? 0 : (int)Mathf.Pow(2, Value);
    public bool IsEmpty => Value == 0;
    public const int MaxValue = 11;
    
    public event Action<int> OnValueChanged;
    public event Action<int, int> OnPositionChanged;

    public void SetValue(int x, int y, int value)
    {
        if (X != x || Y != y)
        {
            X = x;
            Y = y;
            OnPositionChanged?.Invoke(X, Y);
        }

        if (Value != value)
        {
            Value = value;
            OnValueChanged?.Invoke(Value);
        }
    }
}

