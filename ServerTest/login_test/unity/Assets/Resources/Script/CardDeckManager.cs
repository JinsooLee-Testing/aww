using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class CardDeckManager : MonoBehaviour {

    private static CardDeckManager inst = null;
    List<string> mInsertCardName;
	// Use this for initialization
	void Awake () {
        mInsertCardName = new List<string>();
        inst = this;
    }

    public static CardDeckManager GetInst()
    {
        return inst;
    }
	// Update is called once per frame
	void Update () {
	
	}

    public void InsertCardDeck(string mName)
    {    
        mInsertCardName.Add(mName);
       // Debug.Log("------ card list-------");
       /*foreach (string key in mInsertCardName)
        { 
            
            Debug.Log(key);
           
        }*/
       // Debug.Log("-----------------------");
    }
    /*public void PullCardDeck(string mName)
    {
        mInsertCardName.Remove(mName);
        Debug.Log("------ card list-------");
        foreach (string key in mInsertCardName)
        {

            Debug.Log(key);

        }
        Debug.Log("-----------------------");
    }*/

    void OnGUI()
    {
        //mInsertCardName.Count;
        for(var i = 0; i<mInsertCardName.Count; ++i)
        {
            if (GUI.Button(new Rect((Screen.width / 16) * i, Screen.height - 50, (Screen.width / 16) + (i + 1), Screen.height / 16), mInsertCardName[i]))
                mInsertCardName.Remove(mInsertCardName[i]);
                //Debug.Log(mInsertCardName[i]);
        }
        /*foreach (string key in mInsertCardName)
        {
            if (GUI.Button(new Rect((Screen.width / 16) * mInsertCardName.Count, Screen.height - 50, (Screen.width / 16) + (mInsertCardName.Count + 1), Screen.height / 16), key))
                mInsertCardName.Remove(key);
        }*/
    }


}
