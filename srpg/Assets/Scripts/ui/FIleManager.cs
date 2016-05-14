using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using System.Collections;
using System.IO;
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

public class CardsInfo
{
    public int MapSizeX;
    public int MapSizeY;
    public List<CardUseBase> cardInfos = new List<CardUseBase>();
}
public class FIleManager : MonoBehaviour
{

    private static FIleManager inst = null;
    public static FIleManager Getinst()
    {
        return inst;
    }
    public void SaveStageData(int curnum)
    {

        if (curnum >= StageManager.Getinst().cur_stage)
        {
            XmlDocument xmlFile = new XmlDocument();
            xmlFile.AppendChild(xmlFile.CreateXmlDeclaration("1.0", "utf-8", "yes"));
            XmlNode rootNode = xmlFile.CreateNode(XmlNodeType.Element, "StageInfo", string.Empty);
            xmlFile.AppendChild(rootNode);

            XmlNode hexNode = xmlFile.CreateNode(XmlNodeType.Element, "stage", string.Empty);
            rootNode.AppendChild(hexNode);

            XmlElement mapPos = xmlFile.CreateElement("curstage");
            mapPos.InnerText = curnum.ToString();
            hexNode.AppendChild(mapPos);
            Debug.Log("saveed");
            if (Application.platform == RuntimePlatform.Android)
            {
                TextAsset textAsset = Resources.Load("XML/" + "stage") as TextAsset;
                string strFilePath = Application.persistentDataPath + "/" + "Assets/Resources/XML/stage.xml";
                xmlFile.Save(strFilePath);
            }
            else
                xmlFile.Save("Assets/StreamingAssets/stage.xml");
        }

    }
    public void LoadStageData(string path)
    {
        // xmlFile.Load(MapPath);
        string strFile = path;
        string strPath = string.Empty;
        string strFilePath = Application.persistentDataPath + "/" + strFile;
        XmlDocument xmlFile = new XmlDocument();
        if (Application.platform == RuntimePlatform.Android)
        {
            TextAsset textAsset = Resources.Load("XML/" + strFile) as TextAsset;
            xmlFile.LoadXml(textAsset.text);

        }
        else
        {
            string Path = Application.streamingAssetsPath + "/" + strFile + ".xml";
            xmlFile.Load(Path);
            //xmlFile.Load(MapPath);
        }
        XmlNodeList hexes = xmlFile.SelectNodes("StageInfo/stage");
        foreach (XmlNode hex in hexes)
        {
            string mapposStr = hex["curstage"].InnerText;
            string[] maposes = mapposStr.Split(' ');
            StageManager.Getinst().cur_stage = int.Parse(maposes[0]);
        }

    }
    public void SaveCardData()
    {

        XmlDocument xmlFile = new XmlDocument();
        xmlFile.AppendChild(xmlFile.CreateXmlDeclaration("1.0", "utf-8", "yes"));
        XmlNode rootNode = xmlFile.CreateNode(XmlNodeType.Element, "MapInfo", string.Empty);
        xmlFile.AppendChild(rootNode);

        CardLoadManager cm = CardLoadManager.GetInst();

        int MapSizeX = cm.MapSizeX;
        int MapSizeY = cm.MapSizeY;
        XmlElement size = xmlFile.CreateElement("MapSize");
        size.InnerText = MapSizeX + " " + MapSizeY;
        rootNode.AppendChild(size);
        for (int x = 0; x <= MapSizeX; x++)
        {
            for (int y = 0; y <= MapSizeY; y++)
            {
                XmlNode hexNode = xmlFile.CreateNode(XmlNodeType.Element, "card", string.Empty);
                rootNode.AppendChild(hexNode);

                XmlElement mapPos = xmlFile.CreateElement("CardPos");
                CardUseBase card = cm.cardUse[x][y];
                mapPos.InnerText = card.X + " " + card.Y + " " + card.card_id;
                hexNode.AppendChild(mapPos);
            }
        }

        Debug.Log("saveed");
        if (Application.platform == RuntimePlatform.Android)
        {
            TextAsset textAsset = Resources.Load("XML/" + "cardtest") as TextAsset;
            xmlFile.Save(textAsset.text);
        }
        else
            xmlFile.Save("Assets/StreamingAssets/cardtest.xml");

    }
    public CardsInfo LoadCardData(string path)
    {
        CardsInfo info = new CardsInfo();


        // xmlFile.Load(MapPath);
        string strFile = path;
        string strPath = string.Empty;
        string strFilePath = Application.persistentDataPath + "/" + strFile;
        XmlDocument xmlFile = new XmlDocument();
        if (Application.platform == RuntimePlatform.Android)
        {
            TextAsset textAsset = Resources.Load("XML/" + strFile) as TextAsset;
            xmlFile.LoadXml(textAsset.text);
        }
        else
        {

            string Path = Application.streamingAssetsPath + "/" + strFile + ".xml";
            xmlFile.Load(Path);
            //xmlFile.Load(MapPath);


        }


        XmlNode mapSize = xmlFile.SelectSingleNode("MapInfo/MapSize");

        string mapSizeString = mapSize.InnerText;
        string[] sizes = mapSizeString.Split(' ');
        Debug.Log(info.MapSizeX = int.Parse(sizes[0]));
        int mapSizeX = info.MapSizeX = int.Parse(sizes[0]);
        int mapSizeY = info.MapSizeY = int.Parse(sizes[1]);

        XmlNodeList hexes = xmlFile.SelectNodes("MapInfo/card");
        foreach (XmlNode hex in hexes)
        {
            string mapposStr = hex["CardPos"].InnerText;
            string[] maposes = mapposStr.Split(' ');
            int mapX = int.Parse(maposes[0]);
            int mapY = int.Parse(maposes[1]);
            int id = int.Parse(maposes[2]);


            CardUseBase card = new CardUseBase();
            card.X = mapX;
            card.Y = mapY;
            card.card_id = id;
            info.cardInfos.Add(card);


        }
        return info;
    }
    public Fontlist LoadTextData(string path)
    {
        Fontlist font_list = new Fontlist();


        // xmlFile.Load(MapPath);
        string strFile = path;
        string strPath = string.Empty;
        string strFilePath = Application.persistentDataPath + "/" + strFile;
        XmlDocument xmlFile = new XmlDocument();
        if (Application.platform == RuntimePlatform.Android)
        {
            TextAsset textAsset = Resources.Load("XML/" + strFile) as TextAsset;
            xmlFile.LoadXml(textAsset.text);
        }
        else
        {

            string Path = Application.streamingAssetsPath + "/" + strFile + ".xml";
            xmlFile.Load(Path);
            //xmlFile.Load(MapPath);


        }


        XmlNodeList hexes = xmlFile.SelectNodes("Text/text");
        foreach (XmlNode hex in hexes)
        {
            string mapposStr = hex["textData"].InnerText;
            string[] sizes = mapposStr.Split(',');

            fontinfo font = new fontinfo();
            font.text = sizes[0];
            font.Who_say = sizes[1];
            font.tesx_idx = int.Parse(sizes[2]);
            font_list.bonInfos.Add(font);
        }
        return font_list;
    }
    public MapInfo LoadMap(string MapPath)
    {

        MapInfo info = new MapInfo();


        // xmlFile.Load(MapPath);
        string strFile = MapPath;
        string strPath = string.Empty;
        string strFilePath = Application.persistentDataPath + "/" + strFile;
        XmlDocument xmlFile = new XmlDocument();
        if (Application.platform == RuntimePlatform.Android)
        {

            //WWW wwwUrl = new WWW("jar:file://" + Application.dataPath + "!/assets/" + strFile);


            //  xmlFile.Load(wwwUrl.text.Trim());
            TextAsset textAsset = Resources.Load("XML/" + strFile) as TextAsset;
            xmlFile.LoadXml(textAsset.text);
        }
        else
        {

            string Path = Application.streamingAssetsPath + "/" + strFile + ".xml";
            xmlFile.Load(Path);
            //xmlFile.Load(MapPath);


        }


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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
