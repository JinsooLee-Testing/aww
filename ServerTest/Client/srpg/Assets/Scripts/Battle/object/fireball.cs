using UnityEngine;
using System.Collections;

public class fireball : MonoBehaviour
{
    public Vector3 target;
    public bool fire = false;
    public int effect = 1;
    public Hex targetHex;
    public int damage;
    public float ef_time = 0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ef_time += Time.deltaTime;
        // if (PlayerManager.GetInst().Players[PlayerManager.GetInst().CurTurnIdx].act ==ACT.MAGIC)
        //{

        MapManager.GetInst().ResetMapColor();

        float distance = Vector3.Distance(transform.position, target);
        magic.GetInst().fired = true;
        CameraManager.GetInst().ResetCameraTarget();
        if (distance > 1f) //이동중
        {
            transform.position += (target - transform.position).normalized * 7 * Time.smoothDeltaTime;

            if (ef_time > 0.2)
            {
                EffectManager.GetInst().ShowEffect(this.transform, effect);
                ef_time = 0f;
            }

            target.y = 1;
            if (effect == 1)
            {
                transform.rotation = Quaternion.LookRotation((target - transform.position).normalized);
                Vector3 r = transform.rotation.eulerAngles;
                r.x -= 90;
                transform.rotation = Quaternion.Euler(r);
            }
            else
            {
                if (magic.GetInst().curmagic_id != 6)
                {
                    transform.rotation = Quaternion.LookRotation((target - transform.position).normalized);
                    Vector3 r = transform.rotation.eulerAngles;
                    r.y -= 90;
                    transform.rotation = Quaternion.Euler(r);
                }
            }


        }
        else //다음 목표 hex에 도착함
        {
            transform.position = target;
            magic.GetInst().fired = false;

            MapManager.GetInst().MarkAttackRange(magic.GetInst().targetAI.CurHex, 2);
            PlayerManager pm = PlayerManager.GetInst();


            if (effect == 1)
            {
                if (magic.GetInst().act != ACT.HIT)
                {
                    EffectManager.GetInst().ShowEffect_Fire(targetHex.gameObject, this.gameObject);
                    if (magic.GetInst().curmagic_id == 1)
                        magic.GetInst().targetAI.GetDamage(damage);
                    else
                        magic.GetInst().targetAI.GetDamage(100);
                    CostManager.GetInst().CostDecrease(CostManager.GetInst().Curcostnum);
                }
                else
                    Destroy(this.gameObject);

                magic.GetInst().act = ACT.HIT;
            }
            else
            {
                for (int i = 0; i < pm.Players.Count; ++i)
                {
                    if (pm.Players[i].CurHex.At_Marked == true)
                    {
                        if (pm.Players[i].m_type == Type.MONSTER)
                        {
                            pm.Players[i].GetDamage(damage);
                            EffectManager.GetInst().ShowEffect_water(pm.Players[i].gameObject, this.gameObject, 4);
                        }
                    }
                }
                Destroy(this.gameObject);
                magic.GetInst().targetAI.GetDamage(damage);
                if (magic.GetInst().act != ACT.HIT)
                {
                    EffectManager.GetInst().ShowEffect_water(targetHex.gameObject, this.gameObject, 4);
                    CostManager.GetInst().CostDecrease(CostManager.GetInst().Curcostnum);

                }
                if (magic.GetInst().act != ACT.HIT)
                    magic.GetInst().act = ACT.HIT;


            }
            //PlayerManager.GetInst().Players[PlayerManager.GetInst().CurTurnIdx].act = ACT.IDLE;
            MapManager.GetInst().ResetMapColor();
        }
        // fire = false;
        //   }

    }
    void OnMouseDown()
    {

    }
}
