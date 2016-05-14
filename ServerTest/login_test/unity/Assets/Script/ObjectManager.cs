using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour
{

    void Start()
    {
       
        StartCoroutine(ConnPhpLogin());

    }


    IEnumerator ConnPhpLogin()
    {

        string url = "http://localhost/SelectCard.php";
        WWWForm form = new WWWForm();
        form.AddField("ID", StaticData.userId);
        //form.AddField("PASSWD", StaticData.userPasswd);
        //Debug.Log("카드선택창진입");
        WWW www = new WWW(url, form);

        yield return www;
       // Debug.Log(www.text);
        if (null == www.error)
        {
            Debug.Log(www.text);
        }
        else Debug.Log(www.error);
    }
}