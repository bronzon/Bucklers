using UnityEngine;
using System.Collections;
using UnityEditor;

public class LeChuck : Dialogue {
	public override void CreateDialogue () {
		AddLine("Hand over the key to your boat LeChuck!", Predicates.AndPredicate(se.GameStatePredicate ("gave lechuck switch", false), se.InventoryPredicate("boatkey", false))).npcResponse = NpcResponse.Create("I'm too hungry to do that");
		AddLine ("Now can I have the key?", Predicates.AndPredicate(se.GameStatePredicate ("gave lechuck switch", true), se.InventoryPredicate("boatkey", false)),  se.FGiveInventoryItem("boatkey", "The key to lechucks boat", "icons_48")).npcResponse = NpcResponse.Create("A fair trade for a banana switch!");

		CharacterLine goldLine = AddLine ("Give me all yer gold!");
		goldLine.npcResponse = NpcResponse.Create ("never!");
		goldLine.npcResponse.characterResponses.Add (CharacterLine.Create ("Bad form!"));

		CharacterLine thenYouShallDie = CharacterLine.Create ("Then you shall die!");
		goldLine.npcResponse.characterResponses.Add (thenYouShallDie);
		thenYouShallDie.npcResponse = NpcResponse.Create ("How rude!");
	}

	public override IEnumerator LookAt () {
		yield return StartCoroutine(se.PlayerText ("help me moma i'm scared"));
	}

	public override IEnumerator Give (InventoryItem with) {
		if (with.id == "switch") {
			se.SetState ("gave lechuck switch");
			se.RemoveFromInventory (with);
			yield return se.NpcText ("Thanks, Hard to compete with a good banana switch");
		} else {
			yield break;
		}
	}
}
