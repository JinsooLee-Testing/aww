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
    public PlayerStatus status;
    public Hex CurHex;
    public ACT act;
    public List<Hex> MoveHexes;
    void Awake()
    {
        act = ACT.IDLE;
        status = new PlayerStatus();
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
                transform.position += (nextHex.transform.position - transform.position).normalized * status.MoveSpeed * Time.smoothDeltaTime;
                
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
    public void GetDamage(int damage)
    {
       status.Curhp -= damage;
       if (status.Curhp <= 0)
        {
            Debug.Log("Died");
            PlayerManager.GetInst().RemovePlayer(this);
        }

    }
    public virtual void DrawCommand()
    {
     
    }

}
