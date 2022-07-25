using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public GameObject boardAI;
    public GameObject boardPlayer;
    public GameObject rankPlayer;
    public GameObject rankAI;
    public GameObject scorePlayer;
    public GameObject scoreAI;
    public GameObject canvas;
    private GameObject gm;
    public Vector3 boardFirst;
    public Vector3 boardSecond;
    void Start()
    {
        canvas.SetActive(true);
        boardFirst = boardPlayer.transform.position;
        boardSecond = boardAI.transform.position;
        canvas.SetActive(false);
        gm = GameObject.Find("GameManager");
        Debug.Log(gameObject.name);
        
    }
    
    

    // Update is called once per frame
    void Update()
    {

        scorePlayer.GetComponent<TextMeshProUGUI>().text = gm.GetComponent<GameManager>().playerScore.ToString();
        scoreAI.GetComponent<TextMeshProUGUI>().text = gm.GetComponent<GameManager>().aiScore.ToString();
        
        if (gm.GetComponent<GameManager>().playerScore >= gm.GetComponent<GameManager>().aiScore)
        {
            boardPlayer.transform.position = boardFirst;
            boardAI.transform.position = boardSecond;
            rankPlayer.GetComponent<TextMeshProUGUI>().text = 1.ToString();
            rankAI.GetComponent<TextMeshProUGUI>().text = 2.ToString();
        }
        else
        {
            boardPlayer.transform.position = boardSecond;
            boardAI.transform.position = boardFirst;
            rankPlayer.GetComponent<TextMeshProUGUI>().text = 2.ToString();
            rankAI.GetComponent<TextMeshProUGUI>().text = 1.ToString();
        }
        
    }
}
