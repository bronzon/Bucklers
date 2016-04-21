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
	public static CharacterLine Create (string text, Predicate predicate = null, Action action = null) {
		return new CharacterLine (text, predicate, action);
	}

	private CharacterLine (string text, Predicate predicate = null, Action action = null) {
		this.text = text;
		this.action = action;
		if (predicate == null) {
			this.predicate = Predicates.TruePredicate ();
		} else {
			this.predicate = predicate;
		}
	}

	public static CharacterLine Create () {
		return new CharacterLine ();
	}

	public bool PlayerMeetsRequirements() {	
		return predicate.apply ();
	}

	private CharacterLine() {}

	private Predicate predicate;
	public NpcResponse npcResponse;
	public string text;
	public Action action;
	public bool enabled = true;
}

public abstract class Dialogue : Item {
	private DialogueGui gui;
	private GameObject inventoryGui;
	private GameObject verbsGui;
	public Color speachColor = Color.blue;
	public List<CharacterLine> lines = new List<CharacterLine>();


	protected CharacterLine AddLine(string text, Predicate gameStatePredicate = null, Action action = null) {
		CharacterLine line = CharacterLine.Create (text, gameStatePredicate, action);
		lines.Add (line);
		return line;
	}

	protected override void Awake () {
		base.Awake();
		gui = GameObject.FindGameObjectWithTag ("DialogueGui").GetComponent<DialogueGui> ();
		verbsGui = GameObject.FindGameObjectWithTag ("VerbsGui");
		inventoryGui = GameObject.FindGameObjectWithTag ("InventoryGui");
	}

	void Start() {
		CreateDialogue ();
	}

	public override System.Collections.IEnumerator TalkTo () {
		return ShowDialogue (lines);
	}

	private bool talkingCompleted = true;

	private System.Collections.IEnumerator ShowDialogue(List<CharacterLine> characterLines) {
		verbsGui.SetActive (false);
		inventoryGui.SetActive (false);

		talkingCompleted = false;
		CharacterLine selectedLine = CharacterLine.Create ();
		yield return StartCoroutine (gui.ShowDialogue (characterLines, selectedLine));

		if (selectedLine.action != null) {
			selectedLine.action (); //replace with func that returns coroutine
		}

		yield return StartCoroutine (se.PlayerText (selectedLine.text));
		if (selectedLine.npcResponse != null && selectedLine.npcResponse.npcText != "") {
			if (selectedLine.npcResponse.action != null) {
				selectedLine.npcResponse.action (); //replace with func that returns coroutine
			}
			yield return StartCoroutine (se.NpcText (selectedLine.npcResponse.npcText, speachColor));
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

		verbsGui.SetActive (true);
		inventoryGui.SetActive (true);

	}

	public abstract void CreateDialogue();
}