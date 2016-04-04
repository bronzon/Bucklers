using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Item : MonoBehaviour {
	private VerbSystem verbSystem;
	protected TextSystem textSystem;
	protected Inventory inventory;

	public void Click () {
		switch (verbSystem.CurrentVerb) {
		case(Verb.USE):
			Use (null);
			break;
		case(Verb.LOOK_AT):
			LookAt ();
			break;
		case(Verb.PICK_UP):
			PickUp ();
			break;
		case(Verb.TALK_TO):
			TalkTo ();
			break;
		default:
			Debug.Log ("current verb not implemented: " + verbSystem.CurrentVerb.ToString());
			break;
		}
	}

	// Use this for initialization
	protected virtual void Start () {
		this.verbSystem = GameObject.FindGameObjectWithTag ("VerbSystem").GetComponent<VerbSystem> ();
		this.textSystem = GameObject.FindGameObjectWithTag ("TextSystem").GetComponent<TextSystem> ();
		this.inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		GetComponent<BoxCollider2D> ().isTrigger = true;
	}

	public virtual string GetName(){
		return "";
	}

	public virtual void TalkTo() {
		
	}

	public virtual void Use (Item with) {
		textSystem.WriteText ("I can't use that", new Vector2());
	}

	public virtual void PickUp () {
		textSystem.WriteText ("I can't pick that up", new Vector2());
	}

	public virtual void LookAt() {
			
	}
}
