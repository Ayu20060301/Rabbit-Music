using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "SongData" , menuName = "楽曲データを作成")]

public class SongDatas : ScriptableObject
{
 
    [SerializeField] public string songID;
    [SerializeField] public string songName;
    [SerializeField] public string clipName;
    [SerializeField] public Sprite levelImage;
    [SerializeField] public Color imageColor;
    [SerializeField] public string modeText;
    
}
