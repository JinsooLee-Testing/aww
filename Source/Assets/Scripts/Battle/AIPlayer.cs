﻿using UnityEngine;
using System.Collections;


public class AIPlayer : PlayerBase
{
    
    void Awake()
    {
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
            if(MoveHexes.Count==0)
            {
                act = ACT.IDLE;
                PlayerManager.GetInst().TurnOver();
                return;

            }
            Hex nextHex = MoveHexes[0];
            Vector3 v = nextHex.transform.position;
            v.y = 1.0f;
            float distance = Vector3.Distance(transform.position, v);
            if (distance > 0.1f) //이동중
            {

                
                transform.position += (v - transform.position).normalized * status.MoveSpeed * Time.smoothDeltaTime;    
                transform.rotation = Quaternion.LookRotation((v - transform.position).normalized);
                Vector3 r = transform.rotation.eulerAngles;
                r.y -= 90;
                transform.rotation = Quaternion.Euler(r);
            }
            else //다음 목표 hex에 도착함
            {
                v = nextHex.transform.position;
                v.y = 1.0f;
                transform.position = v;
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
    public void AiProc()
    {
        AIthink ai = AIthink.GetInst();
        //근점 플레이어찾는과정 추가내용 
        //이미 근접상태면 act는 IDLE 유지 이동 필요하면 act는 MOVING으로
        ai.MoveToNearUserPlayer(this);
        if(act==ACT.IDLE)
        {
            ai.AtkAItoUser(this);
        }
       
    }
    void OnMouseDown()
    {
        PlayerManager pm = PlayerManager.GetInst();
        if (pm.Players[pm.CurTurnIdx].act==ACT.ATTACKHIGHLIGHT)
        {
            this.GetDamage(30);
            Debug.Log("attack!!");
            pm.TurnOver();
        }

    }
}
