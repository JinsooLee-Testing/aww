using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Login : MonoBehaviour {
    InputField id;
    InputField pass;
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
        WWWForm form = new WWWForm();
        form.AddField("ID", id.text);
        form.AddField("PASSWD", pass.text);

        WWW www = new WWW(url, form);

        yield return www;

        if(null == www.error)
        {
            Debug.Log(www.text);
            Application.LoadLevel("main_scene");
        }
        else
        {
            Debug.Log(www.error);
            

        }
       
    }
}
