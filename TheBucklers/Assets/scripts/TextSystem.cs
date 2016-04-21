using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class TextSystem : MonoBehaviour {
	public int textOffsetFromCamera = 100;
	public float textSpeedInSecondsShown = 3;
	private Queue<string> sentenceQueue = new Queue<string> ();
	private RoomBounds roomBounds;

	void Start() {
		roomBounds = GameObject.FindGameObjectWithTag ("Room").GetComponent<RoomBounds>();
	}

	public System.Collections.IEnumerator WriteText(string text, Vector2 where, Color? color = null) {
		List<string> sentences = new List<string> { text };
		return WriteText (sentences, where, color);
	}

	public System.Collections.IEnumerator WriteText (List<string> sentences, Vector2 where, Color? color = null) {
		ClampTextInRoom (ref where);
		Color selectedColor;
		if (color != null) {
			selectedColor = (Color)color;
		} else {
			selectedColor = Color.white;
		}

		sentenceQueue.Clear();

		foreach (string sentence in sentences) {
			sentenceQueue.Enqueue(sentence);
		}

		GetComponent<Canvas> ().enabled = true;
		transform.position = where;
		Text text = GetComponentInChildren<Text> ();

		text.text = sentenceQueue.Dequeue ();
		text.color = selectedColor;

		yield return new WaitForSeconds (textSpeedInSecondsShown);

		while (sentenceQueue.Count > 0) {
			Text textComponent = GetComponent<Text> ();
			textComponent.text = sentenceQueue.Dequeue();
			textComponent.color = selectedColor;
			yield return new WaitForSeconds (textSpeedInSecondsShown);
		}

		GetComponent<Canvas> ().enabled = false;

	}

	private void ClampTextInRoom (ref Vector2 where)	{
		float textMin = roomBounds.cameraXMin - textOffsetFromCamera;
		float textMax = roomBounds.cameraXMax + textOffsetFromCamera;
		where.x = Mathf.Clamp(where.x, textMin, textMax);
	}
}
