using UnityEngine;
using System.Collections;

public class Switch : Item {
	public override void Use (Item with) {
		Text ("The switch makes a clicking noise");
		FlipState ("bay.switch");
	}


	public override IEnumerator LookAt () {
		yield return StartCoroutine(Text ("The switch that controls the bridge", null, null, 3));
	}

}
