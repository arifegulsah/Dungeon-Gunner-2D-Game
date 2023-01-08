using UnityEngine;
[System.Serializable] //scriptable objectlerde store edebilmke için serialize etmek gerekiyor

public class Doorway 
{
    public Vector2Int position;
    public Orientation orientation;
    public GameObject doorPrefab;

    #region Header
    [Header("The Upper Left Position/ Sol üst köþe koordinatý/ To Start Copying From")]
    #endregion
    public Vector2Int doorwayStartCopyPosition;
    #region Header
    [Header("The Width Of Tiles In The Doorway To Copy Over")]
    #endregion
    public int doorwayCopyTileWidth;
    #region Header
    [Header("The Height Of Tiles In The Doorway To Copy Over")]
    #endregion
    public int doorwayCopyTileHeight;
    [HideInInspector]
    public bool isConnected = false;
    [HideInInspector]
    public bool isUnavailable = false;


}
