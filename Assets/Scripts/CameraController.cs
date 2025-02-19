using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject focus;
    private Vector3 distance;
    public Vector3 target;
    private float k;
    private Vector3 startPos;
    public float speed;
    private Vector3 startRotate;
    void Start()
    {
        startRotate = gameObject.transform.eulerAngles;
        k = 0;
        distance =  gameObject.transform.position - focus.gameObject.transform.position ;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().inGameEnd)
        {
            if (k < 1)
            {
                k += Time.deltaTime * speed;
            }
            gameObject.transform.position = Vector3.Lerp(startPos, target, k);
            gameObject.transform.eulerAngles = Vector3.Lerp(startRotate, new Vector3(-3f, 360f, 0), k);
        }
        else
        {
            gameObject.transform.position = distance + focus.transform.position;
            startPos = gameObject.transform.position;
        }
    }
}
