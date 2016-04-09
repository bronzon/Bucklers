using UnityEngine;
using System.Collections.Generic;

//example
[RequireComponent(typeof(PolyNavAgent))]
public class ClickToMove : MonoBehaviour{
	private VerbSystem verbSystem;
	private GuiConf guiConf;

	void Start()  {
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

	public void StopPlayer () {
		_agent.Stop ();
	}

	void Update() {
		if (Input.GetMouseButton(0)) {
			var mousePosition = Input.mousePosition;
			if (mousePosition.y > guiConf.GuiIsBelowScreenSpaceCoord) {
				agent.SetDestination(Camera.main.ScreenToWorldPoint (mousePosition) );
			}

		}
	}
}