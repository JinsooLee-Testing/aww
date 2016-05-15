﻿using UnityEngine;
using System.Collections;

public class UserPlayer : PlayerBase
{

    public string[] na;
    bool hited = false;
    public bool Equip = false;
    public int x=0;
    public int y=0;
  
    public int z=0;
  
    public GameObject eqip;
    Equipment equip;
    GameObject q;
    Condition condi;
    float brokentime = 0f;
    public int Attack;
    public int hp;
    public string line = "\n";
    public string info;
    public string info2;
    public string named;
    public string equip_type;
    public string condition = "none";
     
    void Awake()
    {
        anim = GetComponent<Animator>();
        act = ACT.IDLE;

        status = new PlayerStatus();
        status.Name = named;
        status.Curhp = hp;
        status.Maxhp = hp;
        status.Attack=Attack;
        status.info = info;
        line = line.Replace(line, "\n");
        status.info = info + line + info2;
        main_char = true;
        live = true;
        m_type = Type.USER;
        Equip = false;

        q = (GameObject)Resources.Load("Prefabs/ui/stun");

    }
   
    void Start()
    {
    }
    // Update is called once per frame
    void OnMouseDown()
    {
      
        if(magic.GetInst().curmagic_id==5)
        {

            status.Heal(80);
            EffectManager.GetInst().ShowEffect_Summon(this.CurHex.gameObject, 12, 1);
            CostManager.GetInst().CostDecrease(CostManager.GetInst().Curcostnum);
            MapManager.GetInst().ResetMapColor();
            CameraManager.GetInst().ResetCameraTarget();
            magic.GetInst().curmagic_id = 0;
        }
        if (magic.GetInst().curmagic_id == 8)
        {
            if (Equip == false)
            {
                EquipShield();

                EffectManager.GetInst().ShowEffect_Summon(this.CurHex.gameObject, 12, 1);
                CostManager.GetInst().CostDecrease(CostManager.GetInst().Curcostnum);
                MapManager.GetInst().ResetMapColor();
                CameraManager.GetInst().ResetCameraTarget();
                magic.GetInst().curmagic_id = 0;
            }
            else
            {
                MapManager.GetInst().ResetMapColor();
                CameraManager.GetInst().ResetCameraTarget();
            }
        }
        else
        {
            PlayerManager.GetInst().select_object = this;
            PlayerManager.GetInst().SetPickPos(this);
        }
    }
    public void DrawStatus()
    {
      
    }
    public void DestroyEquip()
    {
        brokentime += Time.deltaTime;
        Equip = false;
        equip_type = "null";
    }
    void EquipHelmet()
    {
        if (Equip == false)
        {
            equip = ((GameObject)Instantiate(eqip)).GetComponent<Equipment>();
            Equip = true;
            Vector3 v2 = transform.position;
            v2.y += 1.5f;
            equip.transform.position = v2;
            equip_type = "helmet";
        }
    }
    void EquipShield()
    {
        if (Equip == false)
        {
            equip = ((GameObject)Instantiate(magic.GetInst().magics[4])).GetComponent<Equipment>();
            Equip = true;
            Vector3 v2 = transform.position;
            v2.y += 2.5f;
            equip.transform.position = v2;
            equip_type = "shield";
        }
    }
    void ShowCondition()
    {
        if (act == ACT.STUN&& condition == "none")
        {
            condi = ((GameObject)Instantiate(q)).GetComponent<Condition>();
            Vector3 v2 = transform.position;
            v2.y += 2.5f;
            condi.transform.position = v2;
            condition = "stun";
        }
        else if (condition == "stun"&& act != ACT.STUN)
        {
            Destroy(condi.gameObject);
            condition = "none";
        }

    }
    void Update()
    {

        ShowCondition();
        if (status.Curhp < 0)
            act = ACT.DIYING;
        if (brokentime != 0)
        {
            brokentime += Time.deltaTime;
            if(brokentime>1.5)
            {
               
                GameObject.Destroy(equip.gameObject);
                brokentime = 0f;
            }
        }
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
                        }
                        else
                        {
                            pm.RemoveAfter();
                            pm.RemovePlayer(pm.Players[i]);
                            pm.RemoveAfter();
                        }
                    }
                }

            }
        }
        if (Equip == true)
        {
            Vector3 v2 = transform.position;
            if(equip_type=="helmet")
                v2.y += 1.5f;
            else
                v2.y += 3.5f;
            equip.transform.position = v2;
        }
        if (act==ACT.IDLE)
       {
            anim.SetBool("attack", false);
            anim.SetBool("run", false);
          //  if (pm.Players[pm.CurTurnIdx] == this)
         //  {
        //      ///MapManager.GetInst().SetHexColor(CurHex, Color.black);
         //  }
            //CurHex.Passable = true;
        }
     
        if (act == ACT.MOVING)
        {//이동처리
            CameraManager.GetInst().ResetCameraTarget();
            CurHex.Passable = true;
           
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
            v.y += 1.0f;
            float distance = Vector3.Distance(transform.position, v);
            if (distance >= 0.2f) //이동중
            {

                anim.SetBool("run", true);
                
                transform.position += (v - transform.position).normalized * status.MoveSpeed * Time.smoothDeltaTime;
                if (jump == false)
                {

                    Quaternion s = Quaternion.LookRotation((v - transform.position).normalized);
                    Vector3 r = s.eulerAngles;
                    r.y -= 90;
                    if (Equip == true)
                        equip.transform.rotation = Quaternion.Euler(r);

                    transform.rotation = Quaternion.Euler(r);
                }

            }
            else //다음 목표 hex에 도착함
            {
                anim.SetBool("run", false);
                Hex temp = nextHex;

                
                v = nextHex.transform.position;
                  v.y += 1.0f;
                  transform.position = v;
           
                MoveHexes.RemoveAt(0);
                
                if (MoveHexes.Count <= 0)//최종 dest
                {
                    Manager.GetInst().MoveCamPos(nextHex);
                    CameraManager.GetInst().InitPos.x = nextHex.x;
                    CameraManager.GetInst().InitPos.z = nextHex.z;
                    CostManager.GetInst().CostDecrease(1);
                    Point temppos = new Point(nextHex.MapPos.GetX(),0, nextHex.MapPos.GetZ());

                    CurHex = MapManager.GetInst().Map[temppos.GetX()][temppos.GetY()][temppos.GetZ()];
                    CurHex.Passable = false;
                    act = ACT.IDLE;
                    if (CurHex.obj_id == 5)
                        EquipHelmet();
                    //PlayerManager.GetInst().TurnOver();
                }

            }
        }

    }

}
