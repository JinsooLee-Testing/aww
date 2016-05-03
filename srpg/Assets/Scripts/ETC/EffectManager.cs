using UnityEngine;
using System.Collections;

public class EffectManager : MonoBehaviour {
    private static EffectManager inst = null;

    public GameObject[] effects = new GameObject[10];
    // Use this for initialization
    public static EffectManager GetInst()
    {
        return inst;
    }
    void Awake()
    {
        inst = this;
        inst.effects[0] = (GameObject)Resources.Load("Prefabs/Effect/attack");
        inst.effects[1] = (GameObject)Resources.Load("Prefabs/Effect/damage");
        inst.effects[2] = (GameObject)Resources.Load("Prefabs/Effect/Boom");
        inst.effects[3] = (GameObject)Resources.Load("Prefabs/Effect/chant");
        inst.effects[4] = (GameObject)Resources.Load("Prefabs/Effect/water");
        inst.effects[5] = (GameObject)Resources.Load("Prefabs/Effect/frame");

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
    public void ShowEffect_water(GameObject hex, GameObject destroy)
    {
        Vector3 v;
        v = hex.transform.position;
        v.y = 2;

        GameObject go = (GameObject)Instantiate(inst.effects[4], v, hex.transform.rotation);

        Destroy(destroy);

    }
    public void ShowEffect(Transform pos,int magic_id)
    {
        if (magic_id == 1)
        {
            GameObject go = (GameObject)Instantiate(inst.effects[5], pos.position, pos.rotation);
        }
        else
        {
            GameObject go = (GameObject)Instantiate(inst.effects[4], pos.position, pos.rotation);
        }

    }
    public void ShowEffect_Fire(GameObject hex,GameObject destroy)
    {
        Vector3 v;
        v = hex.transform.position;
        v.y = 2;

        GameObject go = (GameObject)Instantiate(inst.effects[2], v, hex.transform.rotation);
        
        Destroy(destroy);

    }
    public void ShowEffect_Summon(GameObject hex)
    {
        Vector3 v;
        v = hex.transform.position;
        v.y = 1.2f;
        GameObject go = (GameObject)Instantiate(inst.effects[3], v, hex.transform.rotation);

    }
    public void ShowEffect(GameObject hex)
    {
        Vector3 v;
        v = hex.transform.position;
        v.y = 2;
        GameObject go = (GameObject)Instantiate(inst.effects[0], v,hex.transform.rotation);

    }
  
}
