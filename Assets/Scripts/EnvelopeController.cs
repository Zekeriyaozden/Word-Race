using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvelopeController : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(timer());
    }

    private IEnumerator timer()
    {
        yield return new WaitForSeconds(6f);
        Destroy(gameObject);
    }
    
    void Update()
    {
        
    }
}
