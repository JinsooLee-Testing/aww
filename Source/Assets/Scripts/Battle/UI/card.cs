using UnityEngine;
using System.Collections;

public class card : MonoBehaviour {
    GUIManager gm;
    public GameObject GO_Card;
    // Use this for initialization
    void Start () {
  gm = GUIManager.GetInst();
    }
	
	// Update is called once per frame
	void Update () {
        if (gm.small == false)
        {
            Vector3 v;
            v.x = 1;
            v.y = 1;
            v.z = 1;
            transform.localScale = v;

        }
        else
        {

            Vector3 v;
            v.x = 0.7f;
            v.y = 0.7f;
            v.z = 0.7f;
            //v.z = 0;
            transform.localScale = v;

        }
    }
    void OnGUI()
    {
        float btnW = 200f;
        float btnH = 200f;
        GUI.color = Color.white;
        Rect rect = new Rect(780, (Screen.height / 2 + 500), btnW, btnH);

        if (GUI.Button(rect, ""))
        {
            if (gm.small == false)
            {
                gm.small = true;
            }
            else
            {
                gm.small = false;

            }

        }
    }
}
