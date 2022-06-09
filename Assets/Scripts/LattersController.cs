using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LattersController : MonoBehaviour
{
    public GameObject referance;
    private float distanceZ;
    
    
    void Start()
    {
        distanceZ = gameObject.transform.position.z - referance.gameObject.transform.position.z;
    }

    private void Update()
    {
        gameObject.transform.Translate(new Vector3(0,0,1) * Time.deltaTime * referance.GetComponent<AIController>().speed , Space.Self);
    }

    void LateUpdate()
    {

    }

}
