using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueGui : MonoBehaviour {
	public GameObject textButtonPrefab;
	public delegate void LineSelectedCallback(CharacterLine line);

	public void ShowDialogue(List<CharacterLine> lines, LineSelectedCallback callback) {
		foreach (CharacterLine line in lines) {
			GameObject buttonGO = GameObject.Instantiate (textButtonPrefab) as GameObject;
			Button button = buttonGO.GetComponent<Button> ();
			button.GetComponentInChildren<Text> ().text = line.text;
			button.transform.parent = transform;
			button.transform.Translate(new Vector3(0,10,0));
			button.onClick.AddListener(() => {
				callback(line);
			});
		
		}
	}

	public void Clear() {
		foreach (Transform child in transform) {
			GameObject.Destroy (child);
		}
	}
}
