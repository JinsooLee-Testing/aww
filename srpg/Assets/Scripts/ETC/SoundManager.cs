using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
    private static SoundManager inst = null;
    public AudioClip AC_ATTack;
    public AudioClip click;
    public AudioClip bgm;
    // Use this for initialization
    public static SoundManager GetInst()
    {
        return inst;
    }
    void Awake()
    {
        inst = this;
    }
    // Use this for initialization
    void Start () {
	
	}

	// Update is called once per frame
	void Update () {

    }
    public void PlayClickSound()
    {
        AudioSource.PlayClipAtPoint(click, this.transform.position);
    }
    public void PlayAttackSound()
    {
        AudioSource.PlayClipAtPoint(AC_ATTack,this.transform.position);
    }
    public void PlayMusic(Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(bgm, pos);
    }
}
