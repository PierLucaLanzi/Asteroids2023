using System;
using UnityEngine;
using POLIMIGameCollective;

public class EventListener2SO : MonoBehaviour
{

	[SerializeField] private VoidEventChannelSO spawnEvent;
	
	void OnEnable() {
		// EventManager.StartListening ("Spawn", Spawn);
		spawnEvent.OnEventRaised += Spawn;
	}
	
	void OnDisable() {
		spawnEvent.OnEventRaised -= Spawn;
	}

	// Update is called once per frame
	void Spawn () {
		// EventManager.StopListening ("Spawn", Spawn);
		Debug.Log("SPAWN EVENT");
		// EventManager.StartListening ("Spawn", Spawn);
	}
}
