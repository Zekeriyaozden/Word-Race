using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class ScrableBoardController : MonoBehaviour
{
    public GameObject recallTextWindow;
    public List<String> words;
    public Material mt;
    public List<String> allWords;
    //public GameObject boardDrop;
    
    
    void Start()
    {
        string readFromFilePath = Application.streamingAssetsPath + "/words" + ".txt";
        allWords = File.ReadAllLines(readFromFilePath).ToList();
        Debug.Log(allWords[5]);
    }

    public void skip()
    {
        
    }

    private bool controlWords()
    {
        for (int i = 0; i < words.Count; i++)
        {
            if (!allWords.Contains(words[i].ToLower()))
            {
                return false;
            }
        }
        return true;
    }
    private bool firstLetter(int i,int j)
    {
        int iFirst = i;
        int jFirst = j;
        bool flagHorizLeft = true;
        bool flagHorizRight = true;
        bool flagVertUp = true;
        bool flagVertDown = true;
        
        while (jFirst>= 0 && flagHorizLeft)
        {
            if (jFirst - 1 >= 0 && GameObject.Find(i.ToString() + "-" + (jFirst - 1).ToString()).GetComponent<ScrblDrag>().isFull
            && !!GameObject.Find(i.ToString() + "-" + (jFirst - 1).ToString()).GetComponent<ScrblDrag>().isSubmitted
            )
            {
                jFirst = jFirst - 1;
            }
            else
            {
                flagHorizLeft = false;
            }
        }
        
        while (jFirst <= 8 && flagHorizRight)
        {
            GameObject.Find(i.ToString() + "-" + jFirst.ToString()).GetComponent<ScrblDrag>().cross = true;
            if (jFirst+1 <= 8 && GameObject.Find(i.ToString() + "-" + (jFirst + 1).ToString()).GetComponent<ScrblDrag>().isFull
            && !GameObject.Find(i.ToString() + "-" + (jFirst + 1).ToString()).GetComponent<ScrblDrag>().isSubmitted
            )
            {
                jFirst = jFirst + 1;
            }
            else
            {
                flagHorizRight = false;
            }
        }
            
        //--------------------------------------------------------------------------------------------------------------
        
        while (iFirst>= 0 && flagVertUp)
        {
            if (iFirst - 1 >= 0 && GameObject.Find((iFirst - 1).ToString() + "-" + j.ToString()).GetComponent<ScrblDrag>().isFull
                                && !!GameObject.Find((iFirst - 1).ToString() + "-" + j.ToString()).GetComponent<ScrblDrag>().isSubmitted
            )
            {
                iFirst = iFirst - 1;
            }
            else
            {
                flagVertUp = false;
            }
        }
        
        while (iFirst <= 8 && flagVertDown)
        {
            GameObject.Find(iFirst.ToString() + "-" + j.ToString()).GetComponent<ScrblDrag>().cross = true;
            if (iFirst+1 <= 8 && GameObject.Find((iFirst+1).ToString() + "-" + j.ToString()).GetComponent<ScrblDrag>().isFull
                              && !GameObject.Find((iFirst + 1).ToString() + "-" + j.ToString()).GetComponent<ScrblDrag>().isSubmitted
            )
            {
                iFirst = iFirst + 1;
            }
            else
            {
                flagVertDown = false;
            }
        }
        
        //--------------------------------------------------------------------------------------------------------------
        
        for (int k = 0; k < 9; k++)
        {
            
            for (int l = 0; l < 9; l++)
            {
                if (GameObject.Find(k.ToString() + "-" + l.ToString()).GetComponent<ScrblDrag>().isFull 
                && !GameObject.Find(k.ToString() + "-" + l.ToString()).GetComponent<ScrblDrag>().isSubmitted)
                {
                    if (!GameObject.Find(k.ToString() + "-" + l.ToString()).GetComponent<ScrblDrag>().cross)
                    {
                        Debug.Log("a");
                        return false;
                    }
                }
            }
        }
        return true;
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
        
        
        while (jFirst>= 0 && flagHorizLeft)
        {
            //Debug.Log("j = " + j);
            if (jFirst - 1 >= 0 &&GameObject.Find(i.ToString() + "-" + (jFirst - 1).ToString()).GetComponent<ScrblDrag>().isFull)
            {
                jFirst = jFirst - 1;
            }
            else
            {
                flagHorizLeft = false;
            }
        }
        
        while (jFirst <= 8 && flagHorizRight)
        {
            //Debug.Log("EnterHoriz");
            horiz = String.Concat(horiz , GameObject.Find(i.ToString() + "-" + jFirst.ToString())
                .GetComponent<ScrblDrag>().linked.GetComponent<LattersEndGame>().LatterChar);
            if (jFirst+1 <= 8 && GameObject.Find(i.ToString() + "-" + (jFirst + 1).ToString()).GetComponent<ScrblDrag>().isFull)
            {
                jFirst = jFirst + 1;
            }
            else
            {
                flagHorizRight = false;
            }
        }
        
        //--------------------------------------------------------------------------------------------------------------

        iFirst = i;
        jFirst = j;
        
        while (iFirst >= 0 && flagVertUp)
        {
            if (jFirst-1 >= 0 && GameObject.Find((iFirst - 1).ToString() + "-" + j.ToString()).GetComponent<ScrblDrag>().isFull)
            {
                iFirst = iFirst - 1;
            }
            else
            {
                flagVertUp = false;
            }
        }
        
        while (iFirst <= 8 && flagVertDown)
        {
            //Debug.Log("EnterHoriz");
            vertical = String.Concat(vertical , GameObject.Find(iFirst.ToString() + "-" + j.ToString())
                .GetComponent<ScrblDrag>().linked.GetComponent<LattersEndGame>().LatterChar);
            if (iFirst+1<=8 && GameObject.Find((iFirst + 1).ToString() + "-" + j.ToString()).GetComponent<ScrblDrag>().isFull)
            {
                iFirst = iFirst + 1;
            }
            else
            {
                flagVertDown = false;
            }
        }

        //--------------------------------------------------------------------------------------------------------------

        
        Debug.Log("-" + horiz + "-");
        Debug.Log("-" + vertical + "-");
        for (int k = 0; k < words.Count; k++)
        {
            if (words[k].Equals(horiz))
            {
                isWordExist = true;
            }
        }
        
        if (horiz.Length > 1 && !isWordExist)
        {
            words.Add(horiz);
        }
        
        //--------------------------------------------------------------------------------------------------------------

        isWordExist = false;
        
        for (int k = 0; k < words.Count; k++)
        {
            if (words[k].Equals(vertical))
            {
                isWordExist = true;
            }
        }
        
        if (vertical.Length > 1 && !isWordExist)
        {
            words.Add(vertical);
        }
        
        //--------------------------------------------------------------------------------------------------------------
        
        return control2D(i, j);
        
    }

    public void submit()
    {

        bool firstLetterFlag = true;
        int firstI = 0;
        int firstJ = 0;
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
                    if (firstLetterFlag)
                    {
                        firstI = I;
                        firstJ = J;
                        firstLetterFlag = false;
                    }
                    if (!control2D(I, J))
                    {
                        control2DFlag = false;
                    }
                }
                J++;
            }
            I++;
        }

        Debug.Log("controlFlag = " + control2DFlag);
        Debug.Log("firstLetter = " + firstLetter(firstI,firstJ));
        
        
        if (control2DFlag && firstLetter(firstI, firstJ))
        { 
            Debug.Log(true);
        }
        else
        {
            Debug.Log("as");
            returnLetter();
        }
        
        I = 0;
        J = 0;

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

        if (controlWords())
        {
            Debug.Log(true);
        }
        else
        {
            Debug.Log(false);
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
        
        words.Clear();
        
    }

    public void returnLetter()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                GameObject go = GameObject.Find(i.ToString() + "-" + j.ToString());
                go.GetComponent<ScrblDrag>().cross = false;
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
