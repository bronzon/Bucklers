using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class TextSystem : MonoBehaviour {
	public Color defaultColor;
	private Queue<string> sentenceQueue = new Queue<string> ();

	public System.Collections.IEnumerator WriteText(string text, Vector2 where, Color? color = null, float showSentenceForSeconds = 3f) {
		List<string> sentences = new List<string> { text };
		return WriteText (sentences, where, color, showSentenceForSeconds);
	}

	public System.Collections.IEnumerator WriteText (List<string> sentences, Vector2 where, Color? color = null, float showSentenceForSeconds = 3f) {
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

}
