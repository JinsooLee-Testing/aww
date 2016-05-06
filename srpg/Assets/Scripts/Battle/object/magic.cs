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
        act = ACT.IDLE;
    }
    void Start()
    {

        transform.position = v;
    }
    void Update()
    {

    }
   public void SetTarget(Hex v,Hex start,int y)
    {
        act = ACT.IDLE;
        if (type == "fire")
        {
            act = ACT.IDLE;
            fireball[] fireb = new fireball[10];
            for (int i = 0; i < 10; ++i)
            {
                
                fireb[i] = ((GameObject)Instantiate(magics[0])).GetComponent<fireball>();
                fireb[i].targetHex = v;
                Vector3 v2 = v.transform.position;
                v2 = new Vector3(v2.x, 2, v2.z);
                Vector3 Start = start.transform.position;
                Start.x = Random.Range(0, 15);
                Start.z = Random.Range(0, 15);
                Start.y = 13;
                fireb[i].target = v2;
                fireb[i].transform.position = Start;
               
                fireb[i].fire = true;
            }
        }
        else if (type == "wall")
        {

            //  wall wal = ((GameObject)Instantiate(magics[2])).GetComponent<wall>();
        }
        else
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
    }

    // Update is called once per frame

}
