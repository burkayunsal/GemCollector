using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoinManager : Singleton<CoinManager>
{

    public List<Coin> lsCoins = new List<Coin>();

    [SerializeField] public Transform sellSpawnPoint; 
    public void MoveCoins() 
    {
        Vector3 playerPos = PlayerController.I.GetPlayerPosition();
        
        for (int i = 0; i < lsCoins.Count; i++)
        {
            Coin c = lsCoins[i];
            c.transform.DOMove(playerPos, .5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                SaveLoadManager.AddCoin(1);
                lsCoins.Remove(c);
                c.OnDeactivate();
            });
        }
    }
    
    
}
