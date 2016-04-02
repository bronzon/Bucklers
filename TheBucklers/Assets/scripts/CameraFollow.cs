using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	private RoomBounds roomBounds;

	void Start() {
		roomBounds = GameObject.FindGameObjectWithTag ("Room").GetComponent<RoomBounds>();
	}

	void Update () {
		Camera.main.transform.position = new Vector3(Mathf.Clamp(transform.position.x, roomBounds.cameraXMin, roomBounds.cameraXMax), Camera.main.transform.position.y, Camera.main.transform.position.z);
	}
}
