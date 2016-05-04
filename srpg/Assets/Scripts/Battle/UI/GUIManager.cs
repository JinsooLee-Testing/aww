using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GUIManager : MonoBehaviour {
    private static GUIManager inst = null;
    private PlayerManager pm = null;
    public GameObject ui;
    public GameObject talk;
    public GameObject click;
    public GameObject popup;
    public clickthis a = new clickthis();
    public clickthis popa = new clickthis();
    public talkbox talk_box=new talkbox();
    public uibox ui_box = new uibox();
    public bool battle = true;
    public bool small = false;
    public bool create = true;
    public bool tutorial=false;
    public int tidx = 2;
    Vector3 InitPos1;
    Vector3 InitPos2;
    void Awake()
    {
        inst = this;
     
        pm = PlayerManager.GetInst();
        if(tutorial==true)
        talk_box = ((GameObject)Instantiate(talk)).GetComponent<talkbox>();
        else       
         ui_box = ((GameObject)Instantiate(ui)).GetComponent<uibox>();
    }
	// Use this for initialization
	void Start () {

    }
	public void CreateTalkBox()
    {
       
       
    }
    public void CreateUI()
    {
        if (create == true)
        {
            GameObject.Destroy(talk_box.gameObject);
            ui_box = ((GameObject)Instantiate(ui)).GetComponent<uibox>();
            CreatePos(new Vector3(-39.67f, 74.27f, -1.71f));
            InitPos1 = new Vector3(-39.67f, 74.27f, -1.71f);
            CreatePop(new Vector3(-35.57f, 76.97f, 0.09f));
            InitPos2 = new Vector3(-35.57f, 76.97f, 0.09f);
            create = false;
        }
    }
    public void CreatePos(Vector3 pos)
    {

        a = ((GameObject)Instantiate(click)).GetComponent<clickthis>();
        a.transform.position = pos;

    }
    public void MovePos(int idx)
    {
       
            Vector3 pos = InitPos1;
           
          
                
            Vector3 pos2 = InitPos2;
            if (idx == 3)
            {
                pos.x += 2;
                pos2.x += 2;
            }
            if (idx == 4)
            {
                pos2.x += 5;
                pos.x += 5;
            }
            if (idx == 5)
            {
                pos2.x += 12;
                pos.x += 12;
            }
            a.transform.position = pos;
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
