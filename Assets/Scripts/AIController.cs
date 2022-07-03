using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private float _speed;
    private float direction;
    private float speedTmpForBack;
    public List<GameObject> ObjectList = new List<GameObject>();
    public List<GameObject> _ObjList = new List<GameObject>();
    private float distance;
    private float _distance;
    public bool isJumping;
    public float speedTmp;
    private GameObject gameManager;
    public bool flag;
    public GameObject target;
    private bool gameIsGoing;
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        flag = true;
        StartCoroutine(Timer());
        _speed = GameObject.Find("GameManager").GetComponent<GameManager>().speedAIChar;
    }

    private IEnumerator Timer()
    {
        checkTarget();

        while (ObjectList.Count != 0)
        {
            yield return new WaitForSeconds(0.1f);
            checkTarget();
        }
    }

    private void checkTarget1()
    {
        _ObjList.Clear();
        GameObject gobj;
        for (int i = 0; i < ObjectList.Count; i++)
        {
            Debug.Log(ObjectList[i].transform.position.z - gameObject.transform.position.z + "-" + ObjectList[i].name);
            if (Mathf.Abs(ObjectList[i].transform.position.z - gameObject.transform.position.z) < 50f)
            {
                _ObjList.Add(ObjectList[i].gameObject);
            }
        }

        float distanceX;
        if (_ObjList.Count > 0)
        {
            distanceX = Mathf.Abs(_ObjList[0].gameObject.transform.position.x - gameObject.transform.position.x);
            
            gobj = _ObjList[0];
            for (int i = 0; i < _ObjList.Count; i++)
            {
                if (distanceX < Mathf.Abs(_ObjList[i].gameObject.transform.position.x - gameObject.transform.position.x))
                {
                    gobj = _ObjList[i];
                    distanceX = Mathf.Abs(_ObjList[i].gameObject.transform.position.x - gameObject.transform.position.x);
                }
            }
            target = gobj;
        }
        
    }
    private void checkTarget()
    {
        flag = true;
        _ObjList.Clear();
        if (ObjectList.Count == 0)
        {
            flag = false;
        }

        distance = 0f;
        for(int i = 0 ; i < ObjectList.Count ; i++)
        {
            GameObject  _target  = ObjectList[i].gameObject;
            
            if (_target.gameObject.transform.position.z < gameObject.transform.position.z)
            {
                ObjectList.Remove(_target);
            }
            else if (_target.gameObject.GetComponent<AITarget>().targetable == false)
            {
                ObjectList.Remove(_target);
            }
            else
            {
                if (_target.transform.position.z < gameObject.transform.position.z + 40f)
                {
                    _ObjList.Add(_target);
                }
            }
        }

        if (_ObjList.Count > 0)
        {
            flag = true;
            distance = Mathf.Abs(gameObject.transform.position.x - _ObjList[0].gameObject.transform.position.x);
            target = _ObjList[0];
            for (int i = 0; i < _ObjList.Count; i++)
            {
                GameObject _target = _ObjList[i];
                _distance = Mathf.Abs(gameObject.transform.position.x - _ObjList[i].gameObject.transform.position.x);
                if (distance > _distance)
                {
                    distance = _distance;
                    target = _ObjList[i];
                }
            }
        }
        else
        {
            flag = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameIsGoing = gameManager.GetComponent<GameManager>().gameIsGoing;
        if (gameIsGoing)
        {
            
            if (isJumping)
            {
                gameObject.transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,transform.eulerAngles.z);
                if (gameObject.transform.position.y < 0.3f)
                {
                    isJumping = false;
                    gameManager.GetComponent<GameManager>().speedAIChar = gameManager.GetComponent<GameManager>().speedTmp;
                }
            }
        
        
            _speed = GameObject.Find("GameManager").GetComponent<GameManager>().speedAIChar;
            direction = 0;
            if (flag == true)
            {
                if (gameObject.transform.position.x > target.gameObject.transform.position.x + 0.4f)
                {
                    // gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x,-45f,0f);
                    direction = -1f;
                }else if (gameObject.transform.position.x < target.gameObject.transform.position.x - 0.4f)
                {
                    //gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x,45f,0f);
                    direction = 1f;
                }
                else
                {
                    gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x,0f,0f);
                    direction = 0;
                }
            }
            else
            {
                gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x,0f,0f);
                direction = 0;
            }

            gameObject.transform.Translate(new Vector3(direction,0,1) * Time.deltaTime * _speed,Space.Self);
        }
        else
        {
            
        }
    }
}
