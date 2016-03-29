using UnityEngine;
using System.Collections;

public class JukeBox : Item {

	public override void Use (Item with) {
		Debug.Log ("Woppa gangnam style");
	}

	public override void LookAt () {
		Debug.Log ("The jukebox is filled with power ballads from the 70ies");
	}
}
