﻿using UnityEngine;
using System.Collections;

public class Key : Item {
	public override void PickUp () {
		inventory.AddItem (this);
		GetComponent<SpriteRenderer> ().enabled = false;
	}

	public override string GetName () {
		return "Door key";
	}

	public override void LookAt () {
		textSystem.WriteText ("Looks like the key to the door", new Vector2());
	}
}