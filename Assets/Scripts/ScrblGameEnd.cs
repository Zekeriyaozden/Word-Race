using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScrblGameEnd : MonoBehaviour
{
    private GameObject gm;
    public Vector3 startPosLetters;
    public float offset;
    public List<GameObject> letterList;
    void Start()
    {
        gm = GameObject.Find("GameManager");
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
            gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>().PlayerStack[i].GetComponent<LattersController>().enabled = false;
            gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>().PlayerStack[i]
                .AddComponent<DragAndDrop>();
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
            
        }

        int count = 0;
        int letterCount = 25;
        while (true)
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
        }

        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>().PlayerStack
                .Count > 1)
            {
                fillTheLetters();
            }    
        }
    }
}
