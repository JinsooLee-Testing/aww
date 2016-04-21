using UnityEngine;
using System.Text;
 using System.IO;
using System.Collections;

public class font : MonoBehaviour {
    string s;

    public TextMesh text;
    public int Max;
    string[] values;
    string t = "";
    void Start()
    {

        LoadTextFile("text/data.txt");
        text = GetComponent<TextMesh>();
        text.text = values[TextDialog.GetInst().currentTextNumber]; ;

    }



    // Update is called once per frame

    void Update()
    {
      //  if(Max>= TextDialog.GetInst().currentTextNumber)
        text.text = values[TextDialog.GetInst().currentTextNumber];
    }
   void LoadTextFile(string fileName)
    {

     
        string line = "";
        
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

}
