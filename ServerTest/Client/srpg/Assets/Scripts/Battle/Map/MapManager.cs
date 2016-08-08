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
    public Point wallpos;
    public bool wall = false;
    public float timeout = 0f;
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

    public Hex[][][] Map;


    void Awake()
    {

        inst = this;
        initDirs();

        SetHexSize();
    }
    public void CreateTestMap(string MapPath)
    {
        MapInfo info = FIleManager.Getinst().LoadMap(MapPath);
        if (info == null)
        {

        }
        if (info.bonInfos == null)
        {


        }
        CreateXMLmap(info);
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
        if (timeout != 0f)
        {
            timeout += Time.deltaTime;
        }
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

                    //Map[x][y][z].SetMesh();
                    Vector3 pos2 = GetWorldPos(x, 0, z);

                    Map[x][y][z].transform.position = pos2;
                    Map[x][y][z].SetMapPos(x, 0, z);
                    Map[x][y][z].Passable = true;

                }
            }
        }
    }
    public void CreateXMLmap(MapInfo info)
    {
        MapSizeX = info.MapSizeX;
        MapSizeY = info.MapSizeY;
        MapSizeZ = info.MapSizeZ;
        Map = new Hex[info.MapSizeX + 1][][];
        for (int x = 0; x <= info.MapSizeX; x++)
        {
            Map[x] = new Hex[info.MapSizeY + 1][];
            for (int y = 0; y <= info.MapSizeY; y++)
            {
                Map[x][y] = new Hex[info.MapSizeZ + 1];

            }
        }

        for (int i = 0; i < info.bonInfos.Count; ++i)
        {

            int x1 = info.bonInfos[i].MapPosX;
            int y1 = info.bonInfos[i].MapPosY;
            int z1 = info.bonInfos[i].MapPosZ;

            Map[x1][y1][z1] = ((GameObject)Instantiate(GO_hex)).GetComponent<Hex>();
            Map[x1][y1][z1].mat_name = info.bonInfos[i].mat_name;
            if (y1 == 0)
            {
                Map[x1][y1][z1].SetMesh();
            }
            Vector3 pos2 = GetWorldPos(x1, y1, z1);
            Map[x1][y1][z1].transform.position = pos2;
            Map[x1][y1][z1].SetMapPos(x1, y1, z1);
            Map[x1][y1][z1].Passable = info.bonInfos[i].Passable;
            Map[x1][y1][z1].mesh_draw = info.bonInfos[i].mat_draw;
            Map[x1][y1][z1].obj_id = info.bonInfos[i].obj_id;
            Map[x1][y1][z1].obj_y = info.bonInfos[i].obj_y;
        }

    }
    public void LoadObjMap()
    {
    }
    public Hex GetPlayerHex(int x, int y, int z)
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
    public void MarkTile(Point pos, int range, Hex hex)
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
                        //int distance = (GetDistance(hex, Map[x][y][z]));
                        //헥사곤 상의 셀과 셀간의 공식

                        //if (distance <= range && distance != 0)
                        //{
                        // Map[x][y][z].GetComponent<Renderer>().material.color = Color.green;
                        // if (Map[x][y][z].GetComponent<Renderer>().material.color == Color.green)
                        //    highLighedCount++;
                        // }
                    }
                }
            }

            // for(int i=0;i<5;++i)
            //  Map[s_x][s_y][s_z+i].GetComponent<Renderer>().material.color = Color.green;



            if (highLighedCount == 0)
            {
                Map[pos.GetX()][pos.GetY()][pos.GetZ()].GetComponent<Renderer>().material = Map[pos.GetX() + 1][pos.GetY()][pos.GetZ()].mat_move;
            }
            else
            {
                ResetMapColor();
                Map[pos.GetX()][pos.GetY()][pos.GetZ()].GetComponent<Renderer>().material = Map[pos.GetX() + 1][pos.GetY()][pos.GetZ()].mat_move; ;
            }

        }
    }

    public bool HilightMoveRange(Hex start, int moveRange)
    {
        int highLighedCount = 0;

        if (PlayerManager.GetInst().Players[PlayerManager.GetInst().CurTurnIdx].m_type != Type.MONSTER)
        {
            int tempx = start.x - moveRange + 3;
            int maxx = start.x + moveRange + 3;
            int tempz = start.z - moveRange + 3;
            int maxz = start.z + moveRange + 3;

            for (int x = 0; x <= MapSizeX; x++)
            {
                for (int z = 0; z <= MapSizeZ; z++)
                {
           

                    if (Map[x][0][z].Passable == true)
                    {
                        int distance = (GetDistance(start, Map[x][0][z]));
                        //헥사곤 상의 셀과 셀간의 공식

                        if (distance <= moveRange && distance != 0)
                        {
                            if (IsReachAble(start, Map[x][0][z], moveRange))
                            {

                                Map[x][0][z].GetComponent<Renderer>().material = Map[x][0][z].mat_move;
                                Map[x][0][z].At_Marked = true;


                                highLighedCount++;
                                if (1 <= MapSizeY)
                                {
                                    if (Map[x][1][z].mesh_draw == true)
                                    {

                                        Map[x][1][z].GetComponent<Renderer>().material = Map[x][1][z].mat_move;
                                        Map[x][0][z].At_Marked = true;
                                        highLighedCount++;
                                    }
                                }
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
                        Map[x][y][z].GetComponent<Renderer>().material = Map[x][y][z].mat_attack;
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
    public void MarkShockRange()
    {
        for (int i = 5; i > 3; --i)
        {
            EffectManager.GetInst().ShowEffect_Summon(Map[3][0][i].gameObject, 14, 1);
            Map[3][0][i].At_Marked = true;
            Map[3][0][i].GetComponent<Renderer>().material = Map[3][0][i].mat_attack;
        }
        for (int i = 6; i > 2; --i)
        {
            EffectManager.GetInst().ShowEffect_Summon(Map[2][0][i].gameObject, 13, 1);
            Map[2][0][i].At_Marked = true;
            Map[2][0][i].GetComponent<Renderer>().material = Map[2][0][i].mat_attack;
        }
        for (int i = 8; i > 0; --i)
        {
            EffectManager.GetInst().ShowEffect_Summon(Map[1][0][i].gameObject, 11, 1);
            Map[1][0][i].At_Marked = true;
            Map[1][0][i].GetComponent<Renderer>().material = Map[1][0][i].mat_attack;
        }

        for (int i = 5; i > 3; --i)
        {
            EffectManager.GetInst().ShowEffect_Summon(Map[6][0][i].gameObject, 14, 1);
            Map[6][0][i].At_Marked = true;
            Map[6][0][i].GetComponent<Renderer>().material = Map[6][0][i].mat_attack;
        }
        for (int i = 6; i > 2; --i)
        {
            EffectManager.GetInst().ShowEffect_Summon(Map[7][0][i].gameObject, 13, 1);
            Map[7][0][i].At_Marked = true;
            Map[7][0][i].GetComponent<Renderer>().material = Map[7][0][i].mat_attack;
        }
        for (int i = 8; i > 0; --i)
        {
            EffectManager.GetInst().ShowEffect_Summon(Map[8][0][i].gameObject, 11, 1);
            Map[8][0][i].At_Marked = true;
            Map[8][0][i].GetComponent<Renderer>().material = Map[8][0][i].mat_attack;
        }

        for (int i = 4; i < 7; ++i)
        {
            EffectManager.GetInst().ShowEffect_Summon(Map[i][0][6].gameObject, 14, 1);
            Map[i][0][6].At_Marked = true;
            Map[i][0][6].GetComponent<Renderer>().material = Map[i][0][6].mat_attack;
        }
        for (int i = 4; i < 6; ++i)
        {
            EffectManager.GetInst().ShowEffect_Summon(Map[i][0][7].gameObject, 13, 1);
            Map[i][0][7].At_Marked = true;
            Map[i][0][7].GetComponent<Renderer>().material = Map[i][0][7].mat_attack;
        }
        for (int i = 4; i < 6; ++i)
        {
            EffectManager.GetInst().ShowEffect_Summon(Map[i][0][8].gameObject, 11, 1);
            Map[i][0][8].At_Marked = true;
            Map[i][0][8].GetComponent<Renderer>().material = Map[i][0][8].mat_attack;
        }

        for (int i = 4; i < 7; ++i)
        {
            EffectManager.GetInst().ShowEffect_Summon(Map[i][0][3].gameObject, 14, 1);
            Map[i][0][3].At_Marked = true;
            Map[i][0][3].GetComponent<Renderer>().material = Map[i][0][3].mat_attack;
        }
        for (int i = 4; i < 6; ++i)
        {
            EffectManager.GetInst().ShowEffect_Summon(Map[i][0][2].gameObject, 13, 1);
            Map[i][0][2].At_Marked = true;
            Map[i][0][2].GetComponent<Renderer>().material = Map[i][0][2].mat_attack;
        }
        for (int i = 4; i < 6; ++i)
        {
            EffectManager.GetInst().ShowEffect_Summon(Map[i][0][1].gameObject, 11, 1);
            Map[i][0][1].At_Marked = true;
            Map[i][0][1].GetComponent<Renderer>().material = Map[i][0][1].mat_attack;
        }
    }
    public void MarkAttackRange(Hex start, int AtkRange,bool show)
    {

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
                        if(show==true)
                        {
                              //EffectManager.GetInst().ShowEffect_Summon(Map[x][y][z].gameObject, 12, 1);
                        }
                        Map[x][y][z].At_Marked = true;
                        
                        Map[x][y][z].GetComponent<Renderer>().material = Map[x][y][z].mat_attack;
                    }
                }
            }
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

                    if (Map[x][y][z].default_matid == 1)
                        Map[x][y][z].GetComponent<Renderer>().material = Map[x][y][z].mat1;
                    else if (Map[x][y][z].default_matid == 2)
                        Map[x][y][z].GetComponent<Renderer>().material = Map[x][y][z].mat2;
                    else if (Map[x][y][z].default_matid == 3)
                        Map[x][y][z].GetComponent<Renderer>().material = Map[x][y][z].mat3;
                    else if (Map[x][y][z].default_matid == 4)
                        Map[x][y][z].GetComponent<Renderer>().material = Map[x][y][z].mat4;
                    else
                        Map[x][y][z].GetComponent<Renderer>().material = Map[x][y][z].mat5;
                    Map[x][y][z].Marked = false;
                    Map[x][y][z].At_Marked = false;

                }
            }
        }

    }
    public void MarkWall(Point Mappos)
    {

        if (wall == false && Map[Mappos.GetX()][Mappos.GetY()][Mappos.GetZ()].Passable == true)
        {
            Map[Mappos.GetX() + 1][Mappos.GetY()][Mappos.GetZ()].GetComponent<Renderer>().material = Map[Mappos.GetX() + 1][Mappos.GetY()][Mappos.GetZ()].mat_move;
            Map[Mappos.GetX() + 1][Mappos.GetY()][Mappos.GetZ()].At_Marked = true;
            Map[Mappos.GetX()][Mappos.GetY()][Mappos.GetZ() + 1].GetComponent<Renderer>().material = Map[Mappos.GetX() + 1][Mappos.GetY()][Mappos.GetZ()].mat_move;
            Map[Mappos.GetX()][Mappos.GetY()][Mappos.GetZ() + 1].At_Marked = true;
            wall = true;
            wallpos = Mappos;

        }
    }
    public void ResetMapColor(Point pos)
    {
        int x = pos.GetX();
        int y = pos.GetY();
        int z = pos.GetZ();
        Map[x][y][z].GetComponent<Renderer>().material.color = Map[x][y][z].mat_color;
        Map[x][y][z].Marked = false;
        Map[x][y][z].At_Marked = false;
        wall = false;
    }
    public List<Hex> GetPath(Hex start, Hex dest)
    {
        OpenList = new List<Path>();
        ClosedList = new List<Path>();
        List<Hex> rtnVal = new List<Hex>();
        int H = (int)(MapManager.GetInst().GetDistance(start, dest));
        timeout += Time.deltaTime;
        Path startPath = new Path(null, start, 0, H);
        ClosedList.Add(startPath);
        Path result = Recursive_FindPath(startPath, dest);
        if (result == null)
            return null;
        timeout = 0f;
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
        if (timeout > 3.0f)
        {
            timeout = 0f;
            return null;
        }
        List<Hex> neibhors = GetNeibhors(parent.GetHex());
        foreach (Hex h in neibhors)
        {
            Path nevP = new Path(parent, h, parent.GetDepth() + 1, (int)(MapManager.GetInst().GetDistance(h, dest)));

            AddToOpenList(nevP);
            if ((int)(MapManager.GetInst().GetDistance(h, dest)) > 10)
                return null;
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
        if (path == null)
        {
            return false;
        }
        if (path.Count == 0 || path.Count >= Range)
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
        // return((pos1.GetX() - pos2.GetX())+( pos1.GetZ() - pos2.GetZ()));
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
    public void openDoor()
    {
        for (int x = 0; x <= MapSizeX; x++)
        {
            for (int z = 0; z <= MapSizeZ; z++)
            {
                if (Map[x][0][z].obj_id == 1)
                {
                    Map[x][0][z].Passable = true;
                    Destroy(Map[x][0][z].obj);
                }

            }
        }
    }
}
