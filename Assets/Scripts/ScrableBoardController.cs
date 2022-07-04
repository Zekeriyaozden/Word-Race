using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrableBoardController : MonoBehaviour
{
    public List<GameObject> letterList;
    public List<GameObject> currentLetterList;
    
    void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.TryGetComponent(out ScrableCaseController scc))
            {
                if (scc.prevLevels)
                {
                    foreach (var letter in letterList)
                    {
                        if (letter.gameObject.GetComponent<LattersEndGame>().LatterChar == scc.letter)
                        {
                            GameObject go = Instantiate(letter, gameObject.transform.GetChild(i).gameObject.transform.position, Quaternion.identity);
                            go.GetComponent<Rigidbody>().isKinematic = true;
                            go.GetComponent<Rigidbody>().useGravity = false;
                            go.transform.GetChild(0).gameObject.GetComponent<Collider>().enabled = false;
                            go.transform.eulerAngles = new Vector3(90f, 180f, 0);
                            go.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                            go.transform.SetParent(GameObject.Find("UIManager").transform);
                            continue;
                        }
                    }
                }
            }
        }

        StartCoroutine(currentLetter());
    }


    private IEnumerator currentLetter()
    {

        foreach (var current in currentLetterList)
        {
            yield return new WaitForSeconds(.3f);
            foreach (var letter in letterList)
            {
                if (letter.gameObject.GetComponent<LattersEndGame>().LatterChar ==
                    current.gameObject.GetComponent<ScrableCaseController>().letter)
                {
                    GameObject go = Instantiate(letter, current.gameObject.transform.position, Quaternion.identity);
                    go.GetComponent<Rigidbody>().isKinematic = true;
                    go.GetComponent<Rigidbody>().useGravity = false;
                    go.transform.GetChild(0).gameObject.GetComponent<Collider>().enabled = false;
                    go.transform.eulerAngles = new Vector3(90f, 180f, 0);
                    go.transform.SetParent(GameObject.Find("UIManager").transform);
                    go.GetComponent<Animator>().enabled = true;
                }
            }
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
