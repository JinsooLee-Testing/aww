using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Login : MonoBehaviour {
    InputField id;
    InputField pass;
    SelectStageManager SSM;
    //public int stageNum;
    // Use this for initialization
    public void setId(InputField id)
    {
        this.id = id;
    }
    public void setPass(InputField pass)
    {
        this.pass = pass;
    }
	public void loginphp()
    {
        StartCoroutine(Printphp());
    }
	
	// Update is called once per frame
    IEnumerator Printphp()
    {
        string url = "http://localhost/testconnectCsharp.php";
       // string url = "http://localhost/Aww/login.php";
        WWWForm form = new WWWForm();
        form.AddField("ID", id.text);
        form.AddField("PASSWD", pass.text);

        WWW www = new WWW(url, form);

        yield return www;

        
        if(null == www.error)
        {
            Debug.Log(www.text);
            if ("0" == www.text)
            {
                SSM.setStageNum(0);
                Application.LoadLevel("main_scene");
            }
            else if ("1" == www.text)
            {
                SSM.setStageNum(1);
                Application.LoadLevel("main_scene");
            }
            else if ("2" == www.text)
            {
                SSM.setStageNum(2);
                Application.LoadLevel("main_scene");
            }
            if ("3" == www.text)
            {
                SSM.setStageNum(3);
                Application.LoadLevel("main_scene");
            }
        }
        else
        {
            Debug.Log(www.error);     
        }
       
    }
}
