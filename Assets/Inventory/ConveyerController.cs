using System;
using FarrokhGames.Shared;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConveyerController : MonoBehaviour
    {
    public InventoryCreator dropPort;

    public void OnClick(){
        
        bool result = dropPort.AutoAddItem(GetComponent<Item>().item);
        if (result){
           Destroy(gameObject);
        }
    }

}
