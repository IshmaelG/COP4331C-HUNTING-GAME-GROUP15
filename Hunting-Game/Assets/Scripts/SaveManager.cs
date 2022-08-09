using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public SaveData activeSave;
    public bool hasLoaded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        instance = this;
        Load();
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.K))
    //     {
    //         Save();
    //     }
    //     if (Input.GetKeyDown(KeyCode.L))
    //     {
    //         Load();
    //     }
    // }

    public void Save()
    {
        string dataPath = Application.persistentDataPath;

        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save74", FileMode.Create);
        serializer.Serialize(stream, activeSave);
        stream.Close();

        Debug.Log("Saved");
    }

    public void Load()
    {
        string dataPath = Application.persistentDataPath;

        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save74"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save74", FileMode.Open);
            activeSave = serializer.Deserialize(stream) as SaveData;
            stream.Close();

            Debug.Log("Loaded");   
            hasLoaded = true;
        }
    }

    public void DeleteSaveData()
    {
        string dataPath = Application.persistentDataPath;

        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save74"))
            File.Delete(dataPath + "/" + activeSave.saveName + ".save74");
    }
}

[System.Serializable]
public class SaveData 
{
    public string saveName;
    public bool level1Complete;
    public bool level2Complete;
    public bool level3Complete;
    public int weapon;
}
