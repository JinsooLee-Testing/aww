using UnityEngine;
using System.Collections;

public class TextDialog : MonoBehaviour {
    public int currentTextNumber = 0;
    private static TextDialog inst = null;
    public bool talk_mode=false;
    // Use this for initialization
    public static TextDialog GetInst()
    {
        return inst;
    }
    void Awake()
    {
        inst = this;
    }
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseDown()
    {
        if (talk_mode == true)
        {
            Debug.Log("text");
            currentTextNumber++;
        }
    }
}
