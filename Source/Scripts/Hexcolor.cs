using UnityEngine;
using System.Collections;

public class Hexcolor : Hex {
 
	void Start () {

        
	}
	// Update is called once per frame
	void Update () {
        if(onto==1)
            GetComponent<Renderer>().material.color = Color.green;
            
    }
	
}
