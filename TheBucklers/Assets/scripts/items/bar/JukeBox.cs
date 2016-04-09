using UnityEngine;
using System.Collections;

public class JukeBox : Item {

	public override void Use (Item with) {
		Text("Woppa gangnam style");
	}

	public override void LookAt () {
		Text ("The jukebox looks filled with power ballads from the 70ies");
	}
}
