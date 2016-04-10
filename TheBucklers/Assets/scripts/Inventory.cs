using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	private List<Item> items = new List<Item>();
	private InventoryGui gui;

	public void AddItem(Item item) {
		items.Add (item);
		gui.Add (item);
	}
		
	void Start () {
		gui = GameObject.FindGameObjectWithTag ("InventoryGui").GetComponent<InventoryGui> ();
	}

	void Update () {
	
	}
}
