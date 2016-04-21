using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterSizeController))]
public class PlayerController : MonoBehaviour {
	public float baseSpeed = 1;
	private Animator animator;
	private CharacterSizeController sizeController;

	void Awake () {
		animator = GetComponent<Animator>();
		sizeController = GetComponent <CharacterSizeController> ();
	}

	// Update is called once per frame
	void Update () {
		Vector2 direction = new Vector2(Input.GetAxisRaw ("Horizontal"),Input.GetAxisRaw ("Vertical"));
		if (direction.magnitude == 0) {
			animator.enabled = false;
		} else {
			animator.enabled = true;
		}
		animator.SetInteger ("horizontal", (int)direction.x);
		animator.SetInteger ("vertical", (int)direction.y);

		if (direction.x > 0) {
			transform.localRotation = Quaternion.Euler (0, 0, 0);
		} else {
			transform.localRotation = Quaternion.Euler(0, 180, 0);
		}

		if (direction.magnitude > float.Epsilon) {
			float actualSpeed = sizeController.scalePercentage * baseSpeed;
			Vector2 estimatedPos = new Vector2(transform.position.x + direction.x*actualSpeed, transform.position.y + direction.y*actualSpeed);
			RaycastHit2D hit = Physics2D.Linecast (transform.position, estimatedPos, 1 << LayerMask.NameToLayer("Background"));
			if (hit.collider == null) {
				transform.position = estimatedPos;
			}
		}
	}
}
