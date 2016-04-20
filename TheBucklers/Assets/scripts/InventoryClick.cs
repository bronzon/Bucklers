using UnityEngine;
using System.Collections;

public class InventoryClick : MonoBehaviour {

	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero, Mathf.Infinity, 1 << LayerMask.NameToLayer("UI"));
			if (hit.collider != null) {
				InventoryItem item = hit.collider.gameObject.GetComponent<InventoryItem> ();
				if (item != null) {
					StartCoroutine(item.Click ());
				}
			}
		}
	}
}
