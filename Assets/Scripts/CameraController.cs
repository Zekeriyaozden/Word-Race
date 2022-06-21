using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject focus;
    private Vector3 distance;
    void Start()
    {
        distance =  gameObject.transform.position - focus.gameObject.transform.position ;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        gameObject.transform.position = distance + focus.transform.position;
    }
}
