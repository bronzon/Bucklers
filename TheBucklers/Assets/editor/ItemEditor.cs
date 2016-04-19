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
			item.inventoryIcon = EditorGUILayout.ObjectField (item.inventoryIcon, typeof(Sprite), false) as Sprite;
			item.inventoryLookatText = EditorGUILayout.TextField ("look at text:", item.inventoryLookatText); 
			item.inventoryId =  EditorGUILayout.TextField ("unique id", item.inventoryId); 
		}

	}
}
