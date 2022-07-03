using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameMechanics : MonoBehaviour
{

    public bool isCanPlayeble;
    private int failCounter;
    private GameObject gm;
    void Start()
    {
        gm = GameObject.Find("GameManager");
        isCanPlayeble = true;
        failCounter = 0;
    }

    private IEnumerator isCanPlayebleFalse()
    {
        isCanPlayeble = false;
        yield return new WaitForSeconds(1f);
        isCanPlayeble = true;
    }


    public void fail(int i)
    {
        if (i == 1)
        {
            
        }else if (i == 2)
        {
            
        }else if (i == 3)
        {
            
        }
    }
    
    void Update()
    {
        if (gameObject.GetComponent<HintTableController>().findTargetBox() == null)
        {
            isCanPlayeble = false;
            gm.GetComponent<GameManager>().UIManagerRunner.GetComponent<UIManagerRunner>().NextVisible();
        }
        if (isCanPlayeble)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitInfo = new RaycastHit();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "Letter")
                {
                    print ("It's working");
                    if (gameObject.GetComponent<HintTableController>().findTargetBox() != null)
                    {
                        if (hitInfo.transform.gameObject.GetComponent<LattersEndGame>().LatterChar == gameObject
                            .GetComponent<HintTableController>().findTargetBox().GetComponent<LetterBoxController>()
                            .latter)
                        {
                            gameObject.GetComponent<HintTableController>().goTable(hitInfo.transform.gameObject,gameObject.GetComponent<HintTableController>().findTargetBox());
                        }
                        else
                        {
                            Debug.Log("Fail");
                            if (failCounter < 3)
                            {
                                failCounter++;
                            }

                            if (failCounter == 3)
                            {
                                Debug.Log("Game Over");
                            }
                        }
                    }
                }
            }
        }
    }

}
