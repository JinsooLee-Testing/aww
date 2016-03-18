using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
    private static Manager inst = null;
    MapManager mm;
    PlayerManager pm;
    GUIManager gm;

    public static Manager GetInst()
    {
        return inst;
    }
    void Awake()
    {
        inst = this;
        mm = MapManager.GetInst();
        pm = PlayerManager.GetInst();
        gm = GUIManager.GetInst();

    }
	// Use this for initialization
	void Start () {
        mm = MapManager.GetInst();
        pm = PlayerManager.GetInst();

        mm.CreateMap();
        pm.GenPlayerTest();
	}
	
	// Update is called once per frame
	void Update () {
        //gm.DrawGUI();
        CheckMouseZoom();
        CheckMouseButtonDown();
	}
    void CheckMouseZoom()
    {
        // 마으스 최저 5 최대 25
        float mouse =Input.GetAxis("Mouse ScrollWheel");
        float mouseY = GetComponent<Camera>().transform.position.y + mouse * 5f;
        if (mouseY < 5)
        {
            mouseY = 5;
        }
        else if (mouseY > 25)
        {
            mouseY = 25;
        }
        Vector3 newPos = new Vector3(GetComponent<Camera>().transform.position.x, mouseY, GetComponent<Camera>().transform.position.z);
        GetComponent<Camera>().transform.position = newPos;
    }
    void CheckMouseButtonDown()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Debug.Log("Mouse1Down");
            pm.MouseInputProc(1);

        }
        
    }
    public void MoveCamPosToTile(Hex hex)
    {
        float destX = hex.transform.position.x;
        float destZ = hex.transform.position.z;

     //   GetComponent<Camera>().transform.position = new Vector3(destX, GetComponent<Camera>().transform.position.y, destZ);
    }
}
