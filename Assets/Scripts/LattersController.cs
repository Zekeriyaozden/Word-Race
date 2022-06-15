using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LattersController : MonoBehaviour
{
    public GameObject referance;
    public GameObject node;
    private float lerpSpeed;
    private float distanceZ;
    private Quaternion qt;
    private float xAngle;
    public string ownership;

    void Start()
    {
        lerpSpeed = GameObject.Find("GameManager").GetComponent<GameManager>().lerpSpeed;
        distanceZ = gameObject.transform.position.z - referance.gameObject.transform.position.z;
    }

    private void LateUpdate()
    {
        lerpSpeed = GameObject.Find("GameManager").GetComponent<GameManager>().lerpSpeed;
        gameObject.transform.position = new Vector3((Mathf.Lerp(node.gameObject.transform.position.x , gameObject.transform.position.x,Time.deltaTime * lerpSpeed)),gameObject.transform.position.y,distanceZ + referance.gameObject.transform.position.z);
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
