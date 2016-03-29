using UnityEngine;
using System.Collections;

public class Door : Item {
	public override void Use (Item with) {
		textSystem.WriteText ("The door is locked and you don't have the key", new Vector2(0,0));
	}

	public override void LookAt () {
		textSystem.WriteText ("It looks sturdy enough to withstand a nuclear attack", new Vector2 (0, 0));
	}
}
