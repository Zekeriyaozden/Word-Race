using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectController : MonoBehaviour
{
    private bool Enterable;
    private GameObject gm;
    private float X;
    private float Y;
    void Start()
    {
        Enterable = true;
        gm = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ownerPlayer()
    {
        if (Enterable)
        {
            Enterable = false;
            gameObject.GetComponent<AITarget>().targetable = false;
            gameObject.transform.SetParent(gm.GetComponent<GameManager>().referanceParentPlayer.transform);
            gameObject.GetComponent<LattersController>().enabled = true;
            gameObject.GetComponent<LattersController>().referance = gm.GetComponent<GameManager>().Player;
            gameObject.GetComponent<LattersController>().ownership = "Player";
            gameObject.transform.position = ((gm.GetComponent<GameManager>().referanceParentPlayer
                .GetComponent<ParentPlayerController>().PlayerStack.Count + 1) * new Vector3(0, 0, 1.6f)) + (gm
                .GetComponent<GameManager>().referanceParentPlayer
                .GetComponent<ParentPlayerController>().referance.transform.position);
            gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>().PlayerStack
                .Add(gameObject);
            gameObject.GetComponent<LattersController>().node = gm.GetComponent<GameManager>().referanceParentPlayer
                .GetComponent<ParentPlayerController>().PlayerStack[gm.GetComponent<GameManager>().referanceParentPlayer
                    .GetComponent<ParentPlayerController>().PlayerStack.Count - 2];
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gm.GetComponent<CollectAnimations>().PlayerCollectAnim();
            gameObject.GetComponent<CollectController>().enabled = false;
        }
    }

    private void ownerAI()
    {
        if (Enterable)
        {
            Enterable = false;
            gameObject.GetComponent<AITarget>().targetable = false;
            gameObject.transform.SetParent(gm.GetComponent<GameManager>().referanceParentAI.transform);
            gameObject.GetComponent<LattersController>().enabled = true;
            gameObject.GetComponent<LattersController>().referance = gm.GetComponent<GameManager>().AI;
            gameObject.GetComponent<LattersController>().ownership = "AI";
            gameObject.transform.position = ((gm.GetComponent<GameManager>().referanceParentAI
                .GetComponent<ParentAIController>().AIStack.Count + 1) * new Vector3(0, 0, 1.6f)) + (gm
                .GetComponent<GameManager>().referanceParentAI
                .GetComponent<ParentAIController>().referance.transform.position);
        
            gm.GetComponent<GameManager>().referanceParentAI.GetComponent<ParentAIController>().AIStack
                .Add(gameObject);
            gameObject.GetComponent<LattersController>().node = gm.GetComponent<GameManager>().referanceParentAI
                .GetComponent<ParentAIController>().AIStack[gm.GetComponent<GameManager>().referanceParentAI
                    .GetComponent<ParentAIController>().AIStack.Count - 2];
            
            
            //gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.GetComponent<CollectController>().enabled = false;   
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Letter")
        {
            if (other.gameObject.GetComponent<LattersController>().ownership == "Player")
            {
                ownerPlayer();
            }
            else if (other.gameObject.GetComponent<LattersController>().ownership == "AI")
            {
                ownerAI();
            }
            
        }
        else
        {
            if (other.tag == "Player")
            {
                ownerPlayer();
            }
            else if (other.tag == "AI")
            {
                ownerAI();
            }
        }
    }
}
