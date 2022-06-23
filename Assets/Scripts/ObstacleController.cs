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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Letter")
        {
            if (other.gameObject.GetComponent<LattersController>().ownership == "Player")
            {
                if (gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                    .PlayerStack.Contains(other.gameObject))
                {
                    index = gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                        .PlayerStack.IndexOf(other.gameObject);
                    count = gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                        .PlayerStack.Count - index;
                    for (int i = 1; i <= count; i++)
                    {
                        
                        GameObject tmp = gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                            .PlayerStack[gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                                .PlayerStack.Count - 1];
                        
                        gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentPlayerController>()
                                .PlayerStack.Remove(tmp);
                            Destroy(tmp);
                            //Giden Harflere nolacak -- şimdilik Destroy();
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
                        .AIStack.Count - index;
                    for (int i = 1; i <= count; i++)
                    {
                        GameObject tmp = gm.GetComponent<GameManager>().referanceParentAI.GetComponent<ParentAIController>()
                            .AIStack[gm.GetComponent<GameManager>().referanceParentPlayer.GetComponent<ParentAIController>()
                                .AIStack.Count - 1];
                        if (!tmp.GetComponent<LattersController>().isProtected)
                        {
                            gm.GetComponent<GameManager>().referanceParentAI.GetComponent<ParentAIController>()
                                .AIStack.Remove(tmp);

                            //Giden Harflere nolacak -- şimdilik Destroy();
                            Destroy(tmp);
                        }
                        else
                        {
                            Debug.Log( tmp.name + " ");
                        }
                    }

                    count = gm.GetComponent<GameManager>().referanceParentAI.GetComponent<ParentAIController>()
                        .AIStack.Count;
                    
                    
                } 
            }
        }
    }
}
