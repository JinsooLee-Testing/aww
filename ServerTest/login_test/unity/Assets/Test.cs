using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Printphp());
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator Printphp()
    {

        //string url = "http://localhost/Aww/xmlLogin.php";
        string url = "http://52.78.5.177/test.php";
     
        WWW www = new WWW(url);

        yield return www;


        if (null == www.error)
        {


            Debug.Log(www.text);

        }
        else
        {
            Debug.Log(www.error);
        }

    }
}
