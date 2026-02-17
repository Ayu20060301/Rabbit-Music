using UnityEngine;

[CreateAssetMenu(fileName = "SongDataBase",menuName = "楽曲データベースを作成")]

public class SongDataBase : ScriptableObject
{
    [SerializeField] public SongDatas[] songData;  
}
