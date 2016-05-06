using UnityEngine;
using System.Collections;

public class ani : MonoBehaviour {
    ACT act;
	// Use this for initialization
	void Start () {
        act = ACT.IDLE;

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 v = transform.position;
        if (v.x >= -95)
            act = ACT.MOVING;
        
        else if (v.x < -110)
            act = ACT.IDLE;

        if (act == ACT.IDLE)
        {
            v.x += Time.deltaTime;
            transform.rotation = Quaternion.Euler(30, 90, 0);
        }
        else
        {
            v.x -= Time.deltaTime;
            transform.rotation = Quaternion.Euler(-30, 90, 0);
        }
        transform.position = v;
	}
}
