using UnityEngine;
using UnityEditor;
using System.Collections;
[CustomEditor(typeof(InteractionPoint), true)]
public class InteractionPointEditor : Editor {
 public void OnSceneGUI()
    {
		var t = (target as InteractionPoint);

      //  EditorGUI.BeginChangeCheck();
		Handles.PositionHandle(new Vector2(t.transform.position.x + t.localTransform.x, t.transform.position.y + t.localTransform.y) , Quaternion.identity);
      
    }
} 
