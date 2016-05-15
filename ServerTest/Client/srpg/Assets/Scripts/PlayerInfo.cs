using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour
{
    public string type;
    void Awake()
    {
        
    }
    void Start()
    {
        
    }
    void Update()
    {
       PlayerBase pb= PlayerManager.GetInst().select_object;
       if(type=="attack")
        {
            GetComponent<TextMesh>().text = (pb.status.Attack).ToString();
        }
        if (type == "hp")
        {
            GetComponent<TextMesh>().text = (pb.status.Curhp).ToString();
        }
        if (type == "name")
        {
            GetComponent<TextMesh>().text = (pb.status.Name);
        }
        if (type == "info")
        {
            GetComponent<TextMesh>().text = (pb.status.info);
        }
    }
}
