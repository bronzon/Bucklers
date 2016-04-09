﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Item : MonoBehaviour {
	private VerbSystem verbSystem;
	private TextSystem textSystem;
	private GameObject player;

	protected Inventory inventory;

	public delegate void VerbExecutedCallback ();
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
		this.player = GameObject.FindGameObjectWithTag ("Player");
		GetComponent<BoxCollider2D> ().isTrigger = true;
	}

	public virtual string GetName(){
		return "";
	}

	public virtual void TalkTo() {

	}

	public virtual void Use (Item with) {
		textSystem.WriteText ("I can't use that", player.transform.position);
	}

	public virtual void PickUp () {
		textSystem.WriteText ("I can't pick that up", player.transform.position);
	}

	public virtual void LookAt() {
	}

	public void Text(string text, Vector2? pos = null, Color? color = null, float showForSeconds=3f, TextSystem.TextCallback callback = null) {
		Vector2 finalPos; 
		if (pos == null) {
			finalPos = new Vector2(player.transform.position.x, player.transform.position.y);
		} else {
			finalPos = (Vector2)pos;
		}
		textSystem.WriteText (text, finalPos, color, showForSeconds, callback);
	}

	public void FreezePlayer() {
		player.GetComponent<ClickToMove> ().StopPlayer ();
		player.GetComponent<ClickToMove> ().enabled = false;	
	}

	public void UnfreezePlayer() {
		player.GetComponent<ClickToMove> ().enabled = true;	
	}
}
