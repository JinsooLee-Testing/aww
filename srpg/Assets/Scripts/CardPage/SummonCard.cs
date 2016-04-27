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
                PlayerManager.GetInst().HilightSummons();
                act = ACT.SUMMONES;
                CostManager.GetInst().CostDecrease(3);
                On_active = false;
            }
        }
    }
}
