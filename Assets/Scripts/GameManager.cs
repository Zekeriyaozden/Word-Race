using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject finishController;
    public GameObject UIManagerRunner;
    public int playerScore;
    public bool inGameEnd;
    public GameObject letterGenerator;
    public float protectTime;
    public GameObject particle;
    public GameObject protect;
    public float AIWhenIsStop;
    public float DistanceOfMainAndAI;
    public float lerpSpeed;
    public float speedMainChar;
    public float speedAIChar;
    public GameObject Player;
    public GameObject AI;
    public GameObject referanceParentPlayer;
    public GameObject referanceParentAI;
    //-------------------------------------------
    public GameObject HintTab;
    [HideInInspector]
    public float speedTmp;
    /*[HideInInspector]*/public bool gameIsGoing;
    private float tempSpeedOfAI;
    private bool onArea;
    private bool flag;
    private bool gameStartBool;
    void Start()
    {
        gameStartBool = true;
        gameIsGoing = false;
        speedTmp = speedMainChar;
        flag = true;
        onArea = false;
        tempSpeedOfAI = speedAIChar;
    }
    void Update()
    {

        
        HintTab.GetComponent<HintTableController>().findTargetBox();
        
        if (inGameEnd && !gameIsGoing)
        {
            letterGenerator.GetComponent<LetterBoardGenerator>().enabled = true;
        }
        if (Input.GetMouseButtonDown(0) && gameStartBool)
        {
            gameIsGoing = true;
            gameStartBool = false;
        }
        if (SpeedIncrease())
        {
            flag = true;
            speedAIChar = 7f;
        }else if (!SpeedIncrease() && !onArea && flag)
        {
            flag = false;
            speedAIChar = tempSpeedOfAI;
        }
        
    }

    public void Hint()
    {
        GameObject target = HintTab.gameObject.GetComponent<HintTableController>().findTargetBox();
        for (int i = 0; i < letterGenerator.transform.childCount; i++)
        {
            if (letterGenerator.gameObject.transform.GetChild(i).gameObject.GetComponent<LattersEndGame>().LatterChar == target.GetComponent<LetterBoxController>().latter)
            {
                if (target.gameObject.GetComponent<LetterBoxController>().isEmpty)
                {
                    target.gameObject.GetComponent<LetterBoxController>().isEmpty = false;
                    HintTab.gameObject.GetComponent<HintTableController>().goTable(letterGenerator.gameObject.transform.GetChild(i).gameObject,target);
                    i = 500;
                }
            }
        }
    }
    
    public bool SpeedIncrease()
    {
        if (distanceOfMainAndAI() > DistanceOfMainAndAI)
        {
            onArea = true;
            return true;
        }
        else
        {
            if (distanceOfMainAndAI() > 0 && distanceOfMainAndAI() < AIWhenIsStop)
            {
                onArea = false;
            }
            return false;
        }
    }
    
    
    public float distanceOfMainAndAI()
    {
        return (Player.gameObject.transform.position.z - AI.gameObject.transform.position.z);
    }
    
}
