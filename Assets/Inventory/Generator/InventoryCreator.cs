﻿using UnityEngine;
using FarrokhGames.Inventory;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InventoryRenderer))]
public class InventoryCreator : MonoBehaviour
{
    [SerializeField] private int _width = 8;
    [SerializeField] private int _height = 4;
    [SerializeField] private ItemDefinition[] _definitions;
    [SerializeField] private bool _fillEmpty = false; // Should the inventory get completely filled?

    InventoryManager inventory;

    void Start()
    {
        // Create inventory
        inventory = new InventoryManager(_width, _height);

        // // Fill inventory with random items
        // var tries = (_width * _height) / 3;
        // for (var i = 0; i < tries; i++)
        // {
        //     inventory.Add(_definitions[Random.Range(0, _definitions.Length)].CreateInstance());
        // }

        // Sets the renderers's inventory to trigger drawing
        GetComponent<InventoryRenderer>().SetInventory(inventory);

        // Log items being dropped on the ground
        inventory.OnItemDropped += (item) =>
        {
            var dropPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            print(dropPosition);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (!hit){ return; }
            DesireSystem crewMember = hit.collider.GetComponent<DesireSystem>();
            if(crewMember != null)
            {
                Debug.Log ("Target: " + crewMember);
                crewMember.GetItem(item);
            }
        };
    }

    public bool AutoAddItem(ItemDefinition newItem){
        return inventory.Add(newItem.CreateInstance());
    }
}
