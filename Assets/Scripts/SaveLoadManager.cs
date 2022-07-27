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
        if (currentLevel == 0)
        {
            if (PlayerPrefs.GetInt("currentLevel", 0) != 0)
            {
                SceneManager.LoadScene(PlayerPrefs.GetInt("currentLevel", 0));   
            }
        }
        
        gm = GameObject.Find("GameManager");
        if (currentLevel != 4)
        {
            PlayerPrefs.SetInt("currentLevel",currentLevel);
        }
        else
        {
            PlayerPrefs.SetInt("currentLevel",0);
        }

        Load();
    }

    public void sceneManageControl()
    {
        if (currentLevel == 4)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(currentLevel + 1);
        }
    }

    private void OnApplicationQuit()
    {
        Save();
        Debug.Log("quit");
    }

    public void Save()
    {
        ///*
        PlayerPrefs.SetInt("playerScore",gm.gameObject.GetComponent<GameManager>().playerScore);
        PlayerPrefs.SetInt("aiScore",gm.gameObject.GetComponent<GameManager>().aiScore);
       //*/
        /*
       PlayerPrefs.SetInt("currentLevel",0); 
       PlayerPrefs.SetInt("playerScore",0);
       PlayerPrefs.SetInt("aiScore", 0);*/
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
