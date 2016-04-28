using UnityEngine;
using System.Collections;

public class SummonCard : CardUseBase {
    ACT act;
   
    void Start () {
        //transform.position = pos;
         
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseDown()
    {
    
        if (CostManager.GetInst().cur_cost_num >= 3)
        {
            if (On_active == true)
            {
                PlayerBase pb = PlayerManager.GetInst().Players[PlayerManager.GetInst().CurTurnIdx];
                Manager.GetInst().MoveCamPosToTile(pb.CurHex);
                PlayerManager.GetInst().HilightSummons();
             
                act = ACT.SUMMONES;
                CostManager.GetInst().CostDecrease(3);
                On_active = false;
            }
        }
    }
}
