using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
public class MapInfo
{
    public int MapSizeX;
    public int MapSizeY;
    public int MapSizeZ;


    public List<boxinfo> bonInfos = new List<boxinfo>();
}
public class fileMagr {
    private static fileMagr inst = null;
    public static fileMagr GetInst()
    {
        if(inst==null)
        {
            inst = new fileMagr();
        }
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


            boxinfo box = new boxinfo();
            box.X = mapX;
            box.Y = mapY;
            box.Z = mapZ;
            box.Passable = pass;
            box.mat_name = mat_name;
            box.mesh_draw = draw;
            box.objId = obj_id;
            box.y = obj_y;
            info.bonInfos.Add(box);


        }
        return info;
    }
    public void SaveData()
    {
        XmlDocument xmlFile = new XmlDocument();
        xmlFile.AppendChild(xmlFile.CreateXmlDeclaration("1.0", "utf-8", "yes"));
        XmlNode rootNode = xmlFile.CreateNode(XmlNodeType.Element, "MapInfo", string.Empty);
        xmlFile.AppendChild(rootNode);

        MapMgr mm = MapMgr.GetInst();

        int MapSizeX = MapMgr.GetInst().MapSizeX;
        int MapSizeY = MapMgr.GetInst().MapSizeY;
       int MapSizeZ = MapMgr.GetInst().MapSizeZ;


        XmlElement size = xmlFile.CreateElement("MapSize");
        size.InnerText = MapSizeX + " " + MapSizeY + " " + MapSizeZ;
        rootNode.AppendChild(size);
        for (int x=0;x<=MapSizeX;x++)
        {
            for(int y=0;y<=MapSizeY;y++)
            {
                for(int z=0;z<=MapSizeZ; z++)
                {
                    XmlNode hexNode = xmlFile.CreateNode(XmlNodeType.Element, "box", string.Empty);
                    rootNode.AppendChild(hexNode);

                    XmlElement mapPos = xmlFile.CreateElement("MapPos");
                    boxinfo box = mm.Map[x][y][z];
                    mapPos.InnerText=box.X+" "+box.Y+" "+box.Z;
                    hexNode.AppendChild(mapPos);

                    XmlElement passable = xmlFile.CreateElement("Passable");
                    passable.InnerText = box.Passable.ToString();
                    hexNode.AppendChild(passable);

                    XmlElement material = xmlFile.CreateElement("Material");
                    material.InnerText = box.mat_name;
                    hexNode.AppendChild(material);

                    XmlElement mesh = xmlFile.CreateElement("Mesh");
                    mesh.InnerText = box.mesh_draw.ToString();
                    hexNode.AppendChild(mesh);

                    XmlElement obj = xmlFile.CreateElement("Object");
                    obj.InnerText = box.objId + " " + box.y;
                    hexNode.AppendChild(obj);

                }
            }
        }
        xmlFile.Save("test.xml");
    }
    void Start () {
	
	}
	
	
	void Update () {
	
	}
}
