using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Unity i�erisindeki Toolbar�m�zda bulunan assets k�sm�na ekleme yap�yoruz
[CreateAssetMenu(fileName = "RoomNodeGraph", menuName = "Scriptable Objects/Dungeon/Room Node Graph")]
public class RoomNodeGraphSO : ScriptableObject
{
    [HideInInspector] public RoomNodeTypeListSO roomNodeTypeList;
    [HideInInspector] public List<RoomNodeSO> roomNodeList = new List<RoomNodeSO>();
    [HideInInspector] public Dictionary<string, RoomNodeSO> roomNodeDictionary = new Dictionary<string, RoomNodeSO>();

    //bu noktadan itibaren connection lines yapabilmek i�in func verece�iz
    #region Editor Code

#if UNITY_EDITOR

    [HideInInspector] public RoomNodeSO roomNodeToDrawLineFrom = null; //ba�lant� �izgisini ba�latt�g�m�z node.
    [HideInInspector] public Vector2 linePosition;

    public void SetNodeToConnectionLineFrom(RoomNodeSO node, Vector2 position)
    {
        roomNodeToDrawLineFrom = node;
        linePosition = position;
    }

#endif

#endregion
}
