using UnityEngine;
using System.Collections;

public class ui_setting : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseDown()
    {
        GUIManager.GetInst().CreateResult();
    }
}
