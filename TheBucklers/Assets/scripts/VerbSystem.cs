using UnityEngine;
using System.Collections;

public enum Verb {
	WALK,
	USE,
	TALK,
	LOOK_AT,
	PICK_UP
}

public class VerbSystem : MonoBehaviour {
	private Verb currentVerb = Verb.WALK;
	public Verb CurrentVerb {
		get {
			return this.currentVerb;
		}
	}
	public void SetCurrentVerb (string verb) {
		currentVerb = (Verb)System.Enum.Parse (typeof(Verb), verb);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
