using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
		string url = "http://119.207.205.67/Aww/AwwLogin.php";
        WWWForm form = new WWWForm();
        form.AddField("ID", id.text);
        form.AddField("PASSWD", password.text);

        WWW www = new WWW(url, form);

        yield return www;


        if (null == www.error)
        {
            Debug.Log(www.text);
            StaticData.userID = id.text;
            StaticData.userPassword = password.text;
            //setUserId(id.text);
            //setUserPasswd(pass.text);
            //Application.LoadLevel("MainTitle");
              //int.Parse()
                StaticData.userStage = int.Parse( www.text);
                Application.LoadLevel("MainTitle");

            //else if ("1" == www.text)
            //{
            //    StaticData.userStage = 1;
            //    Application.LoadLevel("MainTitle");
            //}
            //else if ("2" == www.text)
            //{
            //    StaticData.userStage = 2;
            //    Application.LoadLevel("MainTitle");
            //}
            //if ("3" == www.text)
            //{
            //    StaticData.userStage = 3;
            //    Application.LoadLevel("MainTitle");
            //}
            //if ("4" == www.text)
            //{
            //    StaticData.userStage = 4;
            //    Application.LoadLevel("MainTitle");
            //}
            //if ("5" == www.text)
            //{
            //    StaticData.userStage = 5;
            //    Application.LoadLevel("MainTitle");
            //}
        }
        else
        {
            Debug.Log(www.error);
        }

    }


}