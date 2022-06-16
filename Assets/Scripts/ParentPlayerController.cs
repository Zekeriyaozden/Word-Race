using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentPlayerController : MonoBehaviour
{

    public  List<GameObject> PlayerStack = new List<GameObject>();
    public GameObject last;
    public GameObject referance;
    void Start()
    {
        PlayerStack.Add(last);
       
    }

    // Update is called once per frame
    void Update()
    {

    }
}
