using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class GameEventSystemMap : MonoBehaviour
{
    private static GameEventSystemMap instance;

    public static GameEventSystemMap Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameEventSystemMap>();
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (StaticClass.loadScene)
            GameEventSystemMap.Instance.LoadData();
        StaticClass.loadScene = true;
    }

    [SerializeField]
    private GameData data;

    public event Action<GameData> OnSaveData;
    public event Action<GameData> OnLoadData;

    public void LoadData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        FileStream stream = new FileStream(Application.dataPath + "/../save.xml", FileMode.Open);
        GameData tmp = serializer.Deserialize(stream) as GameData;

        if (tmp != null)
        {
            data = tmp;
        }

        stream.Close();
        OnLoadData?.Invoke(data);
    }
    public void SaveData()
    {
        OnSaveData?.Invoke(data);

        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        FileStream stream = new FileStream(Application.dataPath + "/../save.xml", FileMode.Create);
        serializer.Serialize(stream, data);
        stream.Close();
    }

}
