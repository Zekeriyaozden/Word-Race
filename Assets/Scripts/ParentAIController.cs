using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentAIController : MonoBehaviour
{
    public  Stack<GameObject> AIStack = new Stack<GameObject>();
    public GameObject last;
    void Start()
    {
        AIStack.Push(last);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
