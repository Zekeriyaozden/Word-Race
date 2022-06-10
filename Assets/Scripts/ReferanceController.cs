using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferanceController : MonoBehaviour
{
    public GameObject go;
    private Vector3 distance;
    void Start()
    {
        distance = gameObject.transform.position - go.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = distance + go.transform.position;
    }
}
