using UnityEngine;
using UnityEditor;
using System.Collections;
[CustomEditor(typeof(Item), true)]
public class ItemEditor : Editor {
	public override void OnInspectorGUI () {
		base.OnInspectorGUI ();
		Item item = target as Item;
		if (item.pickable) {
			EditorGUILayout.LabelField ("Inventory icon");
			item.icon = EditorGUILayout.ObjectField (item.icon, typeof(Sprite), false) as Sprite;
		}

	}
}
