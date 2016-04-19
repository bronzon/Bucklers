using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryGui : MonoBehaviour {
	public GameObject inventoryItemPrefab;

	public void Add(string name, string lookatText, Sprite icon) {
		GameObject instance = GameObject.Instantiate (inventoryItemPrefab);
		instance.GetComponent<Image> ().sprite = icon;
		instance.transform.parent = transform;
		instance.transform.position = transform.position;
		InventoryItem ii = instance.AddComponent<InventoryItem> ();
		ii.id = name;
		ii.lookAtText = lookatText;
	}


}
