using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameField : MonoBehaviour
{
    [Header("Field Properties")]
    public float CellSize;
    public float Spacing;
    public int FieldSize;

    [Space(10)]
    [SerializeField]
    private Cell cellPref;
    [SerializeField]
    private CellView cellViewPref;
    [SerializeField]
    private RectTransform rt;

    private Cell[,] field;

    private void Start()
    {
        GenerateField();
    }
    private void CreateField()
    {
        field = new Cell[FieldSize, FieldSize];
        
        float fieldWidth = FieldSize * (CellSize + Spacing) + Spacing;
        rt.sizeDelta = new Vector2(fieldWidth, fieldWidth);
        
        float startX = -(fieldWidth / 2) + (CellSize / 2) + Spacing;
        float startY = (fieldWidth / 2) - (CellSize / 2) - Spacing;

        for (int x = 0; x < FieldSize; x++)
        {
            for (int y = 0; y < FieldSize; y++)
            {
                var cell = Instantiate(cellPref, transform, false);
                cell.transform.localPosition = new Vector2(startX + (x * (CellSize + Spacing)), startY - (y * (CellSize + Spacing)));
                
                var cellView = Instantiate(cellViewPref, transform, false);
                cellView.transform.localPosition = new Vector2(startX + (x * (CellSize + Spacing)), startY - (y * (CellSize + Spacing)));
                
                cellView.Init(cell);

                field[x, y] = cell;
                
                cell.SetValue(x, y, 0);
            }
        }
    }

    public void GenerateField()
    {
        if (field == null)
            CreateField();
        for (int x = 0; x < FieldSize; x++)
            for (int y = 0; y < FieldSize; y++)
                field[x, y].SetValue(x, y, 0);
        CreateCell();
    }
    
    private Cell GetEmptyPosition()
    {
        var emptyCells = new List<Cell>();
        
        for (int x = 0; x < FieldSize; x++)
        for (int y = 0; y < FieldSize; y++)
            if (field[x, y].IsEmpty)
                emptyCells.Add(field[x, y]);

        if (emptyCells.Count == 0)
            return null;
        
        var emptyCell = emptyCells[Random.Range(0, emptyCells.Count)];
        return emptyCell;
    }
    
    private void CreateCell()
    {
        var emptyCell = GetEmptyPosition();
        if (emptyCell == null)
            throw new SystemException("No empty cell");
        int value = Random.Range(0, 10) == 0 ? 2 : 1;
        emptyCell.SetValue(emptyCell.X, emptyCell.Y, value);
    }
}
