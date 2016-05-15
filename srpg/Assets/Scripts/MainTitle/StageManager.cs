using UnityEngine;
using System.Collections;

public class StageManager : MonoBehaviour {

    private static StageManager inst = null;
    public float cur_stage = 0;
    public string path = "stage";
    public static StageManager Getinst()
    {
        return inst;
    }
    void Awake()
    {
        inst = this;
        FIleManager.Getinst().LoadStageData(path);
    }
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
