using UnityEngine;

namespace FarrokhGames.Inventory
{
    public class TestItem : IInventoryItem
    {
        public string Name { get; private set; }
        public Sprite Sprite { get; private set; }
        public InventoryShape Shape { get; private set; }
        public int Points { get; private set; }

        public TestItem(string name, Sprite sprite, InventoryShape shape, int points)
        {
            Name = name;
            Sprite = sprite;
            Shape = shape;
            Points = points;
        }
    }
}