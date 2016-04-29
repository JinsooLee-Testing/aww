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
    public int mat_id;
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
    public MapInfo LoadMap()
    {
        MapInfo info = new MapInfo();

        XmlDocument xmlFile = new XmlDocument();
        xmlFile.Load("test.xml");
        XmlNode mapSize = xmlFile.SelectSingleNode("MapInfo/MapSize");
      
        string mapSizeString = mapSize.InnerText;
        string[] sizes = mapSizeString.Split(' ');
        Debug.Log(info.MapSizeX = int.Parse(sizes[0]));
        int mapSizeX = info.MapSizeX = int.Parse(sizes[0]);
        int mapSizeY = info.MapSizeX = int.Parse(sizes[1]);
        int mapSizeZ = info.MapSizeX = int.Parse(sizes[2]);

        XmlNodeList hexes = xmlFile.SelectNodes("MapInfo/box");
        foreach(XmlNode hex in hexes)
        {
            string mapposStr = hex["MapPos"].InnerText;
            string[] maposes = mapposStr.Split(' ');
            int mapX = int.Parse(maposes[0]);
            int mapY = int.Parse(maposes[1]);
            int mapZ = int.Parse(maposes[2]);

            string passalbe =hex["Passable"].InnerText;
            bool pass = passalbe == "True";
  
         
           string mat = hex["Material"].InnerText;
            string[] mat_st = mapposStr.Split(' ');
            int mat_id = int.Parse(mat_st[0]);
            boxInfo box = new boxInfo();
            box.MapPosX = mapX;
            box.MapPosY = mapY;
            box.MapPosZ = mapZ;
            box.Passable = pass;
            box.mat_id = 1;
           
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
