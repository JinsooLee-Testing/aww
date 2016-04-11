using UnityEngine;
using System.Collections;

public class BattleManager:MonoBehaviour {
    private static BattleManager inst = null;

    private float normalAttackTime = 0f;
    private PlayerBase attacker = null;
    private PlayerBase defender = null;
    public static BattleManager GetInst()
    {
        return inst;
    }

    void Awake()
    {
        inst = this;
    }
    void start()
    {

    }
	// Update is called once per frame
	void Update () {
        if(normalAttackTime!=0)
        {
            normalAttackTime += Time.smoothDeltaTime;
            if(normalAttackTime>=0.018f)
            {
                normalAttackTime = 0f;
                Debug.Log("attack!!" + attacker.status.Name + "to" + defender.status.Name);
                defender.GetDamage(80);
                PlayerManager.GetInst().SetTurnOverTime(0.2f);
            }
        }
	
	}
    public void AttackAtoB(PlayerBase a,PlayerBase b)
    {
        a.transform.rotation=Quaternion.LookRotation((b.CurHex.transform.position-a.transform.position).normalized);
        Vector3 r = transform.rotation.eulerAngles;
        r.y -= 90;
        a.transform.rotation = Quaternion.Euler(r);
        //a.anim.SetBool("Attack",true);
        a.act = ACT.ATTACKING;
        normalAttackTime = Time.smoothDeltaTime;
        attacker = a;
        defender = b;
    }
}
