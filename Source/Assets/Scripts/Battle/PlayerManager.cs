using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {
    private static PlayerManager inst = null;
    public int parentidx = 0;
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
        /*
        userplayer = ((GameObject)Instantiate(GO_player)).GetComponent<UserPlayer>();
         hex = MapManager.GetInst().GetPlayerHex(8, 0, 5);
        userplayer.CurHex = hex;
        userplayer.transform.position = userplayer.CurHex.transform.position;
        Players.Add(userplayer);
        */
        AIPlayer aiplayer = ((GameObject)Instantiate(GO_aiplayer)).GetComponent<AIPlayer>();
        hex = MapManager.GetInst().GetPlayerHex(1, 0, 8);
        aiplayer.CurHex = hex;
        Vector3 p = aiplayer.CurHex.transform.position;
        p.y = 1;

        aiplayer.transform.position = p;
       
       
        Players.Add(aiplayer);
    }
    public void MovePlayer(Hex start,Hex dest)
    {
        PlayerBase pb = Players[CurTurnIdx];
        if (MapManager.GetInst().IsReachAble(start, dest, pb.status.MoveRange) == false)
        {
            return; 
        }
        if (pb.act == ACT.MOVEHILIGHT)
        {
            float distance = MapManager.GetInst().GetDistance(start, dest);
            if (distance <= Players[CurTurnIdx].status.MoveRange && distance != 0 && dest.Passable == true)
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
        Manager.GetInst().MoveCamPosToTile(Players[CurTurnIdx].CurHex);
      
    }
    /*
    void OnGUI()
    {
        Players[CurTurnIdx].DrawCommand();
    }
    */
    public void RemovePlayer(PlayerBase pb)
    {
    
            
        Players.Remove(pb);
        GameObject.Destroy(pb.gameObject);
    }
    public void MouseInputProc(int btn)
    {
        if(btn==1)
        {
            //step - aI일때는 리턴
        
            PlayerBase pb = Players[CurTurnIdx];
            if(pb is AIPlayer)
            {
                return;
            }
            //step1 idle 할일 x
            ACT act = Players[CurTurnIdx].act;
            if(act==ACT.IDLE)
            {
                MapManager.GetInst().ResetMapColor();
                return;
            }
            //step2 attack 무브일떄 하이라이트 초기화 
            if (act == ACT.MOVEHILIGHT||act==ACT.ATTACKHIGHLIGHT)
            {
                MapManager.GetInst().ResetMapColor();
                Players[CurTurnIdx].act = ACT.IDLE;
                
                return;
            }
        }
    }

}
