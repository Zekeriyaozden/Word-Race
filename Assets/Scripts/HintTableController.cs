using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintTableController : MonoBehaviour
{
    public List<GameObject> LetterBoard;
    public float letterSpeed;
    public GameObject targetUI;
    public List<GameObject> AnswerLetters;
    private GameObject gm;
    private GameObject parentPlayer;
    public bool onGameEnd;
    public Mesh mesh;
    void Start()
    {
        gameObject.SetActive(false);
        onGameEnd = false;
        gm = GameObject.Find("GameManager");
        parentPlayer = gm.GetComponent<GameManager>().referanceParentPlayer;
    }

    public void goTable(GameObject gObj , GameObject obj)
    {
        gObj.AddComponent<LetterMovementEndGame>();
        gObj.GetComponent<LetterMovementEndGame>().speed = letterSpeed;
        gObj.GetComponent<LetterMovementEndGame>().target =
            obj.transform.position + new Vector3(0, 0, -0.6f);
        obj.GetComponent<LetterBoxController>().isEmpty = false;
        gObj.GetComponent<LattersController>().enabled = false;
        gObj.GetComponent<LetterMovementEndGame>().isGoingUI = false;
        gObj.transform.GetChild(0).gameObject.GetComponent<MeshFilter>().mesh = mesh;
    }

    private void goUI(GameObject gObj)
    {
        if (gObj.gameObject.GetComponent<LattersController>().level == 1)
        {
            gm.GetComponent<GameManager>().playerScore += 50;
        }else if (gObj.gameObject.GetComponent<LattersController>().level == 2)
        {
            gm.GetComponent<GameManager>().playerScore += 100;
        }else if (gObj.gameObject.GetComponent<LattersController>().level == 3)
        {
            gm.GetComponent<GameManager>().playerScore += 150;
        }
        gObj.AddComponent<LetterMovementEndGame>();
        gObj.GetComponent<LetterMovementEndGame>().speed = letterSpeed;
        gObj.GetComponent<LetterMovementEndGame>().target = targetUI.gameObject.transform.position;
        gObj.GetComponent<LattersController>().enabled = false;
        gObj.GetComponent<LetterMovementEndGame>().isGoingUI = true;
        gObj.transform.GetChild(0).gameObject.GetComponent<MeshFilter>().mesh = mesh;
    }

    public void detectLatter(GameObject gObj)
    {
        int tempInt = AnswerLetters.Count;
        bool tempBool = true;
        foreach (var box in AnswerLetters)
        {
            tempInt = tempInt - 1;
            if (box.gameObject.GetComponent<LetterBoxController>().isEmpty &&
                box.gameObject.GetComponent<LetterBoxController>().latter ==
                gObj.gameObject.GetComponent<LattersEndGame>().LatterChar)
            {
                tempBool = false;
                goTable(gObj , box);
                break;
            }

            if (tempBool && tempInt == 0)
            {
                goUI(gObj);
            }
        }
    }
    

    /*public void detectLatter()
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
    }*/

    public GameObject findTargetBox()
    {
        int cnt = AnswerLetters.Count;

        foreach (var box in AnswerLetters)
        {
            if (box.gameObject.GetComponent<LetterBoxController>().isEmpty)
            {
                return box.gameObject;
            }
        }
        return null;
    }
    void Update()
    {
        if (onGameEnd)
        {
            
            if (findTargetBox() != null)
            {
                Debug.Log(findTargetBox().name);
            }
            else
            {
            
            }
        }
        
    }
}
