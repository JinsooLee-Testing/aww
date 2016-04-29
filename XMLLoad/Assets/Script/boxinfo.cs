using UnityEngine;
using System.Collections;

public class boxinfo : MonoBehaviour {
    public int X;
    public int Y;
    public int Z;
    public int mat_id = 0;
    public bool Passable;

	// Use this for initialization
    void Awake()
    {
        Passable = true;
    }
	void Start () {
	
	}
    void OnMouseOver()
    {

    }
    // Update is called once per frame
    void Update () {
	
	}
    public void SetMapPos(int x,int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    public void SetPassable(bool passable)
    {
        Passable = passable;
    }
    void OnMouseDown()
    {
       
        if (GUIMgr.GetInst().structable == true)
        {
            GUIMgr.GetInst().CurStruct = (GameObject)GameObject.Instantiate(GUIMgr.GetInst().CurStruct);
            Vector3 v = transform.position;
            v.y = 1;
            GUIMgr.GetInst().CurStruct.transform.position = v;
        }

        GetComponent<Renderer>().material = GUIMgr.GetInst().Curmat;
        mat_id=GUIMgr.GetInst().CurMatIdx;
        if (GUIMgr.GetInst().passble == false)
        {
            if(Passable==true)
            {
                Passable = false;
                GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                Passable = true;
                GetComponent<Renderer>().material.color = Color.white;
            }
        }



    }
}
