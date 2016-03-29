using UnityEngine;
using System.Collections;

public class ItemClick : MonoBehaviour {
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 1 << LayerMask.NameToLayer("Items"));
			if (hit.collider != null) {
				Item item = hit.collider.gameObject.GetComponent<Item> ();
				if (item != null) {
					item.Click ();
				}
			}
		}
	}
}
