using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
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
                .GetComponent<ParentPlayerController>().PlayerStack.Count + 1) * new Vector3(0, 0, 1.3f)) + (gm
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
        gameObject.GetComponent<AITarget>().targetable = false;
        gameObject.transform.SetParent(gm.GetComponent<GameManager>().referanceParentAI.transform);
        gameObject.GetComponent<LattersController>().enabled = true;
        gameObject.GetComponent<LattersController>().referance = gm.GetComponent<GameManager>().AI;
        gameObject.GetComponent<LattersController>().ownership = "AI";
       /* X = Mathf.Cos(gm.GetComponent<GameManager>().referanceParentAI.GetComponent<ParentAIController>().last.transform
            .rotation.eulerAngles.x) * 1.3f;
        Y =         X = Mathf.Sin(gm.GetComponent<GameManager>().referanceParentAI.GetComponent<ParentAIController>().last.transform
            .rotation.eulerAngles.x) * 1.3f;*/
        gameObject.transform.position =
            gm.GetComponent<GameManager>().referanceParentAI.GetComponent<ParentAIController>().last.transform
                .position + new Vector3(0, 0, 1.3f);
       // gameObject.transform.rotation = gm.GetComponent<GameManager>().referanceParentAI
       //     .GetComponent<ParentAIController>().last.transform.rotation;
        gm.GetComponent<GameManager>().referanceParentAI.GetComponent<ParentAIController>().last = gameObject;
        gm.GetComponent<GameManager>().referanceParentAI.GetComponent<ParentAIController>().AIStack.Push(gameObject);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<CollectController>().enabled = false;
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
