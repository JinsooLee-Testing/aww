using UnityEngine;
using System.Collections;

public class SummonCard : CardBase {
    CardBase card;
    public GameObject GO_image;
    ACT act = ACT.IDLE;
    void Start () {
        //transform.position = pos;
        card = ((GameObject)Instantiate(GO_image)).GetComponent<CardBase>();
         
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseDown()
    {

    }
}
