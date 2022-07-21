using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrableBoardController : MonoBehaviour
{
    public List<String> words;
    public Material mt;
    //public GameObject boardDrop;
    
    
    void Start()
    {
       // boardDrop = null;
    }

    public void skip()
    {
        
    }

    private bool control2D(int i,int j)
    {
        bool horiz = false;
        bool vert = false;
        if (j - 1 >= 0)
        {
            if (!GameObject.Find(i.ToString() + "-" + (j - 1).ToString()).GetComponent<ScrblDrag>().isSubmitted &&
                GameObject.Find(i.ToString() + "-" + (j - 1).ToString()).GetComponent<ScrblDrag>().isFull)
            {
                horiz = true;
            }
        }        if (j + 1 <= 8)
        {
            if (!GameObject.Find(i.ToString() + "-" + (j + 1).ToString()).GetComponent<ScrblDrag>().isSubmitted &&
                GameObject.Find(i.ToString() + "-" + (j + 1).ToString()).GetComponent<ScrblDrag>().isFull)
            {
                horiz = true;
            }
        }
        if (i - 1 >= 0)
        {
            if (!GameObject.Find((i - 1).ToString() + "-" + j.ToString()).GetComponent<ScrblDrag>().isSubmitted &&
                GameObject.Find((i - 1).ToString() + "-" + j.ToString()).GetComponent<ScrblDrag>().isFull)
            {
                vert = true;
            }
        }
        if (i + 1 <= 8)
        {
            if (!GameObject.Find(i.ToString() + "-" + j.ToString()).GetComponent<ScrblDrag>().isSubmitted &&
                GameObject.Find(i.ToString() + "-" + j.ToString()).GetComponent<ScrblDrag>().isFull)
            {
                vert = true;
            }
        }

        if (vert && horiz)
        {
            return false;
        }
        if(!vert && !horiz)
        {
            return false;
        }
        return true;
    }

    private bool controlLetter(int i,int j)
    {


        bool isWordExist = false;
        int iFirst = i;
        int jFirst = j;
        string horiz = "";
        string vertical = "";
        /*string s = "";
        s = String.Concat(s, "a");
        s = String.Concat(s, "BBA");
        Debug.Log(s);*/
        bool flagHorizLeft = true;
        bool flagHorizRight = true;
        bool flagVertUp = true;
        bool flagVertDown = true;

        if (i > 8 || j > 8)
        {
            flagHorizLeft = false;
            flagHorizRight = false;
            flagVertDown = false;
            flagVertUp = false;
        }

        
        while (jFirst >= 0 && flagHorizLeft)
        {
            //Debug.Log("j = " + j);
            if (GameObject.Find(i.ToString() + "-" + (jFirst - 1).ToString()).GetComponent<ScrblDrag>().isFull)
            {
                jFirst = jFirst - 1;
            }
            else
            {
                flagHorizLeft = false;
            }
        }
        
        while (jFirst+1 <= 8 && flagHorizRight)
        {
            //Debug.Log("EnterHoriz");
            horiz = String.Concat(horiz , GameObject.Find(i.ToString() + "-" + jFirst.ToString())
                .GetComponent<ScrblDrag>().linked.GetComponent<LattersEndGame>().LatterChar);
            if (GameObject.Find(i.ToString() + "-" + (jFirst + 1).ToString()).GetComponent<ScrblDrag>().isFull)
            {
                jFirst = jFirst + 1;
            }
            else
            {
                flagHorizRight = false;
            }
        }


        Debug.Log("-" + horiz + "-");
        for (int k = 0; k < words.Count; k++)
        {
            if (words[k].Equals(horiz))
            {
                isWordExist = true;
            }
        }

        if (!isWordExist)
        {
            words.Add(horiz);
        }
        return control2D(i, j);

    }

    public void submit()
    {

        bool control2DFlag = true;
        bool flag = true;
        GameObject controllttr;
        int I, J;
        I = 0;
        J = 0;
        int controlI = 9;
        int contrelJ = 9;
        
        while (I < 9)
        {
            J = 0;
            while (J < 9)
            {
                controllttr = GameObject.Find(I.ToString() + "-" + J.ToString());
                if (controllttr.GetComponent<ScrblDrag>().isFull && !controllttr.GetComponent<ScrblDrag>().isSubmitted)
                {
                    if (!control2D(I, J))
                    {
                        control2DFlag = false;
                    }
                }
                J++;
            }
            I++;
        }

        Debug.Log("control flag = "+control2DFlag);
        
        I = 0;
        J = 0;
        //control2DFlag
        if (true)
        {
            while (I < 9)
            {
                J = 0;
                while (J < 9)
                {
                    controllttr = GameObject.Find(I.ToString() + "-" + J.ToString());
                    if (controllttr.GetComponent<ScrblDrag>().isFull && !controllttr.GetComponent<ScrblDrag>().isSubmitted)
                    {
                        controlLetter(I, J);
                    }
                    J++;
                }
                I++;
            }
        }


        

        //controllttr = null;
        /*for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                controllttr = GameObject.Find(i.ToString() + "-" + j.ToString());
                if (controllttr.GetComponent<ScrblDrag>().isFull && !controllttr.GetComponent<ScrblDrag>().isSubmitted)
                {
                    controllttr.GetComponent<ScrblDrag>().isSubmitted = true;
                }
            }
        }*/

        /*if (!controlLetter(I,J))
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    GameObject go = GameObject.Find(i.ToString() + "-" + j.ToString());
                    if (go.GetComponent<ScrblDrag>().isFull && !go.GetComponent<ScrblDrag>().isSubmitted)
                    {
                        go.GetComponent<ScrblDrag>().isSubmitted = true;
                    }
                }
            }
        }*/
    }

    public void returnLetter()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                GameObject go = GameObject.Find(i.ToString() + "-" + j.ToString());
                if (go.GetComponent<ScrblDrag>().isFull && !go.GetComponent<ScrblDrag>().isSubmitted)
                {
                    if (go.GetComponent<ScrblDrag>().linked != null)
                    {
                        go.GetComponent<ScrblDrag>().isFull = false;
                        go.GetComponent<ScrblDrag>().linked.GetComponent<DragAndDrop>().isPlayable = true;
                        go.GetComponent<ScrblDrag>().linked.AddComponent<LetterReturn>();
                        go.GetComponent<ScrblDrag>().linked.GetComponent<LetterReturn>().targetPos = go.GetComponent<ScrblDrag>().linked.GetComponent<DragAndDrop>().posStart;
                        go.GetComponent<ScrblDrag>().linked.GetComponent<LetterReturn>().targetScale = new Vector3(1.8f, 1.8f, 1.8f);
                        go.GetComponent<ScrblDrag>().linked = null;
                    }
                }
            }
        }
    }


    void Update()
    {
        
    }
}
