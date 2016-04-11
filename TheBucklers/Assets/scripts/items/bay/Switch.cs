using UnityEngine;
using System.Collections;

public class Switch : Item {
	public override void Use (Item with) {
		Text ("The switch makes a clicking noise");
		FlipState ("bay.switch");
	}

	private bool looking = false;
	public override IEnumerator LookAt () {
		looking = true;
		Text ("The switch that controls the bridge", null, null, 3, ()=> {
			looking = false;
		});

		yield return new WaitUntil (() => {
			return !looking;
		});
	}

}
