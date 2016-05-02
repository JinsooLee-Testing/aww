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

        if (type == "fire")
        {
            fireball fireb = ((GameObject)Instantiate(magics[0])).GetComponent<fireball>();
            fireb.targetHex = v;
            Vector3 v2 = v.transform.position;
            v2 = new Vector3(v2.x, 2, v2.z);
            Vector3 Start = start.transform.position;
            Start.y = y;
            Start.x -= 1;
            fireb.target = v2;
            fireb.transform.position = Start;
            fireb.fire = true;
        }
        else if (type == "wall")
        {
          //  wall wal = ((GameObject)Instantiate(magics[2])).GetComponent<wall>();
        }
        else
        {
            fireball fireb = ((GameObject)Instantiate(magics[1])).GetComponent<fireball>();
            fireb.targetHex = v;
            Vector3 v2 = v.transform.position;
            v2 = new Vector3(v2.x, 0, v2.z);
            Vector3 Start = start.transform.position;
            Start.y = y;

            fireb.target = v2;
            fireb.transform.position = Start;
            fireb.fire = true;
        }
    }

    // Update is called once per frame

}
