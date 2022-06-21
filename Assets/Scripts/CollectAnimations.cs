using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAnimations : MonoBehaviour
{
    public float AnimWaitTime;
    public GameObject playerParent;
    public GameObject AIParent;

    public void PlayerCollectAnim()
    {
        StartCoroutine(playerAnim());
    }

    public void AICollectAnim()
    {
        
    }

    private IEnumerator playerAnim()
    {
        int count;
        count = playerParent.GetComponent<ParentPlayerController>().PlayerStack.Count;
        for (int i = count - 1; i > 0; i--)
        {
            playerParent.GetComponent<ParentPlayerController>().PlayerStack[i].gameObject.transform.localScale =
                new Vector3(.8f, .8f, .8f);
            yield return new WaitForSeconds(AnimWaitTime);
            playerParent.GetComponent<ParentPlayerController>().PlayerStack[i].gameObject.transform.localScale =
                new Vector3(.6f, .6f, .6f);
        }


    }

}
