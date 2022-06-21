using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxXLeft;
    public float maxXRight;
    private GameObject gameManager;
    private float _speed;
    private Vector3 _mousePosStart;
    public float speedTmp;
    public bool isJumping;
    private bool isTouched;
    private float xCordinate;
    public Quaternion lookAt;
    //private Vector3 _lookAtV3;
    void Start()
    {
        isJumping = false;
        gameManager = GameObject.Find("GameManager");
        _speed = gameManager.GetComponent<GameManager>().speedMainChar;
        xCordinate = 0;
    }

    // Update is called once per frame
    void Update()
    {

        
        if (isJumping)
        {
            if (gameObject.transform.position.y < 0.3f)
            {
                isJumping = false;
                gameManager.GetComponent<GameManager>().speedMainChar = speedTmp;
            }
        }
        _speed = gameManager.GetComponent<GameManager>().speedMainChar;
        Control();

    }


    private void Control()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _mousePosStart = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _mousePosStart = Vector3.zero;
            xCordinate = 0;
        }

        if (_mousePosStart != Vector3.zero)
        {
            if (Input.mousePosition.x > _mousePosStart.x)
            {
                //Debug.Log(_mousePosStart.x + "" + Input.mousePosition.x);
                if (Input.mousePosition.x - _mousePosStart.x > 80)
                {
                    _mousePosStart.x = Input.mousePosition.x - 80;
                }
                xCordinate = (45f/80f) * (Input.mousePosition.x - _mousePosStart.x);
            }
            
            if (Input.mousePosition.x < _mousePosStart.x)
            {
                if (_mousePosStart.x - Input.mousePosition.x > 80)
                {
                    _mousePosStart.x = Input.mousePosition.x + 80;
                }
                xCordinate = (45f/80f) * (Input.mousePosition.x - _mousePosStart.x);
            }
        }

        if (gameObject.transform.position.x < maxXLeft && xCordinate < 0)
        {
            xCordinate = 0f;
        }else if (gameObject.transform.position.x > maxXRight && xCordinate > 0)
        {
            xCordinate = 0f;
        }
        gameObject.transform.rotation = Quaternion.Euler(transform.eulerAngles.x , xCordinate , 0f);
  
        
        
        
        gameObject.transform.Translate(0,0,1f * _speed * Time.deltaTime,Space.Self);
        
    }
    
    
    
}
