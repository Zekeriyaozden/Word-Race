using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject gameManager;
    private float _speed;
    private bool isTouched;
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        _speed = gameManager.GetComponent<GameManager>().speedMainChar;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(0))
        {
            Debug.Log("up");
        }
        
        
    }
}
