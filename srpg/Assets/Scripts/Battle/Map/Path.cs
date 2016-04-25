using UnityEngine;
using System.Collections;

public class Path
{
    public Path Parent;
    Hex curHex;
    int F;
    int H;//현재부터 도착점
    int G; //시작점부터 현재까지
    public int GetF()
    {
        return F;
    }
    public int GetDepth()
    {
        return G;
    }
    public Hex GetHex()
    {
        return curHex;
    }
    public Path(Path parent, Hex hex, int g, int h)
    {
        curHex = hex;
        Parent = parent;
        G = g;
        H = h;
        F = G + H;
    }

}
