﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public struct LineSelectedCallback {
	public CharacterLine line;
}

public class DialogueGui : MonoBehaviour {
	public GameObject textButtonPrefab;

	private bool showingGui = false;
	public System.Collections.IEnumerator ShowDialogue(List<CharacterLine> lines, CharacterLine selectedLine) {
		showingGui = true;
		int yOffset = 0;
		for (int i = 0; i < lines.Count; i++) {
			var line = lines [i];
			if (!line.PlayerMeetsRequirements ()) {
				continue;
			}
			GameObject buttonGO = GameObject.Instantiate (textButtonPrefab) as GameObject;
			Button button = buttonGO.GetComponent<Button> ();
			button.GetComponentInChildren<Text> ().text = line.text;
			button.transform.parent = transform;
			button.transform.position = transform.position;
			button.transform.Translate(new Vector3(0,yOffset,0));
			yOffset -= 30;
			button.onClick.AddListener(Functor(selectedLine, line));
		}
		yield return new WaitUntil (() => {
			return showingGui == false;
		});
	}

	private UnityEngine.Events.UnityAction Functor(CharacterLine selectedLine, CharacterLine line) {
		return () => {
			Clear ();
			selectedLine.text = line.text;
			selectedLine.npcResponse = line.npcResponse;
			selectedLine.action = line.action;
			showingGui = false;
		};
	}

	public void Clear() {
		foreach (Transform child in transform) {
			GameObject.Destroy (child.gameObject);
		}
	}
}
