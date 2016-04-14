using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(InteractionPoint))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Item : MonoBehaviour {


	public bool movable = false;

	public bool pickable = false;
	[HideInInspector]
	public string inventoryId;
	[HideInInspector]
	public Sprite inventoryIcon;
	[HideInInspector]
	public string inventoryLookatText;

	private VerbSystem verbSystem;
	private TextSystem textSystem;
	private GameState gameState;

	private GameObject player;
	private InteractionPoint interactionPoint;
	private Inventory inventory;

	public delegate void VerbExecutedCallback ();

	public IEnumerator Click () {
		switch (verbSystem.CurrentVerb) {
		case(Verb.USE):
			yield return StartCoroutine (MoveToInteractionPoint ());
			yield return StartCoroutine (Use (null));
			UnfreezePlayer();
			break;
		case(Verb.LOOK_AT):
			yield return StartCoroutine (MoveToInteractionPoint ());
			yield return StartCoroutine (LookAt ());
			yield return new WaitForSeconds (0.2f);
			UnfreezePlayer();
			break;
		case(Verb.PICK_UP):
			yield return StartCoroutine (MoveToInteractionPoint ());
			PickUp ();
			break;
		case(Verb.TALK_TO):
			yield return StartCoroutine (MoveToInteractionPoint ());
			yield return StartCoroutine (TalkTo ());
			UnfreezePlayer();
			break;
		default:
			Debug.Log ("current verb not implemented: " + verbSystem.CurrentVerb.ToString());
			break;
		}

		verbSystem.CurrentVerb = Verb.WALK; 
	}

	// Use this for initialization
	protected virtual void Start () {
		this.verbSystem = GameObject.FindGameObjectWithTag ("VerbSystem").GetComponent<VerbSystem> ();
		this.textSystem = GameObject.FindGameObjectWithTag ("TextSystem").GetComponent<TextSystem> ();
		this.inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		this.gameState = GameState.Instance;

		this.player = GameObject.FindGameObjectWithTag ("Player");
		GetComponent<BoxCollider2D> ().isTrigger = true;
		interactionPoint = GetComponent<InteractionPoint> ();
	

		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer> ();
		spriteRenderer.sortingOrder = -(int)(transform.position.y * 100);
	
	}

	void Update() {
		if (!movable) {
			return;
		}
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer> ();
		spriteRenderer.sortingOrder = -(int)(transform.position.y * 100);
	}

	public virtual string GetName(){
		return "";
	}

	public virtual IEnumerator TalkTo() {
		yield break;
	}

	public virtual IEnumerator Use (Item with) {
		return textSystem.WriteText ("I can't use that", player.transform.position);
	}

	public virtual void PickUp () {
		if (pickable) {
			AddToInventory (this);
			Destroy(this.gameObject);
		} else {
			textSystem.WriteText ("I can't pick that up", player.transform.position);
		}
	}


	public virtual IEnumerator LookAt() {
		yield break;			
	}

	public IEnumerator Text(string text, Vector2? pos = null, Color? color = null, float showForSeconds=3f) {
		Vector2 finalPos; 
		if (pos == null) {
			finalPos = new Vector2(player.transform.position.x, player.transform.position.y);
		} else {
			finalPos = (Vector2)pos;
		}
		return textSystem.WriteText (text, finalPos, color, showForSeconds);
	}

	public delegate void InteractionComplete ();

	private bool movingToInteractionPoint = false;

	private IEnumerator MoveToInteractionPoint() {
		movingToInteractionPoint = true;
		Vector2 target = new Vector2(transform.position.x + interactionPoint.localTransform.x, transform.position.y+interactionPoint.localTransform.y);

		PolyNavAgent polyNavAgent = player.GetComponent<PolyNavAgent> ();
		polyNavAgent.SetDestination (target);
		System.Action action = null;
		action =  () => {
			polyNavAgent.OnDestinationReached -= action;
			movingToInteractionPoint = false;
		};
		polyNavAgent.OnDestinationReached += action;
		yield return new WaitUntil(()=> { return !movingToInteractionPoint;});
	}

	public void UnfreezePlayer() {
		player.GetComponent<ClickToMove> ().enabled = true;	
	}

	public void AddToInventory(Item item) {
		inventory.AddItem (item);
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
