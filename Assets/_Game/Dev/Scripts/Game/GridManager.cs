using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GridManager : Singleton<GridManager>
{
    [SerializeField] private Grid gridPrefab;

    public Grid GetPrefab() => gridPrefab;

     public List<GridSpawner> lsSpawners = new List<GridSpawner>();
    [HideInInspector] public List<Grid> lsGrids = new List<Grid>();

    public void OnStart()
    {
        for (int i = 0; i < lsSpawners.Count; i++)
        {
            lsSpawners[i].CreateGrids();
        }
        GemManager.I.InitGemGrowth();
    }

    public void AssignSpawner(GridSpawner spawner)
    {
        if (!lsSpawners.Contains(spawner))
        {
            lsSpawners.Add(spawner);
        }
    }
}