using UnityEngine;
using System.Collections.Generic;
[RequireComponent(typeof(Icons))]

public class Inventory : MonoBehaviour {
	private InventoryGui gui;
	private HashSet<string> ownedItems = new HashSet<string> ();

	public bool HasItem (string itemId)	{
		return ownedItems.Contains (itemId);
	}

	public void AddItem(string inventoryId, string inventoryLookAtText, Sprite inventoryIcon) {
		ownedItems.Add (inventoryId);
		gui.Add (inventoryId, inventoryLookAtText, inventoryIcon);
	}

	public void AddItem(string inventoryId, string inventoryLookAtText, string inventoryIcon) {
		AddItem (inventoryId, inventoryLookAtText, GetComponent<Icons>().spriteByName[inventoryIcon]);
	}

	public void RemoveItem (InventoryItem item) {
		ownedItems.Remove (item.id);
		DestroyImmediate (item.gameObject);	
		gui.Resort ();
	}
		
	void Awake () {
		gui = GameObject.FindGameObjectWithTag ("InventoryGui").GetComponent<InventoryGui> ();
	}

	void Update () {
	
	}
}
