using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryGui : MonoBehaviour {
	public GameObject inventoryItemPrefab;
	public int widthInItems = 3;
	public int xOffset = 30;
	public int yOffset = 15;

	public void Add(string name, string lookatText, Sprite icon) {
		GameObject instance = GameObject.Instantiate (inventoryItemPrefab);
		instance.GetComponent<Image> ().sprite = icon;
		instance.transform.SetParent (transform, false);
		InventoryItem ii = instance.AddComponent<InventoryItem> ();
		ii.id = name;
		ii.lookAtText = lookatText;
		Resort ();
	}


	public void Resort () {
		int yPos = 0;
		int xPos = 0;
	
		for (int i = 1; i <= transform.childCount; i++) {
			Transform child = transform.GetChild (i-1);
			child.localPosition = new Vector2 (xPos, yPos);
			xPos += xOffset;
		
			if (i % widthInItems == 0) {
				yPos -= yOffset;
				xPos = 0;
			}
		};
	}
}
