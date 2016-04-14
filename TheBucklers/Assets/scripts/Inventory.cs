using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	private InventoryGui gui;

	public void AddItem(Item inventoryItem) {
		gui.Add (inventoryItem.inventoryId, inventoryItem.inventoryLookatText, inventoryItem.inventoryIcon);
	}
		
	void Start () {
		gui = GameObject.FindGameObjectWithTag ("InventoryGui").GetComponent<InventoryGui> ();
	}

	void Update () {
	
	}
}
