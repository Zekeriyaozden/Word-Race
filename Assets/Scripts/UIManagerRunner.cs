using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerRunner : MonoBehaviour
{
    public List<int> HintCost;
    public GameObject Hint;
    private GameObject gm;
    public int HintIndex;
    public GameObject text;
    public GameObject tryAgain;
    void Start()
    {
        HintIndex = 0;
        gm = GameObject.Find("GameManager");
    }

    public void HintVisible(bool isVisib)
    {
        Hint.gameObject.transform.parent.gameObject.SetActive(isVisib);
    }

    public void NextVisible()
    {
        Hint.gameObject.transform.parent.GetChild(0).gameObject.SetActive(true);
        Hint.gameObject.transform.parent.GetChild(1).gameObject.SetActive(false);
    }

    public void tryAgainVisible()
    {
        tryAgain.gameObject.SetActive(true);
    }

    public void tryAgainController()
    {
        Debug.Log("sds");
    }
    

    public void HintController()
    {
        if (HintIndex < HintCost.Count)
        {
            if (HintCost[HintIndex] <= gm.GetComponent<GameManager>().playerScore)
            {
                gm.GetComponent<GameManager>().Hint();
                gm.GetComponent<GameManager>().playerScore -= HintCost[HintIndex];
                HintIndex++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        text.GetComponent<TextMeshProUGUI>().text= gm.GetComponent<GameManager>().playerScore.ToString();

        if (HintIndex < HintCost.Count)
        {
            Hint.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = HintCost[HintIndex].ToString();
        }

        
        if (HintCost[HintIndex] > gm.GetComponent<GameManager>().playerScore)
        {
            Hint.gameObject.GetComponent<Button>().interactable = false;
            
        }
        else
        {
            Hint.gameObject.GetComponent<Button>().interactable = true;
        }
        
    }
}
