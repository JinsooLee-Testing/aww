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
    public GameObject GO_tree2;
    public GameObject GO_pick;
    public GameObject npc;
    public int Monster_num = 0;
    public List<PlayerBase> Players = new List<PlayerBase>();
    public int CurTurnIdx = 0;
    public float m_y;
    public Transform PlayersParent;
    public int S_x = 0;
    public int S_z =0;
    public int EnemyCount = 0;
    public int EnemyTurnCount = 0;
    public PlayerBase select_object;
    public TURN turn = TURN.PLAYERTURN;
    public int nextScene_Num = 3;
    public pick pick_ob = new pick();
    public bool isnpc = false;

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
        inst.npc = (GameObject)Resources.Load("Prefabs/Player/npc");

    }
    // Use this for initialization
    void Start() {


    }
    void CheckTurnOver()
    {
        if (curTurnOverTiem != 0)
        {
            curTurnOverTiem += Time.deltaTime;
            if (curTurnOverTiem >= turnOverTiem)
            {
                curTurnOverTiem = 0;
                Players[CurTurnIdx].anim.SetBool("attack", false);
                TurnOver();
            }
        }
    }
    // Update is called once per frame
    void Update() {
        CheckTurnOver();
       
        if (select_object.act != ACT.DIYING)
        {
            if (select_object != null)
            {
                Vector3 temp = select_object.transform.position;
                temp.y = 3;
                pick_ob.transform.position = temp;
            }
        }

        // if(Players[CurTurnIdx].act==ACT.MOVING)
        //Manager.GetInst().MoveCamPosToTile(Players[CurTurnIdx].CurHex);
    }
    public void HilightSummons()
    {
        Players[CurTurnIdx].CurHex.Passable = true;
        MapManager.GetInst().HilightMoveRange(Players[CurTurnIdx].CurHex, 3);
        Players[CurTurnIdx].act = ACT.SUMMONES;
    }
    public void GenPlayer(int x, int z)
    {
        UserPlayer player = ((GameObject)Instantiate(GO_tree)).GetComponent<UserPlayer>();
        Hex hex = MapManager.GetInst().GetPlayerHex(x, 0, z);
        player.CurHex = hex;
        player.CurHex.Passable = false;
        Vector3 v = player.CurHex.transform.position;
        v.y = 1.5f;
        player.transform.position = v;
        player.m_type = Type.USER;
        int find = 0;
        for (int i = 0; i < Players.Count; ++i)
        {
            if (player.m_type == Type.MAINCHARACTER)
            {
                find = i;
            }

        }
        Players.Insert(1, player);

        MapManager.GetInst().ResetMapColor();
    }
    public void GenNpc(int x, int z)
    {
        npc player = ((GameObject)Instantiate(inst.npc)).GetComponent<npc>();
        Hex hex = MapManager.GetInst().GetPlayerHex(x, 0, z);
        player.CurHex = hex;
        player.CurHex.Passable = false;
        Vector3 v = player.CurHex.transform.position;
        v.y = 1.0f;
        player.transform.position = v;

    }
    public void GenAIPlayer(int x, int z)
    {
        AIPlayer player = ((GameObject)Instantiate(GO_tree2)).GetComponent<AIPlayer>();
        Hex hex = MapManager.GetInst().GetPlayerHex(x, 0, z);
        player.CurHex = hex;
        player.CurHex.Passable = false;
        Vector3 v = player.CurHex.transform.position;
        v.y = 1.5f;
        player.transform.position = v;
        player.m_type = Type.MONSTER;
        EnemyCount++;
        Players.Add(player);

        MapManager.GetInst().ResetMapColor();
    }
    public void SetPickPos(PlayerBase pb)
    {
        Vector3 temp = pb.transform.position;
        temp.y = 3;
        pick_ob.transform.position = temp;
    }
    public void GenPlayerTest()
    {
        pick_ob = ((GameObject)Instantiate(GO_pick)).GetComponent<pick>();
        UserPlayer userplayer = ((GameObject)Instantiate(GO_player)).GetComponent<UserPlayer>();
        Hex hex = MapManager.GetInst().GetPlayerHex(S_x, 0, S_z);
        userplayer.CurHex = hex;
        Vector3 v = userplayer.CurHex.transform.position;
        v.y = 1.0f;
        userplayer.transform.position = v;
        userplayer.m_type = Type.MAINCHARACTER;
        Players.Add(userplayer);
        select_object = userplayer;
        v.y = 4.0f;
        pick_ob.transform.position = v;

        for (int i = 0; i < PlayersParent.childCount; i++)
        {
            var player = PlayersParent.GetChild(i).GetComponent<AIPlayer>();
            if (player != null)
            {
                //player.transform.position = curpos;           
                hex = MapManager.GetInst().GetPlayerHex(player.x, player.y, player.z);
                player.CurHex = hex;
                Vector3 curpos = player.CurHex.transform.position;
                curpos.y = player.m_y;
                player.CurHex.Passable = false;

                player.transform.position = curpos;
                EnemyCount++;
                EnemyTurnCount++;
                Players.Add(player);
            }
            else
                Debug.LogError("Invalid object in cells paretn game object");
        }
        if (isnpc == true)
            GenNpc(7,3);

    }
    public void MovePlayer(Hex start, Hex dest)
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
        pb.CurHex.Passable = false;

        if (pb.act != ACT.CASTING)
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
        if (pb2.act == ACT.CASTING)
        {
            pb2.casting = true;
        }
        select_object = pb2;

        CameraManager.GetInst().ResetCameraTarget();

    }
    public void RemoveAfter()
    {

        if (CurTurnIdx >= Players.Count)
        {
            CurTurnIdx = 0;
        }
        MapManager.GetInst().ResetMapColor();
    }
    public void RemovePlayer(PlayerBase pb)
    {
        pb.CurHex.Passable = true;
        if (pb.m_type == Type.MONSTER || pb.m_type==Type.BOSS|| pb.m_type == Type.GOLEM)
        {
           
            EnemyCount--;
            Debug.Log(EnemyCount);
            if (EnemyCount<=0)
            {
                Players.Remove(pb);
                GameObject.Destroy(pb.gameObject);
                GUIManager.GetInst().CreateResult();
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
         SceneManager.LoadScene(1);
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
