using UnityEngine;
using System.Collections;

public class UserPlayer : PlayerBase
{
   
    void Awake()
    {
        anim = GetComponent<Animator>();
        act = ACT.IDLE;
        status = new PlayerStatus();
        main_char = true;
        live = true;
        m_type = Type.USER;
    }
    void Start()
    {
    }
    // Update is called once per frame
    void onMouseDown()
    {
       
    }
    public void DrawStatus()
    {
      
    }
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
            Vector3 v = nextHex.transform.position;
           v.y = 1.0f;
            float distance = Vector3.Distance(transform.position, v);
            if (distance > 0.1f) //이동중
            {
                anim.SetBool("running", true);
                transform.position += (v - transform.position).normalized * status.MoveSpeed * Time.smoothDeltaTime;             
                transform.rotation = Quaternion.LookRotation((v - transform.position).normalized);
                Vector3 r = transform.rotation.eulerAngles;
                r.y -= 90;
                transform.rotation =Quaternion.Euler(r);
           
 

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
                    //PlayerManager.GetInst().TurnOver();
                }

            }
        }

    }

}
