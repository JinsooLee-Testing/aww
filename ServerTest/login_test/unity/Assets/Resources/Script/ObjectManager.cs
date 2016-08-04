using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
public class ObjectManager : MonoBehaviour
{
    //CardBase cb = null;
    void Start()
    {
        StartCoroutine(LoadCard());
        //cb = new CardBase();
    }

    

    IEnumerator LoadCard()
    {
        string url = "http://localhost/Aww/AwwSelectObjectCard.php";

        WWW www = new WWW(url);

        yield return www;
        if (null == www.error)
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(www.text.Trim());

            foreach (XmlNode node in xmlDoc.SelectNodes("XML/DATA"))
            {
               /* Debug.Log("NAME: " + node.SelectSingleNode("NAME").InnerText);
                Debug.Log("ATT: " + node.SelectSingleNode("ATT").InnerText);
                Debug.Log("HP: " + node.SelectSingleNode("HP").InnerText);
                Debug.Log("MOVE: " + node.SelectSingleNode("MOVE").InnerText);
                Debug.Log("VIEW: " + node.SelectSingleNode("VIEW").InnerText);
                Debug.Log("SIZE: " + node.SelectSingleNode("SIZE").InnerText);
                Debug.Log("ATTRIBUTE: " + node.SelectSingleNode("ATTRIBUTE").InnerText);
                Debug.Log("PLAYERSELECT: " + node.SelectSingleNode("PLAYERSELECT").InnerText);*/
            }
        }
        else
            Debug.Log("Load Fail");
    }
}