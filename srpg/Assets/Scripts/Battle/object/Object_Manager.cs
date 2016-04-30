using UnityEngine;
using System.Collections.Generic;

public class Object_Manager : MonoBehaviour {
    private static Object_Manager inst = null;
    public int index = 0;
    public List<magic> magices = new List<magic>();
    public GameObject[] Structures = new GameObject[10];
    public static Object_Manager GetInst()
    {
        return inst;
    }
    // Use this for initialization
    void Awake()
    {
        inst = this;
        inst.Structures[0] = (GameObject)Resources.Load("object/carrot");
        inst.Structures[1] = (GameObject)Resources.Load("object/carrot2");
        inst.Structures[2] = (GameObject)Resources.Load("object/carrotbasket");
        inst.Structures[3] = (GameObject)Resources.Load("object/tree");
        inst.Structures[4] = (GameObject)Resources.Load("object/bone");
        inst.Structures[5] = (GameObject)Resources.Load("object/bonebig");
    }

    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
