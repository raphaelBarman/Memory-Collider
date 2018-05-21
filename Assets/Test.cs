using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        string jsonString = File.ReadAllText(Application.dataPath + "/decades.json");
        var data = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jsonString);
        foreach(string key in data.Keys)
        {
            Debug.Log(key);
            List<string> values = null;
            if (data.TryGetValue(key, out values))
            {
                Debug.Log(values[0]);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

[System.Serializable]
public class StringStringDictionary
{
    public string key;
    public string[] value;
}

[System.Serializable]
public class StringStringDictionaryArray
{
    public StringStringDictionary[] items;
}
