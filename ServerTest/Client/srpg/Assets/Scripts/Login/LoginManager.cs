using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LoginManager : MonoBehaviour
{
    InputField id;
    InputField password;
    // Use this for initialization
    public void SetID(InputField id)
    {
        this.id = id;
    }
    
    public void SetPassword(InputField password)
    {
        this.password = password;
    }

    public void LoginPhp()
    {
        StartCoroutine(Printphp());
    }

    IEnumerator Printphp()
    {
        string url = "http://localhost/Aww/AwwLogin.php";
        //string url = "http://localhost/testconnectCsharp.php";
        // string url = "http://localhost/Aww/login.php";
        WWWForm form = new WWWForm();
        form.AddField("ID", id.text);
        form.AddField("PASSWD", password.text);

        WWW www = new WWW(url, form);

        yield return www;


        if (null == www.error)
        {
            Debug.Log(www.text);
            //StaticData.userId = id.text;
            //StaticData.userPasswd = pass.text;
            //setUserId(id.text);
            //setUserPasswd(pass.text);
            Application.LoadLevel("MainTitle");
            //if ("0" == www.text)
            //{
            //    //StaticData.userStage = 0;
            //    //Application.LoadLevel("main_scene");
            //}
            //else if ("1" == www.text)
            //{
            //    //StaticData.userStage = 1;
            //    //Application.LoadLevel("main_scene");
            //}
            //else if ("2" == www.text)
            //{
            //    //StaticData.userStage = 2;
            //    //Application.LoadLevel("main_scene");
            //}
            //if ("3" == www.text)
            //{
            //    //StaticData.userStage = 3;
            //    Application.LoadLevel("main_scene");
            //}
        }
        else
        {
            Debug.Log(www.error);
        }

    }


}