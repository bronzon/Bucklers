using UnityEngine;
using System.Collections;

public class Switch : Item {
	public override IEnumerator Use (InventoryItem with) {
		if (with == null) {
			yield return StartCoroutine (Text ("The switch makes a clicking noise"));
			FlipState ("bay.switch");
		} else {
			yield break;
		}

	}


	public override IEnumerator LookAt () {
		yield return StartCoroutine(Text ("The switch that controls the bridge", null, null, 3));
	}

}
