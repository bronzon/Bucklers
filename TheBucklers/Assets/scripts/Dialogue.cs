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
	public List<CharacterLine> lines = new List<CharacterLine>();
	protected CharacterLine AddLine(string text) {
		CharacterLine line = CharacterLine.Create (text);
		lines.Add (line);
		return line;
	}

	protected override void Start () {
		base.Start ();
		CreateDialogue ();
	}
	public override void Talk () {
		foreach (CharacterLine line in lines) {
			Debug.Log (line.text);
		}
	}

	public abstract void CreateDialogue();
}