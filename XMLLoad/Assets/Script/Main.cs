using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
    fileMagr test;
    GUIMgr gm;
    MapMgr mm;
	// Use this for initialization
	void Start () {
        gm = GUIMgr.GetInst();
        test = fileMagr.GetInst();

      
        //gm.DrawLeftLayout();

    }
	
	// Update is called once per frame
	void Update () {
        CheckMouseWheel();
        CheckArrow();
    }
    void CheckMouseWheel()
    {
        float wheel = Input.GetAxis("Mouse ScrollWheel");
        if(wheel!=0)
        {
            GetComponent<Camera>().orthographicSize += wheel*5f;

        }
    }
    void OnGUI()
    {
        gm.DrawLeftLayout();
    }
    void CheckArrow()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if(vertical==0&& horizontal==0)
        {
            return;
        }
        transform.position = new Vector3(transform.position.x + horizontal, transform.position.y, transform.position.z + vertical);
    }
}
