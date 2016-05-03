using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Point
{
    int X;
    float Y;
    int Z;
    public void SetY(float y)
    {
        Y = y;
    }
    public int GetX()
    {
        return X;
    }
    public int GetY()
    {
        return (int)Y;
    }
    public int GetZ()
    {
        return Z;
    }
    public Point(int x, float y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    public override string ToString()
    {
        return "["+X+" "+Y+" "+Z+"]";
    }
    public static Point operator +(Point p1,Point p2)
    {
       return new Point(p1.GetX() + p2.GetX(), p1.GetY() + p2.GetY(), p1.GetZ() + p2.GetZ());
    }
    public static bool operator ==(Point p1,Point p2)
    {
        return (p1.GetX() == p2.GetX() && p1.GetY() == p2.GetY() && p1.GetZ() == p2.GetZ());
    }
    public static bool operator !=(Point p1, Point p2)
    {
        return (p1.GetX() != p2.GetX() && p1.GetY() != p2.GetY() && p1.GetZ() != p2.GetZ());
    }
    public static Point operator -(Point p1, Point p2)
    {
        return new Point(p1.GetX() - p2.GetX(), p1.GetY() - p2.GetY(), p1.GetZ() - p2.GetZ());
    }

}









 




public class Hex : MonoBehaviour {
    public Point MapPos;
    public Mesh mesh;
    public GameObject obj;
    public int obj_id = 0;
    public float obj_y = 1;
    public int matid;
    public bool Passable = true;
    public bool Marked = false;
    public bool mesh_draw = false;
    public string mat_name = "soil";
    public bool At_Marked = false;
    public bool is_object = false;

    public int x, y, z;
    public float object_y;
    public Material mat1;
    public Material mat2;
    public Material mat3;
    
    public Color mat_color = Color.white;
	// Use this for initialization
    void Start()
    {
      
            if (mesh_draw == true)
            {
                GetComponent<MeshFilter>().mesh = mesh;
                if (MapPos.GetY() == 0)
                    SetCol();

            }
            else
            {

                GetComponent<MeshFilter>().mesh = null;
                Passable = false;
                if (MapPos.GetY() == 0)
                    SetCol();
            }
        
        if (obj_id != 0)
        {

            obj = (GameObject)GameObject.Instantiate(Object_Manager.GetInst().Structures[obj_id - 1]);
            Vector3 v = transform.position;
            obj.transform.position = new Vector3(v.x, obj_y-0.2f, v.z);
            
        }
        if (mat_name == "soil")
        {
            GetComponent<Renderer>().material = mat1;

        }
        if (mat_name == "grass")
        {
          GetComponent<Renderer>().material = mat2;
                
        }
        if (mat_name == "fire")
        {
            GetComponent<Renderer>().material = mat3;

        }

    }
    
	// Update is called once per frame
	void Update () {
        /*
        RaycastHit hit = new RaycastHit();
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began) || Input.GetTouch(i).phase.Equals(TouchPhase.Moved) || Input.GetTouch(i).phase.Equals(TouchPhase.Stationary))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                if (Physics.Raycast(ray, out hit))
                {
                    MapManager.GetInst().MarkTile(MapPos, 0,this);
                    
                }

            }
        }
        */
    }
 
    public void SetMapPos(Point pos)
    {
        pos.SetY((float)1);
        MapPos = pos;
        
    }
    public void SetCol()
    {
        GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
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
    public void SetMat(int id)
    {
        matid = id;
    }

    public void SetMapPos(int x,float y,int z)
    {
        MapPos = new Point(x, y, z);
    }
    void OnMouseOver()
    {

        //MapManager.GetInst().MarkTile(MapPos,0,this);
    }
    
    void OnMouseDown()
    {
        
        PlayerManager pm = PlayerManager.GetInst();
        PlayerBase pb = pm.Players[pm.CurTurnIdx];
        Debug.Log(MapPos + "OnMouseDown");

        
      
        if (pb.act==ACT.MAGIC)
        {
            if (magic.GetInst().type == "wall")
            {
                MapManager.GetInst().MarkWall(this.MapPos);
            }
            if(GetComponent<Renderer>().material.color==Color.green)
            {
               
                if (MapManager.GetInst().wallpos.GetX()>=MapPos.GetX())
                {
                    for (int i = 0; i < 3; ++i)
                    {
                        if (MapManager.GetInst().Map[MapPos.GetX() - i][MapPos.GetY()][MapPos.GetZ()].Passable == true)
                        {
                            obj = (GameObject)GameObject.Instantiate(Object_Manager.GetInst().Structures[6]);
                            Vector3 v = transform.position;
                            obj.transform.position = new Vector3(v.x, 1, v.z - i);

                            obj = (GameObject)GameObject.Instantiate(Object_Manager.GetInst().Structures[6]);
                            MapManager.GetInst().Map[MapPos.GetX()][MapPos.GetY()][MapPos.GetZ() - i].Passable = false;
                        }
                    }
                    
                    //obj.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                    MapManager.GetInst().ResetMapColor();
                    CameraManager.GetInst().ResetCameraTarget();

                }
                else
                {
                    for (int i = 0; i < 3; ++i)
                    {
                        if (MapManager.GetInst().Map[MapPos.GetX() - i][MapPos.GetY()][MapPos.GetZ()].Passable == true)
                        {
                            obj = (GameObject)GameObject.Instantiate(Object_Manager.GetInst().Structures[6]);
                            Vector3 v = transform.position;
                            obj.transform.position = new Vector3(v.x - i, 1, v.z);
                            obj.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                            obj = (GameObject)GameObject.Instantiate(Object_Manager.GetInst().Structures[6]);
                            MapManager.GetInst().Map[MapPos.GetX() - i][MapPos.GetY()][MapPos.GetZ()].Passable = false;
                        }
                    }
                    MapManager.GetInst().ResetMapColor();
                    CameraManager.GetInst().ResetCameraTarget();
                    
                }
            }
        }
        if (pb.act == ACT.SUMMONES)
          {
                if (Passable == true && (GetComponent<Renderer>().material.color == Color.green) || (GetComponent<Renderer>().material.color == Color.gray))
                {
                    PlayerManager.GetInst().GenPlayer(MapPos.GetX(), MapPos.GetZ());
                    pb.act = ACT.IDLE;
                EffectManager.GetInst().ShowEffect_Summon(pb.CurHex.gameObject,3,1.2f);
                CameraManager.GetInst().ResetCameraTarget();
                MapManager.GetInst().ResetMapColor();
                }
          
        }
            if (pb.act == ACT.IDLE)
           {            
           }
            else if (pb.act == ACT.MOVEHILIGHT)
            {
            if(Passable==true)
                pm.MovePlayer(pm.Players[pm.CurTurnIdx].CurHex, this);

            }

        }
    
}
