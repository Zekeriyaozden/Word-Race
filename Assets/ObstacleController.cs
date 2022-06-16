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
            Debug.Log(other.name);
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

                        //Giden Harflere nolacak -- şimdilik Destroy();
                        Destroy(tmp);
                    }
                    
                }
            }else if (other.gameObject.GetComponent<LattersController>().ownership == "AI")
            {
                
            }
        }
    }
}
