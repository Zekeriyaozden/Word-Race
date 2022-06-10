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
        Debug.Log(PlayerStack.Count);
        if (PlayerStack.Count > 3)
        {
            while (PlayerStack.Count >= 0)
            {
                Debug.Log(PlayerStack.Pop());
            }
        }
    }
}
