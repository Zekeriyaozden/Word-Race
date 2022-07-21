using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrblDrag : MonoBehaviour
{
    private Material firstMt;
    private Material activeMt;
    private GameObject gameManager;
    public bool isEnter;
    private bool flagEnter;
    public bool isFull;
    public bool isSubmitted;
    public GameObject linked;
    public bool cross;

    void Start()
    {
        cross = false;
        linked = null;
        isSubmitted = false;
        isFull = false;
        flagEnter = true;
        //gameManager.GetComponent<GameManager>().scrblI = 9;
        //gameManager.GetComponent<GameManager>().scrblJ = 9;
        activeMt = transform.parent.gameObject.GetComponent<ScrableBoardController>().mt;
        gameManager = GameObject.Find("GameManager");
        firstMt = gameObject.GetComponent<MeshRenderer>().materials[0];
    }
    
    private void entered()
    {
        if (gameManager.GetComponent<GameManager>().dropAndDrag == new Vector3(0, 0, 0))
        {
            isEnter = false;
        }
        else
        {
            if (gameManager.GetComponent<GameManager>().dropAndDrag.x >= transform.position.x - 1 &&
                gameManager.GetComponent<GameManager>().dropAndDrag.x <= transform.position.x + 1)
            {
                if (gameManager.GetComponent<GameManager>().dropAndDrag.y >= transform.position.y - 1 &&
                    gameManager.GetComponent<GameManager>().dropAndDrag.y <= transform.position.y + 1)
                {
                    isEnter = true;
                }
                else
                {
                    isEnter = false;
                }
            }
            else
            {
                isEnter = false;
            }
        }
    }

    private void changeTheColor()
    {
        Material[] first = new []{firstMt};
        Material[] active = new []{activeMt};
        
        
        if (isEnter)
        {
            flagEnter = false;
            gameObject.GetComponent<MeshRenderer>().materials = active;
            gameManager.GetComponent<GameManager>().scrblI = int.Parse(gameObject.name.Split("-")[0]);
            gameManager.GetComponent<GameManager>().scrblJ = int.Parse(gameObject.name.Split("-")[1]);
        }
        else
        {
            if (!flagEnter)
            {
                gameManager.GetComponent<GameManager>().scrblI = 9;
                gameManager.GetComponent<GameManager>().scrblJ = 9;
                flagEnter = true;
            }
            gameObject.GetComponent<MeshRenderer>().materials = first;
        }

        if (isFull)
        {
            gameObject.GetComponent<MeshRenderer>().materials = first;
        }
        
        }
    void Update()
    {
        entered();
        changeTheColor();
    }
}
