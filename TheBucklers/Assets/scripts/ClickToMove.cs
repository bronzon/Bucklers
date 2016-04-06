using UnityEngine;
using System.Collections.Generic;

//example
[RequireComponent(typeof(PolyNavAgent))]
public class ClickToMove : MonoBehaviour{
	private VerbSystem verbSystem;
	private GuiConf guiConf;

	void Start()  {
		verbSystem = GameObject.FindGameObjectWithTag ("VerbSystem").GetComponent<VerbSystem> ();
		guiConf = GameObject.FindGameObjectWithTag ("Gui").GetComponent<GuiConf> ();
	}

	private PolyNavAgent _agent;
	public PolyNavAgent agent{
		get
		{
			if (!_agent){
				_agent = GetComponent<PolyNavAgent>();
			}
			return _agent;			
		}
	}

	void Update() {
		if (verbSystem.CurrentVerb == Verb.WALK && Input.GetMouseButton(0)) {
			var mousePosition = Input.mousePosition;
			if (mousePosition.y > guiConf.GuiIsBelowScreenSpaceCoord) {
				agent.SetDestination(Camera.main.ScreenToWorldPoint (mousePosition) );
			}

		}
	}
}