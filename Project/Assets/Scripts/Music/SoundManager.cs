using System.Threading;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    public AudioSource audio;
    public AudioClip[] clip;


    //効果音の種類
    public enum Sound
    {
        HIT, //ヒット
        MISS, //ミス,
        END,  //曲の終了時に流す
        NONE = 0
    }
   
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //効果音の更新
    public void SoundUpdate(Sound sound)
    {
        switch(sound)
        {
            case Sound.HIT:  //ヒット音
                audio.PlayOneShot(clip[0]);
                break;
            case Sound.MISS: //ミス
                audio.PlayOneShot(clip[1]);
                break;
            case Sound.END: //曲の終了時に流す
                audio.PlayOneShot(clip[2]);
                break;
            default:
                sound = Sound.NONE;
                break;

        }
    }

}
