using UnityEngine;
using System.Collections;

public class Key : Item {

	public override string GetName () {
		return "Door key";
	}

	public override void LookAt () {
		Text ("Looks like the key to the door");
	}
}
