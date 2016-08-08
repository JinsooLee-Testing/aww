using UnityEngine;
using System.Collections;

public class SummonCard : CardUseBase {
    ACT act;
    public int summon_id;
    void Start () {
        //transform.position = pos;
        card_id = summon_id;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseDown()
    {
        if (InGame == true && PlayerManager.GetInst().Players[PlayerManager.GetInst().CurTurnIdx].act != ACT.STUN)
        {
            if (CostManager.GetInst().cur_cost_num >= cost)
            {
                if (On_active == true)
                {
                    PlayerBase pb = PlayerManager.GetInst().Players[PlayerManager.GetInst().CurTurnIdx];
                    Manager.GetInst().MoveCamPosToTile(pb.CurHex);
                    PlayerManager.GetInst().HilightSummons();

                    act = ACT.SUMMONES;
                    CostManager.GetInst().CostDecrease(cost);
                    On_active = false;
                }
            }
        }
        else
        {
            if (PlayerManager.GetInst().Players[PlayerManager.GetInst().CurTurnIdx].act != ACT.STUN)
            CardLoadManager.GetInst().OnCard(summon_id);
        }
    }
}
