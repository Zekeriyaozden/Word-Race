using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LattersController : MonoBehaviour
{
    public GameObject referance;
    private float distanceZ;
    private Quaternion qt;
    private float xAngle;

    void Start()
    {
        distanceZ = gameObject.transform.position.z - referance.gameObject.transform.position.z;
    }

    private void Update()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,distanceZ);
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
