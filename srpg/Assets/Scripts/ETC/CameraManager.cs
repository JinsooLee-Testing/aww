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

   
    
    public static CameraManager GetInst()
    {
        return inst;
    }
    void Awake()
    {
        inst = this;
        transform.position = pos;
        InitPos = pos;
        xangle = Default_Iso_x;
        yangle = Default_Iso_y;
        Vector3 r = transform.rotation.eulerAngles;
        r.x = xangle;
        r.y = yangle;
        InitRot = r;
        transform.rotation = Quaternion.Euler(r);
    }
    public void SetPosition(Vector3 m_pos)
    {
        transform.position = m_pos;
    }
    public void SetAngle(Vector3 m_rot)
    {
        transform.rotation = Quaternion.Euler(m_rot);
    }
    public void ResetCameraTarget()
    {
        //transform.position = InitPos;
        transform.rotation = Quaternion.Euler(InitRot);
    }
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
