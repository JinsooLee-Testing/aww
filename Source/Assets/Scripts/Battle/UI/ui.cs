using UnityEngine;
using System.Collections;

public class ui : MonoBehaviour {

    GUIManager gm;
    // Use this for initialization
    void Start () {

        gm = GUIManager.GetInst();
    }
    void Update () {
       // CheckMouseButtonDown();

    }
    void OnGUI()
    {
        float btnW = 200f;
        float btnH = 200f;
        GUI.color = Color.white;
        Rect rect = new Rect(10, (Screen.height / 2 +370), btnW, btnH);
        
        if (GUI.Button(rect, ""))
        {
            

        }
    }
}
