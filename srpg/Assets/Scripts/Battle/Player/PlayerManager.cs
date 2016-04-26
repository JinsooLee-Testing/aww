using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public enum TURN
{
    PLAYERTURN,
    NPCTURN

}

public class PlayerManager : MonoBehaviour {
    private static PlayerManager inst = null;
    private float turnOverTiem;
    private int Max_CurIdx;

    public float curTurnOverTiem;
    public int parentidx = 0;
    public GameObject GO_player;
    public GameObject GO_aiplayer;
    public GameObject GO_tree;
    public int Monster_num = 0;
    public List<PlayerBase> Players = new List<PlayerBase>();
    public int CurTurnIdx = 0;
    public float m_y;
    public Transform PlayersParent;
    public Transform ObcParent;
    public int EnemyCount=0;
    public int EnemyTurnCount = 0;

    public TURN turn= TURN.PLAYERTURN;
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
                Players[CurTurnIdx].anim.SetBool("attack", false);
                TurnOver();
            }
        }
    }
	// Update is called once per frame
	void Update () {
        CheckTurnOver();
       // if(Players[CurTurnIdx].act==ACT.MOVING)
            //Manager.GetInst().MoveCamPosToTile(Players[CurTurnIdx].CurHex);
    }
    public void HilightSummons()
    {
        Players[CurTurnIdx].CurHex.Passable = true;
        MapManager.GetInst().HilightMoveRange(Players[CurTurnIdx].CurHex, 3);
        Players[CurTurnIdx].act = ACT.SUMMONES;
    }
    public void GenPlayer(int x,int z)
    {
        UserPlayer player = ((GameObject)Instantiate(GO_tree)).GetComponent<UserPlayer>();
        Hex hex = MapManager.GetInst().GetPlayerHex(x, 0, z);
        player.CurHex = hex;
        Vector3 v = player.CurHex.transform.position;
        v.y = 1.5f;
        player.transform.position = v;
        player.m_type = Type.USER;
        Players.Add(player);

        MapManager.GetInst().ResetMapColor();
    }

    public void GenPlayerTest()
    {
        UserPlayer userplayer = ((GameObject)Instantiate(GO_player)).GetComponent<UserPlayer>();
        Hex hex = MapManager.GetInst().GetPlayerHex(2, 0, 0);
        userplayer.CurHex = hex;
        Vector3 v = userplayer.CurHex.transform.position;
        v.y = 1.0f;
        userplayer.transform.position = v;
        userplayer.m_type = Type.MAINCHARACTER;
        Players.Add(userplayer);
        
        for (int i = 0; i < ObcParent.childCount; i++)
        {
            var obj = ObcParent.GetChild(i).GetComponent<tree>();
            if (obj != null)
            {
                //player.transform.position = curpos;           
                hex = MapManager.GetInst().GetPlayerHex(obj.x, 0, obj.z);
                obj.CurHex = hex;
                Vector3 curpos = obj.CurHex.transform.position;
                curpos.y = 2f;
                obj.transform.position = curpos;
          
            }
            else
                Debug.LogError("Invalid object in cells paretn game object");
        }
        for (int i = 0; i < PlayersParent.childCount; i++)
        {
            var player = PlayersParent.GetChild(i).GetComponent<AIPlayer>();
            if (player != null)
            {           
                //player.transform.position = curpos;           
                hex = MapManager.GetInst().GetPlayerHex(player.x, player.y, player.z);
                player.CurHex = hex;
                Vector3 curpos = player.CurHex.transform.position;
                curpos.y = m_y;
                player.transform.position = curpos;
                EnemyCount++;
                EnemyTurnCount++;
                Players.Add(player);
            }
            else
                Debug.LogError("Invalid object in cells paretn game object");
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
        {
            CurTurnIdx++;
        }
        if (CurTurnIdx >= Players.Count)
        {        
            CurTurnIdx = 0;       
        }
        PlayerBase pb2 = Players[CurTurnIdx];
        if (pb2.m_type == Type.MAINCHARACTER)
        {
            CostManager.GetInst().AddCost();
        }

        CameraManager.GetInst().ResetCameraTarget();

    }
   
    public void RemovePlayer(PlayerBase pb)
    {
        pb.CurHex.Passable = true;
        if (pb.m_type==Type.MONSTER)
        {
           
            EnemyCount--;
            if (EnemyCount<=0)
            {
                Players.Remove(pb);
                GameObject.Destroy(pb.gameObject);
                SceneManager.LoadScene(2);
            }
            else
            {
               Players.Remove(pb);
                GameObject.Destroy(pb.gameObject);
            }
        }
        else if (pb.m_type == Type.MAINCHARACTER)
        {
            Players.Remove(pb);
            GameObject.Destroy(pb.gameObject);
         SceneManager.LoadScene(0);
        }
        else
        {
            Players.Remove(pb);
            GameObject.Destroy(pb.gameObject);
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
