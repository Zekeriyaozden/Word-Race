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

    public GameObject AIChar;
    private Vector3 AICharStart;

    public bool isFinished;
    private float k;
    private float f;
    private bool mainCharTrig;
    private bool AICharTrig;
    private GameObject gameManager;
    private GameObject hint;
    public bool mainCharBool;
    public bool AIcharBool;
    void Start()
    {
        mainCharBool = false;
        AIcharBool = false;


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
        if (mainCharBool && AIcharBool)
        {
            isFinished = true;
            hint.gameObject.SetActive(true);
            gameManager.GetComponent<GameManager>().inGameEnd = true;
            gameManager.GetComponent<GameManager>().gameIsGoing = false;
            gameManager.GetComponent<GameManager>().UIManagerRunner.GetComponent<UIManagerRunner>().HintVisible(true);
        }
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
            AIChar.gameObject.GetComponent<AIController>().enabled = false;
            AIChar.transform.position = Vector3.Lerp(AICharStart, AICharTarget, f);
            AIChar.transform.eulerAngles = new Vector3(0, x, 0);
        }
        
    }

    private void setPointAI(GameObject gObj)
    {
        if (gObj.gameObject.GetComponent<LattersController>().level == 1)
        {
            gameManager.GetComponent<GameManager>().aiScore += gameManager.GetComponent<GameManager>().levelPoint[0];
        }else if (gObj.gameObject.GetComponent<LattersController>().level == 2)
        {
            gameManager.GetComponent<GameManager>().aiScore += gameManager.GetComponent<GameManager>().levelPoint[1];
        }else if (gObj.gameObject.GetComponent<LattersController>().level == 3)
        {
            gameManager.GetComponent<GameManager>().aiScore += gameManager.GetComponent<GameManager>().levelPoint[2];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            mainCharBool = true;

            mainCharStart = MainChar.transform.position;
            mainCharTrig = true;
            gameManager.GetComponent<GameManager>().Player.GetComponent<PlayerController>().idleAnim();
        }
        else if (other.gameObject.tag == "AI")
        {
            AIcharBool = true;

            AICharStart = AIChar.transform.position;
            AICharTrig = true;
            gameManager.GetComponent<GameManager>().AI.GetComponent<AIController>().idleAnim();
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
                setPointAI(other.gameObject);
                Destroy(other.gameObject);
            }
        }
        
    }
}
