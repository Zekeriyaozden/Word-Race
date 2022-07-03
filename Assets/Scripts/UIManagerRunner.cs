using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerRunner : MonoBehaviour
{
    public List<int> HintCost;
    public GameObject Hint;
    private GameObject gm;
    public int HintIndex;
    void Start()
    {
        HintIndex = 0;
        gm = GameObject.Find("GameManager");
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
