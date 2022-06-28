using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerFinishController : MonoBehaviour
{
    public Vector3 MaincharTarget;
    public Vector3 AICharTarget;
    public float speed;
    public GameObject MainChar;
    private Vector3 mainCharStart;
    private bool MainFinish;
    public GameObject AIChar;
    private Vector3 AICharStart;
    private bool AIFinish;
    public bool isFinished;
    private float k;
    private GameObject gameManager;
    void Start()
    {
        MainFinish = false;
        AIFinish = false;
        isFinished = false;
        k = 0;
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinished)
        {
            if (k < 1f)
            {
                k += Time.deltaTime * speed;
            }
            //Main y = -118 , AI y = 118
            float s = Mathf.Lerp(0, 118, k);
            MainChar.transform.eulerAngles = new Vector3(0, s * -1f, 0);
            AIChar.transform.eulerAngles = new Vector3(0, s, 0);
            MainChar.transform.position = Vector3.Lerp(mainCharStart, MaincharTarget, k);
            AIChar.transform.position = Vector3.Lerp(AICharStart, AICharTarget, k);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            MainFinish = true;
        }
        else if (other.gameObject.tag == "AI")
        {
            AIFinish = true;
        }
        
        if (MainFinish && AIFinish)
        {
            mainCharStart = MainChar.transform.position;
            AICharStart = AIChar.transform.position;
            isFinished = true;
            gameManager.GetComponent<GameManager>().inGameEnd = true;
            gameManager.GetComponent<GameManager>().gameIsGoing = false;
        }
        
    }
}
