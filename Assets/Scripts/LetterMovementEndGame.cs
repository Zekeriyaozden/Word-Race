using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterMovementEndGame : MonoBehaviour
{
    public float speed;
    public Vector3 target;
    public float k;
    public Vector3 startPos;
    public bool isGoingUI;
    void Start()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        Destroy(rb);
        startPos = gameObject.transform.position;
        k = 0;
    }

    private IEnumerator UI()
    {
        isGoingUI = false;
        yield return new WaitForSeconds(.1f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (k < 1)
        {
            k += speed * Time.deltaTime;
        }
        else
        {
            if (isGoingUI)
            {
                StartCoroutine(UI());
            }
        }

        gameObject.transform.eulerAngles = new Vector3(Mathf.Lerp(0f,90f,k), 180f, 0);
        gameObject.transform.position = Vector3.Lerp(startPos, target, k);


    }
}
