using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GUIManager : MonoBehaviour {
    private static GUIManager inst = null;
    private PlayerManager pm = null;
    public bool battle = false;
    public bool small = false;
    void Awake()
    {
        inst = this;
        pm = PlayerManager.GetInst();

    }
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
    }
    public static GUIManager GetInst()
    {
        return inst;
    }
    public void DrawGUI()
    {

        OnGUI();
    }
  void OnGUI()
    {
        pm = PlayerManager.GetInst();
        if (pm.Players != null)
        {
            if (pm.Players.Count > 0)
            {
                DrawCommand(pm.Players[pm.CurTurnIdx]);
            }
        }
    }
    public void DrawCommand(PlayerBase pb)
    {

        float cmdW = 300;
        int cmdCnt = 3;
        float btnW = 100;
        float btnH = 200;
        if (battle == true)
        { 
        GUILayout.BeginArea(new Rect(Screen.width - cmdW, Screen.height - cmdCnt * btnH, cmdW, cmdCnt * btnH), GUI.skin.window);
        Rect rect = new Rect(0, Screen.height / 2 + 100, btnW, btnH);
        if (GUILayout.Button("Move", GUILayout.Height(100)))
        {
            Debug.Log("Move");

            if (MapManager.GetInst().HilightMoveRange(pb.CurHex, pb.status.MoveRange))
            {
                pb.act = ACT.MOVEHILIGHT;
            }
        }
        if (GUILayout.Button("Attack", GUILayout.Height(100)))
        {
            Debug.Log("Attack");

            if (MapManager.GetInst().HilightAttackRange(pb.CurHex, pb.status.attackRange))
            {
                pb.act = ACT.ATTACKHIGHLIGHT;
            }
        }
        if (GUILayout.Button("Turn Over", GUILayout.Height(100)))
        {
            Debug.Log("Turn Over");
            PlayerManager.GetInst().TurnOver();

        }
        if (GUILayout.Button("Title", GUILayout.Height(100)))
        {
            SceneManager.LoadScene(0);
        }
        GUILayout.EndArea();
        }
    }
    /*
    public  void DrawCommand(PlayerBase pb)
    {
        float btnW = 100f;
        float btnH = 75f;
        
        Rect rect = new Rect(0, Screen.height / 2 + 100, btnW, btnH);
    
        if (GUI.Button(rect, "Move"))
        {
            Debug.Log("Move");

            if (MapManager.GetInst().HilightMoveRange(pb.CurHex, pb.status.MoveRange))
            {
                pb.act = ACT.MOVEHILIGHT;
            }
        }
        rect = new Rect(100, (Screen.height / 2 + 100), btnW, btnH);
        if (GUI.Button(rect, "Attack"))
        {
            Debug.Log("Attack");

            if (MapManager.GetInst().HilightAttackRange(pb.CurHex, pb.status.attackRange))
            {
                pb.act = ACT.ATTACKHIGHLIGHT;
            }
        }
        rect = new Rect(200, (Screen.height / 2 + 100), btnW, btnH);
        if (GUI.Button(rect, "Turn Over"))
        {
            Debug.Log("Turn Over");
            PlayerManager.GetInst().TurnOver();

        }
        
        
    }
    */
}
