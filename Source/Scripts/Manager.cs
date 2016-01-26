using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
    MapManager mm;
    PlayerManager pm;
    void Awake()
    {
        mm = MapManager.GetInst();
        pm = PlayerManager.GetInst();
    }
	// Use this for initialization
	void Start () {
        mm = MapManager.GetInst();
        pm = PlayerManager.GetInst(); 
        mm.CreateMap();
        pm.GenPlayerTest();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
