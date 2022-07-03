using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndGameMechanics : MonoBehaviour
{
    public GameObject greenTick;
    public GameObject confety;
    public GameObject[] XCross;
    public GameObject[] xCrossTarget;
    public bool isCanPlayeble;
    private int failCounter;
    private GameObject gm;
    private bool flagConfety;
    void Start()
    {
        gm = GameObject.Find("GameManager");
        isCanPlayeble = true;
        failCounter = 0;
        flagConfety = true;
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
            XCross[0].gameObject.AddComponent<FailCrossController>().target = xCrossTarget[0].gameObject.transform.position;
        }else if (i == 2)
        {
            XCross[1].gameObject.AddComponent<FailCrossController>().target = xCrossTarget[1].gameObject.transform.position;
        }else if (i == 3)
        {
            XCross[2].gameObject.AddComponent<FailCrossController>().target = xCrossTarget[2].gameObject.transform.position;
        }
    }

    private IEnumerator Confety()
    {

        while (true)
        {
            Instantiate(confety, new Vector3(-9.06f, 12f, 137f), Quaternion.identity);
            yield return new WaitForSeconds(.6f);
        }
    }
    
    void Update()
    {
        if (failCounter >= 3)
        {
            gm.GetComponent<GameManager>().UIManagerRunner.gameObject.GetComponent<UIManagerRunner>().tryAgainVisible();
            gm.GetComponent<GameManager>().UIManagerRunner.gameObject.GetComponent<UIManagerRunner>().HintVisible(false);
        }
        if (gameObject.GetComponent<HintTableController>().findTargetBox() == null)
        {
            isCanPlayeble = false;
            if (flagConfety)
            {
                StartCoroutine(Confety());
                flagConfety = false;
            }
            gm.GetComponent<GameManager>().UIManagerRunner.GetComponent<UIManagerRunner>().NextVisible();
        }
        if (isCanPlayeble)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitInfo = new RaycastHit();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "Letter")
                {
                    if (gameObject.GetComponent<HintTableController>().findTargetBox() != null)
                    {
                        if (hitInfo.transform.gameObject.GetComponent<LattersEndGame>().LatterChar == gameObject
                            .GetComponent<HintTableController>().findTargetBox().GetComponent<LetterBoxController>()
                            .latter)
                        {
                            GameObject gt = Instantiate(greenTick, hitInfo.transform.position, Quaternion.identity);
                            gt.transform.localScale = new Vector3(.25f, .25f, .25f);
                            gameObject.GetComponent<HintTableController>().goTable(hitInfo.transform.gameObject,gameObject.GetComponent<HintTableController>().findTargetBox());
                        }
                        else
                        {
                            Debug.Log("Fail");
                            if (failCounter < 3)
                            {
                                failCounter++;
                                fail(failCounter);
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
