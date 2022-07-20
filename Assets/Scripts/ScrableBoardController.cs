using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrableBoardController : MonoBehaviour
{
    public Material mt;
    //public GameObject boardDrop;
    
    
    void Start()
    {
       // boardDrop = null;
    }

    public void skip()
    {
        
    }

    public void submit()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                GameObject go = GameObject.Find(i.ToString() + "-" + j.ToString());
                if (go.GetComponent<ScrblDrag>().isFull && !go.GetComponent<ScrblDrag>().isSubmitted)
                {
                    go.GetComponent<ScrblDrag>().isSubmitted = true;
                }
            }
        }
    }

    public void returnLetter()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                GameObject go = GameObject.Find(i.ToString() + "-" + j.ToString());
                if (go.GetComponent<ScrblDrag>().isFull && !go.GetComponent<ScrblDrag>().isSubmitted)
                {
                    if (go.GetComponent<ScrblDrag>().linked != null)
                    {
                        go.GetComponent<ScrblDrag>().isFull = false;
                        go.GetComponent<ScrblDrag>().linked.GetComponent<DragAndDrop>().isPlayable = true;
                        go.GetComponent<ScrblDrag>().linked.transform.position =
                            go.GetComponent<ScrblDrag>().linked.GetComponent<DragAndDrop>().posStart;
                        go.GetComponent<ScrblDrag>().linked.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
                        go.GetComponent<ScrblDrag>().linked = null;
                    }
                }
            }
        }
    }


    void Update()
    {
        
    }
}
