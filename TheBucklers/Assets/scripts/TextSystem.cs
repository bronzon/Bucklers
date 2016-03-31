﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TextSystem : MonoBehaviour {
	private Queue<string> sentenceQueue = new Queue<string> ();
	private float timer = 0;
	private float showSentenceForSeconds = 1;

	public void WriteText(string text, Vector2 where, float showSentenceForSeconds = 3f) {
		List<string> sentences = new List<string> (new string[]{ text });
		WriteText (sentences, where, showSentenceForSeconds);
	}

	public void WriteText (List<string> sentences, Vector2 where, float showSentenceForSeconds = 3f) {
		sentenceQueue.Clear();

		foreach (string sentence in sentences) {
			sentenceQueue.Enqueue(sentence);
		}

		GetComponent<Canvas> ().enabled = true;
		transform.position = where;
		Text text = GetComponentInChildren<Text> ();
		text.text = sentenceQueue.Dequeue ();

		timer = showSentenceForSeconds;
		this.showSentenceForSeconds = showSentenceForSeconds;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0) {
			timer -= Time.deltaTime;
			if (timer <= 0) {
				if (sentenceQueue.Count > 0) {
					Text textComponent = GetComponentInChildren<Text> ();
					textComponent.text = sentenceQueue.Dequeue();
					timer = showSentenceForSeconds;
				} else {
					GetComponent<Canvas> ().enabled = false;
				}
			}
		}
	}
}