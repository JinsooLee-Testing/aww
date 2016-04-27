using UnityEngine;
using System.Collections;

public class magic : MonoBehaviour
{


    private static magic inst = null;
    public Vector3 target;
    public Hex targetHex;
    public GameObject GO_mis;
    public AIPlayer targetAI;
    public bool fired = false;
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
    }
    void Start()
    {

        transform.position = v;
    }
    void Update()
    {

    }
   public void SetTarget(Hex v,Hex start)
    {
        fireball fireb = ((GameObject)Instantiate(GO_mis)).GetComponent<fireball>();
        fireb.targetHex = v;
        Vector3 v2 = v.transform.position;
        v2 = new Vector3(v2.x, 2, v2.z);
        Vector3 Start = start.transform.position;
        Start.y = 3f;
        fireb.target = v2;
        fireb.transform.position = Start;
        fireb.fire = true;
    }
    // Update is called once per frame

}
