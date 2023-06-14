
using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class GemManager : Singleton<GemManager>
{
    private int _enumLength;

    private void Start()
    {
        _enumLength = Enum.GetValues(typeof(GemType)).Length;
    }

    public void InitGemGrowth()
    {
        foreach (Grid grid in GridManager.I.lsGrids)
        {
            StartGemGrowth(grid);
        }
    }

    private GemType GetRandomGemType() => (GemType)Random.Range(0, _enumLength);
 
    public void StartGemGrowth(Grid g)
    {
        GemType type = GetRandomGemType();
            Gem _gem;
            if (type == GemType.Green)
            {
                _gem = PoolManager.I.GetObject<GreenGem>();
            }
            else if  (type == GemType.Pink)
            {
                _gem = PoolManager.I.GetObject<PinkGem>();
            } 
            else
            {
                _gem = PoolManager.I.GetObject<YellowGem>();
            }

            _gem.parentGrid = g;
            _gem.transform.SetParent(g.transform);
            _gem.transform.localPosition = Vector3.zero.WithZ(-0.4f);
    }
    
}





