using System;
using System.Collections.Generic;
using UnityEngine;
public class NpcResponse {
	public static NpcResponse Create(string npcText) {
		return new NpcResponse (npcText);
	}
		
	private NpcResponse (string npcText)	{
		this.npcText = npcText;
	}

	public string npcText;
	public List<CharacterLine> characterResponses = new List<CharacterLine>();
}

public class CharacterLine {
	public static CharacterLine Create (string text) {
		return new CharacterLine (text);
	}

	private CharacterLine (string text) {
		this.text = text;
	}

	public static CharacterLine Create () {
		return new CharacterLine ();
	}

	private CharacterLine() {
	}
	public NpcResponse npcResponse;
	public string text;
	public bool enabled = true;
}

public abstract class Dialogue : Item {
	private DialogueGui gui;

	public List<CharacterLine> lines = new List<CharacterLine>();
	protected CharacterLine AddLine(string text) {
		CharacterLine line = CharacterLine.Create (text);
		lines.Add (line);
		return line;
	}

	protected override void Start () {
		base.Start ();
		gui = GameObject.FindGameObjectWithTag ("DialogueGui").GetComponent<DialogueGui> ();
		CreateDialogue ();
	}

	public override System.Collections.IEnumerator TalkTo () {
		return ShowDialogue (lines);
	}

	private bool talkingCompleted = true;

	private System.Collections.IEnumerator ShowDialogue(List<CharacterLine> characterLines) {
		talkingCompleted = false;
		CharacterLine selectedLine = CharacterLine.Create ();
		yield return StartCoroutine (gui.ShowDialogue (characterLines, selectedLine));
		yield return StartCoroutine (Text (selectedLine.text, null, null, 3.0f));
		if (selectedLine.npcResponse != null && selectedLine.npcResponse.npcText != "") {
			yield return StartCoroutine (Text (selectedLine.npcResponse.npcText, transform.position, null, 3));
			if (selectedLine.npcResponse.characterResponses.Count > 0) {
				yield return ShowDialogue (selectedLine.npcResponse.characterResponses);
			} else {
				talkingCompleted = true;
			}
		} else {
			talkingCompleted = true;
		}
		yield return new WaitUntil (() => {
			return talkingCompleted;
		});
	}

	public abstract void CreateDialogue();
}