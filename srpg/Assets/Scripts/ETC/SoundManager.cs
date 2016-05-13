using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
    private static SoundManager inst = null;
    public AudioClip AC_ATTack;
    public AudioClip click;
    public AudioClip bgm;
    public AudioClip victory;
    // Use this for initialization
    public static SoundManager GetInst()
    {
        return inst;
    }
    void Awake()
    {
        inst = this;
        inst.victory = (AudioClip)Resources.Load("Sound/victory");
    }
    // Use this for initialization
    void Start () {
	
	}

	// Update is called once per frame
	void Update () {

    }

    public void PlayVictory()
    {
        AudioClip.DestroyImmediate(bgm);
        AudioSource.PlayClipAtPoint(victory, this.transform.position);
        
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
