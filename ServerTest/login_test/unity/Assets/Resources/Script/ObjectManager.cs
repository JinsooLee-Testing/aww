using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
public class ObjectManager : MonoBehaviour
{
    private static ObjectManager inst = null;
    public static ObjectManager GetInst()
    {
        return inst;
    }
    //CardBase cb = null;
    void Awake()
    {
        inst = this;
        StartCoroutine(LoadCard());
        //cb = new CardBase();
    }

    public void startSaveCoroutine(string str)
    {
        StartCoroutine(SaveCard(str));
    }
    IEnumerator SaveCard(string str) // 카드 정보를 php 로 전송 
    {
      
        string url = "http://localhost/Aww/AwwCardDeckSave.php";
       WWWForm form = new WWWForm();
         form.AddField("CARD",str);
        
        WWW www = new WWW(url,form);
        yield return www;
        if (null == www.error)
        {
            //Debug.Log("not error");
            Debug.Log(www.text);
        }
        else
            Debug.Log(www.error);
    }
    IEnumerator LoadCard() // 카드 정보를 php로부터  받아옴 
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
                //Debug.Log("NAME: " + node.SelectSingleNode("NAME").InnerText);

               /* Debug.Log("ATT: " + node.SelectSingleNode("ATT").InnerText);
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