using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterSpawner : MonoBehaviour
{
    private Vector3 v3;
    public GameObject gm;
    public float timer;
    void Start()
    {
        v3 = gameObject.transform.position + new Vector3(-1f,0.24f,0);
        StartCoroutine(spawner());
    }

    private IEnumerator spawner()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(timer);
            GameObject gO = Instantiate(gm, v3, Quaternion.identity);
            gO.transform.eulerAngles += new Vector3(0, 180, 0);
        }
    }
    
}