using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GUIManager : MonoBehaviour {
    private static GUIManager inst = null;
    private PlayerManager pm = null;
    public string fontPath;
    public int next_scene;
    public int cur_scene;
    public GameObject ui;
    public GameObject talk;
    public GameObject click;
    public GameObject popup;
    GameObject result;
    public clickthis a = new clickthis();
    public clickthis popa = new clickthis();
    public talkbox talk_box=new talkbox();
    public uibox ui_box = new uibox();
    public bool battle = true;
    public bool small = false;
    public bool create = true;
    public bool tutorial=false;
    public bool talkmode = false;
    public bool talkscene = false;
    public string named = "denti";
    public int tidx = 2;
    Vector3 InitPos1;
    Vector3 InitPos2;
    void Awake()
    {
        inst = this;
        inst.result = (GameObject)Resources.Load("Prefabs/ui/result");
        if (talkscene == false)
            pm = PlayerManager.GetInst();
        //ui_box = ((GameObject)Instantiate(ui)).GetComponent<uibox>();
        if (talkscene == false)
        {
            if (CameraManager.GetInst().event_mode == false)
            {
                if (tutorial == true)
                    talk_box = ((GameObject)Instantiate(talk)).GetComponent<talkbox>();
                else
                    ui_box = ((GameObject)Instantiate(ui)).GetComponent<uibox>();
            }
        }
        else
        {
            talk_box = ((GameObject)Instantiate(talk)).GetComponent<talkbox>();
        }
       
    }
	// Use this for initialization
	void Start () {
        
    }
	public void CreateTalkBox()
    {
        
        talk_box = Instantiate(talk).GetComponent<talkbox>();
        talk_box.transform.position=(new Vector3(50, 50, 50));
        talkmode = true;


    }
    public void CreateResult()
    {
        result = ((GameObject)Instantiate(result)).GetComponent<GameObject>();
        FIleManager.Getinst().SaveStageData(next_scene);
        SoundManager.GetInst().PlayVictory();
    }
    public void DestoryTalkBox()
    {
        Destroy(talk_box.gameObject);
        talkmode = false;
    }
 
    public void CreateUI()
    {
        if (create == true)
        {
            GameObject.Destroy(talk_box.gameObject);
            ui_box = ((GameObject)Instantiate(ui)).GetComponent<uibox>();
            /*
            CreatePos(new Vector3(-39.67f, 74.27f, -1.71f));
            InitPos1 = new Vector3(-39.67f, 74.27f, -1.71f);
            CreatePop(new Vector3(-35.57f, 76.97f, 0.09f));
            InitPos2 = new Vector3(-35.57f, 76.97f, 0.09f);
            */
            //CreatePos(new Vector3(-39.67f, 74.27f, -1.71f));
            InitPos1 = new Vector3(-39.67f, 74.27f, -1.71f);
            CreatePop(new Vector3(-21.3f, 76.45f, 0.09f));
            InitPos2 = new Vector3(-21.3f, 76.45f, 0.09f);
            create = false;
        }
    }
    public void CreatePos(Vector3 pos)
    {

        a = ((GameObject)Instantiate(click)).GetComponent<clickthis>();
        a.transform.position = pos;

    }
    public void DestroyPop()
    {
        Destroy(popa.gameObject);
    }
    public void MovePos(int idx)
    {
       
            Vector3 pos = InitPos1;
             
            Vector3 pos2 = InitPos2;
            if (idx == 3)
            {
                pos.x += 3;
                pos2.x += 3;
            }
            if (idx == 4)
            {
                pos2.x += 6;
                pos.x += 6;
            }
            if (idx == 5)
            {
                pos2.x += 12;
                pos.x += 12;
            pos.y -= 2;
            pos2.y -= 2;
        }
        if (idx == 8)
        {
            pos2.x += 1200;
            pos.x += 1200;
        }
        //a.transform.position = pos;
        popa.transform.position = pos2;
      

    }
    public void CreatePop(Vector3 pos)
    {

        popa = ((GameObject)Instantiate(popup)).GetComponent<clickthis>();
        popa.transform.position = pos;
    }
  
    // Update is called once per frame
    void Update () {
    }
    public static GUIManager GetInst()
    {
        return inst;
    }
    public void DrawGUI(int id)
    {
        if (id == 1)
        {
            GameObject s = ((GameObject)Instantiate(ui)).GetComponent<GameObject>();
        }
        else
        {
            GameObject s = ((GameObject)Instantiate(talk)).GetComponent<GameObject>();
        }
        //OnGUI();
    }

    public void DrawCommand(PlayerBase pb)
    {


    }

}
