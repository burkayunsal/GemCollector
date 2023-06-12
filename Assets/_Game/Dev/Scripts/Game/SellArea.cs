using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellArea : MonoBehaviour
{
 
    private bool stopCoroutine = false;
    [SerializeField] private Transform SellPoint;
    
    public void OnPlayerEnter()
    {
        stopCoroutine = false;
        StartCoroutine(ReleaseRoutine());
    }
    IEnumerator ReleaseRoutine()
    {
        while (!stopCoroutine)
        {
            yield return new WaitForSeconds(0.025f);
        }
    }

    public void OnPlayerExit()
    {
        stopCoroutine = true;
        
    }

}
