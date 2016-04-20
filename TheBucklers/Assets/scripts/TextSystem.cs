﻿using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class TextSystem : MonoBehaviour {
	public Color defaultColor;
	public int textOffsetFromCamera = 100;

	private Queue<string> sentenceQueue = new Queue<string> ();
	private RoomBounds roomBounds;

	void Start() {
		roomBounds = GameObject.FindGameObjectWithTag ("Room").GetComponent<RoomBounds>();
	}

	public System.Collections.IEnumerator WriteText(string text, Vector2 where, Color? color = null, float showSentenceForSeconds = 3f) {
		List<string> sentences = new List<string> { text };
		return WriteText (sentences, where, color, showSentenceForSeconds);
	}

	public System.Collections.IEnumerator WriteText (List<string> sentences, Vector2 where, Color? color = null, float showSentenceForSeconds = 3f) {
		ClampTextInRoom (ref where);
		if (color != null) {
			color = defaultColor;
		}

		sentenceQueue.Clear();

		foreach (string sentence in sentences) {
			sentenceQueue.Enqueue(sentence);
		}

		GetComponent<Canvas> ().enabled = true;
		transform.position = where;
		Text text = GetComponentInChildren<Text> ();

		text.text = sentenceQueue.Dequeue ();

		yield return new WaitForSeconds (showSentenceForSeconds);

		while (sentenceQueue.Count > 0) {
			Text textComponent = GetComponent<Text> ();
			textComponent.text = sentenceQueue.Dequeue();
			yield return new WaitForSeconds (showSentenceForSeconds);
		}

		GetComponent<Canvas> ().enabled = false;

	}

	private void ClampTextInRoom (ref Vector2 where)	{
		float textMin = roomBounds.cameraXMin - textOffsetFromCamera;
		float textMax = roomBounds.cameraXMax + textOffsetFromCamera;
		where.x = Mathf.Clamp(where.x, textMin, textMax);
	}
}
