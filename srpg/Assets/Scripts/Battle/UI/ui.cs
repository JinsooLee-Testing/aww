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
                MapManager.GetInst().ResetMapColor();
                PlayerBase pb = pm.Players[pm.CurTurnIdx];
                if (pb.act != ACT.MOVING)
                {
                    Manager.GetInst().MoveCamPosToTile(pb.CurHex);
                    PlayerManager.GetInst().select_object = pb;
                    pb.CurHex.Passable = true;
                    SoundManager.GetInst().PlayClickSound();
                    if (pb.m_type != Type.MONSTER)
                    {

                        if (MapManager.GetInst().HilightMoveRange(pb.CurHex, pb.status.MoveRange))
                        {
                            pm.Players[pm.CurTurnIdx].act = ACT.MOVEHILIGHT;
                        }

                    }
                }
            }


        }
        if (act == "attack")
        {
            MapManager.GetInst().ResetMapColor();

            SoundManager.GetInst().PlayClickSound();
            Debug.Log("Attack");
            PlayerBase pb = pm.Players[pm.CurTurnIdx];
            if (CostManager.GetInst().cur_cost_num >= 1)
            {
                if (pb.act != ACT.MOVING)
                {
                    Manager.GetInst().MoveCamPosToTile(pb.CurHex);
                    PlayerManager.GetInst().select_object = pb;
                    pb.CurHex.Passable = true;
                    if (pb.m_type != Type.MONSTER)
                    {
                        if (MapManager.GetInst().HilightAttackRange(pb.CurHex, pb.status.attackRange))
                        {
                            pm.Players[pm.CurTurnIdx].act = ACT.ATTACKHIGHLIGHT;
                        }
                    }
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
