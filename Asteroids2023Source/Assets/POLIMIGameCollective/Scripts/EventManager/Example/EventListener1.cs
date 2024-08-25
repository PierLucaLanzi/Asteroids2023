using UnityEngine;
using POLIMIGameCollective;
using UnityEngine.UI;
    
public class EventListener1 : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
		EventManager.StartListening ("Explode",Explode);
		EventManager.StartListening ("RunAway",RunAway);
	}

	void Explode () {
		EventManager.StopListening ("Explode", Explode);
		Debug.Log ("Explode");
	}

	void RunAway () {
		EventManager.StopListening ("RunAway", RunAway);
		Debug.Log ("RunAway");
	}
}
