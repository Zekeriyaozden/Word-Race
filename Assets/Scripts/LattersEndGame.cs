using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LattersEndGame : MonoBehaviour
{
    public string LatterChar;
    public bool siss;
    void Start()
    {
        siss = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (siss == true)
        {
            Debug.Log(gameObject.transform.localPosition);
            Debug.Log(gameObject.transform.position + "---Possss---");
        }
    }
}
