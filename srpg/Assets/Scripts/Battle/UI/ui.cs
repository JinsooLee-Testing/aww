using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ui : MonoBehaviour
{

    private PlayerManager pm = null;
    GUIManager gm;
    public string act = "IDLE";
    // Use this for initialization
    void Start()
    {

        gm = GUIManager.GetInst();
        pm = PlayerManager.GetInst();
    }
    void Update()
    {
        // CheckMouseButtonDown();

    }
    void OnMouseDown()
    {
        if (act == "move")
        {
            Debug.Log("move");
            PlayerBase pb = pm.Players[pm.CurTurnIdx];
            if (MapManager.GetInst().HilightMoveRange(pb.CurHex, pb.status.MoveRange))
            {
                pm.Players[pm.CurTurnIdx].act = ACT.MOVEHILIGHT;
            }

        }
        if (act == "attack")
        {
            Debug.Log("Attack");
            PlayerBase pb = pm.Players[pm.CurTurnIdx];
            if (MapManager.GetInst().HilightAttackRange(pb.CurHex, pb.status.attackRange))
            {
                pm.Players[pm.CurTurnIdx].act = ACT.ATTACKHIGHLIGHT;
            }
        }
        if (act == "exit")
        {
            Debug.Log("Attack");
            SceneManager.LoadScene(0);
        }
    }
}
