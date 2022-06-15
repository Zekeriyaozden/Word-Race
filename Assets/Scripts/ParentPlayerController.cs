using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentPlayerController : MonoBehaviour
{

    public  Stack<GameObject> PlayerStack = new Stack<GameObject>();
    public GameObject last;
    void Start()
    {
        PlayerStack.Push(last);
       
    }

    // Update is called once per frame
    void Update()
    {

    }
}
