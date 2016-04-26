using UnityEngine;
using System.Collections;

public class EffectManager : MonoBehaviour {
    private static EffectManager inst = null;
    public GameObject GO_attackEffect;
	// Use this for initialization
    public static EffectManager GetInst()
    {
        return inst;
    }
    void Awake()
    {
        inst = this;
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowEffect(GameObject hex)
    {
        Vector3 v;
        v = hex.transform.position;
        v.y = 2;
        GameObject go = (GameObject)Instantiate(GO_attackEffect,v,hex.transform.rotation);

    }
}
