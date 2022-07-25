using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ScrblGameEnd : MonoBehaviour
{
    private GameObject gm;
    public Vector3 startPosLetters;
    public float offset;
    public List<GameObject> letterCollectedList;
    public List<GameObject> letterList;
    public GameObject ui;
    public GameObject button;
    public GameObject emptyObj;
    void Start()
    {
        gm = GameObject.Find("GameManager");
        fillTheEmptyObjects();
    }

    private void fillTheEmptyObjects()
    {
        for (int i = 1; i <= 10; i++)
        {
            GameObject gObj = Instantiate(emptyObj);
            Vector3 target;
            if (i <= 5)
            {
                target = startPosLetters + new Vector3((i - 1) * (offset), 0, 0);
            }
            else
            {
                target = startPosLetters + new Vector3(((i - 1) % 5) * (offset), -offset , 0);
            }
            gObj.transform.position = target;
            gObj.GetComponent<BuyTheLetter>().target = target;
            letterCollectedList.Add(gObj);
        }
    }
    
    public void spawnNewObject()
    {
        gm.GetComponent<GameManager>().playerScore -= gm.GetComponent<GameManager>().pointForLetterBuy[0];
        gm.GetComponent<GameManager>().pointForLetterBuy.Remove(gm.GetComponent<GameManager>().pointForLetterBuy[0]);
        int count = letterList.Count;
        int rand = Random.Range(0, 26);
        if (!isListFull())
        {
            GameObject spawnedLetter = Instantiate(letterList[rand]);
            bool flag = true;
            for (int i = 0; i < letterCollectedList.Count; i++)
            {
                if (flag && letterCollectedList[i].GetComponent<BuyTheLetter>().gObj == null)
                {
                    spawnedLetter.transform.position = letterCollectedList[i].GetComponent<BuyTheLetter>().target;
                    letterCollectedList[i].GetComponent<BuyTheLetter>().gObj = spawnedLetter;
                    spawnedLetter.AddComponent<DragAndDrop>();
                    Vector3 targetAngle = new Vector3(90f, 180f, 0);
                    Vector3 targetScale = new Vector3(1.8f, 1.8f, 1.8f);
                    spawnedLetter.transform.localScale = targetScale;
                    spawnedLetter.transform.eulerAngles = targetAngle;
                    flag = false;
                }
            }
        }
    }

    IEnumerator scrabbleUI()
    {
        yield return new WaitForSeconds(3f);
        ui.SetActive(true);
        gm.GetComponent<GameManager>().isPlayableLetterDrag = true;
    }

    private IEnumerator _instant(GameObject go)
    {
        go.transform.localScale = new Vector3(2f, 2f, 2f);
        yield return new WaitForSeconds(.3f);
        go.transform.localScale = new Vector3(1f, 1f, 1f);
        yield return new WaitForSeconds(.3f);
        go.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
    }
    private IEnumerator instant(GameObject go,Vector3 v3)
    {
        Debug.Log("log-instant");
        yield return new WaitForSeconds(.001f);
        go.transform.localScale = new Vector3(0,0,0);
        go.transform.position = v3;
        go.transform.eulerAngles = new Vector3(90, 180, 0);
        //StartCoroutine(_instant(go));
    }
    private void fillTheLetters()
    {
        gm.GetComponent<GameManager>().inGameEnd = true;
        for (int i = 1;
            i < gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>().PlayerStack.Count;
            i++)
        {
            if (i < 11)
            {
                GameObject gObj = letterCollectedList[i - 1];
                int flag = i;
                //i = gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>().PlayerStack.Count - i;
                gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>().PlayerStack[i].GetComponent<LattersController>().enabled = false;
                gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>().PlayerStack[i]
                    .AddComponent<LetterAnim>();
                gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>().PlayerStack[i]
                    .GetComponent<LetterAnim>().time = 3f;
                Vector3 target;
                if (i <= 5)
                {
                    target = startPosLetters + new Vector3((i - 1) * (offset), 0, 0);
                }
                else
                {
                    target = startPosLetters + new Vector3(((i - 1) % 5) * (offset), -offset , 0);
                }

                gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>().PlayerStack[i]
                    .GetComponent<LetterAnim>().targetPos = target;
                gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>().PlayerStack[i]
                    .GetComponent<LattersEndGame>().target = target;
                gObj.GetComponent<BuyTheLetter>().gObj = gm.GetComponent<GameManager>().referanceParentPlayer
                    .GetComponent<ParentPlayerController>().PlayerStack[i];
            }
            else
            {
                int flag = i;
                //i = gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>().PlayerStack.Count - i;
                Destroy(gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>().PlayerStack[i].gameObject);
                i = flag;
            }
        }

        int count = 0;
        int letterCount = 25;
        /*while (true)
        {
            Debug.Log(gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>().PlayerStack
                .Count + count);
            if (gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>().PlayerStack
                .Count + count <= 10)
            {
                int s = Random.Range(0, letterCount);
                bool gFlag = false;
                for (int i = 1;
                    i < gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                        .PlayerStack.Count;
                    i++)
                {
                    if (gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                            .PlayerStack[i].GetComponent<LattersEndGame>().LatterChar ==
                        letterList[s].GetComponent<LattersEndGame>().LatterChar)
                    {
                        gFlag = true;
                    }
                }

                if (!gFlag)
                {
                    GameObject lttr = Instantiate(letterList[s]);
                    lttr.AddComponent<DragAndDrop>();
                    letterCount--;
                    letterList.Remove(letterList[s]);
                    Debug.Log(lttr.name);
                    count++;
                    int _count = gm.GetComponent<GameManager>().referanceParentPlayer
                        .GetComponent<ParentPlayerController>()
                        .PlayerStack.Count - 1 + count;
                    if (_count <= 5)
                    {
                        Debug.Log("-"+_count);
                        Vector3 v3 = startPosLetters + new Vector3(((_count - 1) * offset) , 0, 0);
                        StartCoroutine(instant(lttr, v3));
                    }
                    else
                    {
                        Vector3 v3 = startPosLetters + new Vector3((((_count - 1) % 5) * offset) , -offset, 0);
                        StartCoroutine(instant(lttr, v3));
                    }
                }
            }
            else
            {
                break;
            }
        }*/

        
        
    }

    private bool isListFull()
    {
        int _count = 0;
        for (int i = 0; i < letterCollectedList.Count; i++)
        {
            Debug.Log(i);
            if (letterCollectedList[i].GetComponent<BuyTheLetter>().gObj)
            {
                _count++;
            }
        }
        Debug.Log("-->-->"+_count);
        if (_count < 10)
        {
            return false;
        }
        else
        {
            return true;
        }

        
    }
    public void btnControl()
    {

        if (isListFull() || gm.GetComponent<GameManager>().playerScore < gm.GetComponent<GameManager>().pointForLetterBuy[0] || gm.GetComponent<GameManager>().pointForLetterBuy.Count <= 1)
        {
            button.GetComponent<Button>().interactable = false;
        }
        else
        {
            button.GetComponent<Button>().interactable = true;
        }
    }    

    // Update is called once per frame
    void Update()
    {
        btnControl();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gm.GetComponent<GameManager>().isEndGame = true;
            StartCoroutine(scrabbleUI());
            if (gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>().PlayerStack
                .Count > 1)
            {
                fillTheLetters();
            }    
        }

        if (other.gameObject.tag == "AI")
        {
            if (gm.GetComponent<GameManager>().referanceParentAI.GetComponent<ParentAIController>().AIStack
                .Count > 1)
            {
                for (int i = 1;
                    i < gm.GetComponent<GameManager>().referanceParentAI.GetComponent<ParentAIController>().AIStack
                        .Count;
                    i++)
                {
                    Destroy(gm.GetComponent<GameManager>().referanceParentAI.GetComponent<ParentAIController>().AIStack[i].gameObject);
                }
            }    
        }
    }
}
