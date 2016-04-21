using UnityEngine;
using System.Collections;

public class Switch : Item {
	public override IEnumerator Use () {
		yield return StartCoroutine (se.PlayerText ("The switch makes a clicking noise"));
		se.FlipState ("bay.switch");
	}


	public override IEnumerator LookAt () {
		yield return StartCoroutine(se.PlayerText ("The switch that controls the bridge"));
	}

}
