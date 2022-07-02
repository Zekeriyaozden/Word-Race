using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject playersScorboard;
    public GameObject AIScorboard;
    public GameObject playerScoreText;
    public GameObject AIScoreText;
    public int playersScore;
    public int AIScore;
    public int firstScoreAI;
    public int firstScorePlayer;
    private float k;
    private bool flag;
    private bool swipeFlag;
    private Vector3 targetAI;
    private Vector3 targetPlayer;
    public int nextSceneIndex;
    void Start()
    {
        flag = false;
        Load();
        k = 0;
        targetAI = playersScorboard.gameObject.transform.position;
        targetPlayer = AIScorboard.gameObject.transform.position;
    }

    public void nextButton()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
    private void Load()
    {
        firstScorePlayer = PlayerPrefs.GetInt("firstScorePlayer" , 0);
        firstScoreAI = PlayerPrefs.GetInt("firstScoreAI" , 0);
        AIScore = PlayerPrefs.GetInt("aiScore");
        playersScore = PlayerPrefs.GetInt("playerScore");
        PlayerPrefs.SetInt("firstScorePlayer" , playersScore);
        PlayerPrefs.SetInt("firstScoreAI" , AIScore);
        if (firstScorePlayer < firstScoreAI)
        {
            swipeFlag = false;
            flag = true;
            Vector3 temp;
            temp = playersScorboard.transform.position;
            playersScorboard.transform.position = AIScorboard.transform.position;
            AIScorboard.transform.position = temp;
        }
    }

    private IEnumerator inrease()
    {
        while ((!(playersScore == firstScorePlayer)) && (!(AIScore == firstScoreAI)))
        {
            if (playersScore > firstScorePlayer)
            {
                playersScore--;
            }
            else
            {
                if (playersScore != firstScorePlayer)
                {
                    playersScore++;
                }
            }
            
            if (AIScore > firstScoreAI)
            {
                AIScore--;
            }
            else
            {
                if (AIScore != firstScoreAI)
                {
                    AIScore++;
                }
            }
            yield return new WaitForSeconds(.01f);
        }

        if (AIScore > playersScore)
        {
            if (!flag)
            {
                swipeFlag = true;
            }
        }
        else
        {
            if (flag)
            {
                swipeFlag = true;
            }
        }
    }

    private void swipe()
    {
        playersScorboard.transform.position = Vector3.Lerp(targetAI, targetPlayer, k);
        AIScorboard.transform.position = Vector3.Lerp(targetPlayer, targetAI, k);
    }


    // Update is called once per frame
    void Update()
    {
        if (swipeFlag)
        {
            if (k < 1f)
            {
                k += Time.deltaTime * 3f;
                swipe();
            }
        }
    }
}
