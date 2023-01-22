using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResources : MonoBehaviour
{
    private static GameResources instance;

    public static GameResources Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameResources>("GameResources"); //burada resources.load derken asl�nda unity i�erisinde Resources isimli olu�turduugm ve i�erisine prefablar�m� koyacak oldugum klas�rden bahsetmil oluyorum. Bu unityinin alg�lad�g� �zel isimde bir klas�r namedir. t�pk� editor kelimesinde oldugu gibi.
            }
            return instance;
        }
    }

    #region Header DUNGEON
    [Header("DUNGEON")]
    #endregion
    #region Tooltip
    [Tooltip("Populate with the dungeon RoomNodeTypeListSO")]
    #endregion
    public RoomNodeTypeListSO roomNodeTypeList;

    #region Header PLAYER
    [Space(10)]
    [Header("PLAYER")]
    #endregion
    #region Tooltip
    [Tooltip("The current SO - this is used to reference the current player between scenes")]
    #endregion
    public CurrentPlayerSO currentPlayer;

    #region Header MATERIALS
    [Space(10)]
    [Header("MATERIALS")]
    #endregion
    #region Tooltip
    [Tooltip("Dimmed Materials")]
    #endregion
    public Material dimmedMaterial;
}
