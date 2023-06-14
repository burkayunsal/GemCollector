using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StackManager : Singleton<StackManager>
{
   [SerializeField] private Transform stackPool;
   public List<Gem> lsCollectedGems = new List<Gem>();
   public void Collect(Gem gem)
   {
      if (!lsCollectedGems.Contains(gem))
      {
         lsCollectedGems.Add(gem);
         gem.parentGrid.SpawnGem();
         PlaceGem(gem);
      }
   }
   void PlaceGem(Gem g)
   {
      g.transform.SetParent(stackPool);
      int count = lsCollectedGems.Count - 1;
      float posY = count * 0.7f;
      g.OnCollected(new Vector3(0, posY, 0));
   }
   
   public void Drop(Transform targetPos)
   {
      Gem lastGem = GetLastGem();
      if (lastGem != null)
      {
         SaveLoadManager.AddGem(lastGem.gemType);
         RemoveGem(lastGem);
         lastGem.OnDrop(targetPos);
      }
   }
   
   public Gem GetLastGem()
   {
      Gem g = null;

      if (lsCollectedGems.Count > 0)
      {
         g = lsCollectedGems[lsCollectedGems.Count - 1];
      }
      return g;
   }
   
   public void RemoveGem(Gem g)
   {
      if (lsCollectedGems.Contains(g))
      {
         lsCollectedGems.Remove(g);
      }
   }
}
