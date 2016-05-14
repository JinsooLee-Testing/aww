using UnityEngine;
using System.Collections;

public class magic : MonoBehaviour
{


    private static magic inst = null;
    public Vector3 target;
    public Hex targetHex;

    public AIPlayer targetAI;
    public bool fired = false;
    public string type = "fire";
    public ACT act;
    public GameObject[] magics = new GameObject[10];
    public float ef_time = 0f;
    public int curmagic_id;
    public static magic GetInst()
    {
        return inst;
    }
    public Vector3 v;
    public bool fire = false;
    void Awake()
    {
        target = new Vector3(0, 1, 0);
        inst = this;
        inst.magics[0] = (GameObject)Resources.Load("magic/fireball");
        inst.magics[1] = (GameObject)Resources.Load("magic/water");
        inst.magics[2] = (GameObject)Resources.Load("magic/wall");
        inst.magics[3] = (GameObject)Resources.Load("magic/waterfall");
        inst.magics[4] = (GameObject)Resources.Load("magic/shield");
        act = ACT.IDLE;
    }
    void Start()
    {

        transform.position = v;
    }
    void Update()
    {
        if (type == "wind")
        {
            ef_time += Time.deltaTime;
            if (ef_time > 5)
            {
                MapManager.GetInst().ResetMapColor();
                ef_time = 0;
                type = "null";
            }
        }
        
     

    }
   public void SetTarget(Hex v,Hex start,int y)
    {
        act = ACT.IDLE;
        if (type == "fire")
        {
            act = ACT.IDLE;
            if (curmagic_id == 1)
            {
                fireball[] fireb = new fireball[8];
                for (int i = 0; i < 8; ++i)
                {

                    fireb[i] = ((GameObject)Instantiate(magics[0])).GetComponent<fireball>();
                    fireb[i].targetHex = v;
                    Vector3 v2 = v.transform.position;
                    v2 = new Vector3(v2.x, 2, v2.z);
                    Vector3 Start = start.transform.position;
                    if (i == 2)
                        Start.z -= 6;
                    if (i == 3)
                        Start.z += 6;
                    if (i == 4)
                        Start.z += 3;
                    if (i == 5)
                        Start.z -= 3;
                    if (i == 6)
                        Start.x += 3;
                    if (i == 7)
                        Start.x += 3;
                    if (i == 1)
                        Start.x -= 6;
                    if (i == 0)
                        Start.x += 6;
                    Start.y = 10;
                    fireb[i].target = v2;
                    fireb[i].transform.position = Start;

                    fireb[i].fire = true;
                }
            }
            else
            {
                fireball fireb = new fireball();
                fireb = ((GameObject)Instantiate(magics[0])).GetComponent<fireball>();
                fireb.targetHex = v;
                Vector3 v2 = v.transform.position;
                v2 = new Vector3(v2.x, 2, v2.z);
                Vector3 Start = start.transform.position;
                Start.y = 9;
                fireb.target = v2;
                fireb.transform.position = Start;

                fireb.fire = true;
            }
        }
        else if (type == "wall")
        {

            //  wall wal = ((GameObject)Instantiate(magics[2])).GetComponent<wall>();
        }
        else if(type == "wind")
        {
          
            EffectManager.GetInst().ShowEffect_Summon(v.gameObject, 10, 2);
            MapManager.GetInst().ResetMapColor();
            MapManager.GetInst().MarkAttackRange(v, 3);
            v.At_Marked = true;
         
      
            CameraManager.GetInst().ResetCameraTarget();
            CostManager.GetInst().CostDecrease(CostManager.GetInst().Curcostnum);
        }
        else if (type == "water")
        {
            if (curmagic_id == 2)
            {
                fireball[] fireb = new fireball[4];
                for (int i = 0; i < 4; ++i)
                {
                    fireb[i] = ((GameObject)Instantiate(magics[1])).GetComponent<fireball>();
                    fireb[i].targetHex = v;
                    Vector3 v2 = v.transform.position;
                    v2 = new Vector3(v2.x, 0, v2.z);
                    Vector3 Start = start.transform.position;
                    if (i == 2)
                        Start.z -= 8;
                    if (i == 3)
                        Start.z += 8;
                    if (i == 1)
                        Start.x -= 8;
                    if (i == 0)
                        Start.x += 8;
                    Start.y = y;

                    fireb[i].target = v2;
                    fireb[i].transform.position = Start;
                    fireb[i].fire = true;

                }
            }
            else
            {
                fireball fireb = new fireball();
    
                    fireb = ((GameObject)Instantiate(magics[3])).GetComponent<fireball>();
                    fireb.targetHex = v;
                    Vector3 v2 = v.transform.position;
                    v2 = new Vector3(v2.x, 0, v2.z);
                    Vector3 Start = start.transform.position;
                    Start.y = 10f;
                    fireb.target = v2;
                    fireb.transform.position = Start;
                    fireb.fire = true;          
            }
        }
        
    }

    // Update is called once per frame

}
