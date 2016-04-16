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
    SUMMONES
}
public class PlayerBase : MonoBehaviour {
    public PlayerStatus status;
    public Animator anim;
    public Hex CurHex;
    public ACT act;
    public bool main_char;
    public bool live;
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


}
