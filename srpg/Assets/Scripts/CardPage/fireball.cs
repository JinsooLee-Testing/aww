using UnityEngine;
using System.Collections;

public class fireball : MonoBehaviour {
    public Vector3 target;
    public bool fire = false;
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

                }
                else //다음 목표 hex에 도착함
                {
                transform.position = target;
                magic.GetInst().fired = false;
                magic.GetInst().targetAI.GetDamage(180);
                EffectManager.GetInst().ShowEffect_Fire(targetHex.gameObject, this.gameObject);
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
