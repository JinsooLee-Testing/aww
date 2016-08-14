using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class CardDeckManager : MonoBehaviour {

    private static CardDeckManager inst = null;
    public List<string> mInsertCardName;
    private byte mMaximumcount  = 0;
    byte mCount;
	// Use this for initialization
	void Awake () {
        mMaximumcount = 4;
        mInsertCardName = new List<string>();
        inst = this;
        mCount = 0;
    }

    public static CardDeckManager GetInst()
    {
        return inst;
    }
	// Update is called once per frame
	void Update () {
	
	}

    public void InsertCardDeck(string mName) // 리스트에 넣는다. 
    {
        ++mCount;
        //Debug.Log("Count++ :" + mCount);
        if (mCount <= mMaximumcount)
        {
            mInsertCardName.Add(mName);
            //mInsertCardName.Add(mName);
        }
        else
        {
            //Debug.Log("Maximumcount");
            mCount = mMaximumcount;
        }
    }

    void SetJson(List<string> str)
    {
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
    public string SaveToString()
    {
        //Debug.Log(JsonUtility.ToJson(this));
        return JsonUtility.ToJson(this);
    }
    void OnGUI()
    {
        for(var i = 0; i< mInsertCardName.Count; ++i) // 카드 제거 
        {
            if (GUI.Button(new Rect(((Screen.width / 4) * i), Screen.height - 50, (Screen.width / 4) + (i + 1), Screen.height / 10), mInsertCardName[i]))
            {
                mInsertCardName.Remove(mInsertCardName[i]);
                --mCount;
                //Debug.Log("Count-- :" + mCount);
                if (0>mCount) mCount = 0;
            }
                //Debug.Log(mInsertCardName[i]);
        }

        if (GUI.Button(new Rect(0, 0, 60, 60), "덱 1")) // 덱에 세이브 
        {
            if (mMaximumcount > mInsertCardName.Count)
            {

                Debug.Log("카드에 덱을 채워 넣으세요");
            }
            else
            {
                // SetJson(mInsertCardName);
                //Debug.Log(SaveToString());
                ObjectManager.GetInst().startSaveCoroutine(SaveToString());
                //Debug.Log("덱 1에 세이브 완료 ");
            }

        }
           
        /*foreach (string key in mInsertCardName)
        {
            if (GUI.Button(new Rect((Screen.width / 16) * mInsertCardName.Count, Screen.height - 50, (Screen.width / 16) + (mInsertCardName.Count + 1), Screen.height / 16), key))
                mInsertCardName.Remove(key);
        }*/
    }


}
