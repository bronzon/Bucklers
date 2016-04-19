using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	private InventoryGui gui;

	public void AddItem(string inventoryId, string inventoryLookAtText, Sprite inventoryIcon) {
		gui.Add (inventoryId, inventoryLookAtText, inventoryIcon);
	}
		
	void Start () {
		gui = GameObject.FindGameObjectWithTag ("InventoryGui").GetComponent<InventoryGui> ();
	}

	void Update () {
	
	}
}
