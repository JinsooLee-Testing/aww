using UnityEngine;
using System.Collections;

 public class Manager : MonoBehaviour
{ 
    private static Manager inst = null;
    MapManager mm;
    PlayerManager pm;
    GUIManager gm;
    FIleManager fm;
    public string MapPath;
    public GameObject GO_Damage;
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
        fm = FIleManager.Getinst();

    }
	// Use this for initialization
	void Start () {
        mm = MapManager.GetInst();
        pm = PlayerManager.GetInst();
        mm.CreateTestMap(MapPath);
       // mm.CreateMap();
        
        mm.LoadObjMap();
        pm.GenPlayerTest();
        SoundManager.GetInst().PlayMusic(transform.position);
       

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
        Vector3 pos =new Vector3(destX, destZ, destZ);
        Vector3 rot = new Vector3(70,45,0);
        
        CameraManager.GetInst().SetPosition(pos);
        CameraManager.GetInst().SetAngle(rot);
    }
    public Hex damgedhex;
    public int damged;
    IEnumerable ShowDamage()
    {
        Debug.Log("Ss");
        
        Vector3 v2 = damgedhex.transform.position;
        v2.y = 2.0f;
        GameObject Damage = (GameObject)GameObject.Instantiate(GO_Damage, v2, Manager.GetInst().gameObject.transform.rotation);
        TextMesh tm = Damage.GetComponent<TextMesh>();
        tm.text = "" + damged;
        tm.color = Color.red;

        //yield return new WaitForSeconds(0.5f);
        /*
        for(float i=1;i>=0;i-=0.05f)
         {
             tm.color = new Vector4(255, 0, 0, i);
             yield return new WaitForFixedUpdate();
          }
        //Destroy(Damage);
        */
        return null;
     
    }
}
