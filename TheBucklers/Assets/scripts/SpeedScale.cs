using UnityEngine;
using System.Collections;

public class SpeedScale : MonoBehaviour {
	private float scale = 1;
	public float Scale {
		get {
			return this.scale;
		}
		set {
			scale = value;
		}
	}

}
