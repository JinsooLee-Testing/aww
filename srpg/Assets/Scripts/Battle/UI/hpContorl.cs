using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class hpContorl : MonoBehaviour {

    private static hpContorl inst = null;
    public static hpContorl GetInst()
    {
        return inst;
    }
    public Vector3 InitPos;
    public Vector3 InitScale;
    public List<Vector3> pos;
    public List<Vector3> Local;
    // Use this for initialization
 
    void Awake()
    {
        inst = this;
        transform.position = InitPos;
        transform.localScale = InitScale;



    }
    void ReSetPos()
    {
        transform.position = pos[0];
        transform.localScale = Local[0];
    }
    void Start () {
        for (int i = 0; i <= 10; ++i)
        {
            pos.Add(new Vector3(InitPos.x, InitPos.y, InitPos.z));
            InitPos.x -= 0.3f;
        }
        for (int i = 0; i <= 10; ++i)
        {
            Local.Add(new Vector3(InitScale.x, InitScale.y, InitScale.z));
            InitScale.x += 0.1f;
        }



    }
    public void SetPos()
    {
        ReSetPos();
        float barpos = (PlayerManager.GetInst().select_object.status.Curhp / PlayerManager.GetInst().select_object.status.Maxhp) * 100.0f;

        barpos /= 10;
        
        int percent = (int)barpos;
  
        transform.position = pos[10-percent];
        transform.localScale = Local[10 - percent];

    
    }
    public void SetDIePos()
    {
        ReSetPos();

  
        transform.position = pos[10];
        transform.localScale = Local[10];


    }
    // Update is called once per frame
    void Update () {
        if(PlayerManager.GetInst().select_object.act!=ACT.DIYING)
            SetPos();
        else
        {
            SetDIePos();
        }

    }
}
