using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
public class ObjectManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ConnectSkillCardPHP());
    }

    //void XMLtoStringParse(string s)
    //{
    //    XmlDocument xmldoc = new XmlDocument();
    //    xmldoc.LoadXml(s);

    //    XmlNodeList nodes = xmldoc.SelectNodes("record/item");

    //    foreach(XmlNode node in nodes)
    //    {
    //        Debug.Log("SKILLNAME: " + node.SelectSingleNode("SKILLNAME").InnerText);
    //        Debug.Log("COST: " + node.SelectSingleNode("COST").InnerText);
    //        Debug.Log("TYPE: " + node.SelectSingleNode("TYPE").InnerText);
    //        Debug.Log("DAMAGE: " + node.SelectSingleNode("DAMAGE").InnerText);
    //        Debug.Log("RANGEVIEW: " + node.SelectSingleNode("RANGEVIEW").InnerText);
    //        Debug.Log("RANGEOFACT: " + node.SelectSingleNode("RANGEOFACT").InnerText);
    //    }

    //}

    IEnumerator ConnectSkillCardPHP()
    {
        string url = "http://localhost/Aww/AwwSelectSkillCard.php";

        WWW www = new WWW(url);

        yield return www;
        if (null == www.error)
        {
            //Debug.Log("Loaded following XML" + www.data);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(www.text.Trim());


            foreach (XmlNode node in xmlDoc.SelectNodes("XML/DATA"))
            {
                Debug.Log("SKILLNAME: " + node.SelectSingleNode("SKILLNAME").InnerText);
                Debug.Log("COST: " + node.SelectSingleNode("COST").InnerText);
                Debug.Log("TYPE: " + node.SelectSingleNode("TYPE").InnerText);
                Debug.Log("DAMAGE: " + node.SelectSingleNode("DAMAGE").InnerText);
                Debug.Log("RANGEVIEW: " + node.SelectSingleNode("RANGEVIEW").InnerText);
                Debug.Log("RANGEOFACT: " + node.SelectSingleNode("RANGEOFACT").InnerText);
            }
        }
        else
            Debug.Log("Load FAil");
    }
}