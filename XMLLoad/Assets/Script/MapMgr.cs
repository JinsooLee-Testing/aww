using UnityEngine;
using System.Collections;

public class MapMgr  {
    private static MapMgr inst;
    public boxinfo[][][] Map;
    private GameObject Maproot;
    public float HexW;
    public float HexH;
    public float BoxH; //Awake
    public int MapSizeX =5;
    public int MapSizeY =0;
    public int MapSizeZ = 5;
    
    private GameObject hex;
    void Awake()
    {
     
        
    }
    public Vector3 GetWorldPos(int x, int y, int z)
    {
        float X, Y, Z;
        X = x * HexW;
        Y = y * BoxH;
        Z = (z) * HexH;
        return new Vector3(X, Y, Z);
    }
    public static MapMgr GetInst()
    {
        if(inst ==null)
        {
            inst = new MapMgr();
            inst.hex = (GameObject)Resources.Load("Prefabs/soil");
            inst.HexW = inst.hex.GetComponent<Renderer>().bounds.size.x;
            inst.HexH = inst.hex.GetComponent<Renderer>().bounds.size.z;
            inst.BoxH = inst.hex.GetComponent<Renderer>().bounds.size.y;
        }
        return inst;
    }
    public void Destroymap()
    {
        GameObject.Destroy(Maproot);

    }
  
	public void CreateMap(int sizeX, int sizeY,int sizeZ)
    {
        MapSizeZ = sizeZ;
         MapSizeX = sizeX;
         MapSizeY = sizeY;
        Maproot = new GameObject("Map");
        if (sizeX>0&&sizeY>=0&&sizeZ>0)
        {
            Map = new boxinfo[MapSizeX + 1][][];
            for (int x = 0; x <= MapSizeX; x++)
            {
                Map[x] = new boxinfo[MapSizeY + 1][];
                for (int y = 0; y <= MapSizeY; y++)
                {
                    Map[x][y] = new boxinfo[MapSizeZ + 1];
                    for (int z = 0; z <= MapSizeZ; z++)
                    {
                        GameObject box = (GameObject)GameObject.Instantiate(hex);
                        boxinfo box2 = box.GetComponent<boxinfo>();
                        box2.transform.parent = Maproot.transform;
                        //Vector3 pos2 = GetWorldPos(x, y, z);
                        //box2.transform.position = pos2;
                        box2.transform.position = GetWorldPos(x, y, z); 
                        box2.SetMapPos(x, y, z);
                        box2.SetPassable(true);
                        Map[x][y][z] = box2;
                    }
                }
            }
        }
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
