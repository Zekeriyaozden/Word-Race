using System;
using System.Collections;
using System.Collections.Generic;
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
    public Quaternion lookAt;
    public GameObject speedTrailer;
    public bool isTrailer;
    private bool gameIsGoing;
    private float mousePos;
    private Vector3 objPos;
    private float startPosY;
    //private Vector3 _lookAtV3;
    void Start()
    {
        startPosY = transform.position.y;
        objPos = Vector3.zero;
        mousePos = 0;
        isTrailer = false;
        isJumping = false;
        gameManager = GameObject.Find("GameManager");
        _speed = gameManager.GetComponent<GameManager>().speedMainChar;

    }

    public void idleAnim()
    {
        gameObject.GetComponent<Animator>().SetBool("idle",true);
        gameObject.GetComponent<Animator>().SetBool("run",false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrailer)
        {
            speedTrailer.SetActive(true);
        }
        else
        {
            speedTrailer.SetActive(false);
        }
        gameIsGoing = gameManager.GetComponent<GameManager>().gameIsGoing;
        if (!gameIsGoing)
        {
            gameObject.GetComponent<Animator>().SetBool("idle",true);
            gameObject.GetComponent<Animator>().SetBool("run",false);
        }
        else
        {
            if (!gameManager.GetComponent<GameManager>().finishController.GetComponent<RunnerFinishController>()
                .mainCharBool)
            {
                gameObject.GetComponent<Animator>().SetBool("idle",false);
                gameObject.GetComponent<Animator>().SetBool("run",true);
            }
        }

        if (gameIsGoing)
        {
              
            if (gameObject.transform.eulerAngles.x > 30 && gameObject.transform.eulerAngles.x < 330)
            {
                gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x,
                    gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
            }

        
            if (isJumping)
            {
                gameObject.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y,
                    transform.eulerAngles.z);
                if (gameObject.transform.position.y < 0.3f)
                {
                    gameObject.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y,
                        transform.eulerAngles.z);
                    isJumping = false;
                    foreach (var stackMember in gameManager.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>().PlayerStack)
                    {
                        if (stackMember != gameManager.GetComponent<GameManager>().referanceParentPlayer
                            .GetComponent<ParentPlayerController>().PlayerStack[0])
                        {
                            stackMember.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;   
                            stackMember.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                        }
                    }
                    gameManager.GetComponent<GameManager>().speedMainChar = gameManager.GetComponent<GameManager>().speedTmp;
                }
            }
            _speed = gameManager.GetComponent<GameManager>().speedMainChar;
            Control();
        }
        else
        {
            
        }
    }


    private void Control()
    {

        if (Input.GetMouseButtonDown(0))
        {
            objPos = gameObject.transform.position;
            _mousePosStart = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mousePos = 0;
            objPos = new Vector3(0, 0, 0);
            _mousePosStart = Vector3.zero;

        }

        if (objPos != Vector3.zero)
        {
            if (Input.mousePosition.x > _mousePosStart.x)
            {
                mousePos = Input.mousePosition.x - _mousePosStart.x;
            }
            else
            {
                mousePos = Input.mousePosition.x - _mousePosStart.x;
            }
        }
        if (gameObject.transform.position.x < maxXLeft)
        {
            mousePos = 0f;
        }else if (gameObject.transform.position.x > maxXRight)
        {
            mousePos = 0f;
        }

        float tr = mousePos * 0.02f;
        if (objPos.x + tr > maxXLeft && objPos.x + tr < maxXRight)
        {
            gameObject.transform.position = new Vector3(objPos.x + tr, startPosY , transform.position.z);
        }

        
        
        
        /*if (_mousePosStart != Vector3.zero)
        {
            if (Input.mousePosition.x > _mousePosStart.x)
            {
                //Debug.Log(_mousePosStart.x + "" + Input.mousePosition.x);
                if (Input.mousePosition.x - _mousePosStart.x > 120)
                {
                    _mousePosStart.x = Input.mousePosition.x - 120;
                }
                xCordinate = (Input.mousePosition.x - _mousePosStart.x);
            }
            
            if (Input.mousePosition.x < _mousePosStart.x)
            {
                if (_mousePosStart.x - Input.mousePosition.x > 120)
                {
                    _mousePosStart.x = Input.mousePosition.x + 120;
                }
                xCordinate = (Input.mousePosition.x - _mousePosStart.x);
            }
        }

        if (gameObject.transform.position.x < maxXLeft && xCordinate < 0)
        {
            xCordinate = 0f;
        }else if (gameObject.transform.position.x > maxXRight && xCordinate > 0)
        {
            xCordinate = 0f;
        }
        //gameObject.transform.Translate(0,0,0 * _speed * Time.deltaTime,Space.Self);*/
        
        
        
        gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        

        gameObject.transform.Translate(new Vector3(0, 0, 1f) * _speed * Time.deltaTime,Space.Self);
        
    }

    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log(other.gameObject.name);
    }
}
