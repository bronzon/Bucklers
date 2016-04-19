using UnityEngine;
using System.Collections;
using UnityEditor;

public class LeChuck : Dialogue {
	public override void CreateDialogue () {
		AddLine("Prepare to die!").npcResponse = NpcResponse.Create("I am not impressed, ok bye");

		CharacterLine goldLine = AddLine ("Give me all yer gold!", FSetState("bay.gold", true));
		goldLine.npcResponse = NpcResponse.Create ("ok");
		goldLine.npcResponse.characterResponses.Add (CharacterLine.Create ("woho I rule"));
		CharacterLine thankYou = CharacterLine.Create ("Thank you sir");
		goldLine.npcResponse.characterResponses.Add (thankYou);
		thankYou.npcResponse = NpcResponse.Create ("You are welcome");
	}

	public override IEnumerator LookAt () {
		yield return StartCoroutine(Text ("help me moma i'm scared", null, null, 3));
	}

	public override IEnumerator Use(InventoryItem inventoryItem) {
		if (inventoryItem.id == "grogg") {
			return Text ("Thanks I like grog", transform.position);
		} else {
			return base.Use (inventoryItem);
		}
	}

}
