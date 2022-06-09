using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public float speed;
    void Start()
    {
        speed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(new Vector3(0,0,1) * Time.deltaTime * speed,Space.Self);
    }
}
