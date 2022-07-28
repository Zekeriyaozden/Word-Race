using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ScrableBoardController : MonoBehaviour
{
    public GameObject yourTurnCanvas;
    public List<GameObject> letters;
    public List<GameObject> buttons;
    public List<bool> AIVert;
    public GameObject cube;
    //0 = vert , 1 = horiz
    public int turn;
    private GameObject gameManager;
    public List<string> words;
    public Material mt;
    public List<string> allWords;
    public List<string> aiWords;
    public bool isAIableToText;
    public bool isPlayerAbleToText;
    public GameObject canvas;
    public GameObject scoreBoard;
    private string readFromFilePath;
    private string readFromFilePathAI;
    public TextAsset txtAll;
    public TextAsset txtAI;
    public bool isFirst;
    void Start()
    {
        isAIableToText = true;
        isPlayerAbleToText = true;
        turn = 0;
        gameManager = GameObject.Find("GameManager");

        string alls = txtAll.text;
        string ai = txtAI.text;
        allWords = alls?.Split('\n').ToList();
        for (int i = 0; i < allWords.Count; i++)
        {
            allWords[i] = allWords[i].Trim();
        }
        aiWords = ai?.Split('\n').ToList();
        for (int i = 0; i < aiWords.Count; i++)
        {
            aiWords[i] = aiWords[i].Trim();
        }

/*
        if (Application.platform == RuntimePlatform.Android)
        {
            readFromFilePath = "jar:file://" + Application.dataPath + "!/assets/words.txt";
            readFromFilePathAI = "jar:file://" + Application.dataPath + "!/assets/wordlist.txt";
            allWords = File.ReadAllLines(readFromFilePath).ToList();
            aiWords = File.ReadAllLines(readFromFilePathAI).ToList();
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            readFromFilePath =  Application.dataPath + "/words.txt";
            readFromFilePathAI =  Application.dataPath+ "/wordlist.txt";
            allWords = File.ReadAllLines(readFromFilePath).ToList();
            aiWords = File.ReadAllLines(readFromFilePathAI).ToList();
        }
        
        else
        {
#if UNITY_IOS
            readFromFilePath =  Application.dataPath + "/words.txt";
            readFromFilePathAI =  Application.dataPath+ "/wordlist.txt";
            allWords = File.ReadAllLines(readFromFilePath).ToList();
            aiWords = File.ReadAllLines(readFromFilePathAI).ToList();
#elif UNITY_ANDROID

            readFromFilePath = "jar:file://" + Application.dataPath + "!/assets/words.txt";
            readFromFilePathAI = "jar:file://" + Application.dataPath + "!/assets/wordlist.txt";
            allWords = File.ReadAllLines(readFromFilePath).ToList();
            aiWords = File.ReadAllLines(readFromFilePathAI).ToList();
#else
            readFromFilePath =  Application.dataPath + "/words.txt";
            readFromFilePathAI =  Application.dataPath+ "/wordlist.txt";
            allWords = File.ReadAllLines(readFromFilePath).ToList();
            aiWords = File.ReadAllLines(readFromFilePathAI).ToList();
#endif
        }
*/
        Debug.Log(readFromFilePath + "-" + readFromFilePathAI);

        

    }
    public void skip()
    {
        returnLetter();
        isPlayerAbleToText = false;
        turn = 1;
        AIGamePlay();
    }
    private bool isCorrectLocation(int i,int j)
    {
        bool isCorrect = false;
        
        if (j - 1 >= 0)
        {
            if (GameObject.Find(i.ToString() + "-" + (j - 1).ToString()).GetComponent<ScrblDrag>().isSubmitted)
            {
                isCorrect = true;
            }
        }        if (j + 1 <= 8)
        {
            if (GameObject.Find(i.ToString() + "-" + (j + 1).ToString()).GetComponent<ScrblDrag>().isSubmitted)
            {
                isCorrect = true;
            }
        }
        
        //--------------------------------------------------------------------------------------------------------------
        
        if (i - 1 >= 0)
        {
            if (GameObject.Find((i - 1).ToString() + "-" + j.ToString()).GetComponent<ScrblDrag>().isSubmitted)
            {
                isCorrect = true;
            }
        }
        if (i + 1 <= 8)
        {
            if (GameObject.Find((i+1).ToString() + "-" + j.ToString()).GetComponent<ScrblDrag>().isSubmitted)
            {
                isCorrect = true;
            }
        }

        Debug.Log(isCorrect);

        if (gameManager.GetComponent<GameManager>().isFirstLevel)
        {
            if (i == 4 && j == 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return isCorrect;

    }
    private bool controlWords()
    {
        if (words.Count == 0)
        {
            return false;
        }
        
        for (int i = 0; i < words.Count; i++)
        {
            Debug.Log(words[i].ToLower());
            string s = words[i].ToLower();
            s = s.Trim();
            if (!allWords.Contains(s))
            {
                return false;
            }
        }
        return true;
    }

    private bool isCorrectLoc()
    {
        if (isFirst)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (!GameObject.Find(i.ToString() + "-" + (j).ToString()).GetComponent<ScrblDrag>()
                        .isSubmitted && GameObject.Find(i.ToString() + "-" + (j).ToString())
                        .GetComponent<ScrblDrag>().isFull)
                    {
                        if (i == 4 && j == 4)
                        {
                            gameManager.GetComponent<GameManager>().isFirst = false;
                            return true;
                        }
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (!GameObject.Find(i.ToString() + "-" + (j).ToString()).GetComponent<ScrblDrag>()
                        .isSubmitted && GameObject.Find(i.ToString() + "-" + (j).ToString())
                        .GetComponent<ScrblDrag>().isFull)
                    {
                        if (j < 8)
                        {
                            if (GameObject.Find(i.ToString() + "-" + (j+1).ToString()).GetComponent<ScrblDrag>().isSubmitted)
                            {
                                return true;
                            } 
                        }
                        if (j > 0)
                        {
                            if (GameObject.Find(i.ToString() + "-" + (j - 1).ToString()).GetComponent<ScrblDrag>()
                                .isSubmitted)
                            {
                                return true;
                            }
                        }
                        if(i > 0)
                        {
                            if (GameObject.Find((i - 1).ToString() + "-" + (j).ToString()).GetComponent<ScrblDrag>()
                                .isSubmitted)
                            {
                                return true;
                            }
                        }
                        if (i < 8)
                        {
                            if (GameObject.Find((i + 1).ToString() + "-" + (j).ToString()).GetComponent<ScrblDrag>()
                                .isSubmitted)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
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
            if (jFirst - 1 >= 0 && GameObject.Find(i.ToString() + "-" + (jFirst - 1).ToString()).GetComponent<ScrblDrag>().isFull)
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
        
        while (iFirst>= 0 && flagVertUp)
        {
            if (iFirst - 1 >= 0 && GameObject.Find((iFirst - 1).ToString() + "-" + j.ToString()).GetComponent<ScrblDrag>().isFull)
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
            if (iFirst+1 <= 8 && GameObject.Find((iFirst+1).ToString() + "-" + j.ToString()).GetComponent<ScrblDrag>().isFull)
            {
                iFirst = iFirst + 1;
            }
            else
            {
                flagVertDown = false;
            }
        }
        
        //--------------------------------------------------------------------------------------------------------------

        bool flag = true;
        
        for (int k = 0; k < 9; k++)
        {
            
            for (int l = 0; l < 9; l++)
            {
                if (GameObject.Find(k.ToString() + "-" + l.ToString()).GetComponent<ScrblDrag>().isFull 
                && !GameObject.Find(k.ToString() + "-" + l.ToString()).GetComponent<ScrblDrag>().isSubmitted)
                {
                    if (!GameObject.Find(k.ToString() + "-" + l.ToString()).GetComponent<ScrblDrag>().cross)
                    {
                        flag =  false;
                    }
                }
            }
        }
        
        for (int k = 0; k < 9; k++)
        {
            for (int l = 0; l < 9; l++)
            { 
                if (GameObject.Find(k.ToString() + "-" + l.ToString()).GetComponent<ScrblDrag>().cross) 
                { 
                    GameObject.Find(k.ToString() + "-" + l.ToString()).GetComponent<ScrblDrag>().cross = false;
                }
            }
        }
        
        return flag;
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
        
        //--------------------------------------------------------------------------------------------------------------
        
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
            if (!GameObject.Find((i+1).ToString() + "-" + j.ToString()).GetComponent<ScrblDrag>().isSubmitted &&
                GameObject.Find((i+1).ToString() + "-" + j.ToString()).GetComponent<ScrblDrag>().isFull)
            {
                vert = true;
            }
        }

        

        if (vert && horiz)
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
            if (iFirst-1 >= 0 && GameObject.Find((iFirst - 1).ToString() + "-" + j.ToString()).GetComponent<ScrblDrag>().isFull)
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

        Debug.Log("horiz:"+horiz);
        Debug.Log("vert:"+vertical);
        
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
        bool controlAllWords = false;
        bool ControlBeforeWord = false;
        bool correctLocation = false;
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

        Debug.Log(control2DFlag);
        Debug.Log(firstLetter(firstI,firstJ));
        
        
        if (control2DFlag && firstLetter(firstI, firstJ) && isCorrectLoc())
        {
            ControlBeforeWord = true;
        }
        else
        {
            ControlBeforeWord = false;
            returnLetter();
        }
        
        I = 0;
        J = 0;

        if (ControlBeforeWord)
        {
            while (I < 9)
            {
                J = 0;
                while (J < 9)
                {

                    controllttr = GameObject.Find(I.ToString() + "-" + J.ToString());
                    if (controllttr.GetComponent<ScrblDrag>().isFull && !controllttr.GetComponent<ScrblDrag>().isSubmitted)
                    {
                        Debug.Log("-----" + isCorrectLocation(I,J) + "---" + correctLocation);
                        if (!correctLocation && isCorrectLocation(I, J))
                        {
                            Debug.Log("-----------s------------");
                            correctLocation = true;
                        }
                        Debug.Log("controlLetter");
                        controlLetter(I, J);
                    }
                    J++;
                }
                I++;
            }
            
            if (controlWords())
            {
                controlAllWords = true;
            }
            else
            {
                controlAllWords = false;
            }
            
        }


        
        

        
        

        controllttr = null;
        if (controlAllWords)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    controllttr = GameObject.Find(i.ToString() + "-" + j.ToString());
                    if (controllttr.GetComponent<ScrblDrag>().isFull && !controllttr.GetComponent<ScrblDrag>().isSubmitted)
                    {
                        List<GameObject> ls = cube.GetComponent<ScrblGameEnd>().letterCollectedList;
                        for (int k = 0; k < ls.Count; k++)
                        {
                            if (controllttr.GetComponent<ScrblDrag>().linked == cube.GetComponent<ScrblGameEnd>().letterCollectedList[k].GetComponent<BuyTheLetter>().gObj)
                            {
                                cube.GetComponent<ScrblGameEnd>().letterCollectedList[k].GetComponent<BuyTheLetter>().gObj = null;
                            }
                        }
                        controllttr.GetComponent<ScrblDrag>().isSubmitted = true;
                    }
                }
            }
            increaseToPoint(true);
            turn = 1;
            AIGamePlay();
        }
        else
        {
            returnLetter();
        }


        
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
    private void hideOfButtons(int _turn)
    {
        if (_turn == 0)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].GetComponent<Button>().interactable = true;
                gameManager.GetComponent<GameManager>().isPlayableLetterDrag = true;
            }
        }else if (_turn == 1)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].GetComponent<Button>().interactable = false;
                gameManager.GetComponent<GameManager>().isPlayableLetterDrag = false;
            }
        }
    }

    //------------------------------------------------------------------------------------------------------------------
    private List<int> checkToTable()
    {
        List<int> list = new List<int>();

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                GameObject go = GameObject.Find(i.ToString() + "-" + j.ToString());
                if (go.GetComponent<ScrblDrag>().isSubmitted)
                {
                    if (i == 0 || i == 8 || j == 0 || j == 8)
                    {
                        
                    }
                    else
                    {
                        Debug.Log(i);
                        Debug.Log(j);
                        if (GameObject.Find((i - 1).ToString() + "-" + (j-1).ToString()).GetComponent<ScrblDrag>()
                                .isSubmitted
                            || GameObject.Find((i - 1).ToString() + "-" + (j + 1).ToString()).GetComponent<ScrblDrag>()
                                .isSubmitted
                            || GameObject.Find((i + 1).ToString() + "-" + (j - 1).ToString()).GetComponent<ScrblDrag>()
                                .isSubmitted
                            || GameObject.Find((i + 1).ToString() + "-" + (j + 1).ToString()).GetComponent<ScrblDrag>()
                                .isSubmitted
                        )
                        {
                            
                        }
                        else
                        {
                            if (!GameObject.Find((i + 1).ToString() + "-" + (j).ToString()).GetComponent<ScrblDrag>()
                                .isSubmitted && !GameObject.Find((i - 1).ToString() + "-" + (j).ToString()).GetComponent<ScrblDrag>()
                                .isSubmitted)
                            {
                                Debug.Log(i*10+j + "->" + false);
                                list.Add((i * 10) + j);
                                AIVert.Add(false);
                            }
                            
                            else if (!GameObject.Find((i).ToString() + "-" + (j + 1).ToString()).GetComponent<ScrblDrag>()
                                .isSubmitted && !GameObject.Find((i).ToString() + "-" + (j-1).ToString()).GetComponent<ScrblDrag>()
                                .isSubmitted)
                            {
                                Debug.Log(i*10+j + "->" + true);
                                list.Add((i * 10) + j);
                                AIVert.Add(true);
                            }
                        }
                    }
                }
            }
        }

        
        return list;
    }

    private bool checkToField(int i, int j,int _firstI,
        int _firstJ,
        int size,bool vert)
    {

        bool _control = true;
        if (!vert)
        {
            if (i - 1 >= 0)
            {
                if (GameObject.Find((i - 1).ToString() + "-" + j.ToString()).GetComponent<ScrblDrag>()
                    .isSubmitted)
                {
                    return false;
                }
            }



            int t = 0;
            while (t < size)
            {
                if (i < 0 || i > 8)
                {
                    return false;
                }
                
                if (i + 1 <= 8)
                {
                    if (i + 1 != _firstI)
                    {
                        if (GameObject.Find((i + 1).ToString() + "-" + j.ToString()).GetComponent<ScrblDrag>()
                            .isSubmitted)
                        {
                            return false;
                        }
                    }
                }

                if (j > 0)
                {
                    if (_firstI != i && GameObject.Find(i.ToString() + "-" + (j - 1).ToString())
                        .GetComponent<ScrblDrag>().isSubmitted)
                    {
                        return false;
                    }
                }
                
                if (j < 8)
                {

                    if (_firstI != i && GameObject.Find(i.ToString() + "-" + (j + 1).ToString())
                        .GetComponent<ScrblDrag>().isSubmitted)
                    {
                        return false;
                    }
                }
                Debug.Log(i + "<-->" + j);
                if (_firstI != i)
                {
                    if (GameObject.Find(i.ToString() + "-" + j.ToString()).GetComponent<ScrblDrag>().isSubmitted)
                    {
                        return false;
                    }
                }
               

                i++;
                t++;
            }
                
        }

        if (vert)
        {
            if (j - 1 >= 0 && GameObject.Find(i.ToString() + "-" + (j-1).ToString()).GetComponent<ScrblDrag>()
                .isSubmitted)
            {
                return false;
            }

            int t = 0;
            while (t < size)
            {
                if (j < 0 || j > 8)
                {
                    return false;
                }
                
                if (j + 1 <= 8)
                {
                    if (j + 1 != _firstJ)
                    {
                        if (GameObject.Find((i).ToString() + "-" + (j+1).ToString()).GetComponent<ScrblDrag>()
                            .isSubmitted)
                        {
                            return false;
                        }
                    }
                }

                if (i != 0)
                {
                    if (_firstJ != j && GameObject.Find((i-1).ToString() + "-" + j.ToString())
                        .GetComponent<ScrblDrag>().isSubmitted)
                    {
                        return false;
                    }
                }
                
                if (j != 8)
                {
                    if (_firstJ != j && GameObject.Find((i+1).ToString() + "-" + j.ToString())
                        .GetComponent<ScrblDrag>().isSubmitted)
                    {
                        return false;
                    }
                }

                if (_firstJ != j && GameObject.Find(i.ToString() + "-" + j.ToString()).GetComponent<ScrblDrag>().isSubmitted)
                {
                    return false;
                }

                t++;
                j++;
            }
                
        }

        return true;
    }

    IEnumerator TextForAI(int startI,int startJ,int firstI,int firstJ,bool verts,string s)
    {
        if (gameManager.GetComponent<GameManager>().isEndGame)
        {
                    char[] charSet = s.ToCharArray();
        if (!verts)
        {
            
            for (int i = 0; i < gameManager.GetComponent<GameManager>().AIWordSize; i++)
            {
                for (int j = 0; j < letters.Count; j++)
                {
                    if (startI != firstI)
                    {
                        if (letters[j].GetComponent<LattersEndGame>().LatterChar.ToLower().Equals(charSet[i].ToString()))
                        {
                            GameObject ltr = Instantiate(letters[j]);
                            ltr.transform.position = GameObject.Find(startI.ToString() + "-" + startJ.ToString())
                                .transform.position + new Vector3(0,0,-0.04f);
                            GameObject.Find(startI.ToString() + "-" + startJ.ToString()).GetComponent<ScrblDrag>()
                                .isSubmitted = true;
                            GameObject.Find(startI.ToString() + "-" + startJ.ToString()).GetComponent<ScrblDrag>()
                                .isFull = true;
                            GameObject.Find(startI.ToString() + "-" + startJ.ToString()).GetComponent<ScrblDrag>()
                                .linked = ltr;
                            ltr.transform.eulerAngles = new Vector3(90, 180, 0);
                            ltr.transform.localScale = Vector3.one;
                        }
                    }
                }
                startI++;
                yield return new WaitForSeconds(.4f);
            }

        }
        else
        {
            for (int i = 0; i < gameManager.GetComponent<GameManager>().AIWordSize; i++)
            {
                for (int j = 0; j < letters.Count; j++)
                {
                    if (startJ != firstJ)
                    {
                        if (letters[j].GetComponent<LattersEndGame>().LatterChar.ToLower().Equals(charSet[i].ToString()))
                        {
                            GameObject ltr = Instantiate(letters[j]);
                            ltr.transform.position = GameObject.Find(startI.ToString() + "-" + startJ.ToString())
                                .transform.position + new Vector3(0,0,-0.04f);
                            GameObject.Find(startI.ToString() + "-" + startJ.ToString()).GetComponent<ScrblDrag>()
                                .isSubmitted = true;
                            GameObject.Find(startI.ToString() + "-" + startJ.ToString()).GetComponent<ScrblDrag>()
                                .isFull = true;
                            GameObject.Find(startI.ToString() + "-" + startJ.ToString()).GetComponent<ScrblDrag>()
                                .linked = ltr;
                            ltr.transform.eulerAngles = new Vector3(90, 180, 0);
                            ltr.transform.localScale = Vector3.one;
                        }
                    }
                }
                startJ++;
                yield return new WaitForSeconds(.4f);
            }
            
        }
        StartCoroutine(turnToPlayer(1f,true));
        }
      /*  char[] charSet = s.ToCharArray();
        if (!verts)
        {
            
            for (int i = 0; i < gameManager.GetComponent<GameManager>().AIWordSize; i++)
            {
                for (int j = 0; j < letters.Count; j++)
                {
                    if (startI != firstI)
                    {
                        if (letters[j].GetComponent<LattersEndGame>().LatterChar.ToLower().Equals(charSet[i].ToString()))
                        {
                            GameObject ltr = Instantiate(letters[j]);
                            ltr.transform.position = GameObject.Find(startI.ToString() + "-" + startJ.ToString())
                                .transform.position + new Vector3(0,0,-0.04f);
                            GameObject.Find(startI.ToString() + "-" + startJ.ToString()).GetComponent<ScrblDrag>()
                                .isSubmitted = true;
                            GameObject.Find(startI.ToString() + "-" + startJ.ToString()).GetComponent<ScrblDrag>()
                                .isFull = true;
                            GameObject.Find(startI.ToString() + "-" + startJ.ToString()).GetComponent<ScrblDrag>()
                                .linked = ltr;
                            ltr.transform.eulerAngles = new Vector3(90, 180, 0);
                            ltr.transform.localScale = Vector3.one;
                        }
                    }
                }
                startI++;
                yield return new WaitForSeconds(.4f);
            }

        }
        else
        {
            for (int i = 0; i < gameManager.GetComponent<GameManager>().AIWordSize; i++)
            {
                for (int j = 0; j < letters.Count; j++)
                {
                    if (startJ != firstJ)
                    {
                        if (letters[j].GetComponent<LattersEndGame>().LatterChar.ToLower().Equals(charSet[i].ToString()))
                        {
                            GameObject ltr = Instantiate(letters[j]);
                            ltr.transform.position = GameObject.Find(startI.ToString() + "-" + startJ.ToString())
                                .transform.position + new Vector3(0,0,-0.04f);
                            GameObject.Find(startI.ToString() + "-" + startJ.ToString()).GetComponent<ScrblDrag>()
                                .isSubmitted = true;
                            GameObject.Find(startI.ToString() + "-" + startJ.ToString()).GetComponent<ScrblDrag>()
                                .isFull = true;
                            GameObject.Find(startI.ToString() + "-" + startJ.ToString()).GetComponent<ScrblDrag>()
                                .linked = ltr;
                            ltr.transform.eulerAngles = new Vector3(90, 180, 0);
                            ltr.transform.localScale = Vector3.one;
                        }
                    }
                }
                startJ++;
                yield return new WaitForSeconds(.4f);
            }
            
        }
        StartCoroutine(turnToPlayer(1f,true));*/
    }
    private void FindToWord(List<int> startI , List<int> startJ ,int firstI, int firstJ , List<bool> verts)
    {
        if (startI.Count > 0)
        {
            List<int> indexOfWord = new List<int>();
            int rand = Random.Range(0, (startI.Count - 1));
            Debug.Log(startI[rand] + "-" + startJ[rand]);
            for (int i = 0; i < aiWords.Count; i++)
            {

                char[] charAr = aiWords[i].ToCharArray();
                if (charAr.Length == gameManager.GetComponent<GameManager>().AIWordSize)
                {
                    if (!verts[rand])
                    {
                        if (charAr[firstI - startI[rand]].ToString()
                            .Equals((GameObject.Find(firstI.ToString() + "-" + firstJ.ToString())).GetComponent<ScrblDrag>().linked.GetComponent<LattersEndGame>().LatterChar.ToLower()))
                        {
                            indexOfWord.Add(i);
                        }
                    }
                    else
                    {
                        if (charAr[firstJ - startJ[rand]].ToString()
                            .Equals((GameObject.Find(firstI.ToString() + "-" + firstJ.ToString())).GetComponent<ScrblDrag>().linked.GetComponent<LattersEndGame>().LatterChar.ToLower()))
                        {
                            indexOfWord.Add(i);
                        }
                    }
                    
                }
            }

            if (indexOfWord.Count > 0)
            {
                int randInd = Random.Range(0, (indexOfWord.Count - 1));
                Debug.Log(aiWords[indexOfWord[randInd]]);
                StartCoroutine(TextForAI(startI[rand], startJ[rand], firstI, firstJ, verts[rand],
                    aiWords[indexOfWord[randInd]]));
            }
            else
            {
                turnToPlayer(1f,false);
            }
            
            

        }
        else
        {
            turnToPlayer(1f,false);
        }
        
    
    }
    private bool checkToWord(int I,int J,bool vert)
    {
        List<int> startI = new List<int>();
        List<int> startJ = new List<int>();
        List<bool> vertS = new List<bool>();
        if (!vert)
        {
            string s = GameObject.Find(I.ToString() + "-" + J.ToString()).GetComponent<ScrblDrag>().linked
                .GetComponent<LattersEndGame>().LatterChar;
            int sizeOfWord = gameManager.GetComponent<GameManager>().AIWordSize;
            for (int i = 1; i <= sizeOfWord; i++)
            {
                if (checkToField((I - sizeOfWord + i), J, I , J , sizeOfWord, vert))
                {
                    startI.Add((I - sizeOfWord + i));
                    startJ.Add(J);
                    vertS.Add(vert);
                }
            }
            
            FindToWord(startI,startJ,I,J,vertS);
        }
        else
        {
            string s = GameObject.Find(I.ToString() + "-" + J.ToString()).GetComponent<ScrblDrag>().linked
                .GetComponent<LattersEndGame>().LatterChar;
            int sizeOfWord = gameManager.GetComponent<GameManager>().AIWordSize;
            for (int i = 1; i <= sizeOfWord; i++)
            {

                if (checkToField(I, (J - sizeOfWord + i) , I , J , sizeOfWord, vert))
                {

                    startI.Add(I);
                    startJ.Add((J - sizeOfWord + i));
                    vertS.Add(vert);
                }
            }
            
            FindToWord(startI,startJ,I,J,vertS);
        }

        if (startI.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator turnSign()
    {
        yourTurnCanvas.SetActive(true);
        yield return new WaitForSeconds(2f);
        yourTurnCanvas.SetActive(false);
    }
    IEnumerator turnToPlayer(float second,bool able)
    {
        if (!able)
        {
            isAIableToText = false;
        }
        else
        {
            increaseToPoint(false);
            isAIableToText = true;
            isPlayerAbleToText = true;
        }
        yield return new WaitForSeconds(second);
        turn = 0;
        StartCoroutine(turnSign());
        AIVert.Clear();
    }
    private void AIGamePlay()
    {
        AIVert.Clear();
        List<int> ls = checkToTable();
        int rand = Random.Range(0, 100);
        if (rand < (100-gameManager.GetComponent<GameManager>().probabilityOfAIPlay))
        {
            StartCoroutine(turnToPlayer(3f,false));
        }
        else
        {
            if (ls.Count > 0)
            {
                bool success = false;
                int _count = 0;
                int cntOfList = ls.Count;
                do
                {
                    
                    int rnd = Random.Range(0, cntOfList - 1);
                    int _i = ls[rnd]/10;
                    int _j = ls[rnd] % 10;
                    bool _vert = AIVert[rnd];
                    Debug.Log(_i + "-" + _j + "-" + _vert);
                    success = checkToWord(_i, _j, _vert);

                } while (_count > 3 && !success);

                if (success)
                {
                    
                }else
                {
                    StartCoroutine(turnToPlayer(3f,false));
                }
                
            }
            else
            {
                StartCoroutine(turnToPlayer(3f,false));
            }
        }
    }
    
    //------------------------------------------------------------------------------------------------------------------

    public void increaseToPoint(bool isPlayer)
    {
        if (isPlayer)
        {
            gameManager.GetComponent<GameManager>().playerScore += gameManager.GetComponent<GameManager>().playerPointForEachLetter;
        }
        else
        {
            gameManager.GetComponent<GameManager>().aiScore += gameManager.GetComponent<GameManager>().AIPointForEachLetter;
        }
    }

    public void hideToLetters()
    {
        int _count = cube.GetComponent<ScrblGameEnd>().letterCollectedList.Count;
        for (int i = 0; i < _count; i++)
        {
            if(cube.GetComponent<ScrblGameEnd>().letterCollectedList[i].GetComponent<BuyTheLetter>().gObj)
            cube.GetComponent<ScrblGameEnd>().letterCollectedList[i].GetComponent<BuyTheLetter>().gObj.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        isFirst = gameManager.GetComponent<GameManager>().isFirst;
        if (turn == 1)
        {
            if (gameManager.GetComponent<GameManager>().isEndGame)
            {
                gameManager.GetComponent<GameManager>().turnAI.SetActive(true);
                gameManager.GetComponent<GameManager>().turnPlayer.SetActive(false);   
            }
            hideOfButtons(1);
        }
        else if (turn == 0)
        {
            if (gameManager.GetComponent<GameManager>().isEndGame)
            {
                gameManager.GetComponent<GameManager>().turnAI.SetActive(false);
                gameManager.GetComponent<GameManager>().turnPlayer.SetActive(true);
            }
            hideOfButtons(0);
        }

        if (!isAIableToText && !isPlayerAbleToText)
        {
            gameManager.GetComponent<GameManager>().isEndGame = false;
            gameManager.GetComponent<GameManager>().turnPlayer.SetActive(false);
            gameManager.GetComponent<GameManager>().turnAI.SetActive(false);
            hideToLetters();
            canvas.SetActive(false);
            scoreBoard.SetActive(true);
        }
        
    }
}
