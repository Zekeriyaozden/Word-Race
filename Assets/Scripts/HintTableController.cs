using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintTableController : MonoBehaviour
{
    public Material[] whiteAndTarget;
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
            gm.GetComponent<GameManager>().playerScore += gm.GetComponent<GameManager>().levelPoint[0];
        }else if (gObj.gameObject.GetComponent<LattersController>().level == 2)
        {
            gm.GetComponent<GameManager>().playerScore += gm.GetComponent<GameManager>().levelPoint[1];
        }else if (gObj.gameObject.GetComponent<LattersController>().level == 3)
        {
            gm.GetComponent<GameManager>().playerScore += gm.GetComponent<GameManager>().levelPoint[2];
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
        
        if (gameObject.GetComponent<HintTableController>().findTargetBox() != null)
        {
            Material[] mt = gameObject.GetComponent<HintTableController>().findTargetBox().GetComponent<MeshRenderer>().materials;
            mt[0] = whiteAndTarget[1];
            gameObject.GetComponent<HintTableController>().findTargetBox().GetComponent<MeshRenderer>().materials = mt;
        }
        
        for (int i = 0; i < gameObject.GetComponent<HintTableController>().AnswerLetters.Count; i++)
        {
            if (gameObject.GetComponent<HintTableController>().AnswerLetters[i] !=
                gameObject.GetComponent<HintTableController>().findTargetBox())
            {
                Debug.Log("aa");
                Material[] mt = gameObject.GetComponent<HintTableController>().AnswerLetters[i].GetComponent<MeshRenderer>().materials;
                mt[0] = whiteAndTarget[0];
                gameObject.GetComponent<HintTableController>().AnswerLetters[i].GetComponent<MeshRenderer>().materials= mt;
            }
        }
        
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
