using UnityEngine;
using System.Collections.Generic;

public class Object_Manager : MonoBehaviour {
    private static Object_Manager inst = null;
    public int index = 0;
    public List<magic> magices = new List<magic>();
    GameObject[] Structures = new GameObject[50];
    public static Object_Manager GetInst()
    {
        return inst;
    }
    // Use this for initialization
    void Awake()
    {
        inst = this;
        /* 
     inst.Structures[0] = (GameObject)Resources.Load("object/carrot");
     inst.Structures[1] = (GameObject)Resources.Load("object/carrot2");
     inst.Structures[2] = (GameObject)Resources.Load("object/carrotbasket");
     inst.Structures[3] = (GameObject)Resources.Load("object/tree");
     inst.Structures[4] = (GameObject)Resources.Load("object/bone");
     inst.Structures[5] = (GameObject)Resources.Load("object/bonebig");
     inst.Structures[6] = (GameObject)Resources.Load("object/wall");
            */
        inst.Structures[0] = (GameObject)Resources.Load("object/chapter2_wall");
        inst.Structures[1] = (GameObject)Resources.Load("object/chapter2_carrotbox");
        inst.Structures[2] = (GameObject)Resources.Load("object/chapter2_carrotfield");
        inst.Structures[3] = (GameObject)Resources.Load("object/chapter2_carrotshelf");
        inst.Structures[4] = (GameObject)Resources.Load("object/chapter2_helmet");
        inst.Structures[5] = (GameObject)Resources.Load("object/chapter2_philar");
        inst.Structures[6] = (GameObject)Resources.Load("object/chapter2_pick2(big)");
        inst.Structures[7] = (GameObject)Resources.Load("object/chapter2_soildum");
        inst.Structures[8] = (GameObject)Resources.Load("object/chapter2_dumbull");
        inst.Structures[9] = (GameObject)Resources.Load("object/chapter2_torchlight");
        inst.Structures[10] = (GameObject)Resources.Load("object/chapter2_wall");
        inst.Structures[11] = (GameObject)Resources.Load("object/chapter2_watchtower");
        inst.Structures[12] = (GameObject)Resources.Load("object/firefeather");
        inst.Structures[13] = (GameObject)Resources.Load("object/tree");
    }
    public GameObject FindObj(int id)
    {
        return Structures[id];
    }
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
