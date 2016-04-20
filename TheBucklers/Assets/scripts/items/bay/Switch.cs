using UnityEngine;
using System.Collections;

public class Switch : Item {
	public override IEnumerator Use () {
		yield return StartCoroutine (PlayerText ("The switch makes a clicking noise"));
		FlipState ("bay.switch");
	}


	public override IEnumerator LookAt () {
		yield return StartCoroutine(PlayerText ("The switch that controls the bridge"));
	}

}
