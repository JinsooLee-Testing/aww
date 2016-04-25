using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ui : MonoBehaviour
{

    private PlayerManager pm = null;
    public string act = "IDLE";
    // Use this for initialization
    void Start()
    {

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
            if (CostManager.GetInst().cur_cost_num >= 1)
            {
                PlayerBase pb = pm.Players[pm.CurTurnIdx];
                if (MapManager.GetInst().HilightMoveRange(pb.CurHex, pb.status.MoveRange))
                {
                    pm.Players[pm.CurTurnIdx].act = ACT.MOVEHILIGHT;
                }
                CostManager.GetInst().CostDecrease(1);
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
        if (act == "turnover")
        {
            PlayerManager.GetInst().TurnOver();
        }
        if (act == "exit")
        {
            Debug.Log("Attack");
            MapManager.GetInst().num = 0;
            SceneManager.LoadScene(0);
        }
    }
}
