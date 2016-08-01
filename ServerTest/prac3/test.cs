using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Add());
	}
	IEnumerator Add()
    {
        string url = "http://localhost/test.php";
        WWWForm form = new WWWForm();
        form.AddField("a", 10);
        form.AddField("b", 50);

        WWW www = new WWW(url, form);
        yield return www;

        if (www.error == null) Debug.Log(www.text);
        else Debug.Log(www.error);
    }


}
