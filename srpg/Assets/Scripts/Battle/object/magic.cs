using UnityEngine;
using System.Collections;

public class magic : MonoBehaviour
{


    private static magic inst = null;
    public Vector3 target;
    public static magic GetInst()
    {
        return inst;
    }
    public Vector3 v;
    public bool fire = false;
    void Awake()
    {
        target = new Vector3(0, 1, 0);
        inst = this;
    }
    void Start()
    {

        transform.position = v;
    }
   public void SetTarget(Vector3 v)
    {
        target = v;

        fire = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (fire == true)
        {
            float distance = Vector3.Distance(transform.position, target);
            if (distance > 0.1f) //이동중
            {

                transform.position += (target - transform.position).normalized * 8 * Time.smoothDeltaTime;
            }
           // fire = false;
        }
        else //다음 목표 hex에 도착함
        {

            transform.position = target;

        }
    }
}
