using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public Vector3 Pos;
    public Vector3 _Pos;
    private GameObject gameManager;
    private Vector3 offset;
    private bool isPlayable;
    private void Start()
    {
        isPlayable = true;
        gameManager = GameObject.Find("GameManager");
    }

    private void OnMouseDown()
    {
        //offset =transform.position - Camera.main.ScreenToWorldPoint((Input.mousePosition));
        //Debug.Log("Offset = "+offset);
        if (isPlayable)
        {
            _Pos = transform.position;
            gameObject.transform.position =
                new Vector3(transform.position.x, transform.position.y, transform.position.z + 5.3f);
            transform.localScale = new Vector3(1f, 1f, 1f);
            Pos = gameObject.transform.position;
        }
    }

    private void OnMouseUp()
    {
        if (isPlayable)
        {
            if (gameManager.GetComponent<GameManager>().scrblI != 9)
            {
                GameObject go = GameObject.Find(gameManager.GetComponent<GameManager>().scrblI.ToString() + "-" + gameManager.GetComponent<GameManager>().scrblJ.ToString());
                if (go.GetComponent<ScrblDrag>().isFull)
                {
                    transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
                    transform.position = _Pos;
                    gameManager.GetComponent<GameManager>().dropAndDrag = new Vector3(0,0,0);   
                }
                else
                {
                    isPlayable = false;
                    gameObject.transform.position =
                        new Vector3(go.transform.position.x, go.transform.position.y, transform.position.z);
                    go.GetComponent<ScrblDrag>().isFull = true;
                }
            }
            else
            {
                transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
                transform.position = _Pos;
                gameManager.GetComponent<GameManager>().dropAndDrag = new Vector3(0,0,0);   
            }   
        }
    }

    private void OnMouseDrag()
    {
        if (isPlayable)
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                Pos.z - Camera.main.transform.position.z);
            Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = objPos;
            gameManager.GetComponent<GameManager>().dropAndDrag = gameObject.transform.position;
        }
    }

    void Update()
    {

    }

}
