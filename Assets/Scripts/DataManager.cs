using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager DataSingleton;

    public TMP_InputField nameInput;
    public string playerName;
    public string bestPlayer;
    public int bestScore;
    private void Awake()
    {
        if (DataSingleton != null)
        {
            Destroy(gameObject);
            return;
        }

        DataSingleton = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        playerName = nameInput.text;
        SceneManager.LoadScene("Game");
    }

    [Serializable]
    class SaveData
    {
        public string bestPlayer;
        public int bestScore;
    }

    public void SaveBest()
    {
        SaveData data = new SaveData();
        data.bestPlayer = playerName;
        data.bestScore = bestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savedata.json", json);
    }

    public void LoadBest()
    {
        string path = Application.persistentDataPath + "/savedata.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayer = data.bestPlayer;
            bestScore = data.bestScore;
        }
    }
}
