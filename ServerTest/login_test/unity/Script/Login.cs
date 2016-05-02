using UnityEngine;
using System.Collections;

public class Login : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Printphp());
	}
	
	// Update is called once per frame
    IEnumerator Printphp()
    {
        string url = "http://localhost/testconnectCsharp.php";
        WWWForm form = new WWWForm();
        form.AddField("ID", "jojihoon");
        form.AddField("PASSWD", 7654321);

        WWW www = new WWW(url, form);

        yield return www;

        if(null == www.error)
        {
            Debug.Log(www.text);

        }
        else
        {
            Debug.Log(www.error);

        }
    }
}
