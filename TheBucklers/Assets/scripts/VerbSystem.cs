using UnityEngine;
using System.Collections;

public enum Verb {
	WALK,
	USE,
	TALK_TO,
	LOOK_AT,
	PICK_UP
}

public class VerbSystem : MonoBehaviour {
	GameObject player;

	private Verb currentVerb = Verb.WALK;
	private InventoryItem selectedItem = null;

	public InventoryItem SelectedItem {
		get {
			return this.selectedItem;
		}
		set {
			selectedItem = value;
		}
	}

	public Verb CurrentVerb {
		get {
			return this.currentVerb;
		} 
		set {
			this.currentVerb = value;
		}
	}

	public void SetCurrentVerb (string verb) {
		currentVerb = (Verb)System.Enum.Parse (typeof(Verb), verb);
		if (currentVerb != Verb.WALK) {
			player.GetComponent<ClickToMove> ().StopPlayer ();
			player.GetComponent<ClickToMove> ().enabled = false;	
		}
	}

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
