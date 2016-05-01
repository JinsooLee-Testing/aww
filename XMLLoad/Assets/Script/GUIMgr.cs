using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 public enum SELECTION
{
    TEXTURE,
    STRUCTURE,
    PASSBLE,
    NOTPASSBLE
};
public class GUIMgr {
    private static GUIMgr inst = null;
    // private static MapMgr mm;
    private string MapSizeX = "0";
    private string MapSizeY = "0";
    private string MapSizeZ = "0";
    public string x = "0";
    public string y = "1";
    public string z = "0";
    private string m_y = "1";
    public bool y_draw =true;
    public Texture[] Texures= new Texture[10];
    public Material[] mat = new Material[10];
    public bool structable = false;
    public bool Load = false;
    public GameObject[] Structures = new GameObject[10];
    //public List<Structure> AddedStructures = new List<Structure>();
    public SELECTION sel;
    public Texture CurTexture;
    public Material Curmat;
    public int CurTextureIdx;
    public GameObject CurStruct;
    public int CurHeight =0;
    public int CurMatIdx;
    public string curSel = "passable";
    public bool passble = true;
    public int CurStructIdx = 0;
    public static GUIMgr GetInst()
    {
        if(inst==null)
        {
            inst = new GUIMgr();
            inst.Texures[0] = (Texture)Resources.Load("texture/soil");
            inst.Texures[1] = (Texture)Resources.Load("texture/grass");
            inst.Texures[2] = (Texture)Resources.Load("texture/fire");
            inst.Texures[3] = (Texture)Resources.Load("texture/road");
     
            inst.mat[0] = (Material)Resources.Load("material/soil");
            inst.mat[1] = (Material)Resources.Load("material/grass");
            inst.mat[2] = (Material)Resources.Load("material/fire");
            inst.mat[3] = (Material)Resources.Load("material/road");

            inst.Structures[0] =(GameObject)Resources.Load("object/carrot");
            inst.Structures[1] = (GameObject)Resources.Load("object/carrot2");
            inst.Structures[2] = (GameObject)Resources.Load("object/carrotbasket");
            inst.Structures[3] = (GameObject)Resources.Load("object/tree");
            inst.Structures[4] = (GameObject)Resources.Load("object/bone");
            inst.Structures[5] = (GameObject)Resources.Load("object/bonebig");
            inst.CurStruct = inst.Structures[0];
            inst.CurTexture = inst.Texures[0];
            inst.CurTextureIdx = 0;
            inst.CurMatIdx = 0;
            inst.Curmat = inst.mat[0];
        }
        return inst;
    }
    public void SetTextures(Texture[] textures)
    {
        Texures = textures;
    }
    public void SetStructures(GameObject[] stru)
    {
        Structures =stru;
    }
    public void DrawLeftLayout()
    {
        GUILayout.BeginArea(new Rect(0, 0, 200f, Screen.height), "MapInfo", GUI.skin.window);
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        GUILayout.Label("MapSizeX:");
        MapSizeX = GUILayout.TextField(MapSizeX);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("MapSizeY:");
        MapSizeY = GUILayout.TextField(MapSizeY);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("MapSizeZ:");
        MapSizeZ = GUILayout.TextField(MapSizeZ);
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        if(GUILayout.Button("Create"))
        {
    
                MapMgr.GetInst().CreateMap(int.Parse(MapSizeX), int.Parse(MapSizeY), int.Parse(MapSizeZ));
  
        }
        if (GUILayout.Button("Reset"))
        {
          
            MapMgr.GetInst().Destroymap();
        }
        if (GUILayout.Button("Save"))
        {
            fileMagr.GetInst().SaveData();
        }
        if (GUILayout.Button("Load"))
        {
            MapInfo info = fileMagr.GetInst().LoadMap();
            MapMgr.GetInst().CreateXMLmap(info);
                
        }
        GUILayout.Label("curcel");
        if (GUILayout.Button("passable"))
        {
            curSel = "passable";
            passble = true;

        }
        if (GUILayout.Button("Notpassble"))
        {
            Debug.Log("ss222222222");
            passble = false;
            curSel = "Notpassble";
        }
        GUILayout.Label("Current Selected");
        GUILayout.BeginHorizontal();
       
        GUILayout.Box(CurTexture, GUILayout.Width(80f), GUILayout.Height(80f));
        //GUILayout.Box(CurStruct, GUILayout.Width(80f), GUILayout.Height(80f));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("NextMat"))
        {
            CurTextureIdx++;
            CurMatIdx = CurTextureIdx;
            if (CurTextureIdx>3)
            {
                CurTextureIdx = 0;
                CurMatIdx = 0;
            }
            CurTexture = Texures[CurTextureIdx];
            Curmat = mat[CurMatIdx];

        }
            GUILayout.EndHorizontal();



        GUILayout.EndArea();
       
    }
    public void DrawRightLayout()
    {

        GUILayout.BeginArea(new Rect(Screen.width-200f, 0, 200f, Screen.height), "MapInfo", GUI.skin.window);
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        GUILayout.Label("x:");
        x = GUILayout.TextField(x);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("y:");
        y = GUILayout.TextField(y);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("z:");
        z = GUILayout.TextField(z);
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();


        if (GUILayout.Button("y_Box"))
        {
            if (y_draw == false)
                y_draw = true;
            else
                y_draw = false;

        }
        if (GUILayout.Button("nextY"))
        {
            CurHeight++;
            if (CurHeight > 2)
                CurHeight = 2;
            Vector3 v = new Vector3(1, 1, 1);
            MapMgr.GetInst().SetActive(CurHeight, v);

        }
        if (GUILayout.Button("prevY"))
        {
            CurHeight--;
            if (CurHeight < 1)
                CurHeight = 1;
            Vector3 v = new Vector3(0, 0, 0);
            MapMgr.GetInst().SetActive(CurHeight, v);

        }
        GUILayout.Label("struct");
        if (GUILayout.Button("struct_on"))
        {
            structable = true;

        }
        if (GUILayout.Button("struct_off"))
        {
            structable = false;
        }
        GUILayout.Label("Current Selected");
        GUILayout.BeginHorizontal();

        GUILayout.Box(CurStruct.name.ToString(), GUILayout.Width(80f), GUILayout.Height(80f));
        //GUILayout.Box(CurStruct, GUILayout.Width(80f), GUILayout.Height(80f));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("NexStruct"))
        {
            CurStructIdx++;
            if (CurStructIdx > 5)
            {
                CurStructIdx = 0;
             }
            CurStruct = Structures[CurStructIdx];
           

        }
        GUILayout.EndHorizontal();



        GUILayout.EndArea();


    }
    public void OnGUI()
    {
        DrawRightLayout();
        DrawLeftLayout();

    }
}
