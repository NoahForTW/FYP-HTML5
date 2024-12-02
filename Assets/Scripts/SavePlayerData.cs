using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using static UnityEditor.Rendering.CameraUI;



[Serializable]
public class PlayerScore {

    public int score = 5 ;
}

public class SavePlayerData : MonoBehaviour
{
    public static PlayerScore playerScore;
    public void SaveData<T>(T currentData) where T : new()
    {
        T data = new T();
        data = currentData;
        XmlSerializer serializer = new XmlSerializer(data.GetType());
        using (StringWriter sw = new StringWriter())
        {
            serializer.Serialize(sw, data);
            PlayerPrefs.SetString(data.ToString(), sw.ToString());
        }
        //var output = JsonUtility.ToJson(data, true);
        //Debug.Log(output);
    }

    public T LoadData<T>() where T : new()
    {
        T data = new T();
        XmlSerializer serializer = new XmlSerializer(data.GetType());
        string playerData = PlayerPrefs.GetString(data.ToString());
        if (playerData.Length == 0)
        {
            return data;
        }
        else
        {
            using (var reader = new System.IO.StringReader(playerData))
            {
                return (T) serializer.Deserialize(reader);
            }
        }
    }

    public void test()
    {
        PlayerScore score = new PlayerScore();
        score.score = 10 ;
        SaveData<PlayerScore>(score);
        var output = JsonUtility.ToJson(LoadData<PlayerScore>(), true);
        Debug.Log(output + LoadData<PlayerScore>().score);
    }


}
