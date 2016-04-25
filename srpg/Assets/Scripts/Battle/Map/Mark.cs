using UnityEngine;
using System.Collections;
public enum MARKTYPE
{
    STRIGHT,
    RANGE,
    CRSS
};
public class Mark {

    public float xrange;
    public float yrange;
    public MARKTYPE type;
}
