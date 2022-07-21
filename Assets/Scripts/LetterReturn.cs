using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterReturn : MonoBehaviour
{
    public Vector3 targetPos;
    private Vector3 currentPos;
    public Vector3 targetScale;
    private Vector3 currentScale;
    public float k;
    void Start()
    {
        currentPos = gameObject.transform.position;
        currentScale = gameObject.transform.localScale;
        k = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (k < 1)
        {
            k += Time.deltaTime * (1f/0.2f);
        }
        gameObject.transform.position = Vector3.Lerp(currentPos, targetPos, k);
        gameObject.transform.localScale = Vector3.Lerp(currentScale, targetScale, k);
        if (k >= 1)
        {
            Destroy(gameObject.GetComponent<LetterReturn>());
            //gameObject.GetComponent<LetterReturn>().enabled = false;
        }
    }
}
