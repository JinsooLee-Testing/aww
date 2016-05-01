using UnityEngine;
using System.Collections;

public class boxinfo : MonoBehaviour
{
    public int X;
    public int Y;
    public int Z;
    public string mat_name = "soil";
    public bool Passable;
    public int objId = 0;
    public float y = 1;
    public Mesh mesh;
    public GameObject obj;
    public bool objn = false;
    public bool mesh_draw = false;

    // Use this for initialization
    void Awake()
    {
        Passable = true;

    }
    void Start()
    {
        
        if (objId != 0)
        {
    
            obj = (GameObject)GameObject.Instantiate(GUIMgr.GetInst().Structures[objId - 1]);
            Vector3 v = transform.position;
            obj.transform.position = new Vector3(v.x, y, v.z);
            Debug.Log(y);
        }
        if (mesh_draw == false)
        {
            GetComponent<MeshFilter>().mesh = null;
            SetCol(new Vector3(0, 0, 0));
        }
        if (mesh_draw==true)
        {
            SetCol(new Vector3(1,1,1));
            GetComponent<MeshFilter>().mesh = mesh;
        }
        if (mat_name == "grass")
            GetComponent<Renderer>().material = GUIMgr.GetInst().mat[1];
        if (mat_name == "fire")
            GetComponent<Renderer>().material = GUIMgr.GetInst().mat[2];
        if (mat_name == "soil")
            GetComponent<Renderer>().material = GUIMgr.GetInst().mat[0];
    }
    void OnMouseOver()
    {

    }
    public void SetObj(int id)
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void SetMapPos(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    public void SetPassable(bool passable)
    {
        Passable = passable;
    }
    public void SetMesh()
    {
        GetComponent<MeshFilter>().mesh = mesh;
        Vector3 v = GetComponent<BoxCollider>().size;
        v.x = 1;
        v.y = 1;
        v.z = 1;
        mesh_draw = true;
        GetComponent<BoxCollider>().size = v;
    }
    public void SetCol(Vector3 size)
    {
        GetComponent<BoxCollider>().size = size;
    }
    void OnMouseDown()
    {

        if (GUIMgr.GetInst().y_draw == true)
        {
            GetComponent<MeshFilter>().mesh = mesh;
            mesh_draw = true;
        }
        else
        {
            GetComponent<MeshFilter>().mesh = null;
            mesh_draw = false;
        }

        if (GUIMgr.GetInst().structable == true)
        {

            obj = (GameObject)GameObject.Instantiate(GUIMgr.GetInst().CurStruct);

            Vector3 v = transform.position;
            v.y = float.Parse(GUIMgr.GetInst().y);

            
            v.y+=1.0f;
            y = v.y;
            Passable = false;
            obj.transform.position = new Vector3(v.x, y, v.z);
            y = v.y - 1;
            objId = GUIMgr.GetInst().CurStructIdx+1;
        }
        else
        {
            if (obj != null)
                Destroy(obj);
        }
        GetComponent<Renderer>().material = GUIMgr.GetInst().Curmat;
        if (GUIMgr.GetInst().CurMatIdx == 1)
            mat_name = "grass";
        else if (GUIMgr.GetInst().CurMatIdx == 2)
            mat_name = "fire";
        else
            mat_name = "soil";
        if (GUIMgr.GetInst().passble == false)
        {
            if (Passable == true)
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
