using UnityEngine;
using System.Collections;
public class Point
{
    int X;
    int Y;
    int Z;
    public int GetX()
    {
        return X;
    }
    public int GetY()
    {
        return Y;
    }
    public int GetZ()
    {
        return Z;
    }
    public Point(int x,int y ,int z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    public override string ToString()
    {
        return "["+X+" "+Y+" "+Z+"]";
    }
    public static Point operator *(Point p1,Point p2)
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
    public Material mat1;
    public Material mat2;
    public Material mat3;
    public int onto = 0;
	// Use this for initialization
    void Start()
    {
        matid = 1;
        GetComponent<Renderer>().material = mat1;
 
    }
    
	// Update is called once per frame
	void Update () {
       
        if (Input.GetKeyDown(KeyCode.R))
        //만약 GetKeyDoen(KeyCode.키값입력)안에 키값이 입력된다면,
        {

            GetComponent<Renderer>().material = mat3;
        }


	}
 
    public void SetMapPos(Point pos)
    {
        MapPos = pos;
    }

    public void SetColor(int on)
    {
        if (on == 1)
        {

            GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (on==2)
        {

            GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if(on==4)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {

            GetComponent<Renderer>().material.color = Color.white;
        }
        
    }
    public void SetMat(int id)
    {
        matid = id;
        if (matid == 1)
            GetComponent<Renderer>().material = mat1;
        if (matid == 2)
            GetComponent<Renderer>().material = mat2;
        if (matid == 3)
            GetComponent<Renderer>().material = mat3;

    }
    public void SetMapPos(int x,int y,int z)
    {
        MapPos = new Point(x, y, z);
    }
    void OnMouseDown()
    {
        PlayerManager pm = PlayerManager.GetInst();
        PlayerBase pb = pm.Players[pm.CurTurnIdx];
        Debug.Log(MapPos + "OnMouseDown");
        if(pb.act==ACT.IDLE)
        {
            if(Passable==true)
            {
                GetComponent<Renderer>().material.color = Color.yellow;
                GetComponent<Renderer>().material = mat2;
                onto = 2;
            }
            else
            {
                GetComponent<Renderer>().material.color = Color.white;
                GetComponent<Renderer>().material = mat1;
                onto = 3;
            }
            Passable=!Passable;
        }
        else if(pb.act==ACT.MOVEHILIGHT)
        {
             pm.MovePlayer(pm.Players[pm.CurTurnIdx].CurHex, this);
        }
    }
}
