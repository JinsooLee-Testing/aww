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
            if(normalAttackTime>=1.00f)
            {
                normalAttackTime = 0f;
                Debug.Log("attack!!" + attacker.status.Name + "to" + defender.status.Name);
                defender.GetDamage(attacker.status.Attack);
                EffectManager.GetInst().ShowEffect(defender.gameObject);
                SoundManager.GetInst().PlayAttackSound();
                defender.CurHex.Passable = false;
                attacker.CurHex.Passable = false;
                
                //EffectManager.GetInst().ShowDamage(defender.CurHex,attacker.status.Attack);
                PlayerManager.GetInst().SetTurnOverTime(0.9f);
            }
        }
	
	}

    public void AttackAtoB(PlayerBase a,PlayerBase b)
    {
        Vector3 v = a.transform.position;
        v.y = 1.0f;
        Vector3 v2 = b.CurHex.transform.position;
        v2.y = PlayerManager.GetInst().m_y;

        
        a.transform.rotation = Quaternion.LookRotation((v2 - v).normalized);
        Vector3 r = a.transform.rotation.eulerAngles;
        r.y -= 90;
        if(a.status.Name!="chick")
         a.transform.rotation = Quaternion.Euler(r);

        a.anim.SetBool("attack", true);
        // a.anim.SetBool("Attack",true);
        a.act = ACT.ATTACKING;
        normalAttackTime = Time.smoothDeltaTime;
         attacker = a;
        defender = b;
    }
}
