using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Login : MonoBehaviour {
    InputField id;
    InputField pass;
    
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
            StaticData.userId = id.text;
            StaticData.userPasswd = pass.text;
            //setUserId(id.text);
            //setUserPasswd(pass.text);
            if ("0" == www.text)
            {
                StaticData.userStage = 0;
                Application.LoadLevel("main_scene");
            }
            else if ("1" == www.text)
            {
                StaticData.userStage = 1;
                Application.LoadLevel("main_scene");
            }
            else if ("2" == www.text)
            {
                StaticData.userStage = 2;
                Application.LoadLevel("main_scene");
            }
            if ("3" == www.text)
            {
                StaticData.userStage = 3;
                Application.LoadLevel("main_scene");
            }
        }
        else
        {
            Debug.Log(www.error);     
        }
       
    }
}
