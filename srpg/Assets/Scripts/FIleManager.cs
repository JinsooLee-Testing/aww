using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using System.Collections;
public class boxInfo
{
    public int MapPosX;
    public int MapPosY;
    public int MapPosZ;
    public bool Passable;
    public bool mat_draw;
    public string mat_name;
    public int obj_id;
    public float obj_y;
}
public class MapInfo
{
    public int MapSizeX;
    public int MapSizeY;
    public int MapSizeZ;
    public List<boxInfo> bonInfos = new List<boxInfo>();
}
public class FIleManager : MonoBehaviour {

    private static FIleManager inst = null;
    public static FIleManager Getinst()
    {
        return inst;
    }
    public MapInfo LoadMap(string MapPath)
    {
        MapInfo info = new MapInfo();

        XmlDocument xmlFile = new XmlDocument();
        xmlFile.Load(MapPath);
        XmlNode mapSize = xmlFile.SelectSingleNode("MapInfo/MapSize");

        string mapSizeString = mapSize.InnerText;
        string[] sizes = mapSizeString.Split(' ');
        Debug.Log(info.MapSizeX = int.Parse(sizes[0]));
        int mapSizeX = info.MapSizeX = int.Parse(sizes[0]);
        int mapSizeY = info.MapSizeY = int.Parse(sizes[1]);
        int mapSizeZ = info.MapSizeZ = int.Parse(sizes[2]);

        XmlNodeList hexes = xmlFile.SelectNodes("MapInfo/box");
        foreach (XmlNode hex in hexes)
        {
            string mapposStr = hex["MapPos"].InnerText;
            string[] maposes = mapposStr.Split(' ');
            int mapX = int.Parse(maposes[0]);
            int mapY = int.Parse(maposes[1]);
            int mapZ = int.Parse(maposes[2]);

            string passalbe = hex["Passable"].InnerText;
            bool pass = passalbe == "True";


            string mat = hex["Material"].InnerText;

            string mat_name = mat;

            string mesh = hex["Mesh"].InnerText;
            bool draw = mesh == "True";

            string posstr = hex["Object"].InnerText;
            string[] obj = posstr.Split(' ');
            int obj_id = int.Parse(obj[0]);
            float obj_y = float.Parse(obj[1]);
            
            boxInfo box = new boxInfo();
            box.MapPosX = mapX;
            box.MapPosY = mapY;
            box.MapPosZ = mapZ;
            box.Passable = pass;
            box.mat_name = mat_name;
            box.mat_draw = draw;
            box.obj_id = obj_id;
            box.obj_y = obj_y;
            info.bonInfos.Add(box);


        }
        return info;
    }
    void Awake()
    {
        inst = this;
    }
 
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
