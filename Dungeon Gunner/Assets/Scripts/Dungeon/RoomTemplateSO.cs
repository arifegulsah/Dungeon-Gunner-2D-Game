using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room_ ", menuName = "Scriptable Objects/Dungeon/Room")]
public class RoomTemplateSO : ScriptableObject
{
    [HideInInspector] public string guid;

    #region Header ROOM PREFAB

    [Space(10)]
    [Header("ROOM PREFAB")]

    #endregion Header ROOM PREFAB
    #region ToolTip

    [Tooltip("The gameobject prefav for the room (this will contain all the tilemaps for the room and enviroment game objects")]

    #endregion ToolTip
    public GameObject prefab;

    [HideInInspector] public GameObject previousPrefab; //kopyalama yapýldýgý zaman guid nin deðiþmesini saðlamak için kullanacaðýz

    #region Header ROOM CONFIG

    [Space(10)]
    [Header("ROOM CONFIGURATION")]

    #endregion Header ROOM CONGIF
    #region Tooltip
    [Tooltip("The room node type SO. The room node types correspond to the room nodes used in the room node graph." +
        "The exceptions being the corridors. In the room node graph there is just one corridor type 'Corridor'." +
        "For the room templates there are 2 corridor node types. CorridorNS and CorridorEW.")]
    #endregion Tooltip
    public RoomNodeTypeSO roomNodeType;

    #region Tooltip
    [Tooltip("If you imagibe a rectangle around the room tilemap that just completely encloses it, the room lower bounds represent the bottom left corner of that rectangle." +
        "This should be determined from the tilemap for the room (using the coordinate brush pointer to get the tilemap grid position for that bottom left corner" +
        "Note: this is the local tilemap position and NOT world posiiton)")]
    #endregion Tooltip
    public Vector2Int lowerBounds;

    #region Tooltip
    [Tooltip("If you imageine a rectangle around the room tilemap that just completely encloses it, the room upper bounds represent the top right corner of that rectangle" +
        "This should be determined from the tilemap for the room (using the coordinates brush pointer to get the tilemap grid position for that top right corner)" +
        "Note: this is the local tilemap position and NOT the world position")]
    #endregion Tooltip
    public Vector2Int upperBounds;

    #region Tooltip
    [Tooltip("There should be a max of four doorways for a room - one for each compass direction." +
        "Pusuladaki her bir taraf için 1 adet kapý olmalýdýr. Toplamda 4 tane yani." +
        "These should have a consistent 3 tile opening size, with the middle tile position being the doorway coordinate 'posiiton")]
    #endregion Tooltip
    [SerializeField] public List<Doorway> doorwayList;
}
