using UnityEngine;
using System.Collections;

public class MagicCard : CardUseBase {
    // Use this for initialization
    public int magic_id = 1;
    bool On_click = false;
    void Awake()
    {
        card_id = magic_id;
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
        if (InGame == true)
        {
            if (CostManager.GetInst().cur_cost_num >= cost )
            {
                if (On_active == true)
                {
                    if (magic_id == 2 || magic_id == 6)
                    {
                        magic.GetInst().type = "water";

                    }
                    else if (magic_id == 3)
                    {
                        magic.GetInst().type = "wall";
                        Debug.Log("fire");
                    }
                    else if (magic_id == 4)
                    {
                        magic.GetInst().type = "wind";
                    }
                    else if (magic_id == 1 || magic_id == 7)
                        magic.GetInst().type = "fire";
                    else
                        magic.GetInst().type = "buff";

                    magic.GetInst().curmagic_id = magic_id;
                    PlayerBase pb = PlayerManager.GetInst().Players[PlayerManager.GetInst().CurTurnIdx];
                    Manager.GetInst().MoveCamPosToTile(pb.CurHex);
                    PlayerManager.GetInst().Players[PlayerManager.GetInst().CurTurnIdx].act = ACT.MAGIC;
                    CostManager.GetInst().Curcostnum = cost;
                    Debug.Log(CostManager.GetInst().Curcostnum);
                    if (magic_id == 3)
                        MapManager.GetInst().HilightAttackRange(pb.CurHex, 2);
                    else
                        MapManager.GetInst().HilightAttackRange(pb.CurHex, 4);
                    On_click = true;

                }
            }
        }
        else
        {
            Debug.Log("Dww");

            CardLoadManager.GetInst().OnCard(magic_id);
            
            
        }
    }
}
