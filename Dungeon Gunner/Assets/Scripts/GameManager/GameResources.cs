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
                instance = Resources.Load<GameResources>("GameResources"); //burada resources.load derken aslýnda unity içerisinde Resources isimli oluþturduugm ve içerisine prefablarýmý koyacak oldugum klasörden bahsetmil oluyorum. Bu unityinin algýladýgý özel isimde bir klasör namedir. týpký editor kelimesinde oldugu gibi.
            }
            return instance;
        }
    }

    #region Header
    [Header("DUNGEON")]
    #endregion
    #region Tooltip
    [Tooltip("Populate with the dungeon RoomNodeTypeListSO")]
    #endregion
    public RoomNodeTypeListSO roomNodeTypeList;

    #region Header MATERIALS
    [Space(10)]
    [Header("MATERIALS")]
    #endregion
    #region Tooltip
    [Tooltip("Dimmed Materials")]
    #endregion
    public Material dimmedMaterial;
}
