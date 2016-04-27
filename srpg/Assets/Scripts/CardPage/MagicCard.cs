using UnityEngine;
using System.Collections;

public class MagicCard : CardUseBase {
    // Use this for initialization
    bool On_click = false;
    void Awake()
    {
    }
    void Start () {
     
    }
	
	// Update is called once per frame
	void Update () {
        if (magic.GetInst().fired == true&& On_click==true)
            On_active = false;
    }
    void OnMouseDown()
    {
        if (CostManager.GetInst().cur_cost_num >= 1)
        {
            if (On_active == true)
            {
         
                PlayerBase pb = PlayerManager.GetInst().Players[PlayerManager.GetInst().CurTurnIdx];
                PlayerManager.GetInst().Players[PlayerManager.GetInst().CurTurnIdx].act = ACT.MAGIC;
                MapManager.GetInst().HilightAttackRange(pb.CurHex, 4);
                On_click = true;
                Debug.Log("fire");
            }
        }
    }
}
