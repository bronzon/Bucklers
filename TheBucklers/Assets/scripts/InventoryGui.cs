using UnityEngine;
using System.Collections.Generic;

public class InventoryGui : MonoBehaviour {
	public void Add(Item item) {
		item.gameObject.GetComponent<SpriteRenderer> ().sprite = item.icon;
		item.gameObject.transform.parent = this.transform;
		item.gameObject.transform.position = this.transform.position;

	}


}
