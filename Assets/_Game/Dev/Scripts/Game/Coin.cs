using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Coin : PoolObject
{
    public override void OnDeactivate()
    {      
       // gameObject.transform.localScale = Vector3.one;
        gameObject.SetActive(false);
    }
    

    public override void OnSpawn()
    {
        gameObject.SetActive(true);
    }

    public void CoinAtSellPoint(Vector3 sellPoint)
    {
        Vector3 coinJumpOffset = new Vector3(Random.Range(-10,10f), 0 ,Random.Range(-10f,10f));
       // Vector3 playerPos = PlayerController.I.GetPlayerPosition();
        
       CoinManager.I.lsCoins.Add(this);
       transform.DOJump(sellPoint + coinJumpOffset, 7f, 1, 1f);

       //     .OnComplete(() =>
       // {
       //      transform.DOMove(playerPos, 2f).SetEase(Ease.Linear).OnComplete(() =>
       //      {
       //          SaveLoadManager.AddCoin(1);
       //          SoundManager.I.PlaySound(SoundName.GoldCollect);
       //          OnDeactivate();
       //      });
       // });

    }

    public void CoinMovementToUpgradeOpen(Vector3 target)
    {
        gameObject.transform.DOJump(target, 7f, 1, .7f).OnComplete(OnDeactivate);
        gameObject.transform.DOScale(Vector3.one * 0.5f, .7f).SetEase(Ease.Linear);
    }
    
    public override void OnCreated()
    {
        OnDeactivate();
    }
    
    
}
