using UnityEngine;
using System.Collections.Generic;

public class Icons : MonoBehaviour {
	public Dictionary<string, Sprite> spriteByName = new Dictionary<string, Sprite>();
	public string spriteSheetNameInResourcesFolder;

	void Awake () {
		Sprite[] sprites = Resources.LoadAll<Sprite>(spriteSheetNameInResourcesFolder);

		foreach (Sprite sprite in sprites)	{
			spriteByName.Add (sprite.name, sprite);
		}
	}
}
