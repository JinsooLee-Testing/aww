using UnityEngine;
using System.Collections;

public class BackScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width-200, 30, 150, (Screen.height)/10), "뒤로가기"))
            Application.LoadLevel("main_scene");

    }
}
