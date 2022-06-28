using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintTableController : MonoBehaviour
{
    public List<GameObject> AnswerLetters;
    private GameObject gm;
    private GameObject parentPlayer;
    private bool onGameEnd;
    void Start()
    {
        for (int i = 0; i < AnswerLetters.Count; i++)
        {
            AnswerLetters[i].SetActive(false);
        }
        gameObject.SetActive(false);
        onGameEnd = false;
        gm = GameObject.Find("GameManager");
        parentPlayer = gm.GetComponent<GameManager>().referanceParentPlayer;
    }

    private void goTable(GameObject gm)
    {
        
    }

    private void goUI(GameObject gm)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.GetComponent<GameManager>().inGameEnd)
        {
            while (parentPlayer.GetComponent<ParentPlayerController>().PlayerStack.Count > 1)
            {
                //parentPlayer.GetComponent<ParentPlayerController>().PlayerStack[1];
            }
        }
        
    }
}
