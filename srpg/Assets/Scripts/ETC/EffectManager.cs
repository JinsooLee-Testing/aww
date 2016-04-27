using UnityEngine;
using System.Collections;

public class EffectManager : MonoBehaviour {
    private static EffectManager inst = null;
    public GameObject GO_attackEffect;
    public GameObject GO_Damage;
    public GameObject GO_boom;
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
    public void ShowDamage(Hex hex,int damage)
    {
     
        Manager.GetInst().damgedhex = hex;
        Manager.GetInst().damged = damage;
       // Manager.GetInst().StartCoroutine("ShowDamage");

    }
    public void ShowEffect_Fire(GameObject hex,GameObject destroy)
    {
        Vector3 v;
        v = hex.transform.position;
        v.y = 2;
        GameObject go = (GameObject)Instantiate(GO_boom, v, hex.transform.rotation);
        Destroy(destroy);

    }
    public void ShowEffect(GameObject hex)
    {
        Vector3 v;
        v = hex.transform.position;
        v.y = 2;
        GameObject go = (GameObject)Instantiate(GO_attackEffect,v,hex.transform.rotation);

    }
  
}
