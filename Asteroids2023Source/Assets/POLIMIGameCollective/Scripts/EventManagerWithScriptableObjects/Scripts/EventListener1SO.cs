using System;
using UnityEngine;
using POLIMIGameCollective;
using UnityEngine.UI;
    
public class EventListener1SO : MonoBehaviour
{

	[SerializeField] private VoidEventChannelSO explodeEvent;
	[SerializeField] private VoidEventChannelSO runawayEvent;
	// Use this for initialization
	void OnEnable () {
		// EventManager.StartListening ("Explode",Explode);
		// EventManager.StartListening ("RunAway",RunAway);
		explodeEvent.OnEventRaised +=Explode;
		runawayEvent.OnEventRaised +=RunAway;
	}

	void Explode () {
		// EventManager.StopListening ("Explode", Explode);
		Debug.Log ("Explode");
	}

	void RunAway () {
		// EventManager.StopListening ("RunAway", RunAway);
		Debug.Log ("RunAway");
	}

	private void OnDisable()
	{
		explodeEvent.OnEventRaised -=Explode;
		runawayEvent.OnEventRaised -=RunAway;
	}
}
