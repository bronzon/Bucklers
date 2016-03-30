using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	List<Item> items = new List<Item>();

	public void AddItem(Item item) {
		items.Add (item);
		//itemGui.redraw(items);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
