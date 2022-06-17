using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LattersController : MonoBehaviour
{
    public GameObject referance;
    public GameObject jumpingObject;
    public bool isJumping;
    public GameObject node;
    private float lerpSpeed;
    private float distanceZ;
    private Quaternion qt;
    private float xAngle;
    private float yPos;
    public string ownership;

    void Start()
    {
        isJumping = false;
        lerpSpeed = GameObject.Find("GameManager").GetComponent<GameManager>().lerpSpeed;
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

/*
 *
 *
 *     public GameObject referance;
    private float distanceZ;
    private Quaternion qt;
    private float xAngle;

    void Start()
    {
        distanceZ = gameObject.transform.position.z - referance.gameObject.transform.position.z;
    }

    private void Update()
    {
        xAngle = (Mathf.Cos((referance.gameObject.transform.eulerAngles.y)*Mathf.Deg2Rad));
        Debug.Log(gameObject.transform.position.z - referance.gameObject.transform.position.z);
        gameObject.transform.Translate(new Vector3(0f, 0f, xAngle) * Time.deltaTime * referance.gameObject.GetComponent<PlayerController>()._speed , Space.Self);
    }
    

 */
