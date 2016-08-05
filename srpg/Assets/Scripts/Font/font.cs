using UnityEngine;
using System.Collections.Generic;
using System.Text;
 using System.IO;
using UnityEngine.SceneManagement;
using System.Collections;
public class fontinfo
{
    public string text;
    public string Who_say;
    public int tesx_idx;
   
}
public class Fontlist
{
    public List<fontinfo> bonInfos = new List<fontinfo>();
}
public class font : MonoBehaviour {
    string s;
    public int currentTextNumber = 0;
    public TextMesh text;
    public int Max;
    public string path;
    public bool maintext = true;
    string[] values;
    string t = "";
    public Fontlist font_list;
    void Start()
    {
        if(GUIManager.GetInst().tutorial==true)
        font_list= FIleManager.Getinst().LoadTextData(path);
        if(GUIManager.GetInst().talkmode)
            font_list = FIleManager.Getinst().LoadTextData(GUIManager.GetInst().fontPath);
        //LoadTextFile("text/data.txt");
        text = GetComponent<TextMesh>();
        if (GUIManager.GetInst().tutorial == true)
            text.text = font_list.bonInfos[1].text;
        maintext = maintext;
    }



    // Update is called once per frame

    void Update()
    {
        if (GUIManager.GetInst().tutorial == true)
        {
            //  if(Max>= TextDialog.GetInst().currentTextNumber)
            if (font_list.bonInfos[currentTextNumber].tesx_idx == 1)
            {
                CameraManager.GetInst().SetPosition(new Vector3(0, 7, 0));
            }
            else if (font_list.bonInfos[currentTextNumber].tesx_idx == 2)
            {
                GUIManager.GetInst().CreateUI();
            }
            else if (font_list.bonInfos[currentTextNumber].tesx_idx >= 3 && font_list.bonInfos[currentTextNumber].tesx_idx<10)
            {
                GUIManager.GetInst().MovePos(font_list.bonInfos[currentTextNumber].tesx_idx);
                if (font_list.bonInfos[currentTextNumber].tesx_idx == 6)
                {
                    PlayerManager.GetInst().GenAIPlayer(5, 5);
                    currentTextNumber++;

                }
                   
                

            }
            
            else if (font_list.bonInfos[currentTextNumber].tesx_idx >= 3 && font_list.bonInfos[currentTextNumber].tesx_idx==100)
            {
                
            }
            else if (font_list.bonInfos[currentTextNumber].tesx_idx >= 3 && font_list.bonInfos[currentTextNumber].tesx_idx == 105)
            {
                GUIManager.GetInst().DestoryTalkBox();
                SceneManager.LoadScene(4);
            }
            else
                CameraManager.GetInst().SetPosition(new Vector3(0, 5, 0));
            //if(maintext==true)
            text.text = font_list.bonInfos[currentTextNumber].text;
            //     else
            //   text.text = font_list.bonInfos[currentTextNumber].Who_say;
        }
        if (GUIManager.GetInst().talkmode)
        {
            text.text = font_list.bonInfos[currentTextNumber].text;
            GUIManager.GetInst().named = font_list.bonInfos[currentTextNumber].Who_say;
            if (font_list.bonInfos[currentTextNumber].tesx_idx == 1)
            {
                GUIManager.GetInst().DestoryTalkBox();
                MapManager.GetInst().openDoor();
            }
        }
    }
   void LoadTextFile(string fileName)
    {

     
        string line = " ";
        
        StreamReader sr = new StreamReader(Application.dataPath + "/Resources/" + fileName, System.Text.Encoding.Default);
        if (sr == null)
        {
            print("Error : " + Application.dataPath + "/Resources/" + fileName);
        }
        else
        {
            int cnt = 0;
            line = sr.ReadLine();
            while (line != null)
            {

                values = line.Split(',');  // 쉼표로 구분한다. 저장시에 쉼표로 구분하여 저장하였다.
                if (values.Length == 0)
                {
                    sr.Close();
                }
                line = sr.ReadLine();    // 한줄 읽는다.
                cnt++;
            }
           // Max = cnt;
            sr.Close();
            print("Loaded " + Application.dataPath + "/Resources/db/" + fileName); 
        }

    }
    void OnMouseDown()
    {
        if (font_list.bonInfos[currentTextNumber].tesx_idx != 8)
        {
            currentTextNumber++;
        }
        

    }
}
