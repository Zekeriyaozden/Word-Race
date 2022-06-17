using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deneme : MonoBehaviour
{
    public GameObject referanceForPlayer;
    public GameObject referaneForAI;
    private int counter;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Letter")
        {
            if (other.GetComponent<LattersController>().ownership == "Player")
            {
                Rigidbody rb = other.GetComponent<Rigidbody>();
                rb.constraints =  RigidbodyConstraints.FreezeRotation;
                rb.useGravity = false;
                other.transform.eulerAngles = new Vector3(0, 180f, 0);
                other.GetComponent<LattersController>().jumpingObject = gameObject;
                other.GetComponent<LattersController>().isJumping = true;
            }else if (other.GetComponent<LattersController>().ownership == "AI")
            {
                
            }
        }
        else if(other.tag == "Player")
        {
            counter = referanceForPlayer.GetComponent<ParentPlayerController>().PlayerStack.Count;
            other.gameObject.GetComponent<PlayerController>().speedTmp =
                GameObject.Find("GameManager").GetComponent<GameManager>().speedMainChar;
            other.gameObject.GetComponent<PlayerController>().isJumping = true;
            other.gameObject.transform.eulerAngles = new Vector3(0, other.gameObject.transform.eulerAngles.y, 0);
            for (int i = 1; i < counter ; i++)
            {

                GameObject.Find("GameManager").GetComponent<GameManager>().speedMainChar = 8f;

                referanceForPlayer.GetComponent<ParentPlayerController>().PlayerStack[i]
                    .GetComponent<LattersController>().jumpingObject = referanceForPlayer.GetComponent<ParentPlayerController>().PlayerStack[0];
            }
        }
    }
}
