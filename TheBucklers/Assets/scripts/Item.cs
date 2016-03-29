using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour {
	private VerbSystem verbSystem;
	protected TextSystem textSystem;

	public void Click () {
		switch (verbSystem.CurrentVerb) {
		case(Verb.USE):
			Use (null);
			break;
		case(Verb.LOOK_AT):
			LookAt ();
			break;
		default:
			Debug.Log ("current verb not implemented: " + verbSystem.CurrentVerb.ToString());
			break;
		}
	}

	// Use this for initialization
	void Start () {
		this.verbSystem = GameObject.FindGameObjectWithTag ("VerbSystem").GetComponent<VerbSystem> ();
		this.textSystem = GameObject.FindGameObjectWithTag ("TextSystem").GetComponent<TextSystem> ();
	}

	public abstract void Use (Item with);
	public abstract void LookAt();
}
