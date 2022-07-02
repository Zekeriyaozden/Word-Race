using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleController : MonoBehaviour
{
    public float zMin;
    public float zMax;
    private GameObject gm;
    private int index;
    private int count;
    private void Start()
    {
        gm = GameObject.Find("GameManager");
    }

    private IEnumerator particle(GameObject other)
    {
        /*GameObject temp =  Instantiate(gm.GetComponent<GameManager>().particle, other.gameObject.transform.position,
            Quaternion.identity);*/
        yield return new WaitForSeconds(.2f);
        /*Destroy(temp.gameObject);*/
    }

    private void ObsMechanics(bool isMain)
    {
        if (isMain)
        {
            GameObject node = gm.gameObject.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>().PlayerStack[0].gameObject;
            int i = 1;
            while (i < gm.gameObject.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>().PlayerStack.Count)
            {
                if (gm.gameObject.GetComponent<GameManager>().referanceParentPlayer
                    .GetComponent<ParentPlayerController>().PlayerStack[i].GetComponent<LattersController>().isDest)
                {
                    if (gm.gameObject.GetComponent<GameManager>().referanceParentPlayer
                        .GetComponent<ParentPlayerController>().PlayerStack.Count > i + 1)
                    {
                        gm.gameObject.GetComponent<GameManager>().referanceParentPlayer
                            .GetComponent<ParentPlayerController>().PlayerStack[i + 1].GetComponent<LattersController>().node = node;
                    }
                    gm.gameObject.GetComponent<GameManager>().referanceParentPlayer
                        .GetComponent<ParentPlayerController>().PlayerStack[i].GetComponent<LattersController>()
                        .enabled = false;
                    gm.gameObject.GetComponent<GameManager>().referanceParentPlayer
                        .GetComponent<ParentPlayerController>().PlayerStack.RemoveAt(i);
                }
                else
                {
                    gm.gameObject.GetComponent<GameManager>().referanceParentPlayer
                            .GetComponent<ParentPlayerController>().PlayerStack[i].GetComponent<LattersController>()
                            .node =
                        node;
                    node = gm.gameObject.GetComponent<GameManager>().referanceParentPlayer
                        .GetComponent<ParentPlayerController>().PlayerStack[i];
                    i++;
                }
            }
        }
        else
        {
            GameObject node = gm.gameObject.GetComponent<GameManager>().referanceParentAI.GetComponent<ParentAIController>().AIStack[0].gameObject;
            int i = 1;
            while (i < gm.gameObject.GetComponent<GameManager>().referanceParentAI.GetComponent<ParentAIController>().AIStack.Count)
            {
                if (gm.gameObject.GetComponent<GameManager>().referanceParentAI
                    .GetComponent<ParentAIController>().AIStack[i].GetComponent<LattersController>().isDest)
                {
                    if (gm.gameObject.GetComponent<GameManager>().referanceParentAI
                        .GetComponent<ParentAIController>().AIStack.Count > i + 1)
                    {
                        gm.gameObject.GetComponent<GameManager>().referanceParentAI
                            .GetComponent<ParentAIController>().AIStack[i + 1].GetComponent<LattersController>().node = node;
                    }
                    gm.gameObject.GetComponent<GameManager>().referanceParentAI
                        .GetComponent<ParentAIController>().AIStack[i].GetComponent<LattersController>()
                        .enabled = false;
                    gm.gameObject.GetComponent<GameManager>().referanceParentAI
                        .GetComponent<ParentAIController>().AIStack.RemoveAt(i);
                }
                else
                {
                    gm.gameObject.GetComponent<GameManager>().referanceParentAI
                            .GetComponent<ParentAIController>().AIStack[i].GetComponent<LattersController>()
                            .node =
                        node;
                    node = gm.gameObject.GetComponent<GameManager>().referanceParentAI
                        .GetComponent<ParentAIController>().AIStack[i];
                    i++;
                }
            }
        }
    }

    private void LetterCollectible(GameObject go , Vector3 v3)
    {
        float z = Random.Range(zMin,zMax);
        float x = Random.Range(-12f,-6.5f);
        go.transform.SetParent(null);
        Destroy(go.GetComponent<CollectController>());
        Destroy(go.GetComponent<LattersController>());
        go.AddComponent<LattersController>().enabled = false;
        go.AddComponent<CollectController>();
        go.GetComponent<CollectController>().enabled = true;
        go.transform.position = v3 + new Vector3(0,0,z);
        go.transform.position = new Vector3(x,go.transform.position.y,go.transform.position.z);
        go.GetComponent<Rigidbody>().useGravity = false;
        go.GetComponent<Rigidbody>().isKinematic = true;
        go.GetComponent<AITarget>().targetable = true;

    }
    
    private void OnTriggerEnter(Collider other )
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
                        LetterCollectible(tmpIsNotProtected[i] , tmpIsNotProtected[i].gameObject.transform.position);
                        //tmpIsNotProtected[i].GetComponent<LattersController>().makeObjectNotWork();
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
                        LetterCollectible(tmpIsNotProtected[i] , tmpIsNotProtected[i].gameObject.transform.position);
                        //tmpIsNotProtected[i].GetComponent<LattersController>().makeObjectNotWork();
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

        
   
   /*if (other.gameObject.tag == "Letter")
        {
            if (other.gameObject.TryGetComponent(out LattersController lc))
            {
                if (lc.ownership == "Player" && !lc.isProtected)
                {
                    int indexOf = gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                        .PlayerStack.IndexOf(other.gameObject);
                    GameObject node = lc.node;
                    for (int i = indexOf;
                        i < gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                            .PlayerStack.Count;
                        i++)
                    {
                        if (gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                            .PlayerStack[i].GetComponent<LattersController>().isProtected)
                        {
                            gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                                .PlayerStack[i].GetComponent<LattersController>().node = node;
                            gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                                .PlayerStack[i].transform.position = new Vector3(gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                                .PlayerStack[i].transform.position.x,gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                                .PlayerStack[i].transform.position.y,node.transform.position.z + 1.6f);
                            node = gm.GetComponent<GameManager>().referanceParentPlayer
                                .GetComponent<ParentPlayerController>()
                                .PlayerStack[i].gameObject;
                        }
                        else
                        {
                            if (gm.GetComponent<GameManager>().referanceParentPlayer
                                .GetComponent<ParentPlayerController>()
                                .PlayerStack.Count > i + 1)
                            {
                                gm.GetComponent<GameManager>().referanceParentPlayer
                                    .GetComponent<ParentPlayerController>()
                                    .PlayerStack[i + 1].gameObject.GetComponent<LattersController>().node = node;
                            }

                            gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                                .PlayerStack[i].GetComponent<LattersController>().isDest = true;
                            gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                                .PlayerStack.Remove(gm.GetComponent<GameManager>().referanceParentPlayer
                                    .GetComponent<ParentPlayerController>()
                                    .PlayerStack[i]);
                            gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                                .PlayerStack[i].transform.GetChild(0).GetComponent<MeshFilter>().mesh = null;
                            gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                                .PlayerStack[i].transform.GetChild(0).GetComponent<Collider>().enabled = false;
                            gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                                .PlayerStack[i].GetComponent<Collider>().enabled = false;
                            gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                                .PlayerStack[i].transform.position = new Vector3(0, 0, 0);
                            gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                                .PlayerStack[i].GetComponent<LattersController>().node = gm;
                        }
                    }

                }else if (lc.ownership == "AI")
                {
                    
                }
            }
            
            
            
            else
            {
                
            }
        }



        if (other.gameObject.tag == "Letter")
        {
            if (other.gameObject.GetComponent<LattersController>().ownership == "Player")
            {
                indexOf = gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>().PlayerStack
                    .IndexOf(other.gameObject);
            
                for (int i = 1;
                    i < gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>().PlayerStack
                        .Count;
                    i++)
                {
                    if (i > indexOf)
                    {
                        if (!gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                            .PlayerStack[i].gameObject.GetComponent<LattersController>().isProtected)
                        {
                            gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                                .PlayerStack[i].gameObject.GetComponent<LattersController>().isDest = true;
                        }
                    }
                }
                ObsMechanics(true);
            }
            else if (other.gameObject.GetComponent<LattersController>().ownership == "AI")
            {
                
            }

        }

*/

    }
}
