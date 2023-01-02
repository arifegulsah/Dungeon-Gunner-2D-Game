using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomNodeType_", menuName ="Scriptable Objects/Dungeon/Room Node Type")]
public class RoomNodeTypeSO : ScriptableObject
{
    public string roomNodeTypeName;

    //kolayca collapslamak için regionlara aldýk. Header olarak verdiðimiz her þey inspectorda görükecektir.
    //zorunda oldugumuz bir þey deðil ama daha temiz ve anlaþýlýr bir kod ve editore sahip olmak için yaptýgýmýz ufak bir iþlem

    #region Header
    [Header("Only flag the RoomNodeTypes that should be visible int the editor")]
    #endregion Header
    public bool displayInNodeGraphEditor = true;
    #region Header
    [Header("One Type Should Be A Corridor")]
    #endregion Header
    public bool isCorridor;
    #region Header
    [Header("One Type Should Be A CorridorNS")]
    #endregion Header
    public bool isCorridorNS;
    #region Header
    [Header("One Type Shpuld Be a CorridorEW")]
    #endregion Header
    public bool isCorridorEW;
    #region Header
    [Header("One Type Should Be A Entrance")]
    #endregion Header
    public bool isEntrance;
    #region Header
    [Header("One Type Should Be A Boss Room")]
    #endregion Header
    public bool isBossRoom;
    #region Header
    [Header("One Type Should Be None (Unassigned)")]
    #endregion Header
    public bool isNone;

    #region Validation
#if UNITY_EDITOR //bu kodun sadece unity editorde çalýþmasýný saðlayacakolan hash
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEmptyString(this, nameof(roomNodeTypeName), roomNodeTypeName);
    } //bu onvalidate fonksiyonu monobehaviourda bulunan özel ve hazýr bi fonksiyondur
#endif
    #endregion
}
