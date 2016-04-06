﻿using UnityEngine;
using System.Collections;

public enum Verb {
	WALK,
	USE,
	TALK_TO,
	LOOK_AT,
	PICK_UP
}

public class VerbSystem : MonoBehaviour {
	private Verb currentVerb = Verb.WALK;

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
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
