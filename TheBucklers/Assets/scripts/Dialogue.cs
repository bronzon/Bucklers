using System;
using System.Collections.Generic;
using UnityEngine;
public class NpcResponse {

	public static NpcResponse Create(string npcText, Action action = null) {
		return new NpcResponse (npcText, action);
	}
		
	private NpcResponse (string npcText, Action action)	{
		this.npcText = npcText;
		this.action = action;
	}

	public string npcText;
	public Action action;

	public List<CharacterLine> characterResponses = new List<CharacterLine>();
}

public class CharacterLine {
	public static CharacterLine Create (string text, Action action = null) {
		return new CharacterLine (text, action);
	}

	private CharacterLine (string text, Action action = null) {
		this.text = text;
		this.action = action;
	}

	public static CharacterLine Create () {
		return new CharacterLine ();
	}

	private CharacterLine() {
	}
	public NpcResponse npcResponse;
	public string text;
	public Action action;
	public bool enabled = true;
}

public abstract class Dialogue : Item {
	private DialogueGui gui;

	public List<CharacterLine> lines = new List<CharacterLine>();

	protected CharacterLine AddLine(string text, Action action = null) {
		CharacterLine line = CharacterLine.Create (text, action);
		lines.Add (line);
		return line;
	}

	protected override void Start () {
		base.Start();
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

		if (selectedLine.action != null) {
			selectedLine.action (); //replace with func that returns coroutine
		}

		yield return StartCoroutine (scriptEngine.PlayerText (selectedLine.text));
		if (selectedLine.npcResponse != null && selectedLine.npcResponse.npcText != "") {
			if (selectedLine.npcResponse.action != null) {
				selectedLine.npcResponse.action (); //replace with func that returns coroutine
			}
			yield return StartCoroutine (scriptEngine.NpcText (selectedLine.npcResponse.npcText));
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