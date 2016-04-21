using UnityEngine;
using System.Collections;

public class TextDialog : MonoBehaviour {
    public int currentTextNumber = 0;
    private static TextDialog inst = null;
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
        Debug.Log("text");
        currentTextNumber++;
    }
}
