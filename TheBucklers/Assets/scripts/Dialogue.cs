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

	public override void TalkTo () {
		ShowDialogue (lines);
	}

	private void ShowDialogue(List<CharacterLine> lines) {
		gui.ShowDialogue (lines, (CharacterLine selectedLine) => {
			if(selectedLine.npcResponse != null && selectedLine.npcResponse.npcText != "") {
				textSystem.WriteText(selectedLine.npcResponse.npcText, transform.position, null, 3, () => {
					if(selectedLine.npcResponse.characterResponses.Count > 0) {
						ShowDialogue(selectedLine.npcResponse.characterResponses);
					}
				});
			}
		});
	}

	public abstract void CreateDialogue();
}