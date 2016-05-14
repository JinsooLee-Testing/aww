using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ObjectManager : MonoBehaviour
{

    string[] cardName;
    string[] cardObjectName;
    //string[] objectName;
    void Start()
    {
        StaticData.userId = "leejinsoo";
        StartCoroutine(ConnectObjectCardPHP());
        StartCoroutine(ConnectSkillCardPHP());
    }

    void InsertCardList(string name)
    {
       cardName = name.Split(':');
        foreach (string element in cardName)
        {
            Debug.Log(element);
        }
    }
    void InsertObjectCardList(string name)
    {
        cardObjectName = name.Split(':');
        foreach (string element in cardObjectName)
        {
            Debug.Log(element);
        }
    }
    IEnumerator ConnectSkillCardPHP()
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
            //InsertCardList(www.text);
            //StartCoroutine(ConnectObjectCardPHP());
        }
        else Debug.Log(www.error);
    }

    IEnumerator ConnectObjectCardPHP()
    {

        string url = "http://localhost/SelectObject.php";
        WWWForm form = new WWWForm();
        //form.AddField("ID", StaticData.userId);
        //form.AddField("PASSWD", StaticData.userPasswd);
        //Debug.Log("카드선택창진입");
        WWW www = new WWW(url, form);

        yield return www;
        // Debug.Log(www.text);
        if (null == www.error)
        {
            Debug.Log(www.text);
            //InsertObjectCardList(www.text);
        }
        else Debug.Log(www.error);
    }
}