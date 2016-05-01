﻿using UnityEngine;
using System.Collections;

public class UserPlayer : PlayerBase
{
    public int hp;
    public string[] na;
    public int x=0;
    public int y=0;
    public int z=0;
   
    void Awake()
    {
        anim = GetComponent<Animator>();
        act = ACT.IDLE;
        status = new PlayerStatus();
        status.Curhp = 150;
        main_char = true;
        live = true;
        m_type = Type.USER;
        
    }
    void Start()
    {
    }
    // Update is called once per frame
    void OnMouseDown()
    {
        PlayerManager.GetInst().select_object = this;
              PlayerManager.GetInst().SetPickPos(this); 
    }
    public void DrawStatus()
    {
      
    }
    void Update()
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

                        }
                        else
                        {
                            pm.RemoveAfter();
                            pm.RemovePlayer(pm.Players[i]);
           
                        }
                    }
                }

            }
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
            if (distance >= 0.1f) //이동중
            {

                anim.SetBool("run", true);
                
                transform.position += (v - transform.position).normalized * status.MoveSpeed * Time.smoothDeltaTime;
                if (jump == false)
                {
                    Quaternion s = Quaternion.LookRotation((v - transform.position).normalized);
                    Vector3 r = s.eulerAngles;
                    r.y -= 90;
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
                   
                    //PlayerManager.GetInst().TurnOver();
                }

            }
        }

    }

}
