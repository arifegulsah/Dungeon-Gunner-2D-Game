using UnityEngine;
using UnityEditor.Callbacks; //edit�rde yap�lan callbackleri capturel�yor.
using UnityEditor;

//�zel bir editor scripti oldugu i�in monobehaviordan de�il de ediotrwindowsdan t�retip namespacei veriyoruz.
public class RoomNodeGraphEditor : EditorWindow
{
    private GUIStyle roomNodeStyle;
    private static RoomNodeGraphSO currentRoomNodeGraph;
    private RoomNodeTypeListSO roomNodeTypeList;

    private const float nodeWidth = 160f;
    private const float nodeHeight = 75f;
    private const int nodePadding = 25;
    private const int nodeBorder = 12;

    //Bu noktadan itibaren kendimize has bir editor penceresi yap�yoruz.
    //Amac�m�z bu script yard�m� ile olu�turdugumuz pencere ile zindanda bulunacak olan odalar�m�z� ayarlamak.
    //O odalar�n say�s�n�/tipini ve birbiriyle nas�l birle�ti�ini vs bu pencere ile dizayn edece�iz.


    [MenuItem("Room Node Graph Editor", menuItem = "Window/Dungeon Editor/Room Node Graph Editor")]
    //menuItem = dedik oras� sayesinde Unity i�erisinde Window'a geldi�imizde art�k Dungeon Editor isminde yeni bi clickable button olucak.
    //Daha sonras�nda haz�r fonksiyonlardan biri olan openwindow() ile i�erisinde bu scriptten �retilen pencere a��l�cak ad�da t�rnak i�erisinde verdi�imiz isimdir.
    private static void OpenWindow()
    {
        GetWindow<RoomNodeGraphEditor>("Room Node Graph Editor");
    }

    //Art�k blank bir penceremiz var. Yani �i bo� ��ini doldurmak i�in OnGui() kullan�l�yor.
    //OnGui'nin i�ini doldurmak i�inde style kullan�l�yor.

    private void OnEnable()
    {
        //node layout sytle belirleniyor.
        roomNodeStyle = new GUIStyle();
        roomNodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
        roomNodeStyle.normal.textColor = Color.white;
        roomNodeStyle.padding = new RectOffset(nodePadding, nodePadding, nodePadding, nodePadding);
        roomNodeStyle.border = new RectOffset(nodeBorder, nodeBorder, nodeBorder, nodeBorder);

        //Load Room Node Types
        roomNodeTypeList = GameResources.Instance.roomNodeTypeList;
    }

    //room node graph so'ya �ift t�klan�nca edit�r� a�
    [OnOpenAsset(0)] //callbacks;
    public static bool OnDoubleClickAsset(int instanceID, int line)
    {
        RoomNodeGraphSO roomNodeGraph = EditorUtility.InstanceIDToObject(instanceID) as RoomNodeGraphSO;

        if (roomNodeGraph != null)
        {
            OpenWindow();
            currentRoomNodeGraph = roomNodeGraph;
            return true;
        }
        return false;
    }



    private void OnGUI()
    {
        //Herhangibir roomnodegraphso se�ildiyse e�er i�leme devam edelim ve windowa �izdirelim.
        if(currentRoomNodeGraph != null)
        {
            ProcessEvents(Event.current);

            DrawRoomNodes();
        }

        if(GUI.changed)
            Repaint();
    }

    private void ProcessEvents(Event currentEvent)
    {
        ProcessRoomNodeGraphEvents(currentEvent);
    }

    private void ProcessRoomNodeGraphEvents(Event currentEvent)
    {
        switch (currentEvent.type)
        {
            case EventType.MouseDown:
                ProcessMouseDownEvent(currentEvent);
                break;

            default:
                break;
        }
    }

    private void ProcessMouseDownEvent(Event currentEvent)
    {
        if(currentEvent.button == 1) //sa�click
        {
            ShowContextMenu(currentEvent.mousePosition);
        }
    }

    private void ShowContextMenu(Vector2 mousePosition)
    {
        GenericMenu menu = new GenericMenu(); //generic menuler �zel dropdownlar olu�turmam�z� sa�l�yor.
        menu.AddItem(new GUIContent("Create Room Node"), false, CreateRoomNode, mousePosition);
        menu.ShowAsContext();
    }

    private void CreateRoomNode(object mousePositionObject)
    {
        CreateRoomNode(mousePositionObject, roomNodeTypeList.list.Find(x=>x.isNone));
    }

    //overloading
    private void CreateRoomNode(object mousePositionObject, RoomNodeTypeSO roomNodeType)
    {
        Vector2 mousePosition = (Vector2)mousePositionObject;

        RoomNodeSO roomNode = ScriptableObject.CreateInstance<RoomNodeSO>();
        currentRoomNodeGraph.roomNodeList.Add(roomNode);
        roomNode.Initialise(new Rect(mousePosition, new Vector2(nodeWidth, nodeHeight)), currentRoomNodeGraph, roomNodeType);
        AssetDatabase.AddObjectToAsset(roomNode, currentRoomNodeGraph);

        AssetDatabase.SaveAssets();
    }

    private void DrawRoomNodes()
    {
        foreach (RoomNodeSO roomNode in currentRoomNodeGraph.roomNodeList)
        {
            roomNode.Draw(roomNodeStyle);
        }
        GUI.changed = true;
    }
}
