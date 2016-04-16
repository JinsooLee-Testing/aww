using UnityEngine;
using System.Collections;

public class CardLoad : MonoBehaviour {
    public Point MapPos;
    // Use this for initialization
    void Start () {
	
	}
    public void SetMapPos(int x, float y)
    {
        MapPos = new Point(x, y,0);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
