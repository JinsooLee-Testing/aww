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
      
            if (CostManager.GetInst().cur_cost_num >= 1)
                {
               
                    PlayerBase pb = pm.Players[pm.CurTurnIdx];
                pb.CurHex.Passable = true;
                SoundManager.GetInst().PlayClickSound();
                if (pb.m_type != Type.MONSTER)
                {

                    if (MapManager.GetInst().HilightMoveRange(pb.CurHex, pb.status.MoveRange))
                    {
                        pm.Players[pm.CurTurnIdx].act = ACT.MOVEHILIGHT;
                    }
                    CostManager.GetInst().CostDecrease(1);
                }
            }
        

        }
        if (act == "attack")
        {
            MapManager.GetInst().ResetMapColor();
            SoundManager.GetInst().PlayClickSound();
            Debug.Log("Attack");
            PlayerBase pb = pm.Players[pm.CurTurnIdx];
            pb.CurHex.Passable = true;
            if (pb.m_type != Type.MONSTER)
            {
                if (MapManager.GetInst().HilightAttackRange(pb.CurHex, pb.status.attackRange))
                {
                    pm.Players[pm.CurTurnIdx].act = ACT.ATTACKHIGHLIGHT;
                }
            }
        }
        if (act == "turnover")
        {
            SoundManager.GetInst().PlayClickSound();
            PlayerManager.GetInst().TurnOver();
        }
        if (act == "exit")
        {
            SoundManager.GetInst().PlayClickSound();
            Debug.Log("Attack");
            MapManager.GetInst().num = 0;
            SceneManager.LoadScene(0);
        }
    }
}
