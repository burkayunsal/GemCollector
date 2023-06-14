using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public int rows;
    public int columns;
    
    private void Start()
    {
        GridManager.I.AssignSpawner(this);
    }

    public void CreateGrids()
    {
        Grid gridPrefab = GridManager.I.GetPrefab();
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Grid grid = Instantiate(gridPrefab, transform);
                grid.Init(i,rows,j,columns);
                GridManager.I.lsGrids.Add(grid);
                grid.id = i == 0 ? j : i * columns + j;
            }
        }
    }
}
