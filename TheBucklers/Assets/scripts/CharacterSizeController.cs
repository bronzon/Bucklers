using UnityEngine;
using System.Collections;

public class CharacterSizeController : MonoBehaviour {
	public float maxAtUnits = -5;
	public float minAtUnits = 5;
	public float maxScale = 5;
	public float minScale = 1;

	public GameObject character;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float unitDiff = Mathf.Abs(maxAtUnits - minAtUnits);
		float playerY = character.transform.position.y;
		float distanceTravelledFromMin = Mathf.Abs(Mathf.Clamp(playerY, maxAtUnits, minAtUnits) - minAtUnits);
		float scalePercentage = distanceTravelledFromMin / unitDiff;
		float scaleDiff = maxScale - minScale;
		float scaledOffset = scaleDiff * scalePercentage;
		float calculatedScale = scaledOffset + minScale;
		character.transform.localScale = new Vector3 (calculatedScale, calculatedScale);
	}
}
