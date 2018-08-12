using UnityEngine;
using UnityEngine.EventSystems;
using FarrokhGames.Inventory;

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

        // Sets the renderers's inventory to trigger drawing
        GetComponent<InventoryRenderer>().SetInventory(inventory);

        // Log items being dropped on the ground
        inventory.OnItemDropped += (item) =>
        {
            DropItem(item);
        };
    }

    private static void DropItem(IInventoryItem item)
    {
        var dropPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (!hit) { return; }
        DesireSystem crewMemberDesire = hit.collider.GetComponent<DesireSystem>();
        if (crewMemberDesire != null)
        {
            crewMemberDesire.GetItem(item);
        }
    }

    public bool AutoAddItem(ItemDefinition newItem){
        IInventoryItem item = newItem.CreateInstance();
        return inventory.Add(item);
    }
}
