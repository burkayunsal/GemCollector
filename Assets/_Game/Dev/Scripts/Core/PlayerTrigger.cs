using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {      
        if(other.CompareTag("Draggable"))
        {
           
           
        }
        else if (other.CompareTag("SellArea"))
        {
            SellArea sellArea = other.GetComponent<SellArea>();
            sellArea.OnPlayerEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SellArea"))
        {
            SellArea sellArea = other.GetComponent<SellArea>();
            sellArea.OnPlayerExit();
            CoinManager.I.MoveCoins();
        }
    }

}
