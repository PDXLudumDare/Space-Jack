using UnityEngine;
using FarrokhGames.Inventory;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 1)]
public class ItemDefinition : ScriptableObject, IInventoryItem
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private InventoryShape _shape;
    [SerializeField] private int points;

    public int Points { get { return this.points; }}
    public string Name { get { return this.name; } }
    public Sprite Sprite { get { return _sprite; } }
    public InventoryShape Shape { get { return _shape; } }

    /// <summary>
    /// Creates a copy if this scriptable object
    /// </summary>
    public IInventoryItem CreateInstance()
    {
        return ScriptableObject.Instantiate(this);
    }
}
