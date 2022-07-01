using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBoardGenerator : MonoBehaviour
{
    public List<GameObject> boardLetters;
    private Vector3 startPos;
    private float xAxisDist;
    private float yAxisDist;
    void Start()
    {
        startPos = new Vector3(-7.4554f, -2.4130f, -0.9731f);
        
        for (int i = 0; i < boardLetters.Count; i++)
        {
            if ((i / 5) < 1)
            {
                GameObject temp = Instantiate(boardLetters[i],gameObject.transform);
                if (temp.gameObject.TryGetComponent(out LattersController lc))
                {
                    lc.enabled = false;
                }
                temp.GetComponent<Rigidbody>().isKinematic = true;
                temp.GetComponent<Rigidbody>().useGravity = false;
                temp.GetComponent<Collider>().isTrigger = true;
                temp.transform.GetChild(0).GetComponent<Collider>().enabled = false;
                
                temp.transform.localPosition = (startPos + (new Vector3(3.6628f, 0, 0) * i));
                temp.transform.eulerAngles = new Vector3(90f, 180f, 0);
                temp.transform.localScale = new Vector3(1.5215f, 1.2973f, 1.2973f);
            }else if ((i / 5) == 1)
            {
                GameObject temp = Instantiate(boardLetters[i],gameObject.transform); 
                if (temp.gameObject.TryGetComponent(out LattersController lc))
                {
                    lc.enabled = false;
                }
                temp.GetComponent<Rigidbody>().isKinematic = true;
                temp.GetComponent<Rigidbody>().useGravity = false;
                temp.GetComponent<Collider>().isTrigger = true;
                temp.transform.GetChild(0).GetComponent<Collider>().enabled = false;
                temp.transform.localPosition = (startPos + new Vector3(0,3.1430f,0) + (new Vector3(3.6628f, 0, 0) * (i % 5)));
                temp.transform.eulerAngles = new Vector3(90f, 180f, 0);
                temp.transform.localScale = new Vector3(1.5215f, 1.2973f, 1.2973f);
            }else if ((i / 5) == 2)
            {
                GameObject temp = Instantiate(boardLetters[i],gameObject.transform);
                if (temp.gameObject.TryGetComponent(out LattersController lc))
                {
                    lc.enabled = false;
                }
                temp.GetComponent<Rigidbody>().isKinematic = true;
                temp.GetComponent<Rigidbody>().useGravity = false;
                temp.GetComponent<Collider>().isTrigger = true;
                temp.transform.GetChild(0).GetComponent<Collider>().enabled = false;
                temp.transform.localPosition = (startPos + new Vector3(0, (3.1430f * 2f),0) + (new Vector3(3.6628f, 0, 0) * (i % 5)));
                temp.transform.eulerAngles = new Vector3(90f, 180f, 0);
                temp.transform.localScale = new Vector3(1.5215f, 1.2973f, 1.2973f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
