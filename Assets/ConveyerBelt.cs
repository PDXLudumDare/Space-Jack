using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FarrokhGames.Inventory;


public class ConveyerBelt : MonoBehaviour {
	[SerializeField] private ItemDefinition[] itemsToSpawn;

	GridLayoutGroup[] itemSlots;
	
	void Start(){
		itemSlots = GetComponentsInChildren<GridLayoutGroup>();
		print(itemSlots.Length);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
