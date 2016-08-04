using UnityEngine;
using System.Collections;

public class CardData : MonoBehaviour {
    string mSkillname;
    int mCost;
    string mType;
    int mDamage;
    int mRangeView;
    int mRangeOfFact;

    private CardData _instance = null;

    public CardData GetInstance()
    {
        if(!_instance)
        {
            Debug.Log("instance = null");

        }
        return _instance;
    }
    // Use this for initialization
    void Awake()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void  setCardObject(string skillName)
    {
        this.mSkillname = skillName;
    }

}

