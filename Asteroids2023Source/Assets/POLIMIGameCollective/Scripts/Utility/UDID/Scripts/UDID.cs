// class to compute a unique id for the hosting machine
// http://answers.unity3d.com/questions/246116/how-can-i-generate-a-guid-or-a-uuid-from-within-un.html

using UnityEngine;
using System;


public class UDID 
{
	private static string _udid = string.Empty;
	private static string _guid = string.Empty;

	public static string GetGUID()
	{
		if (_guid == string.Empty)
		{
			_guid = System.Guid.NewGuid().ToString();
		}

		return _guid;
	}
	
	public static string GetUDID() {
		
		if (_udid == string.Empty)
		{
			InitUDID();
		}

		return _udid;
	}

	private static void InitUDID()
	{
		var random = new System.Random();
		
		DateTime epochStart = new System.DateTime(1970, 1, 1, 8, 0, 0, System.DateTimeKind.Utc);
		double timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds;
			 
		string udid =
			Application.systemLanguage				//Language
		    +"-"+Application.platform                                            //Device    
		                  +"-"+String.Format("{0:X}", Convert.ToInt32(timestamp))                //Time
		                  +"-"+String.Format("{0:X}", Convert.ToInt32(Time.time*1000000))        //Time in game
		                  +"-"+String.Format("{0:X}", random.Next(1000000000));                //random number
			 
		Debug.Log("Generated Unique ID: "+udid);

		_udid = udid;
	}
	
}


