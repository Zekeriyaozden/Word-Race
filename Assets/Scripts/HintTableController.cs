using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintTableController : MonoBehaviour
{
    public float letterSpeed;
    public List<GameObject> AnswerLetters;
    private GameObject gm;
    private GameObject parentPlayer;
    public bool onGameEnd;
    void Start()
    {
        gameObject.SetActive(false);
        onGameEnd = false;
        gm = GameObject.Find("GameManager");
        parentPlayer = gm.GetComponent<GameManager>().referanceParentPlayer;
    }

    private void goTable(GameObject gObj)
    {
        foreach (var obj in AnswerLetters)
        {
            if (obj.GetComponent<LetterBoxController>().latter == gObj.GetComponent<LattersEndGame>().LatterChar)
            {
                Debug.Log("EnterHere");
                if (obj.GetComponent<LetterBoxController>().isEmpty)
                {
                    gObj.AddComponent<LetterMovementEndGame>();
                    gObj.GetComponent<LetterMovementEndGame>().speed = letterSpeed;
                    gObj.GetComponent<LetterMovementEndGame>().target =
                        obj.transform.position + new Vector3(0, 0, -0.6f);
                    obj.GetComponent<LetterBoxController>().isEmpty = false;
                    gObj.GetComponent<LattersController>().enabled = false;
                }
            }
        }
    }

    private void goUI(GameObject gObj)
    {
        //Debug.Log(gObj + "-- UI");
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.GetComponent<GameManager>().inGameEnd)
        {
            while (parentPlayer.GetComponent<ParentPlayerController>().PlayerStack.Count > 1)
            {
                bool isInBoard = false;
                foreach (var board in AnswerLetters)
                {
                    Debug.Log(board.GetComponent<LetterBoxController>().latter);
                    if (board.GetComponent<LetterBoxController>().latter == parentPlayer.GetComponent<ParentPlayerController>().PlayerStack[1].GetComponent<LattersEndGame>().LatterChar)
                    {
                        Debug.Log(parentPlayer.GetComponent<ParentPlayerController>().PlayerStack[1].name);
                        isInBoard = true;
                    }
                }

                if (isInBoard)
                {
                    goTable(parentPlayer.GetComponent<ParentPlayerController>().PlayerStack[1]);
                    parentPlayer.GetComponent<ParentPlayerController>().PlayerStack
                        .Remove(parentPlayer.GetComponent<ParentPlayerController>().PlayerStack[1]);
                }
                else
                {
                    goUI(parentPlayer.GetComponent<ParentPlayerController>().PlayerStack[1]);
                    parentPlayer.GetComponent<ParentPlayerController>().PlayerStack
                        .Remove(parentPlayer.GetComponent<ParentPlayerController>().PlayerStack[1]);
                }
            }
        }
        
    }
}
