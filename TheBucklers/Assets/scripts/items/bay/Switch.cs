using UnityEngine;
using System.Collections;

public class Switch : Item {
	public override void Use (Item with) {
		Text ("The switch makes a clicking noise");
		FlipState ("bay.switch");
	}

	public override void LookAt () {
		Text ("The switch that controls the bridge");
	}

}
