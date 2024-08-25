using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocalizationData
{
    public LocalizationItem[] items;

}

// we make it serializable so that we can create it and save it from JSON
[System.Serializable]
public class LocalizationItem
{
    public string key;
    public string value;
}