using UnityEngine;
using System.Collections;

public class SelectManager : MonoBehaviour {
    RaycastHit mHit;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MousePicking();
    }

    void MousePicking()
    {
        
        if (Input.GetMouseButtonDown(0)) //마우스를 눌르면
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //현재 마우스클릭한 위치

            if (Physics.Raycast(ray, out mHit, 100)) // 피킹이 되면 mHit에 피킹된 오브젝트정보가 달려온다.
            {
                    CardActivate(mHit.transform.name);
            }
        }

    }

    bool CardActivate(string mCardName)
    {
        mCardName =  mCardName.Replace("(Clone)", "");
            CardDeckManager.GetInst().InsertCardDeck(mCardName);
            return true;


        /*if (0 == mCount)
        {
            
            //Debug.Log(mCDM);
            //CardDeckManager.GetInst().InsertCardDeck(mCardName);
            //mCDM.InsertCardDeck(mCardName);
            //Debug.Log(mCardName + " insert Deck");
            return true;
        }
       // mCount -= 1;
        //CardDeckManager.GetInst().PullCardDeck(mCardName);
        //Debug.Log(mCardName + " out Deck");
        return false;*/
    }



}
