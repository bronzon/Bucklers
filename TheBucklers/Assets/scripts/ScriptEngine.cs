using UnityEngine;
using System.Collections;

public class ScriptEngine : MonoBehaviour {
	private VerbSystem verbSystem;
	private TextSystem textSystem;
	private Inventory inventory;
	private GameState gameState;
	private GameObject player;

	void Start () {
		this.verbSystem = GameObject.FindGameObjectWithTag ("VerbSystem").GetComponent<VerbSystem> ();
		this.textSystem = GameObject.FindGameObjectWithTag ("TextSystem").GetComponent<TextSystem> ();
		this.inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		this.gameState = GameState.Instance;
		this.player = GameObject.FindGameObjectWithTag ("Player");	
	}
	

	public IEnumerator NpcText(string text, Color? color = null, float showForSeconds=3f) {
		return textSystem.WriteText (text, new Vector2(transform.position.x, transform.position.y+60), color, showForSeconds);
	}

	public IEnumerator PlayerText(string text, Color? color = null, float showForSeconds=3f) {
		return textSystem.WriteText (text, new Vector2(player.transform.position.x, player.transform.position.y+60), color, showForSeconds);
	}

	private bool moving = false;

	public IEnumerator MoveTo(Vector2 target) {
		moving = true;
		PolyNavAgent polyNavAgent = player.GetComponent<PolyNavAgent> ();
		polyNavAgent.SetDestination (target);
		System.Action action = null;
		action =  () => {
			polyNavAgent.OnDestinationReached -= action;
			moving = false;
		};
		polyNavAgent.OnDestinationReached += action;
		yield return new WaitUntil(()=> { return !moving;});
	}

	public void UnfreezePlayer() {
		player.GetComponent<ClickToMove> ().enabled = true;	
	}

	public void AddToInventory(string inventoryId, string inventoryLookAtText, string inventoryIcon) {
		Sprite icon = Resources.Load (inventoryIcon) as Sprite;
		inventory.AddItem (inventoryId, inventoryLookAtText, icon);
	}

	public void AddToInventory(string inventoryId, string inventoryLookAtText, Sprite inventoryIcon) {
		inventory.AddItem (inventoryId, inventoryLookAtText, inventoryIcon);
	}

	public void SetState(string name, bool set) {
		gameState.SetState (name, set);
	}

	public System.Action FSetState(string name, bool set) {
		return () => {
			SetState (name, set);
		};
	}

	public bool FlipState(string name, bool ifNoValueIsSet=true) {
		return gameState.FlipState (name, ifNoValueIsSet);
	}

	public bool GetState(string name, bool defaultValue=false) {
		return gameState.GetState (name, defaultValue);
	}
}
