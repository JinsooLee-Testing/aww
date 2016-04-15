using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviour {
    private static PlayerManager inst = null;
    public int parentidx = 0;
    public GameObject GO_player;
    public GameObject GO_aiplayer;

    public int Monster_num = 0;
    public List<PlayerBase> Players = new List<PlayerBase>();
    public int CurTurnIdx = 0;
    public float m_y;
    private float turnOverTiem;
    private float curTurnOverTiem;

   

    public void SetTurnOverTime(float time)
    {
        turnOverTiem = time;
        curTurnOverTiem = Time.smoothDeltaTime;
    }
    public static PlayerManager GetInst()
    {
        return inst;
    }
    void Awake()
    {
        turnOverTiem = 0f;
        curTurnOverTiem = 0f;
        inst = this;
    }
	// Use this for initialization
	void Start () {

	}
	void CheckTurnOver()
    {
        if(curTurnOverTiem!=0)
        {
            curTurnOverTiem += Time.deltaTime;
            if(curTurnOverTiem>=turnOverTiem)
            {
                curTurnOverTiem = 0;
                TurnOver();
            }
        }
    }
	// Update is called once per frame
	void Update () {
        CheckTurnOver();
	}
    public void GenPlayer(int x,int z)
    {
        UserPlayer userplayer = ((GameObject)Instantiate(GO_player)).GetComponent<UserPlayer>();
        Hex hex = MapManager.GetInst().GetPlayerHex(1, 0, 1);
        userplayer.CurHex = hex;
        Vector3 v = userplayer.CurHex.transform.position;
        v.y = 1.0f;
        userplayer.transform.position = v;
        Players.Add(userplayer);
    }
    public void GenPlayerTest()
    {
        UserPlayer userplayer = ((GameObject)Instantiate(GO_player)).GetComponent<UserPlayer>();
        Hex hex = MapManager.GetInst().GetPlayerHex(0, 0, 0);
        userplayer.CurHex = hex;
        Vector3 v = userplayer.CurHex.transform.position;
        v.y = 1.0f;
        userplayer.transform.position = v;
        Players.Add(userplayer);

        for (int i = 0; i < Monster_num; ++i)
        {
            AIPlayer aiplayer = ((GameObject)Instantiate(GO_aiplayer)).GetComponent<AIPlayer>();
            hex = MapManager.GetInst().GetPlayerHex(1, 0, 8-i);
            aiplayer.CurHex = hex;
            aiplayer.CurHex.Passable = false;
            Vector3 p = aiplayer.CurHex.transform.position;
            p.y = m_y;
            aiplayer.transform.position = p;
            Players.Add(aiplayer);
        }
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

                MapManager.GetInst().ResetMapColor();
            }
        }
  
    }
    public void TurnOver()
    {
        MapManager.GetInst().ResetMapColor(); 
        PlayerBase pb = Players[CurTurnIdx];
        pb.act = ACT.IDLE;
        if (Players.Count > 0)
            CurTurnIdx++;
        if (CurTurnIdx >= Players.Count)
        {         
               CurTurnIdx = 0;
        }
        Manager.GetInst().MoveCamPosToTile(Players[CurTurnIdx].CurHex);
      
    }

    public void RemovePlayer(PlayerBase pb)
    {
        
        if (pb.main_char == false)
        {
            int cnt = 0;
            for (int i = 0; i < Players.Count; i++)
            {
                if (pb.live == false)
                {
                        cnt++;
                 }
            }
            Players.Remove(pb);
            GameObject.Destroy(pb.gameObject);
            if(cnt+1>=Players.Count)
                SceneManager.LoadScene(2);

        }
        else
        {
            GameObject.Destroy(pb.gameObject);
            SceneManager.LoadScene(0);
        }
    
        
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
