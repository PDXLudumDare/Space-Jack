using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FarrokhGames.Inventory;
using System;

public class ConveyerBelt : MonoBehaviour {
	[SerializeField] int costOfDestroyedItems = 15;
	public ItemDefinition[] itemsToSpawn;
	[SerializeField] Vector2 timebetweenSpawnRange = new Vector2(1, 30f);

	public InventoryCreator dropPort;

	float nextSpawnTime;
	float timeSinceLastSpawn;

	List<GridLayoutGroup> itemSlots;
	
	PlayerStatus status;

	void Start(){
		status = FindObjectOfType<PlayerStatus>();
		itemSlots = new List<GridLayoutGroup>(GetComponentsInChildren<GridLayoutGroup>());
		itemSlots.Remove(GetComponent<GridLayoutGroup>()); //Because Unity is stupid sometimes
		nextSpawnTime = UnityEngine.Random.Range(timebetweenSpawnRange.x, timebetweenSpawnRange.y);
	}

	// Update is called once per frame
	void Update () {
		if (Time.time - timeSinceLastSpawn > nextSpawnTime){
			timeSinceLastSpawn = Time.time;
			SpawnNewItem();
			nextSpawnTime = UnityEngine.Random.Range(timebetweenSpawnRange.x, timebetweenSpawnRange.y);
		}
	}

    private void SpawnNewItem()
    {
        foreach(GridLayoutGroup slot in itemSlots){
			if (slot.transform.childCount == 0){
				ItemDefinition item = GetRandomItem();
				GameObject newItem = new GameObject();
				Image newItemImage = newItem.AddComponent<Image>();
				newItem.AddComponent<Item>().item = item;
				ConveyerController controller = newItem.AddComponent<ConveyerController>();
				controller.dropPort = dropPort;
				Button button = slot.GetComponent<Button>();
				button.onClick.AddListener(controller.OnClick);
				newItemImage.sprite = item.Sprite;
				newItemImage.transform.SetParent(slot.transform); 
				return;
			}
		}
		DestroyAllSlots();
    }

    private void DestroyAllSlots()
    {
		print("Conveyer belt clogged! Destroying all items!");
		status.ChangeSecurityPoints(-costOfDestroyedItems);
		foreach (GridLayoutGroup slot in itemSlots) {
			foreach(Transform child in slot.transform){
				child.SetParent(null);
				Destroy(child.gameObject);
			}
		}

    }

    private ItemDefinition GetRandomItem()
    {
        return itemsToSpawn[UnityEngine.Random.Range(0, itemsToSpawn.Length)];
    }
}
