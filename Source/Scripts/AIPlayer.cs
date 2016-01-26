using UnityEngine;
using System.Collections;

public class AIPlayer : PlayerBase
{

    void Awake()
    {
        act = ACT.IDLE;
        MoveRange = 5;
    }
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if(act==ACT.IDLE)
        {
            PlayerManager pm = PlayerManager.GetInst();
            if(pm.Players[pm.CurTurnIdx]==this)
            { 
                AiProc();
            }
        }
        if (act == ACT.MOVING)
        {//이동처리
            Hex nextHex = MoveHexes[0];
            float distance = Vector3.Distance(transform.position, nextHex.transform.position);
            if (distance > 0.1f) //이동중
            {
                transform.position += (nextHex.transform.position - transform.position).normalized * MoveSpeed * Time.smoothDeltaTime;

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
    public void AiProc()
    {
        AIthink ai = AIthink.GetInst();
        //근점 플레이어찾는과정 추가내용 
        ai.MoveToNearUserPlayer(this);
    }
    void OnMouseDown()
    {
        PlayerManager pm = PlayerManager.GetInst();
        if (pm.Players[pm.CurTurnIdx].act==ACT.ATTACKHIGHLIGHT)
        {
            Debug.Log("attack!!");
            pm.TurnOver();
        }

    }
}
