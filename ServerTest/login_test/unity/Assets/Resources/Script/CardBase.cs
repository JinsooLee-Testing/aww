using UnityEngine;
using System.Collections;

public class CardBase : MonoBehaviour {


    string mSkillName;
    CardBase inst = null;

    public CardBase GetInst()
    {
       if (!inst)
            Debug.Log("inst is not getting");
        return inst;
    }
	// Use this for initialization
	void Start () {
        inst = this;
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void SetCardName(string skilName)
    {
        this.mSkillName = mSkillName;
    }
}
