using UnityEngine;
using UnityEngine.UI;

public class CheckUDID : MonoBehaviour
{

	public Text message;
	
	// Use this for initialization
	void Start ()
	{

		message.text = "GENERATED THIS UDID " + UDID.GetUDID() + "\n" +
		               "GUID " + UDID.GetGUID();
	}	
}
