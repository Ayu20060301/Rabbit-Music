using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] SongDataBase dataBase; //SongDataBaseを格納する変数
    
    AudioSource audioSource;
    AudioClip audioClip;
    string songName;
    bool played;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GManager.instance.start = false;
        songName = "130 からすうさぎ"; //音楽ファイル名
        audioSource = GetComponent<AudioSource>();
        audioClip = (AudioClip)Resources.Load("Musics/" + songName);
        played = false;
    }

    // Update is called once per frame
    void Update()
    {
        //スぺースキーを押したら曲を流す処理
        if(Input.GetKeyDown(KeyCode.Space) && !played)
        {
            GManager.instance.start = true;
            GManager.instance.startTime = Time.time;
            played = true;
            audioSource.PlayOneShot(audioClip);
        }
    }
}
