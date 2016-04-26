using UnityEngine;
using System.Collections;


public class AIPlayer : PlayerBase
{
    public int x, y, z;
    public int hp;
    public int Attack;
    public string m_name;
    void Awake()
    {
        act = ACT.IDLE;
        status = new PlayerStatus();
        status.Name = m_name;
        status.Curhp = hp;
        status.Attack = Attack;
        anim = GetComponent<Animator>();
        main_char = false;
        m_type = Type.MONSTER;
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
                anim.SetBool("attack", false);
                anim.SetBool("run", true);

                transform.position += (v - transform.position).normalized * status.MoveSpeed * Time.smoothDeltaTime;    
                transform.rotation = Quaternion.LookRotation((v - transform.position).normalized);
                Vector3 r = transform.rotation.eulerAngles;
                r.y -= 90;
                //transform.rotation = Quaternion.Euler(r);
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
                    CurHex.Passable = false;
                    act = ACT.IDLE;

                    anim.SetBool("run", false);
                   // CurHex.isonTotile = true;
                    PlayerManager.GetInst().TurnOver();
                }

            }
        }

    }
    public void AiProc()
    {
        if (PlayerManager.GetInst().Players[PlayerManager.GetInst().CurTurnIdx] == this)
        {
            AIthink ai = AIthink.GetInst();
            //근점 플레이어찾는과정 추가내용 
            //이미 근접상태면 act는 IDLE 유지 이동 필요하면 act는 MOVING으로
            if (live == false)
                PlayerManager.GetInst().TurnOver();
            CurHex.Passable = true;
            ai.MoveToNearUserPlayer(this);
           
       
         
        }
    }
    void OnMouseDown()
    {
      
        PlayerManager pm = PlayerManager.GetInst();
        PlayerBase pb = pm.Players[pm.CurTurnIdx];
        BattleManager bm = BattleManager.GetInst();
        if (pm.Players[pm.CurTurnIdx].act==ACT.ATTACKHIGHLIGHT)
        {
            bm.AttackAtoB(pb, this);
            EffectManager.GetInst().ShowEffect(this.gameObject);
            PlayerManager.GetInst().TurnOver();
        }

    }
}
