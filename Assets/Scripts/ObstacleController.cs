using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private GameObject gm;
    private int index;
    private int count;
    private void Start()
    {
        gm = GameObject.Find("GameManager");
    }

    private IEnumerator particle(GameObject other)
    {
        GameObject temp =  Instantiate(gm.GetComponent<GameManager>().particle, other.gameObject.transform.position,
            Quaternion.identity);
        yield return new WaitForSeconds(.4f);
        Destroy(temp.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Letter")
        {
            if (!other.gameObject.GetComponent<LattersController>().isProtected)
            {
                StartCoroutine(particle(other.gameObject));
            }
            if (other.gameObject.GetComponent<LattersController>().ownership == "Player")
            {
                if (gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                    .PlayerStack.Contains(other.gameObject))
                {
                    index = gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                        .PlayerStack.IndexOf(other.gameObject);
                    count = gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                        .PlayerStack.Count;
                    List<GameObject> tmpIsProtected = new List<GameObject>();
                    List<GameObject> tmpIsNotProtected = new List<GameObject>();
                    for (int i = 0; i < count - index; i++)
                    {
                        if (gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                            .PlayerStack[count - 1 - i].GetComponent<LattersController>().isProtected)
                        {
                            tmpIsProtected.Add(gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>().PlayerStack[count - 1- i]);
                        }
                        else
                        {
                            tmpIsNotProtected.Add(gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>().PlayerStack[count - 1- i]);
                        }
                        
                        
                    }

                    for (int i = 0; i < tmpIsNotProtected.Count; i++)
                    {
                        gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                            .PlayerStack.Remove(tmpIsNotProtected[i]);
                        tmpIsNotProtected[i].GetComponent<LattersController>().makeObjectNotWork();
                    }


                    if (tmpIsProtected.Count > 0)
                    {
                        tmpIsProtected[tmpIsProtected.Count - 1].GetComponent<LattersController>().node = gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                            .PlayerStack[gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                                .PlayerStack.IndexOf(tmpIsProtected[tmpIsProtected.Count - 1]) - 1];
                    
                        tmpIsProtected[tmpIsProtected.Count - 1].transform.position =
                            tmpIsProtected[tmpIsProtected.Count - 1].GetComponent<LattersController>().node.transform.position +
                            new Vector3(0, 0, 1.6f);
                    
                        tmpIsProtected[tmpIsProtected.Count - 1].GetComponent<LattersController>().distancer();
                    }
                    

                    for (int i = tmpIsProtected.Count - 2; i >= 0; i--)
                    {
                        Debug.Log(tmpIsProtected[i].name + "tmpIsProtected");
                        tmpIsProtected[i].GetComponent<LattersController>().node = tmpIsProtected[i + 1];
                        tmpIsProtected[i].transform.position =
                            tmpIsProtected[i].GetComponent<LattersController>().node.transform.position +
                            new Vector3(0, 0, 1.6f);
                        tmpIsProtected[i].GetComponent<LattersController>().distancer();
                    }
                    
                    
                    
                    
                    
                    //Destroy(gameObject);
                    //Destroy(gameObject);
                    //Giden Harflere nolacak -- şimdilik Destroy();
                    
                    count = gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                        .PlayerStack.Count;
                    
                    
                }
            }else if (other.gameObject.GetComponent<LattersController>().ownership == "AI")
            {
 if (gm.GetComponent<GameManager>().referanceParentAI.GetComponent<ParentAIController>()
                    .AIStack.Contains(other.gameObject))
                {
                    index = gm.GetComponent<GameManager>().referanceParentAI.GetComponent<ParentAIController>()
                        .AIStack.IndexOf(other.gameObject);
                    count = gm.GetComponent<GameManager>().referanceParentAI.GetComponent<ParentAIController>()
                        .AIStack.Count;
                    List<GameObject> tmpIsProtected = new List<GameObject>();
                    List<GameObject> tmpIsNotProtected = new List<GameObject>();
                    for (int i = 0; i < count - index; i++)
                    {
                        if (gm.GetComponent<GameManager>().referanceParentAI.GetComponent<ParentAIController>()
                            .AIStack[count - 1 - i].GetComponent<LattersController>().isProtected)
                        {
                            tmpIsProtected.Add(gm.GetComponent<GameManager>().referanceParentAI.GetComponent<ParentAIController>().AIStack[count - 1- i]);
                        }
                        else
                        {
                            tmpIsNotProtected.Add(gm.GetComponent<GameManager>().referanceParentAI.GetComponent<ParentAIController>().AIStack[count - 1- i]);
                        }
                        
                        
                    }

                    for (int i = 0; i < tmpIsNotProtected.Count; i++)
                    {
                        gm.GetComponent<GameManager>().referanceParentAI.GetComponent<ParentAIController>()
                            .AIStack.Remove(tmpIsNotProtected[i]);
                        Debug.Log(tmpIsNotProtected[i]);
                        tmpIsNotProtected[i].GetComponent<LattersController>().makeObjectNotWork();
                    }


                    if (tmpIsProtected.Count > 0)
                    {
                        tmpIsProtected[tmpIsProtected.Count - 1].GetComponent<LattersController>().node = gm.GetComponent<GameManager>().referanceParentAI.GetComponent<ParentAIController>()
                            .AIStack[gm.GetComponent<GameManager>().referanceParentAI.GetComponent<ParentAIController>()
                                .AIStack.IndexOf(tmpIsProtected[tmpIsProtected.Count - 1]) - 1];
                    
                        tmpIsProtected[tmpIsProtected.Count - 1].transform.position =
                            tmpIsProtected[tmpIsProtected.Count - 1].GetComponent<LattersController>().node.transform.position +
                            new Vector3(0, 0, 1.6f);
                    
                        tmpIsProtected[tmpIsProtected.Count - 1].GetComponent<LattersController>().distancer();
                    }
                    

                    for (int i = tmpIsProtected.Count - 2; i >= 0; i--)
                    {
                        Debug.Log(tmpIsProtected[i].name + "tmpIsProtected");
                        tmpIsProtected[i].GetComponent<LattersController>().node = tmpIsProtected[i + 1];
                        tmpIsProtected[i].transform.position =
                            tmpIsProtected[i].GetComponent<LattersController>().node.transform.position +
                            new Vector3(0, 0, 1.6f);
                        tmpIsProtected[i].GetComponent<LattersController>().distancer();
                    }
                    
                    
                    
                    
                    
                    //Destroy(gameObject);
                    //Destroy(gameObject);
                    //Giden Harflere nolacak -- şimdilik Destroy();
                    
                    count = gm.GetComponent<GameManager>().referanceParentAI.GetComponent<ParentAIController>()
                        .AIStack.Count;
                    
                } 
            }
        }
    }
}
