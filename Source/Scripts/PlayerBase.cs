using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum ACT
{
    IDLE,
    MOVEHILIGHT,
    MOVING,
    ATTACKHIGHLIGHT
}
public class PlayerBase : MonoBehaviour {
    public Hex CurHex;
    public int MoveRange = 4;
    public float MoveSpeed = 5.0f;
    public int attackRange = 2;
    public ACT act;
    public List<Hex> MoveHexes;
    void Awake()
    {
        act = ACT.IDLE;
    }
	void Start () {
	}
	// Update is called once per frame
	void Update () {
        if (act == ACT.MOVING)
        {//이동처리
            Hex nextHex = MoveHexes[0];
            float distance=Vector3.Distance(transform.position,nextHex.transform.position);
            if(distance>0.1f) //이동중
            {
                transform.position += (nextHex.transform.position - transform.position).normalized * MoveSpeed * Time.smoothDeltaTime;
                
            }
            else //다음 목표 hex에 도착함
            {
                transform.position = nextHex.transform.position;
                MoveHexes.RemoveAt(0);
                if(MoveHexes.Count==0)//최종 dest
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
        float btnW = 100f;
        float btnH = 25f;
        Rect rect = new Rect(0, Screen.height / 2, btnW, btnH );
        if (GUI.Button(rect, "Move"))
        {
            Debug.Log("Move");

            if (MapManager.GetInst().HilightMoveRange(CurHex, MoveRange))
            {
                act = ACT.MOVEHILIGHT;
            }
        }
        rect = new Rect(0, (Screen.height /2)+20, btnW, btnH);
        if (GUI.Button(rect, "Attack"))
        {
            Debug.Log("Attack");

            if (MapManager.GetInst().HilightAttackRange(CurHex, 2))
            {
                act = ACT.ATTACKHIGHLIGHT;
            }
        }
    }

}
