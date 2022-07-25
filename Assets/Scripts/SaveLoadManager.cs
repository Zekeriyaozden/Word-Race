using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SaveLoadManager : MonoBehaviour
{
    public int currentLevel;
    private GameObject gm;
    
    void Start()
    {
        gm = GameObject.Find("GameManager");
        gm.GetComponent<GameManager>().currentScene = 0;
        Debug.Log(currentLevel);
        PlayerPrefs.SetInt("currentLevel",0);
        Load();
    }

    private void OnApplicationQuit()
    {
        Save();
        Debug.Log("quit");
    }

    public void Save()
    {
        PlayerPrefs.SetInt("playerScore",gm.gameObject.GetComponent<GameManager>().playerScore);
        PlayerPrefs.SetInt("aiScore",gm.gameObject.GetComponent<GameManager>().aiScore);
        
    }

    public void Load()
    {
        gm.GetComponent<GameManager>().playerScore = PlayerPrefs.GetInt("playerScore", 0);
        gm.GetComponent<GameManager>().aiScore = PlayerPrefs.GetInt("aiScore", 0);
    }
    
    
    
    void Update()
    {
        Save();
    }
}
