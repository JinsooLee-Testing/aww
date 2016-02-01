using UnityEngine;
using System.Collections;

public class UserPlayer : PlayerBase
{

    void Awake()
    {
        act = ACT.IDLE;
    }
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
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

    public void DrawCommand()
    {
        Rect rect = new Rect(0, Screen.height / 2, 100, 50);
        if (GUI.Button(rect, "Move"))
        {
            Debug.Log("Move");
            act = ACT.MOVEHILIGHT;
            if (MapManager.GetInst().HilightMoveRange(CurHex, MoveRange))
            {

            }
        }
    }
}
