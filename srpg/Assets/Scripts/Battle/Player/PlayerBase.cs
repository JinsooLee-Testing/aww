using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum ACT
{
    IDLE,
    MOVEHILIGHT,
    MOVING,
    ATTACKHIGHLIGHT,
    ATTACKING,
    SUMMONES,
    DIYING
}
public enum Type
{
    USER,
    MONSTER,
    OBJECT,
       MAINCHARACTER
}
public class PlayerBase : MonoBehaviour {
    public PlayerStatus status;
    public Animator anim;
    public Hex CurHex;
    public ACT act;
    public float removeTime = 0;
    public bool main_char;
    public bool live;
    public List<Hex> MoveHexes;
    public Type m_type;
    void Awake()
    {
        act = ACT.IDLE;
        status = new PlayerStatus();
  
        
    }
	void Start () {
	}
	// Update is called once per frame
	void Update () {

	}
    public void GetDamage(int damage)
    {
       status.Curhp -= damage;
       if (status.Curhp <= 0)
        {
            act = ACT.DIYING;
            if (m_type == Type.MONSTER)
                live = false;
            removeTime += Time.deltaTime;
            //PlayerManager.GetInst().RemovePlayer(this);
        }

    }


}
