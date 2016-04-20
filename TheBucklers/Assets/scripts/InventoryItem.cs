using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(ScriptEngine))]
public class InventoryItem : MonoBehaviour {
	public string id;
	public string lookAtText;

	private GameObject player;
	private VerbSystem verbSystem;
	private ScriptEngine scriptEngine;

	protected virtual void Start () {
		this.verbSystem = GameObject.FindGameObjectWithTag ("VerbSystem").GetComponent<VerbSystem> ();
		this.player = GameObject.FindGameObjectWithTag ("Player");
		this.scriptEngine = GetComponent<ScriptEngine>();
	}


	public IEnumerator Click () {
		switch (verbSystem.CurrentVerb) {
		case(Verb.USE):
			verbSystem.SelectedItem = this;
			break;
		case(Verb.LOOK_AT):
			yield return StartCoroutine(scriptEngine.PlayerText(lookAtText));
			yield return new WaitForSeconds (0.2f);
			scriptEngine.UnfreezePlayer();
			break;
		}
	}
}
