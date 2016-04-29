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

    public Texture[] Texures= new Texture[10];
    public Material[] mat = new Material[10];
    public bool structable = false;
    public GameObject[] Structures = new GameObject[10];
    //public List<Structure> AddedStructures = new List<Structure>();
    public SELECTION sel;
    public Texture CurTexture;
    public Material Curmat;
    public int CurTextureIdx;
    public GameObject CurStruct;
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
            inst.Texures[2] = (Texture)Resources.Load("texture/road");
            inst.Texures[3] = (Texture)Resources.Load("texture/fire");
            inst.mat[0] = (Material)Resources.Load("material/soil");
            inst.mat[1] = (Material)Resources.Load("material/grass");
            inst.mat[2] = (Material)Resources.Load("material/road");
            inst.mat[3] = (Material)Resources.Load("material/fire");

            inst.Structures[0] =(GameObject)Resources.Load("object/carrot");
            inst.Structures[1] = (GameObject)Resources.Load("object/carrot2");
            inst.Structures[2] = (GameObject)Resources.Load("object/carrotbasket");
            inst.Structures[3] = (GameObject)Resources.Load("object/tree");
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


        GUILayout.BeginHorizontal();

        if (GUILayout.Button("StructObj"))
        {
            if (structable == false)
                structable = true;
            else
                structable = false;
        }
        if (GUILayout.Button("NextObj"))
        {
            CurStructIdx++;
            if (CurStructIdx > 4)
            {
                CurStructIdx = 0;
            }
            CurStruct = Structures[CurStructIdx];


        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
       
    }
    public void DrawRIghtLayout()
    {
        //GUILayout.BeginArea(new Rect((Screen.width - 200f, 0, 200f, Screen.height), "MapInfo", GUI.skin.window);
        if(GUILayout.Button("Show Struct"))
        {
           
        }

    }
    public void OnGUI()
    {
        DrawLeftLayout();
    }
}
