using UnityEngine;
using System.Collections;

public class connect : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LoadFromPhp();

    }
	
	// Update is called once per frame
	void Update () {
	
	}
    IEnumerator LoadFromPhp()
    {
        string url = "http://localhost/helloworld.php";
        WWW www = new WWW(url);

        yield return www;

        if(www.isDone)
        {
            if (null == www.error)
            {
                Debug.Log("Receive Data; " + www.text);
            }
            else
            {
                Debug.Log("error : " + www.error);
            }
        }
    }
}
