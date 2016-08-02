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
		string url = "http://localhost/Aww/AwwLogin.php";
        WWWForm form = new WWWForm();
        form.AddField("ID", id.text);
        form.AddField("PASSWD", password.text);

        WWW www = new WWW(url, form);

        yield return www;


        if (null == www.error)
        {
            //Debug.Log(www.text);
            Application.LoadLevel("MainTitle");
        }
        else
        {
            Debug.Log(www.error);
        }

    }


}