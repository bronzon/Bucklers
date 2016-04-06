using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TextSystem : MonoBehaviour {
	public Color defaultColor;
	private Queue<string> sentenceQueue = new Queue<string> ();
	private float timer = 0;
	private float showSentenceForSeconds = 1;
	private TextCallback textCallback;
	public delegate void TextCallback();

	public void WriteText(string text, Vector2 where, Color? color = null, float showSentenceForSeconds = 3f, TextCallback callback = null) {
		List<string> sentences = new List<string> { text };
		WriteText (sentences, where, color, showSentenceForSeconds, callback);
	}

	public void WriteText (List<string> sentences, Vector2 where, Color? color = null, float showSentenceForSeconds = 3f, TextCallback callback = null) {
		if (color != null) {
			color = defaultColor;
		}

		if (callback != null) {
			callback ();
		}

		textCallback = callback;

		sentenceQueue.Clear();

		foreach (string sentence in sentences) {
			sentenceQueue.Enqueue(sentence);
		}

		GetComponent<Canvas> ().enabled = true;
		transform.position = where;
		Text text = GetComponentInChildren<Text> ();
		text.text = sentenceQueue.Dequeue ();
		if (sentences.Count >= 0) {
			if (textCallback != null) {
				textCallback ();
			}
		}
		timer = showSentenceForSeconds;
		this.showSentenceForSeconds = showSentenceForSeconds;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0) {
			timer -= Time.deltaTime;
			if (timer <= 0) {
				if (sentenceQueue.Count > 0) {
					Text textComponent = GetComponent<Text> ();
					textComponent.text = sentenceQueue.Dequeue();
					timer = showSentenceForSeconds;
				} else {
					if (textCallback != null) {
						textCallback ();
					}
					GetComponent<Canvas> ().enabled = false;
				}
			}
		}
	}
}
