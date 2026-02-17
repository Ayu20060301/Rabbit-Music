using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


[Serializable]
public class Data
{
    public string name;
    public int maxBlock;
    public int BPM;
    public int offset;
    public Note[] notes;
}
[Serializable]
public class Note
{
    public int type;
    public int num;
    public int block;
    public int LPB;
}

public class NotesManager : MonoBehaviour
{
    
    public int noteNum; //総ノーツ数
    private string m_SongName; //曲名入れる変数 

    public List<int> LaneNum = new List<int>(); //何番のレーンにノーツが落ちてくるか
    public List<int> NoteType = new List<int>(); //何ノーツか
    public List<float> NotesTime = new List<float>(); //ノーツが判定線と重ねる時間
    public List<GameObject> NotesObj = new List<GameObject>(); //GameObject

    [SerializeField] private float m_NotesSpeed;
    [SerializeField] GameObject noteObj;
    [SerializeField] SongDataBase dataBase; //SongDataBaseを格納する変数

    void OnEnable()
    {
        float speed = GManager.instance.noteSpeed;
        if (speed < 1.0f || speed > 20.0f) // 異常な値ならデフォルトに
            speed = PlayerPrefs.GetFloat("NoteSpeed", 12.0f);

        m_NotesSpeed = speed;

        noteNum = 0;
        m_SongName  = dataBase.songData[GManager.instance.songID].songName;
        Load(m_SongName);
    }


    private void Load(string SongName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(SongName);
        string inputString = textAsset.text;
        Data inputJson = JsonUtility.FromJson<Data>(inputString); //Jsonファイルの読み込み

        noteNum = inputJson.notes.Length; //総ノーツ数の指定
        GManager.instance.maxScore = noteNum * 5;


        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            float kankaku = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            float beatSec = kankaku * (float)inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset * 0.01f + GManager.instance.timingOffset * 0.001f;
            NotesTime.Add(time);
            LaneNum.Add(inputJson.notes[i].block);
            NoteType.Add(inputJson.notes[i].type);

            float laneWidth = 1.3f; // 各レーンの横幅
            int laneCount = 6;
            float baseX = -((laneCount - 2) / 1.6f) * laneWidth; // 中央に揃える

            float x = baseX + inputJson.notes[i].block * laneWidth;
            float z = NotesTime[i] * m_NotesSpeed;
            NotesObj.Add(Instantiate(noteObj, new Vector3(x, 0.55f, z), Quaternion.identity));
        }
        
    }
   
}
