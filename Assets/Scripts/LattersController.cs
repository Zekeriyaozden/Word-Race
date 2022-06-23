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
    public float distanceZ;
    private Quaternion qt;
    private float xAngle;
    private float yPos;
    public string ownership;

    void Start()
    {
        isProtected = false;
        controlGroup = 0;
        level = 1;
        isProtected = false;
        isJumping = false;
        lerpSpeed = GameObject.Find("GameManager").GetComponent<GameManager>().lerpSpeed;
        distancer();
    }

    public void makeObjectNotWork()
    {
        gameObject.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
        gameObject.transform.SetParent(null);
        isJumping = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }
    public void distancer()
    {
        distanceZ = gameObject.transform.position.z - referance.gameObject.transform.position.z;
    }
    private void LateUpdate()
    {
        lerpSpeed = GameObject.Find("GameManager").GetComponent<GameManager>().lerpSpeed;
        yPos = gameObject.transform.position.y;
        if (isJumping)
        {
            yPos = jumpingObject.transform.position.y;
        }
        gameObject.transform.position = new Vector3((Mathf.Lerp( gameObject.transform.position.x,node.gameObject.transform.position.x ,Time.deltaTime * lerpSpeed)),yPos,distanceZ + referance.gameObject.transform.position.z);
        if (gameObject.transform.rotation.x > 30)
        {
            gameObject.transform.eulerAngles = new Vector3(30f, 180f, 0f);
        }
        
    }
    

}


