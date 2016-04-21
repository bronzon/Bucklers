using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(InteractionPoint))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(ScriptEngine))]
public abstract class Item : MonoBehaviour {


	public bool movable = false;

	public bool pickable = false;
	[HideInInspector]
	public string inventoryId;
	[HideInInspector]
	public Sprite inventoryIcon;
	[HideInInspector]
	public string inventoryLookatText;

	private InteractionPoint interactionPoint;

	GameObject player;

	private VerbSystem verbSystem;

	protected ScriptEngine se;

	private IEnumerator Interact() {
		yield return StartCoroutine (se.MoveTo (new Vector2 (transform.position.x + interactionPoint.localTransform.x, transform.position.y + interactionPoint.localTransform.y)));
		if (interactionPoint.isLeftInteraction) {
			player.transform.localRotation = Quaternion.Euler (0, 180, 0);
		} else {
			player.transform.localRotation = Quaternion.Euler(0, 0, 0);
		}
	}

	public IEnumerator Click () {
		Debug.Log ("Started Click");
		switch (verbSystem.CurrentVerb) {
		case(Verb.USE):
			yield return StartCoroutine (Interact ());
			if (verbSystem.SelectedItem != null) {
				yield return StartCoroutine (Use (verbSystem.SelectedItem));
			} else {
				yield return StartCoroutine (Use ());
			}
		
			yield return new WaitForSeconds (0.2f);
			se.UnfreezePlayer();
			break;
		case(Verb.LOOK_AT):
			yield return StartCoroutine (Interact ());
			yield return StartCoroutine (LookAt ());
			yield return new WaitForSeconds (0.2f);
			se.UnfreezePlayer();
			break;
		case(Verb.PICK_UP):
			yield return StartCoroutine (Interact ());
			yield return StartCoroutine(PickUp ());
			yield return new WaitForSeconds (0.2f);
			se.UnfreezePlayer();
			break;
		case(Verb.TALK_TO):
			Debug.Log ("Started Talk to");
			yield return StartCoroutine (Interact ());
			yield return StartCoroutine (TalkTo ());
			yield return new WaitForSeconds (0.2f);
			se.UnfreezePlayer ();
			Debug.Log ("Ended Talking to");
			break;
		case(Verb.GIVE):
			Debug.Log ("Started Give: " +verbSystem.SelectedItem.id);
			if (verbSystem.SelectedItem != null) {
				yield return StartCoroutine (Interact ());
				Debug.Log ("interact complete");
				yield return StartCoroutine (Give (verbSystem.SelectedItem));
				yield return new WaitForSeconds (0.2f);
				se.UnfreezePlayer();
				Debug.Log ("Ended Give");
			} else {
				yield break;
			}
			break;
		default:
			Debug.Log ("current verb not implemented: " + verbSystem.CurrentVerb.ToString());
			break;
		}

		verbSystem.CurrentVerb = Verb.WALK; 
	}
		
	public virtual IEnumerator Give (InventoryItem with) {
		yield return se.PlayerText ("I don't think he wants that");
	}

	// Use this for initialization
	protected virtual void Awake () {
		GetComponent<BoxCollider2D> ().isTrigger = true;
		this.se = GetComponent<ScriptEngine> ();
		interactionPoint = GetComponent<InteractionPoint> ();
		this.verbSystem = GameObject.FindGameObjectWithTag ("VerbSystem").GetComponent<VerbSystem>();
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer> ();
		spriteRenderer.sortingOrder = -(int)(transform.position.y * 100);
		this.player = GameObject.FindGameObjectWithTag ("Player");
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

	public virtual IEnumerator LookAt() {
		yield break;			
	}

	public virtual IEnumerator Use () {
		return se.PlayerText ("I can't use that");
	}


	public virtual IEnumerator Use (InventoryItem with) {
		return se.PlayerText ("I can't use that");
	}

	public virtual IEnumerator PickUp () {
		if (pickable) {
			player.GetComponent<Animator> ().SetTrigger ("reach");
			se.AddToInventory (inventoryId, inventoryLookatText, inventoryIcon);
			yield return new WaitForSeconds (.2f);
			GameObject.Destroy (this.gameObject);
		} else {
			yield return se.PlayerText("I can't pick that up");
			se.UnfreezePlayer();
		}
	}
}
