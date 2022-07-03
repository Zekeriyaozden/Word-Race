using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailCrossController : MonoBehaviour
{
    public Vector3 target;
    private float k;
    private float t;
    private float speed;
    private bool secondAnim;
    private Vector3 start;
    void Start()
    {
        start = gameObject.transform.position;
        secondAnim = false;
        speed = 2f;
        k = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (t < 1)
        {
            t += Time.deltaTime * speed;
        }
        else
        {
            secondAnim = true;
        }
        if (k < 1 && secondAnim)
        {
            k += Time.deltaTime * speed;
        }

        gameObject.transform.position = Vector3.Lerp(start, target, k);
        gameObject.transform.localScale = Vector3.Lerp(new Vector3(0,0,0), new Vector3(1f,1f,1f) , t);
    }
}
