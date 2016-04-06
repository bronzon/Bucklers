using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueGui : MonoBehaviour {
	public GameObject textButtonPrefab;
	public delegate void LineSelectedCallback(CharacterLine line);

	public void ShowDialogue(List<CharacterLine> lines, LineSelectedCallback callback) {
		for (int i = 0; i < lines.Count; i++) {
			var line = lines [i];
			GameObject buttonGO = GameObject.Instantiate (textButtonPrefab) as GameObject;
			Button button = buttonGO.GetComponent<Button> ();
			button.GetComponentInChildren<Text> ().text = line.text;
			button.transform.parent = transform;
			button.transform.position = transform.position;
			button.transform.Translate(new Vector3(0,i*-30,0));
			button.onClick.AddListener(() => {
				Clear();
				callback(line);
			});
		}
	}

	public void Clear() {
		foreach (Transform child in transform) {
			GameObject.Destroy (child.gameObject);
		}
	}
}
