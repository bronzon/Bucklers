using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Assertions;

public class SceneSwitcher : MonoBehaviour {
	public string sceneName;
	private GameObject player;
	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void Update () {
		if (GetComponent<PolygonCollider2D>().OverlapPoint(player.GetComponent<Transform>().position)) {
			SceneManager.LoadScene(sceneName);
			Scene scene = SceneManager.GetSceneByName(sceneName);
			Assert.IsTrue(scene.IsValid());
			SceneManager.SetActiveScene(scene);
		}


	}
}
