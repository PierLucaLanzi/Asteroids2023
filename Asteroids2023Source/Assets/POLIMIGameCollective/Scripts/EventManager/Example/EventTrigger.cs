using UnityEngine;
using System.Collections;
using POLIMIGameCollective;

public class EventTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E))
		{
			Debug.Log("Triggering Explode");
			EventManager.TriggerEvent("Explode");
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			EventManager.TriggerEvent ("RunAway");			
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			EventManager.TriggerEvent("Spawn");
		}

	}
}
