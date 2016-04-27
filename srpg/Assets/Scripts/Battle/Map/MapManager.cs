using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    private static MapManager inst = null;
    public GameObject GO_hex;

    public int default_matid = 1;
    public float HexW; //Awake에서 설정
    public float HexH; //Awake
    public float BoxH; //Awake
    public int MapSizeX;
    public int MapSizeY;
    public int MapSizeZ;
    public Transform map;

    public int num = 0;
    List<Path> OpenList;
    List<Path> ClosedList;
    int index = 0;
    Vector3[] objpos = new Vector3[150];
    Mark mark;
    public Point[] Dirs;
    // Use this for initialization
    public void initDirs()
    {
        Dirs = new Point[4];
        Dirs[0] = new Point(+1, 0, 0); //right
        Dirs[1] = new Point(-1, 0, 0);  //left
        Dirs[2] = new Point(0, 0, -1);  //down 
        Dirs[3] = new Point(0, 0, 1);  //up
    }

    Hex[][][] Map;


    void Awake()
    {

        inst = this;
        initDirs();

        SetHexSize();
    }

    public static MapManager GetInst()
    {
        return inst;
    }
    // Update is called once per frame
    void Start()
    {
        inst = this;

    }

    void Update()
    {

    }
    void SetHexSize()
    {
        HexW = GO_hex.GetComponent<Renderer>().bounds.size.x;
        HexH = GO_hex.GetComponent<Renderer>().bounds.size.z;
        BoxH = GO_hex.GetComponent<Renderer>().bounds.size.y;
    }

    public Vector3 GetWorldPos(int x, int y, int z)
    {
        float X, Y, Z;
        X = x * HexW;
        Y = y * BoxH;
        Z = (z) * HexH;
        return new Vector3(X, Y, Z);
    }

    public void CreateMap()
    {
      
        Map = new Hex[MapSizeX + 1][][];
        for (int x = 0; x <= MapSizeX; x++)
        {
            Map[x] = new Hex[MapSizeY + 1][];
            for (int y = 0; y <= MapSizeY; y++)
            {
                Map[x][y] = new Hex[MapSizeZ + 1];
                for (int z = 0; z <= MapSizeZ; z++)
                {
                  
                        Map[x][y][z] = ((GameObject)Instantiate(GO_hex)).GetComponent<Hex>();
                        Map[x][y][z].matid = default_matid;

                        Vector3 pos2 = GetWorldPos(x, 0, z);
                        Map[x][y][z].transform.position = pos2;
                        Map[x][y][z].SetMapPos(x, 0, z);
                         Map[x][y][z].Passable = true;

                }
            }
        }
    }
    public void LoadObjMap()
    {
        for (int i = 0; i < map.childCount; ++i)
        {
            var tile = map.GetChild(i).GetComponent<Hex>();
            if (tile != null)
            {
                    Map[tile.x][tile.y][tile.z] = tile;
                    Map[tile.x][tile.y][tile.z].matid = tile.matid;
                    // Map[x][y][z].Passable = tile.Passable;
                    Map[tile.x][tile.y][tile.z].Passable = false;
                    Vector3 pos = GetWorldPos(tile.x, 0, tile.z);
                    pos.y = tile.object_y;
                    Map[tile.x][tile.y][tile.z].transform.position = pos;
                    Map[tile.x][tile.y][tile.z].SetMapPos(tile.x, 0, tile.z);

            }


        }
    }
    public Hex GetPlayerHex(int x, int y, int z)
    {
        return Map[x][y][z];
    }
    public void MarkTile(Point pos, int range)
    {
        int highLighedCount = 0;
        Point start = pos;

        if (PlayerManager.GetInst().Players[PlayerManager.GetInst().CurTurnIdx].act == ACT.IDLE)
        {
            for (int x = 0; x <= MapSizeX; x++)
            {
                for (int y = 0; y <= MapSizeY; y++)
                {
                    for (int z = 0; z <= MapSizeZ; z++)
                    {
                        if (Map[x][y][z].GetComponent<Renderer>().material.color == Color.green)
                            highLighedCount++;
                    }
                }
            }

            // for(int i=0;i<5;++i)
            //  Map[s_x][s_y][s_z+i].GetComponent<Renderer>().material.color = Color.green;



            if (highLighedCount == 0)
            {
                Map[pos.GetX()][pos.GetY()][pos.GetZ()].GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                ResetMapColor();
                Map[pos.GetX()][pos.GetY()][pos.GetZ()].GetComponent<Renderer>().material.color = Color.green;
            }

        }
    }

    public bool HilightMoveRange(Hex start, int moveRange)
    {
        int highLighedCount = 0;

        for (int x = 0; x <= MapSizeX; x++)
        {
            for (int y = 0; y <= MapSizeY; y++)
            {
                for (int z = 0; z <= MapSizeZ; z++)
                {
                    if (Map[x][y][z].Passable == true)
                    {
                        int distance = (GetDistance(start, Map[x][y][z]));
                        //헥사곤 상의 셀과 셀간의 공식

                        if (distance <= moveRange && distance != 0)
                        {//
                            if (IsReachAble(start, Map[x][y][z], moveRange))
                            {
                                if (default_matid == 1)
                                    Map[x][y][z].GetComponent<Renderer>().material.color = Color.green;
                                else
                                {
                                    Map[x][y][z].GetComponent<Renderer>().material.color = Color.gray;
                                }
                                highLighedCount++;
                            }
                        }
                    }
                }
            }
        }
        if (highLighedCount == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool HilightAttackRange(Hex start, int AtkRange)
    {
        int highLighedCount = 0;
        PlayerManager pm = PlayerManager.GetInst();
        for (int x = 0; x <= MapSizeX; x++)
        {
            for (int y = 0; y <= MapSizeY; y++)
            {
                for (int z = 0; z <= MapSizeZ; z++)
                {
   
                        int distance = (GetDistance(start, Map[x][y][z]));


                        if (distance <= AtkRange && distance != 0)
                        {
                            Map[x][y][z].GetComponent<Renderer>().material.color = Color.red;
                              Map[x][y][z].Marked = true;
                            bool isExit = false;
                            foreach (PlayerBase pb in pm.Players)
                            {
                                if (pb.CurHex.MapPos == Map[x][y][z].MapPos)
                                {
                                    isExit = true;
                                    break;
                                }
                            }

                            if (isExit == true)
                            {
                                //if (IsReachAble(start, Map[x][y][z], AtkRange))
                                //{

                                highLighedCount++;
                                //}

                            }

                        

                    }
                }
            }
        }
        if (highLighedCount == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void ResetMapColor()
    {
        for (int x = 0; x <= MapSizeX; x++)
        {
            for (int y = 0; y <= MapSizeY; y++)
            {
                for (int z = 0; z <= MapSizeZ; z++)
                {

                    Map[x][y][z].GetComponent<Renderer>().material.color = Map[x][y][z].mat_color;
                    Map[x][y][z].Marked = false;
                }
            }
        }

    }
    public void ResetMapColor(Point pos)
    {
        int x = pos.GetX();
        int y = pos.GetY();
        int z = pos.GetZ();
        Map[x][y][z].GetComponent<Renderer>().material.color = Map[x][y][z].mat_color;
    }
    public List<Hex> GetPath(Hex start, Hex dest)
    {
        OpenList = new List<Path>();
        ClosedList = new List<Path>();
        List<Hex> rtnVal = new List<Hex>();
        int H = (int)(MapManager.GetInst().GetDistance(start, dest));

        Path startPath = new Path(null, start, 0, H);
        ClosedList.Add(startPath);
        Path result = Recursive_FindPath(startPath, dest);


            while (result.Parent != null)
            {
                rtnVal.Insert(0, result.GetHex());
                result = result.Parent;
            }

        return rtnVal;
    }
    public Path Recursive_FindPath(Path parent, Hex dest)
    {
        if (parent.GetHex().MapPos == dest.MapPos)
        {
            return parent;
        }
        List<Hex> neibhors = GetNeibhors(parent.GetHex());
        foreach (Hex h in neibhors)
        {
            Path nevP = new Path(parent, h, parent.GetDepth() + 1, (int)(MapManager.GetInst().GetDistance(h, dest)));
            AddToOpenList(nevP);
        }
        Path bestP;
        if (OpenList.Count == 0)
        {
            return null; //목적지까지의 거리가업음
        }
        bestP = OpenList[0];
        foreach (Path p in OpenList)
        {
            if (p.GetF() < bestP.GetF())
            {
                bestP = p;
            }
        }
        OpenList.Remove(bestP);

        ClosedList.Add(bestP);
        return Recursive_FindPath(bestP, dest);
    }
    public void AddToOpenList(Path p)
    {
        foreach (Path inP2 in ClosedList)
        {
            if (p.GetHex().MapPos == inP2.GetHex().MapPos)
                return;
        }
        foreach (Path inP in OpenList)
        {
            if (p.GetHex().MapPos == inP.GetHex().MapPos)
            {
                if (p.GetF() < inP.GetF())
                {
                    OpenList.Remove(inP);
                    OpenList.Add(p);
                    return;
                }
            }
        }
        OpenList.Add(p);
    }
    public List<Hex> GetNeibhors(Hex pos)
    {
        List<Hex> rtn = new List<Hex>();
        Point cur = pos.MapPos;
        if (pos.Passable == false)
        {
            return rtn;
        }

        foreach (Point p in Dirs)
        {
            Point tmp = p + cur;
            rtn.Add(MapManager.GetInst().GetHex(tmp.GetX(), tmp.GetY(), tmp.GetZ()));
        }
        return rtn;
    }
    public bool IsReachAble(Hex start, Hex dest, int Range)
    {
        List<Hex> path = GetPath(start, dest);
        if (path.Count == 0 || path.Count > Range)
        {
            return false;
        }
        return true;
    }
    public int GetDistance(Hex h1, Hex h2)
    {
        Point pos1 = h1.MapPos;
        Point pos2 = h2.MapPos;
        return (int)(Mathf.Sqrt(Mathf.Pow(pos1.GetX() - pos2.GetX(), 2) + Mathf.Pow((pos1.GetZ() - pos2.GetZ()), 2)));

    }

    public Hex GetHex(int x, int y, int z)
    {
        if (x < 0)
            x = 0;
        if (y < 0)
            y = 0;
        if (z < 0)
            z = 0;
        if (x > MapSizeX)
            x = MapSizeX;
        if (y > MapSizeY)
            y = MapSizeY;
        if (z > MapSizeZ)
            z = MapSizeZ;
        return Map[x][y][z];
    }
    public void SetHexColor(Hex hex, Color color)
    {
        hex.GetComponent<Renderer>().material.color = color;
    }
}
