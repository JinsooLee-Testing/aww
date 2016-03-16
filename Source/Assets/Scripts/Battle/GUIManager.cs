using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
    private static GUIManager inst = null;
    private PlayerManager pm = null;

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
}
