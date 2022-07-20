using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterAnim : MonoBehaviour
{
    //04 -> 1.3
    private float k;
    private Vector3 firstPos;
    public Vector3 targetPos;
    private Vector3 firstScale;
    private Vector3 targetScale;
    private Vector3 firstAngle;
    private Vector3 targetAngle;
    public float time;
    void Start()
    {
        firstAngle = gameObject.transform.eulerAngles;
        targetAngle = new Vector3(90f, 180f, 0);
        firstPos = gameObject.transform.position;
        firstScale = new Vector3(.4f, .4f, .4f);
        targetScale = new Vector3(1.8f, 1.8f, 1.8f);
    }

    // Update is called once per frame
    void Update()
    {
        if (k < 1)
        {
            k += Time.deltaTime * (1f/time);
        }
        gameObject.transform.position = Vector3.Lerp(firstPos, targetPos, k);
        gameObject.transform.localScale = Vector3.Lerp(firstScale, targetScale, k);
        gameObject.transform.eulerAngles = Vector3.Lerp(firstAngle, targetAngle, k);

        if (k >= 1)
        {
            gameObject.AddComponent<DragAndDrop>();
            gameObject.GetComponent<LetterAnim>().enabled = false;
        }

    }
}
