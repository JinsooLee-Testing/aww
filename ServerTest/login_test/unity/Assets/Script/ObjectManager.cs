using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
public class ObjectManager : MonoBehaviour
{

    //string[] cardName;
    //string[] cardObjectName;
    //string[] objectName;
    void Start()
    {
        //StaticData.userId = "leejinsoo";
        //StartCoroutine(ConnectObjectCardPHP());
        StartCoroutine(ConnectSkillCardPHP());
    }

    void XMLtoStringParse(string s)
    {
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(s);

        XmlNodeList nodes = xmldoc.SelectNodes("record/item");

       // string st = "";
        foreach(XmlNode node in nodes)
        {
            Debug.Log("SKILLNAME: " + node.SelectSingleNode("SKILLNAME").InnerText);
            Debug.Log("COST: " + node.SelectSingleNode("COST").InnerText);
            Debug.Log("TYPE: " + node.SelectSingleNode("TYPE").InnerText);
            Debug.Log("DAMAGE: " + node.SelectSingleNode("DAMAGE").InnerText);
            Debug.Log("RANGEVIEW: " + node.SelectSingleNode("RANGEVIEW").InnerText);
            Debug.Log("RANGEOFACT: " + node.SelectSingleNode("RANGEOFACT").InnerText);
        }

    }
    //void InsertObjectCardList(string name)
    //{
    //    cardObjectName = name.Split(':');
    //    foreach (string element in cardObjectName)
    //    {
    //        Debug.Log(element);
    //    }
    //}
    IEnumerator ConnectSkillCardPHP()
    {

        string url = "http://localhost/SelectCard.php";
        //WWWForm form = new WWWForm();
       // form.AddField("ID", StaticData.userId);
        //form.AddField("PASSWD", StaticData.userPasswd);
        //Debug.Log("카드선택창진입");
        WWW www = new WWW(url);

        yield return www;
        // Debug.Log(www.text);
        if (null == www.error)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load("http://localhost/SelectCard.php");
           // xmldoc.LoadXml(www.text);
            //xmldoc.Load(www.text);
            XmlNodeList nodes = xmldoc.SelectNodes("record/item");

            // string st = "";
            foreach (XmlNode node in nodes)
            {
                Debug.Log("SKILLNAME: " + node.SelectSingleNode("SKILLNAME").InnerText);
                Debug.Log("COST: " + node.SelectSingleNode("COST").InnerText);
                Debug.Log("TYPE: " + node.SelectSingleNode("TYPE").InnerText);
                Debug.Log("DAMAGE: " + node.SelectSingleNode("DAMAGE").InnerText);
                Debug.Log("RANGEVIEW: " + node.SelectSingleNode("RANGEVIEW").InnerText);
                Debug.Log("RANGEOFACT: " + node.SelectSingleNode("RANGEOFACT").InnerText);
            }
            //XMLtoStringParse(www.text);
            //Debug.Log(www.text);
        }
        //else //Debug.Log(www.error);
    }

    //IEnumerator ConnectObjectCardPHP()
    //{

    //    string url = "http://localhost/SelectObject.php";
    //    WWWForm form = new WWWForm();
    //    //form.AddField("ID", StaticData.userId);
    //    //form.AddField("PASSWD", StaticData.userPasswd);
    //    //Debug.Log("카드선택창진입");
    //    WWW www = new WWW(url, form);

    //    yield return www;
    //    // Debug.Log(www.text);
    //    if (null == www.error)
    //    {
    //        Debug.Log(www.text);
    //        //InsertObjectCardList(www.text);
    //    }
    //    else Debug.Log(www.error);
    //}
}