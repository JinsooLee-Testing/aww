using UnityEngine;
using System.Collections;

public class MagicCard : CardUseBase {
    // Use this for initialization
    public int magic_id = 1;
    bool On_click = false;
    void Awake()
    {
    }
    void Start () {
        On_active = true;
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
                if (magic_id == 2)
                    magic.GetInst().type = "water";
                else
                    magic.GetInst().type = "fire";
                PlayerBase pb = PlayerManager.GetInst().Players[PlayerManager.GetInst().CurTurnIdx];
                Manager.GetInst().MoveCamPosToTile(pb.CurHex);
                PlayerManager.GetInst().Players[PlayerManager.GetInst().CurTurnIdx].act = ACT.MAGIC;
                MapManager.GetInst().HilightAttackRange(pb.CurHex, 4);
                On_click = true;
                Debug.Log("fire");
            }
        }
    }
}
