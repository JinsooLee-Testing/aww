using UnityEngine;
using System.Collections;


public class AIPlayer : PlayerBase
{

    public int x, y, z;
    public float m_y = 1.0f;
    public int hp;
    public int Attack;
    public string m_name;
    public int id = 1;
    public float ef_time = 0f;
  
    void Awake()
    {
        act = ACT.IDLE;
        status = new PlayerStatus();
        status.Name = m_name;
        status.Curhp = hp;
        status.Maxhp = hp;
        status.Attack = Attack;
        anim = GetComponent<Animator>();
        main_char = false;
        //m_type = Type.MONSTER;
        live = true;
        Monster_id = id;
    }
    void Start()
    {

    }
    public void AIProcess()
    {
        PlayerManager pm = PlayerManager.GetInst();
        if (act == ACT.CASTING && casting==true)
        {
            Debug.Log("hh");
            act = ACT.JUMP;
            MapManager.GetInst().MarkAttackRange(CurHex, 4);

            for (int j = 0; j < pm.Players.Count; ++j)
            {
                if (pm.Players[j].CurHex.At_Marked == true)
                {
                    if (pm.Players[j].m_type != Type.MONSTER && pm.Players[j].m_type != Type.BOSS)
                    {
                        if (((UserPlayer)pm.Players[j]).Equip == false)
                        {
                            pm.Players[j].GetDamage(100);
                            EffectManager.GetInst().Play(pm.Players[j].CurHex.gameObject);
                        }
                        else
                        {
                            ((UserPlayer)pm.Players[j]).DestroyEquip();
                        }
                    }
                }
            }
            casting = false;

        }
        if (act == ACT.IDLE)
        {

            if (pm.Players[pm.CurTurnIdx] == this)
            {
                MapManager.GetInst().SetHexColor(CurHex, Color.black);
            }
            if (pm.Players[pm.CurTurnIdx] == this)
            {
                AiProc();
            }
        }
        if (act == ACT.JUMP)
        {
            Vector3 gravity = new Vector3(0, -9.8f, 0);
            Vector3 v = transform.position;
            float speed = 15;
            if (v.y <= 10)
            {
                v.y += Time.deltaTime * speed;
                speed -= Time.deltaTime;
                transform.position = v;
            }
            else
                act = ACT.JUMPDOWN;
            
        }
        if(act==ACT.JUMPDOWN)
        {
            Vector3 gravity = new Vector3(0, -9.8f, 0);
            Vector3 v = transform.position;
            float speed = 12;
            if (v.y >= 1)
            {
                v.y -= Time.deltaTime * speed;
                speed += Time.deltaTime * 2;
                transform.position = v;
                if (ef_time > 0.2)
                {
                    EffectManager.GetInst().ShowEffect_Summon(CurHex.gameObject, 7, v.y);
                    ef_time = 0f;
                }
            }
            else
            {
                act = ACT.IDLE;
       
                EffectManager.GetInst().ShowEffect_Summon(CurHex.gameObject, 8, 1f);
                PlayerManager.GetInst().TurnOver();
            }
        }

        if (act == ACT.MOVING)
        {//이동처리

            if (MoveHexes.Count == 0)
            {
                act = ACT.IDLE;
                PlayerManager.GetInst().TurnOver();
                return;

            }
            Hex nextHex = MoveHexes[0];
            if (MapManager.GetInst().MapSizeY > 0)
            {
                Point p = new Point(nextHex.MapPos.GetX(), nextHex.MapPos.GetY() + 1, nextHex.MapPos.GetZ());
                if (MapManager.GetInst().Map[p.GetX()][p.GetY()][p.GetZ()].mesh_draw == true)
                {
                    nextHex = MapManager.GetInst().Map[p.GetX()][p.GetY()][p.GetZ()];
                    jump = true;
                }
                else
                    jump = false;
            }
            Vector3 v = nextHex.transform.position;
            v.y += m_y;
            float distance = Vector3.Distance(transform.position, v);

            if (distance >= 1.0f) //이동중
            {
                anim.SetBool("attack", false);
                anim.SetBool("run", true);


                transform.position += (v - transform.position).normalized * 3f * Time.smoothDeltaTime;
                if (jump == false)
                {
                    transform.rotation = Quaternion.LookRotation((v - transform.position).normalized);
                    if (id != 1)
                    {
                        Vector3 r = transform.rotation.eulerAngles;
                        r.y -= 90;
                        transform.rotation = Quaternion.Euler(r);
                    }
                }

            }
            else //다음 목표 hex에 도착함
            {

                v = nextHex.transform.position;
                v.y += m_y;
                transform.position = v;
                MoveHexes.RemoveAt(0);

                if (MoveHexes.Count <= 0)//최종 dest
                {
                    anim.SetBool("run", false);
                    act = ACT.IDLE;
                    Point temppos = new Point(nextHex.MapPos.GetX(), 0, nextHex.MapPos.GetZ());

                    CurHex = MapManager.GetInst().Map[temppos.GetX()][temppos.GetY()][temppos.GetZ()];
                    CurHex.Passable = false;
                    PlayerManager.GetInst().TurnOver();

                }

            }
        }
    }
    void RemoveRoutine()
    {
        PlayerManager pm = PlayerManager.GetInst();
        if (removeTime != 0)
        {
            removeTime += Time.deltaTime;
            if (removeTime >= 1.5f)
            {
                for (int i = 0; i < pm.Players.Count; ++i)
                {
                    if (pm.Players[i].act == ACT.DIYING)
                    {
                        if (pm.CurTurnIdx == i)
                        {
                            pm.RemoveAfter();
                            pm.RemovePlayer(pm.Players[i]);
                            pm.RemoveAfter();
                            pm.select_object = pm.Players[pm.CurTurnIdx];
                            //pm.TurnOver();
                            
                        }
                        else
                        {

                            pm.RemoveAfter();
                            pm.RemovePlayer(pm.Players[i]);
                            pm.RemoveAfter();
                            pm.select_object = pm.Players[pm.CurTurnIdx];
                        }
                    }
                }


            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        PlayerManager pm = PlayerManager.GetInst();
        AIProcess();
        ef_time += Time.deltaTime;
        RemoveRoutine();
       
        if (magic.GetInst().type == "wind")
        {
            if (ef_time > 1.0f&&CurHex.At_Marked==true)
            {
                 EffectManager.GetInst().ShowEffect(this.gameObject);
                 SoundManager.GetInst().PlayAttackSound();
                 GetDamage(30);
            }

        }

        if (ef_time > 1.0f)
            ef_time = 0f;
    }
    public void AiProc()
    {
        if (PlayerManager.GetInst().Players[PlayerManager.GetInst().CurTurnIdx] == this)
        {
            AIthink ai = AIthink.GetInst();
            //근점 플레이어찾는과정 추가내용 
            //이미 근접상태면 act는 IDLE 유지 이동 필요하면 act는 MOVING으로


            CurHex.Passable = true;
            if(act != ACT.CASTING)
            ai.MoveToNearUserPlayer(this);
            //if (act == ACT.IDLE)
               


        }
    }
    void OnMouseDown()
    {

        PlayerManager pm = PlayerManager.GetInst();
        PlayerBase pb = pm.Players[pm.CurTurnIdx];
        BattleManager bm = BattleManager.GetInst();

        PlayerManager.GetInst().select_object = this;
        PlayerManager.GetInst().SetPickPos(this);
        if (pb.act == ACT.MAGIC)
        {
            if (CurHex.Marked == true)
            {
                EffectManager.GetInst().ShowEffect_Summon(pb.CurHex.gameObject,3,1.2f);
          
                if (magic.GetInst().type=="fire")
                    magic.GetInst().SetTarget(this.CurHex, this.CurHex, 9);
                else
                    magic.GetInst().SetTarget(this.CurHex, this.CurHex, 1);
                magic.GetInst().targetAI = this;
            }
        }

        if (pm.Players[pm.CurTurnIdx].act == ACT.ATTACKHIGHLIGHT)
        {

            if (CurHex.Marked == true)
            {
                CameraManager.GetInst().ResetCameraTarget();
                MapManager.GetInst().ResetMapColor();
                bm.AttackAtoB(pb, this);

            }


        }

    }
}
