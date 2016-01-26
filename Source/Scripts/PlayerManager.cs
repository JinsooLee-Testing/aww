using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {
    private static PlayerManager inst = null;
    public GameObject GO_player;
    public GameObject GO_aiplayer;
    public List<PlayerBase> Players = new List<PlayerBase>();
    public int CurTurnIdx = 0;


    public static PlayerManager GetInst()
    {
        return inst;
    }
    void Awake()
    {
        inst = this;
    }
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
       
	}
    public void GenPlayerTest()
    {
        UserPlayer userplayer = ((GameObject)Instantiate(GO_player)).GetComponent<UserPlayer>();
        Hex hex = MapManager.GetInst().GetPlayerHex(0, 0, 0);
        userplayer.CurHex = hex;
        userplayer.transform.position = userplayer.CurHex.transform.position;
        Players.Add(userplayer);

        userplayer = ((GameObject)Instantiate(GO_player)).GetComponent<UserPlayer>();
         hex = MapManager.GetInst().GetPlayerHex(5, 0, 0);
        userplayer.CurHex = hex;
        userplayer.transform.position = userplayer.CurHex.transform.position;
        Players.Add(userplayer);

        AIPlayer aiplayer = ((GameObject)Instantiate(GO_aiplayer)).GetComponent<AIPlayer>();
        hex = MapManager.GetInst().GetPlayerHex(2, 0, 5);
        aiplayer.CurHex = hex;
        aiplayer.transform.position = aiplayer.CurHex.transform.position;
        Players.Add(aiplayer);
    }
    public void MovePlayer(Hex start,Hex dest)
    {
        PlayerBase pb = Players[CurTurnIdx];
        if(MapManager.GetInst().IsReachAble(start,dest,pb.MoveRange)==false)
        {
            return; 
        }
        if (pb.act == ACT.MOVEHILIGHT)
        {
            float distance = MapManager.GetInst().GetDistance(start, dest);
            if (distance <= Players[CurTurnIdx].MoveRange && distance != 0 &&dest.Passable==true)
            {
                pb.MoveHexes = MapManager.GetInst().GetPath(start, dest);
                if (pb.MoveHexes.Count == 0)
                    return;
                pb.act = ACT.MOVING;
               //  Players[CurTurnIdx].transform.position = dest.transform.position;
               // Players[CurTurnIdx].CurHex = dest;
               // TurnOver();
                MapManager.GetInst().ResetMapColor();
            }
        }
  
    }
    public void TurnOver()
    {
        MapManager.GetInst().ResetMapColor(); 
        PlayerBase pb = Players[CurTurnIdx];
        pb.act = ACT.IDLE;
        CurTurnIdx++;
        if (CurTurnIdx == Players.Count)
        {
            CurTurnIdx = 0;
        }
    }
    void OnGUI()
    {
        Players[CurTurnIdx].DrawCommand();
    }
}
