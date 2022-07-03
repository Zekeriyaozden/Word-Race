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
    private float f;
    private bool mainCharTrig;
    private bool AICharTrig;
    private GameObject gameManager;
    private GameObject hint;
    void Start()
    {
        MainFinish = false;
        AIFinish = false;
        isFinished = false;
        k = 0;
        f = 0;
        gameManager = GameObject.Find("GameManager");
        hint = gameManager.GetComponent<GameManager>().HintTab;
        mainCharTrig = false;
        AICharTrig = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCharTrig)
        {
            if (k < 1f)
            {
                k += Time.deltaTime * speed;
            }
            float s = Mathf.Lerp(0, 118, k);
            MainChar.gameObject.GetComponent<PlayerController>().enabled = false;
            MainChar.transform.eulerAngles = new Vector3(0, s * -1f, 0);
            MainChar.transform.position = Vector3.Lerp(mainCharStart, MaincharTarget, k);
        }
        
        if (AICharTrig)
        {
            if (f < 1f)
            {
                f += Time.deltaTime * speed;
            }
            float x = Mathf.Lerp(0, 118, f);
            MainChar.gameObject.GetComponent<PlayerController>().enabled = false;
            AIChar.transform.position = Vector3.Lerp(AICharStart, AICharTarget, f);
            AIChar.transform.eulerAngles = new Vector3(0, x, 0);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            MainFinish = true;
            mainCharStart = MainChar.transform.position;
            mainCharTrig = true;
        }
        else if (other.gameObject.tag == "AI")
        {
            AIFinish = true;
            AICharStart = AIChar.transform.position;
            AICharTrig = true;
        }
        
        if (MainFinish && AIFinish)
        {
           // mainCharStart = MainChar.transform.position;
            // AICharStart = AIChar.transform.position;
            isFinished = true;
            hint.gameObject.SetActive(true);
            gameManager.GetComponent<GameManager>().inGameEnd = true;
            gameManager.GetComponent<GameManager>().gameIsGoing = false;
            gameManager.GetComponent<GameManager>().UIManagerRunner.GetComponent<UIManagerRunner>().HintVisible();
        }

        if (other.gameObject.tag == "Letter")
        {
            if (other.gameObject.GetComponent<LattersController>().ownership == "Player")
            {
                if (other.gameObject.transform.childCount > 2)
                {
                    other.gameObject.transform.GetChild(2).GetComponent<MeshFilter>().mesh = null;
                }
                hint.GetComponent<HintTableController>().detectLatter(other.gameObject);
                hint.gameObject.SetActive(true);
                gameManager.GetComponent<GameManager>().inGameEnd = true;
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
        
    }
}
