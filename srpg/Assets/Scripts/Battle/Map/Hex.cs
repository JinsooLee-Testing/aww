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
    public int matid;
    public bool Passable = true;
    public bool isonTotile = true;
    public int x, y, z;
    public float object_y;
    public Material mat1;
    public Material mat2;
    public Material mat3;
    public Color mat_color = Color.white;
	// Use this for initialization
    void Start()
    {
    
        if (matid == 0)
        {
               GetComponent<Renderer>().material = mat1;
                
        }
        if (matid == 1)
        {
            GetComponent<Renderer>().material = mat1;
           
        }
        if (matid == 2)
        {
            GetComponent<Renderer>().material = mat2;
           
        }
        if (matid == 3)
        {
            GetComponent<Renderer>().material = mat3;
        }
       
    }
    
	// Update is called once per frame
	void Update () {
        RaycastHit hit = new RaycastHit();
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began) || Input.GetTouch(i).phase.Equals(TouchPhase.Moved) || Input.GetTouch(i).phase.Equals(TouchPhase.Stationary))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                if (Physics.Raycast(ray, out hit))
                {
                    MapManager.GetInst().MarkTile(MapPos, 0);
                    
                }

            }
        }
    }
 
    public void SetMapPos(Point pos)
    {
        pos.SetY((float)1);
        MapPos = pos;
        
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
        MapManager.GetInst().MarkTile(MapPos,0);
    }

    void OnMouseDown()
    {
        
        PlayerManager pm = PlayerManager.GetInst();
        PlayerBase pb = pm.Players[pm.CurTurnIdx];
        Debug.Log(MapPos + "OnMouseDown");
        
            if (pb.act == ACT.SUMMONES)
            {
                if (Passable == true && (GetComponent<Renderer>().material.color == Color.green) || (GetComponent<Renderer>().material.color == Color.gray))
                {
                    PlayerManager.GetInst().GenPlayer(MapPos.GetX(), MapPos.GetZ());
                    pb.act = ACT.IDLE;
                    MapManager.GetInst().ResetMapColor();
                }
            }
            if (pb.act == ACT.IDLE)
            {
                Vector3 v = transform.position;
                v = new Vector3(v.x, 2, v.z);
                /// magic.GetInst().SetTarget(v);


            }
            else if (pb.act == ACT.MOVEHILIGHT)
            {
                pm.MovePlayer(pm.Players[pm.CurTurnIdx].CurHex, this);

            }

        }
    
}
