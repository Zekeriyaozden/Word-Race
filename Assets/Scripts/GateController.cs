using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public Mesh secondLevelLetterMesh;
    public Mesh thirdLevelLetterMesh;
    public int numberOfGroup;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void changeLevel(GameObject gm)
    {
        if (gm.gameObject.GetComponent<LattersController>().level == 1)
        {
            gm.gameObject.transform.GetChild(0).GetComponent<MeshFilter>().mesh = secondLevelLetterMesh;
            //gm.gameObject.GetComponent<MeshFilter>().mesh = secondLevelLetterMesh;
            gm.gameObject.GetComponent<LattersController>().level = 2;

        }else if (gm.gameObject.GetComponent<LattersController>().level == 2)
        {
            gm.gameObject.transform.GetChild(0).GetComponent<MeshFilter>().mesh = thirdLevelLetterMesh;
            gm.gameObject.GetComponent<LattersController>().level = 3;
        }
    }

    private IEnumerator LetterAnim(GameObject gm)
    {
        gm.gameObject.transform.localScale = new Vector3(0.8f,0.8f,0.8f);
        yield return new WaitForSeconds(.05f);
            changeLevel(gm);
        yield return new WaitForSeconds(.05f);
        
        gm.gameObject.transform.localScale = new Vector3(0.6f,0.6f,0.6f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Letter")
        {
            if (other.gameObject.GetComponent<LattersController>().controlGroup < numberOfGroup)
            {
                other.gameObject.GetComponent<LattersController>().controlGroup += 1; 
                StartCoroutine(LetterAnim(other.gameObject));  
            }
        }
    }
}
