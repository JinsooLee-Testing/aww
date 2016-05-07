using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EffectManager : MonoBehaviour {
    private static EffectManager inst = null;
    public float effect_time = 0;
    GameObject[] effects = new GameObject[20];
    // Use this for initialization
    GameObject curhex;
    public static EffectManager GetInst()
    {
        return inst;
    }
    void Awake()
    {
        inst = this;

        inst.effects[0]=((GameObject)Resources.Load("Prefabs/Effect/attack"));
        inst.effects[1] = ((GameObject)Resources.Load("Prefabs/Effect/damage"));
        inst.effects[2] = ((GameObject)Resources.Load("Prefabs/Effect/Boom"));
        inst.effects[3] = ((GameObject)Resources.Load("Prefabs/Effect/chant"));
        inst.effects[4]= ((GameObject)Resources.Load("Prefabs/Effect/water"));
        inst.effects[5]= ((GameObject)Resources.Load("Prefabs/Effect/frame"));
        inst.effects[6]= ((GameObject)Resources.Load("Prefabs/Effect/chantbig"));
        inst.effects[7]= ((GameObject)Resources.Load("Prefabs/Effect/earth"));
        inst.effects[8]= ((GameObject)Resources.Load("Prefabs/Effect/ring"));
        inst.effects[9]=( (GameObject)Resources.Load("Prefabs/Effect/chant_wait"));
        inst.effects[10] = ((GameObject)Resources.Load("Prefabs/Effect/cyclone"));
     
    }
	void Start () {
 
    }

	
	// Update is called once per frame
	void Update () {
      
        if (effect_time != 0)
        {
            effect_time += Time.deltaTime;
            if (effect_time >= 1.5f)
            {
                Vector3 v;
                v = curhex.transform.position;
                v.y = 2;
                GameObject go = (GameObject)Instantiate(inst.effects[0], v, curhex.transform.rotation);
                curhex = null;
                effect_time = 0;
            }
        }
    }
    public void ShowDamage(Hex hex,int damage)
    {
     
        Manager.GetInst().damgedhex = hex;
        Manager.GetInst().damged = damage;
       // Manager.GetInst().StartCoroutine("ShowDamage");

    }

    public void ShowEffect_water(GameObject hex, GameObject destroy,int id)
    {
        Vector3 v;
        v = hex.transform.position;
        v.y = 2;

        GameObject go = (GameObject)Instantiate(inst.effects[id], v, hex.transform.rotation);
        
        Destroy(destroy);

    }
    public void DestoryEffect(int id)
    {
        GameObject.Destroy(inst.effects[id].gameObject);
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
    public void ShowEffect_Summon(GameObject hex,int id,float y)
    {
        Vector3 v;
        v = hex.transform.position;
        v.y = y;
        GameObject go = (GameObject)Instantiate(inst.effects[id], v, inst.effects[id].transform.rotation);

    }

    public void ShowEffect(GameObject hex)
    {
        Vector3 v;
        v = hex.transform.position;
        v.y = 2;
        GameObject go = (GameObject)Instantiate(inst.effects[0], v,hex.transform.rotation);

    }
    public void Play(GameObject hex)
    {
        effect_time += Time.deltaTime;
        curhex = hex;
    }
  
}
