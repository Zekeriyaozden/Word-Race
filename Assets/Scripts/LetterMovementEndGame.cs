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
    private Vector3 startScale;
    void Start()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        Destroy(rb);
        startPos = gameObject.transform.position;
        k = 0;
        gameObject.transform.SetParent(null);
        startScale = gameObject.transform.localScale;
    }

    private IEnumerator UI()
    {
        isGoingUI = false;
        yield return new WaitForSeconds(.1f);
        //gameObject.GetComponent<LattersController>().enabled = false;
        gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
        if (gameObject.transform.childCount > 2)
        {
            gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().enabled = false;
        }
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

        gameObject.transform.localScale = Vector3.Lerp(startScale, new Vector3(0.6f, 0.6f, 0.6f), k);
        gameObject.transform.eulerAngles = new Vector3(Mathf.Lerp(0f,90f,k), 180f, 0);
        gameObject.transform.position = Vector3.Lerp(startPos, target, k);


    }
}
