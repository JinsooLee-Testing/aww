using UnityEngine;
using System.Collections;

public class npc : PlayerBase {

    // Use this for initialization
    public int x, y, z;
    Hex hex;
    void Awake()
    {
  
    }
    void Start () {
        
   
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseDown()
    {
        GUIManager.GetInst().CreateTalkBox();
    }
}
