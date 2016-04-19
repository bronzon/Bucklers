using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class InventoryItem : MonoBehaviour {
	public string id;
	public string lookAtText;

	private GameObject player;
	private VerbSystem verbSystem;
	private TextSystem textSystem;

	protected virtual void Start () {
		this.verbSystem = GameObject.FindGameObjectWithTag ("VerbSystem").GetComponent<VerbSystem> ();
		this.textSystem = GameObject.FindGameObjectWithTag ("TextSystem").GetComponent<TextSystem> ();
		this.player = GameObject.FindGameObjectWithTag ("Player");
	}


	public void Click () {
		switch (verbSystem.CurrentVerb) {
		case(Verb.USE):
			verbSystem.SelectedItem = this;
			break;
		case(Verb.LOOK_AT):
			StartCoroutine(textSystem.WriteText (lookAtText, new Vector2 (player.transform.position.x, player.transform.position.y)));
			break;
		}
	}
}
