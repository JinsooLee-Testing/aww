using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    private static CameraManager inst = null;
    public Vector3 InitPos;
    private Vector3 InitRot;
    private float Default_Iso_x=30f;
    private float Default_Iso_y = 45;
    public Vector3 pos;
    public float xangle;
    public float yangle;
    ACT act;

    public bool event_mode = false;
    
    public static CameraManager GetInst()
    {
        return inst;
    }
    void Awake()
    {
        inst = this;
        transform.position = pos;
        InitPos = pos;
        Default_Iso_x= xangle;
        Default_Iso_y = yangle;
        Vector3 r = transform.rotation.eulerAngles;
        r.x = xangle;
        r.y = yangle;
        act = ACT.IDLE;
        InitRot = r;
        transform.rotation = Quaternion.Euler(r);
    }
    public void SetPosition(Vector3 m_pos)
    {
        if (event_mode == false)
        {
            if (MapManager.GetInst().MapSizeX > 10)
                m_pos.y = 4.0f;
        }
        transform.position = m_pos;
    }
    public void SetAngle(Vector3 m_rot)
    {
        transform.rotation = Quaternion.Euler(m_rot);
    }
    public void ResetCameraTarget()
    {

        transform.rotation = Quaternion.Euler(InitRot);
    }
    void Start () {
        if (GUIManager.GetInst().talk_box != null && event_mode == true) 
            GUIManager.GetInst().DestoryTalkBox();

    }
	
	// Update is called once per frame
	void Update () {
        if(event_mode==true &&act==ACT.IDLE)
        {
            Vector3 v = transform.position;
            v.z += Time.deltaTime*2;
            v.y += Time.deltaTime;
            Vector3 r = transform.rotation.eulerAngles;
            r.x += Time.deltaTime * 5;
            transform.rotation = Quaternion.Euler(r);

            transform.position = v;
            if (v.z > 14)
                act = ACT.MOVING;
        }
        if(act==ACT.MOVING)
        {
            SetPosition(new Vector3(-0.61f, 4.35f, 7.72f));
            Vector3 r = transform.rotation.eulerAngles;
            r.x = 20;
            r.y = 165.2f;
            InitRot = r;
            transform.rotation = Quaternion.Euler(r);

            GUIManager.GetInst().CreateTalkBox();
            act = ACT.SUMMONES;
        }

    }
}
