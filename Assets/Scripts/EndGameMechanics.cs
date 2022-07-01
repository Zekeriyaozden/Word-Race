using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameMechanics : MonoBehaviour
{

    public bool isCanPlayeble;

    void Start()
    {
        isCanPlayeble = true;
    }
    void Update()
    {
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
                    }
                }
            }
        }
    }

}
