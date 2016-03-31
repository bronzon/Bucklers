using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(WalkMesh))]
class WalkMeshEditor : Editor {
	private WalkMesh walkMesh;

	void OnEnable() {
		walkMesh = target as WalkMesh;	
	}

    void OnSceneGUI () {
		if(walkMesh.points.Count >= 2) {
			walkMesh.points.Add (walkMesh.points [0]); //close the loop before drawing
			Handles.DrawPolyLine(walkMesh.points.ToArray());
			walkMesh.points.RemoveAt (walkMesh.points.Count - 1);
		}   

		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Space) {
			Vector2 mousePos = Event.current.mousePosition;
			mousePos.y = Camera.current.pixelHeight - mousePos.y;
			Vector3 position = Camera.current.ScreenPointToRay(mousePos).origin;
			position.z = 0;
			walkMesh.points.Add (position);
		}
	}
}

