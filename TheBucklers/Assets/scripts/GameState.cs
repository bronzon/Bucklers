using UnityEngine;
using System.Collections.Generic;

public class GameState : MonoBehaviour {
	private Dictionary<string, bool> stateByName = new Dictionary<string, bool> ();

	public void SetState(string name, bool set) {
		stateByName [name] = set;	
	}

	public bool FlipState(string name, bool ifNoValueIsSet=true) {
		if (stateByName.ContainsKey (name)) {
			stateByName [name] = !stateByName[name];
		} else {
			stateByName[name] = ifNoValueIsSet;
		}

		return GetState (name);
	}

	public bool GetState(string name, bool defaultValue=false) {
		if (stateByName.ContainsKey (name)) {
			return stateByName [name];
		} else {
			return defaultValue;
		}
	}

}
