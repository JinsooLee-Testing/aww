using UnityEngine;
using System.Collections;


public class AIPlayer : PlayerBase
{
    
    void Awake()
    {
        act = ACT.IDLE;
        status = new PlayerStatus();
        anim = GetComponent<Animator>();
        main_char = false;
        live = true;
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
          
            if (pm.Players[pm.CurTurnIdx] == this)
            {
                MapManager.GetInst().SetHexColor(CurHex, Color.black);
            }   
            if(pm.Players[pm.CurTurnIdx]==this)
            { 
                AiProc();
            }
        }
        if (act == ACT.MOVING)
        {//이동처리
         //   CurHex.Passable = true;
         
            if (MoveHexes.Count==0)
            {
                act = ACT.IDLE;
                PlayerManager.GetInst().TurnOver();
                return;

            }
            Hex nextHex = MoveHexes[0];
            Vector3 v = nextHex.transform.position;
            v.y = PlayerManager.GetInst().m_y;
            float distance = Vector3.Distance(transform.position, v);
            if (distance > 0.1f) //이동중
            {
                anim.SetBool("run", true);

                transform.position += (v - transform.position).normalized * status.MoveSpeed * Time.smoothDeltaTime;    
                transform.rotation = Quaternion.LookRotation((v - transform.position).normalized);
                Vector3 r = transform.rotation.eulerAngles;
                r.y -= 90;
            }
            else //다음 목표 hex에 도착함
            {
                v = nextHex.transform.position;
                v.y = PlayerManager.GetInst().m_y;
                transform.position = v;
                MoveHexes.RemoveAt(0);
                if (MoveHexes.Count == 0)//최종 dest
                {
                    CurHex = nextHex;
                    act = ACT.IDLE;
                    //  CurHex.Passable = false;
                    anim.SetBool("run", false);
                    PlayerManager.GetInst().TurnOver();
                }

            }
        }

    }
    public void AiProc()
    {
        AIthink ai = AIthink.GetInst();
        //근점 플레이어찾는과정 추가내용 
        //이미 근접상태면 act는 IDLE 유지 이동 필요하면 act는 MOVING으로
        ai.MoveToNearUserPlayer(this);
        if(act==ACT.IDLE)
        {
            anim.SetBool("attack", false);
            ai.AtkAItoUser(this);
     
            
        }
        if (act == ACT.ATTACKING)
        {
            anim.SetBool("attack", true);
        }
    }
    void OnMouseDown()
    {
        PlayerManager pm = PlayerManager.GetInst();
        PlayerBase pb = pm.Players[pm.CurTurnIdx];
        BattleManager bm = BattleManager.GetInst();
        if (pm.Players[pm.CurTurnIdx].act==ACT.ATTACKHIGHLIGHT)
        {
            Vector3 v = pb.transform.position;
            v.y = PlayerManager.GetInst().m_y;
            Vector3 v2 = this.CurHex.transform.position;
            v2.y = PlayerManager.GetInst().m_y;
            pb.transform.rotation = Quaternion.LookRotation((v2 - v).normalized);
            Vector3 r = pb.transform.rotation.eulerAngles;
            r.y -= 90;
            pb.transform.rotation = Quaternion.Euler(r);

            //a.anim.SetBool("Attack",true);
            pb.act = ACT.ATTACKING;
            this.GetDamage(80);
            PlayerManager.GetInst().TurnOver();
        }

    }
}
