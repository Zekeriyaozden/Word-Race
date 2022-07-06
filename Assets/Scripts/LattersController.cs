using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LattersController : MonoBehaviour
{
    public int controlGroup;
    public int level;
    public bool isProtected;
    public GameObject referance;
    public GameObject jumpingObject;
    public bool isJumping;
    public GameObject node;
    private float lerpSpeed;
    public float distanceY;
    private Quaternion qt;
    private float xAngle;
    private float yPos;
    public string ownership;
    public bool isDest;
    private GameObject parent;

    void Start()
    {
        isDest = false;
        isProtected = false;
        controlGroup = 0;
        level = 1;
        isProtected = false;
        isJumping = false;
        lerpSpeed = GameObject.Find("GameManager").GetComponent<GameManager>().lerpSpeed;
        if (ownership != null)
        {
            if (ownership == "Player")
            {
                parent = GameObject.Find("GameManager").GetComponent<GameManager>()
                    .referanceParentPlayer.GetComponent<ParentPlayerController>().PlayerStack[0].gameObject;
                distanceY = gameObject.transform.position.y - parent.gameObject.transform.position.y;
            }
            else
            {
                parent = GameObject.Find("GameManager").GetComponent<GameManager>().referanceParentAI
                    .GetComponent<ParentAIController>().AIStack[0];
                distanceY = gameObject.transform.position.y - parent.gameObject.transform.position.y;
            }
        }
    }

    public void makeObjectNotWork()
    {
        gameObject.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
        gameObject.transform.SetParent(null);
        isJumping = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }
    /*public void distancer()
    {
        distanceZ = gameObject.transform.position.z - referance.gameObject.transform.position.z;
    }*/
    private void LateUpdate()
    {
        lerpSpeed = GameObject.Find("GameManager").GetComponent<GameManager>().lerpSpeed;
        yPos = gameObject.transform.position.y;
        if (isJumping)
        {
            yPos = jumpingObject.transform.position.y;
        }
        gameObject.transform.position = new Vector3((Mathf.Lerp( gameObject.transform.position.x,node.gameObject.transform.position.x ,Time.deltaTime * lerpSpeed)), parent.transform.position.y + distanceY ,parent.transform.position.z);

    }
    

}


