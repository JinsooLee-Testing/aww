using UnityEngine;
using System.Collections;

public class fireball : MonoBehaviour {
    public Vector3 target;
    public bool fire = false;
    public int effect = 1;
    public Hex targetHex;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

            if (PlayerManager.GetInst().Players[PlayerManager.GetInst().CurTurnIdx].act ==ACT.MAGIC)
           {
                MapManager.GetInst().ResetMapColor();
                float distance = Vector3.Distance(transform.position, target);
                magic.GetInst().fired = true;
                 CameraManager.GetInst().ResetCameraTarget();
            if (distance >= 0.1f) //이동중
            {


                transform.position += (target - transform.position).normalized * 7 * Time.smoothDeltaTime;
                transform.rotation = Quaternion.LookRotation((target - transform.position).normalized);
                Vector3 r = transform.rotation.eulerAngles;
                r.y -= 90;
                //transform.rotation = Quaternion.Euler(r);

            }
            else //다음 목표 hex에 도착함
            {
                transform.position = target;
                magic.GetInst().fired = false;

                MapManager.GetInst().MarkAttackRange(magic.GetInst().targetAI.CurHex, 2);
                PlayerManager pm = PlayerManager.GetInst();

                if (effect == 1)
                {
                    EffectManager.GetInst().ShowEffect_Fire(targetHex.gameObject, this.gameObject);
                    magic.GetInst().targetAI.GetDamage(180);
                }
                else
                {
                    for (int i = 0; i < pm.Players.Count; ++i)
                    {
                        if (pm.Players[i].CurHex.At_Marked == true)
                        {
                            if (pm.Players[i].m_type == Type.MONSTER)
                            {
                                pm.Players[i].GetDamage(100);
                                EffectManager.GetInst().ShowEffect_water(pm.Players[i].gameObject, this.gameObject);
                            }
                        }
                    }
                    magic.GetInst().targetAI.GetDamage(100);
                    EffectManager.GetInst().ShowEffect_water(targetHex.gameObject, this.gameObject);
                }
                PlayerManager.GetInst().Players[PlayerManager.GetInst().CurTurnIdx].act = ACT.IDLE;
                CostManager.GetInst().CostDecrease(1);
               

                }
                // fire = false;
          }
        
    }
    void OnMouseDown()
    {

    }
}
