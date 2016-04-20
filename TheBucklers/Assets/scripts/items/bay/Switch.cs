using UnityEngine;
using System.Collections;

public class Switch : Item {
	public override IEnumerator Use () {
		yield return StartCoroutine (scriptEngine.PlayerText ("The switch makes a clicking noise"));
		scriptEngine.FlipState ("bay.switch");
	}


	public override IEnumerator LookAt () {
		yield return StartCoroutine(scriptEngine.PlayerText ("The switch that controls the bridge"));
	}

}
