using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public enum GemType
{
    Green = 0,
    Pink = 1,
    Yellow = 2
}

public class Gem : PoolObject
{
    public GemType gemType;
    
    [SerializeField] public string gemName;
    [SerializeField] public int initialPrice;
    [SerializeField] public Sprite icon;
    [SerializeField] public GameObject gemPrefab;

    [SerializeField] private MeshRenderer mr;
    
    public Grid parentGrid;

    public bool isCollectable;

    public float scaleFactor;
    
    public override void OnDeactivate()
    {
        transform.parent = PoolManager.I.transform;
        parentGrid = null;
        gameObject.SetActive(false);
    }

    public override void OnSpawn()
    {
        gameObject.SetActive(true);
        isCollectable = false;
        ChangeTransparency(0.5f);
        transform.localScale = Vector3.zero;
        StartGrowing();
    }
    
    public override void OnCreated()
    {
        OnDeactivate();
    }
    
    private void ChangeTransparency(float val)
    {
        Color newColor = mr.material.color;
        newColor.a = val; 
        mr.material.color = newColor;
    }
    
    private void StartGrowing()
    {
        transform.DOScale(Vector3.one, 5f);
        new DelayedAction(() =>
        {
            isCollectable = true;
            ChangeTransparency(1f);
        }, 1.25f).Execute(this);
    }

    public void OnCollected(Vector3 target)
    {
        scaleFactor = transform.localScale.x;
        transform.DOLocalJump(target,5f,1, 0.75f);
        transform.localRotation = Quaternion.identity;
    }
   
    public void OnDrop(Transform target)
    {
        Vector3 sellPoint = target.position;
        transform.DOJump(sellPoint, 4f,1,0.75f).OnComplete(() =>
        {
            float priceAmount = scaleFactor * initialPrice * 100;
            SaveLoadManager.AddCoin((int)priceAmount);
            OnDeactivate();
        });
    }
}
