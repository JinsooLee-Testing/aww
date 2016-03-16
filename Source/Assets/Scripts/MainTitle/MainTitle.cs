using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class MainTitle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnGUI()
    {
        float btnW = 200f;
        float btnH = 50f;
        Rect rect = new Rect(Screen.width / 2 - btnW / 2, Screen.height / 2, btnW, btnH);
        if(GUI.Button(rect,"New Game"))
        {
             SceneManager.LoadScene(1);
        }
    }
}
