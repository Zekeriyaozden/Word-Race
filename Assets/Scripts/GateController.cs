using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public bool isChangeLevel;
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

    private IEnumerator protectCour(GameObject go)
    {
        go.GetComponent<LattersController>().isProtected = true;
        GameObject gobj = GameObject.Find("GameManager").GetComponent<GameManager>().protect;
        Instantiate(gobj, go.gameObject.transform);
        yield return new WaitForSeconds(GameObject.Find("GameManager").GetComponent<GameManager>().protectTime);
        if (go != null)
        {
            if (go.TryGetComponent(out LattersController hs))
            {
                go.GetComponent<LattersController>().isProtected = false;
                Destroy(go.transform.GetChild(2).gameObject);   
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Letter")
        {
            if (isChangeLevel)
            {
                if (other.gameObject.GetComponent<LattersController>().controlGroup < numberOfGroup)
                {
                    other.gameObject.GetComponent<LattersController>().controlGroup += 1; 
                    StartCoroutine(LetterAnim(other.gameObject));  
                }
            }
            else
            {
                if (other.gameObject.GetComponent<LattersController>().controlGroup < numberOfGroup)
                {
                    other.gameObject.GetComponent<LattersController>().controlGroup += 1;
                    StartCoroutine(protectCour(other.gameObject));
                }

            }
        }
    }
}
