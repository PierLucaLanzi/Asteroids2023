using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour
{

	public static LocalizationManager instance = null;
	private bool isReady = false;

	private const string missingTextString = "Localized text not found";

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
			
		} else if (instance != this)
		{
			Destroy(gameObject);
		}
		
	}
	
	// 1. load the localization data into an array
	// 2. store this into a dictionary

	private Dictionary<string, string> localizedText = new Dictionary<string, string>();
	// Use this for initialization
	void Start () {
		
	}

	public void LoadLocalizedText(string filename)
	{
		localizedText.Clear();
		string filePath = Path.Combine(Application.streamingAssetsPath, filename);

		if (File.Exists(filePath))
		{
			// read all the data
			string dataAsJson = File.ReadAllText(filePath);
			LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

			// deserialization of unorder collection does not work so 
			// we load an array and copy its content into a dictionary
			for (int i = 0; i < loadedData.items.Length; i++)
			{
				localizedText.Add(loadedData.items[i].key,loadedData.items[i].value);
			}
			
			Debug.Log("Data Loaded. Dictionary contains #"+localizedText.Count+"entries.");
			isReady = true;
		}
		else
		{
			Debug.LogError("Cannot find file!");
		}
	}

	public bool GetIsReady()
	{
		return isReady;
	}

	public string GetLocalizedValue(string key)
	{
		if (localizedText.ContainsKey(key))
			return localizedText[key];
		else return missingTextString;
	}
}
