using UnityEngine;
using System.Collections;

public class UserPlayer : PlayerBase
{
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        act = ACT.IDLE;
        status = new PlayerStatus();
      
    }
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        PlayerManager pm = PlayerManager.GetInst();
       if(act==ACT.IDLE)
       {
            anim.SetBool("running", false);
            if (pm.Players[pm.CurTurnIdx] == this)
           {
               MapManager.GetInst().SetHexColor(CurHex, Color.black);
           }
       }
        if (act == ACT.MOVING)
        {//이동처리
            anim.SetBool("running",true);
            Hex nextHex = MoveHexes[0];
            float distance = Vector3.Distance(transform.position, nextHex.transform.position);
            if (distance > 0.1f) //이동중
            {
                anim.SetBool("running", true);
                transform.position += (nextHex.transform.position - transform.position).normalized * status.MoveSpeed * Time.smoothDeltaTime;
                transform.rotation = Quaternion.LookRotation((nextHex.transform.position - transform.position).normalized);
            }
            else //다음 목표 hex에 도착함
            {
                transform.position = nextHex.transform.position;
                MoveHexes.RemoveAt(0);
                if (MoveHexes.Count == 0)//최종 dest
                {
                    CurHex = nextHex;
                    act = ACT.IDLE;
                    PlayerManager.GetInst().TurnOver();
                }

            }
        }

    }
    /*
    public override void DrawCommand()
    {
        float btnW = 100f;
        float btnH = 75f;
        Rect rect = new Rect(0, Screen.height/2+100, btnW, btnH);
        if (GUI.Button(rect, "Move"))
        {
            Debug.Log("Move");

            if (MapManager.GetInst().HilightMoveRange(CurHex, status.MoveRange))
            {
                act = ACT.MOVEHILIGHT;
            }
        }
        rect = new Rect(100, (Screen.height / 2 + 100), btnW, btnH);
        if (GUI.Button(rect, "Attack"))
        {
            Debug.Log("Attack");

            if (MapManager.GetInst().HilightAttackRange(CurHex, status.attackRange))
            {
                act = ACT.ATTACKHIGHLIGHT;
            }
        }
         rect = new Rect(200,(Screen.height / 2 + 100), btnW , btnH);
         if (GUI.Button(rect, "Turn Over"))
        {
            Debug.Log("Turn Over");
           PlayerManager.GetInst().TurnOver();
            
        }
    }
    */
}
