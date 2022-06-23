using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deneme : MonoBehaviour
{
    public GameObject referanceForPlayer;
    public GameObject referaneForAI;
    private int counterforPlayer;
    private int counterforAI;
    
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
                Rigidbody rb = other.GetComponent<Rigidbody>();
                rb.constraints =  RigidbodyConstraints.FreezeRotation;
                rb.useGravity = false;
                other.transform.eulerAngles = new Vector3(0, 180f, 0);
                other.GetComponent<LattersController>().jumpingObject = gameObject;
                other.GetComponent<LattersController>().isJumping = true;
            }
        }
        else if(other.tag == "Player")
        {
            counterforPlayer = referanceForPlayer.GetComponent<ParentPlayerController>().PlayerStack.Count;
            other.gameObject.GetComponent<PlayerController>().speedTmp =
                GameObject.Find("GameManager").GetComponent<GameManager>().speedMainChar;
            other.gameObject.GetComponent<PlayerController>().isJumping = true;
            other.gameObject.transform.eulerAngles = new Vector3(0, other.gameObject.transform.eulerAngles.y, 0);
            for (int i = 1; i < counterforPlayer ; i++)
            {

                GameObject.Find("GameManager").GetComponent<GameManager>().speedMainChar = 8f;

                referanceForPlayer.GetComponent<ParentPlayerController>().PlayerStack[i]
                    .GetComponent<LattersController>().jumpingObject = referanceForPlayer.GetComponent<ParentPlayerController>().PlayerStack[0];
            }
        }
        
        else if (other.tag == "AI")
        {
            counterforAI = referaneForAI.GetComponent<ParentAIController>().AIStack.Count;
            other.gameObject.GetComponent<AIController>().speedTmp =
                GameObject.Find("GameManager").GetComponent<GameManager>().speedAIChar;
            other.gameObject.GetComponent<AIController>().isJumping = true;
            other.gameObject.transform.eulerAngles = new Vector3(0, other.gameObject.transform.eulerAngles.y, 0);
            for (int i = 1; i < counterforAI ; i++)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().speedAIChar = 8f;
                referaneForAI.gameObject.GetComponent<ParentAIController>().AIStack[i].gameObject.GetComponent<LattersController>().jumpingObject = referaneForAI.gameObject.GetComponent<ParentAIController>().AIStack[0];
            }
        }
    }
}
