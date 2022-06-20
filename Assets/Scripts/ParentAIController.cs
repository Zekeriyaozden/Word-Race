using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentAIController : MonoBehaviour
{
    public  List<GameObject> AIStack = new List<GameObject>();
    public GameObject last;
    public GameObject referance;

    void Start()
    {
        AIStack.Add(last);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
