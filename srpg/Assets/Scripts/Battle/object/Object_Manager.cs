using UnityEngine;
using System.Collections.Generic;

public class Object_Manager : MonoBehaviour {
    private static Object_Manager inst = null;
    public GameObject GO_mis;
    public int index = 0;
    public List<magic> magices = new List<magic>();
    public static Object_Manager GetInst()
    {
        return inst;
    }
    // Use this for initialization
    void Awake()
    {
        inst = this;
    }
    public void GenObject(int x, int z)
    {
        magic userplayer = ((GameObject)Instantiate(GO_mis)).GetComponent<magic>();
        magices[index].transform.position = new Vector3(x+1, 1, z+1);
        magices.Add(userplayer);
        index++;
    }
    void Start () {
        magic mis = ((GameObject)Instantiate(GO_mis)).GetComponent<magic>();
        GenObject(1, 1);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
