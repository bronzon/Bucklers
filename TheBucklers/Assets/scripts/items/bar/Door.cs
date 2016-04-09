using UnityEngine;
using System.Collections;

public class Door : Item {
	public override void Use (Item with) {
		Text("The door is locked and you don't have the key");
	}

	public override void LookAt () {
		Text ("It looks sturdy enough to withstand a nuclear attack");
	}
}
