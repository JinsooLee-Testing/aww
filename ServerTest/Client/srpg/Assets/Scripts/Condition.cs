using UnityEngine;
using System.Collections;

public class Condition :MonoBehaviour{
    public GameObject stun;
    public string state = "none";
    public Condition()
    {
        state = "none";
    }
    public void DrawStun(Vector3 transform)
    {
        GameObject s = ((GameObject)Instantiate(stun)).GetComponent<GameObject>();
        s.transform.position=transform;
    }
}
