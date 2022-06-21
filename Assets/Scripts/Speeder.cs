using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speeder : MonoBehaviour
{
    private MeshRenderer mr;
    private BoxCollider bc;
    private float tmpSpeed;
    public float speedRate;
    private GameObject gm;
    public float speedTimer;
    public bool isCollected;
    
    void Start()
    {
        gm = GameObject.Find("GameManager");
        bc = gameObject.GetComponent<BoxCollider>();
        mr = gameObject.GetComponent<MeshRenderer>();
        isCollected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollected)
        {
            bc.enabled = false;
            mr.enabled = false;
        }
    }

    private IEnumerator timerForSpeed(bool isMain)
    {
        if (isMain)
        {
            tmpSpeed = gm.GetComponent<GameManager>().speedMainChar;
            gm.GetComponent<GameManager>().speedMainChar +=
                gm.GetComponent<GameManager>().speedMainChar * speedRate / 100f;
        }
        else
        {
            Debug.Log(gm.GetComponent<GameManager>().speedAIChar);
            tmpSpeed = gm.GetComponent<GameManager>().speedAIChar;
            gm.GetComponent<GameManager>().speedAIChar +=
                gm.GetComponent<GameManager>().speedAIChar * speedRate / 100f;
            Debug.Log(gm.GetComponent<GameManager>().speedAIChar);
        }
        yield return new WaitForSeconds(speedTimer);
        if (isMain)
        {
            gm.GetComponent<GameManager>().speedMainChar = tmpSpeed;
        }
        else
        {
            Debug.Log("ElseSecond");
            gm.GetComponent<GameManager>().speedAIChar = tmpSpeed;
        }
        
        Destroy(gameObject);
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isCollected = true;
            StartCoroutine(timerForSpeed(true));
        }else if (other.tag == "AI")
        {
            Debug.Log("InCollision");
            isCollected = true;
            StartCoroutine(timerForSpeed(false));
        }
    }
}
