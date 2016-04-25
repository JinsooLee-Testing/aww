using UnityEngine;
using System.Collections;

public class CostManager : MonoBehaviour
{
    private static CostManager inst = null;
    public static CostManager GetInst()
    {
        return inst;
    }
    void Awake()
    {
        inst = this;
    }
    void Start()
    {

    }
    void Update()
    {
    }
 
}
