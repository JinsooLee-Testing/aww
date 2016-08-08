using UnityEngine;
using System.Collections;

public class MainTilteStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SoundManager.GetInst().PlayMusic(new Vector3(0,25,-10));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
