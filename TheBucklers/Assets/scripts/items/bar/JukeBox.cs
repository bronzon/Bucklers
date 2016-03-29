using UnityEngine;
using System.Collections;

public class JukeBox : Item {

	public override void Use (Item with) {
		textSystem.WriteText("Woppa gangnam style", new Vector2(0,0));
	}

	public override void LookAt () {
		textSystem.WriteText ("The jukebox looks filled with power ballads from the 70ies", new Vector2 (0, 0));
	}
}
