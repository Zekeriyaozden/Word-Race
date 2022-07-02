using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SaveLoadManager : MonoBehaviour
{
    public int lastScene;
    void Start()
    {
        Load();
        Debug.Log(lastScene);

    }

    private void OnApplicationQuit()
    {
        Save();
        Debug.Log("quit");
    }

    public void Save()
    {
        PlayerPrefs.SetInt("LastScreen",lastScene);
    }

    public void Load()
    {
        lastScene = PlayerPrefs.GetInt("LastScreen", 0);
    }
    
    void Update()
    {
        
    }
}
