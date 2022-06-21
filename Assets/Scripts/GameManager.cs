using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float AIWhenIsStop;
    public float DistanceOfMainAndAI;
    public float lerpSpeed;
    public float speedMainChar;
    public float speedAIChar;
    public GameObject Player;
    public GameObject AI;
    public GameObject referanceParentPlayer;
    public GameObject referanceParentAI;
    //-------------------------------------------
    private float tempSpeedOfAI;
    private bool onArea;
    void Start()
    {
        onArea = false;
        tempSpeedOfAI = speedAIChar;
    }
    void Update()
    {
        if (SpeedIncrease())
        {
            speedAIChar = 7f;
        }else if (!SpeedIncrease() && !onArea)
        {
            speedAIChar = tempSpeedOfAI;
        }
        
    }

    public bool SpeedIncrease()
    {
        if (distanceOfMainAndAI() > DistanceOfMainAndAI)
        {
            onArea = true;
            return true;
        }
        else
        {
            if (distanceOfMainAndAI() > 0 && distanceOfMainAndAI() < AIWhenIsStop)
            {
                onArea = false;
            }
            return false;
        }
    }
    
    
    public float distanceOfMainAndAI()
    {
        return (Player.gameObject.transform.position.z - AI.gameObject.transform.position.z);
    }
    
}
