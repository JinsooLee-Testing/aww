using UnityEngine;
using System.Collections;

public class magic : MonoBehaviour {
    public GameObject GO_MIS;

    void Start () {
        magic mis = ((GameObject)Instantiate(GO_MIS)).GetComponent<magic>();
        Vector3 pos =PlayerManager.GetInst().Players[PlayerManager.GetInst().CurTurnIdx].transform.position;
        mis.transform.position = pos;

    }

    // Update is called once per frame
    void Update()
    {
        magic mis = ((GameObject)Instantiate(GO_MIS)).GetComponent<magic>();
        Vector3 v = mis.transform.position;
        v.y = PlayerManager.GetInst().m_y;
        v.x = v.x += 50;
        float distance = Vector3.Distance(transform.position, v);

        if (distance > 0.1f) //이동중
        {

            transform.position += (v - transform.position).normalized * 3 * Time.smoothDeltaTime;
        }
    }
}
