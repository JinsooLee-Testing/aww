using UnityEngine;
using System.Collections;
using System.Xml;

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
    public void LoadData()
    {
        XmlDocument xmlFile = new XmlDocument();
        xmlFile.Load("test.xml");
        XmlNode mappSize = xmlFile.SelectSingleNode("MapInfo/MapSize");
        string innerTest = mappSize.InnerText;
        Debug.Log(innerTest);
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
                for(int z=0;z<MapSizeZ; z++)
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
                    material.InnerText =box.mat_id+" "+box.mat_id;
                    hexNode.AppendChild(material);

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
