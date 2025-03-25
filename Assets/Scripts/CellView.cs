using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
  
  [SerializeField] private Image image;
  [SerializeField] private TextMeshProUGUI points;
  [SerializeField] private Cell _cell;

  public bool IsEmpty => _cell.IsEmpty;
  public void Init(Cell cell)
  {
    _cell = cell;
    _cell.OnValueChanged += UpdateValue;
    _cell.OnPositionChanged += UpdatePosition;

    UpdateValue(_cell.Value);
    UpdatePosition(_cell.X, _cell.Y);
  }

  public void UpdateValue(int value)
  {
    points.text = value == 0 ? String.Empty : Math.Pow(2, value).ToString();
    image.color = Color.Lerp(Color.white, Color.red, (float)value / (float)Cell.MaxValue);
  }

  private void UpdatePosition(int x, int y)
  {
    // transform.position = new Vector2(x, y);
  }
}