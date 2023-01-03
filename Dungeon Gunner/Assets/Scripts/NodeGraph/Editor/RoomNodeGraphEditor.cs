using UnityEngine;
using UnityEditor.Callbacks; //editörde yapýlan callbackleri capturelüyor.
using UnityEditor;

//Özel bir editor scripti oldugu için monobehaviordan deðil de ediotrwindowsdan türetip namespacei veriyoruz.
public class RoomNodeGraphEditor : EditorWindow
{
    private GUIStyle roomNodeStyle;
    private static RoomNodeGraphSO currentRoomNodeGraph;
    private RoomNodeSO currentRoomNode = null;
    private RoomNodeTypeListSO roomNodeTypeList;

    private const float nodeWidth = 160f;
    private const float nodeHeight = 75f;
    private const int nodePadding = 25;
    private const int nodeBorder = 12;

    private const float connectingLineWidth = 3f;

    //Bu noktadan itibaren kendimize has bir editor penceresi yapýyoruz.
    //Amacýmýz bu script yardýmý ile oluþturdugumuz pencere ile zindanda bulunacak olan odalarýmýzý ayarlamak.
    //O odalarýn sayýsýný/tipini ve birbiriyle nasýl birleþtiðini vs bu pencere ile dizayn edeceðiz.


    [MenuItem("Room Node Graph Editor", menuItem = "Window/Dungeon Editor/Room Node Graph Editor")]
    //menuItem = dedik orasý sayesinde Unity içerisinde Window'a geldiðimizde artýk Dungeon Editor isminde yeni bi clickable button olucak.
    //Daha sonrasýnda hazýr fonksiyonlardan biri olan openwindow() ile içerisinde bu scriptten üretilen pencere açýlýcak adýda týrnak içerisinde verdiðimiz isimdir.
    private static void OpenWindow()
    {
        GetWindow<RoomNodeGraphEditor>("Room Node Graph Editor");
    }

    //Artýk blank bir penceremiz var. Yani çi boþ Ýçini doldurmak için OnGui() kullanýlýyor.
    //OnGui'nin içini doldurmak içinde style kullanýlýyor.

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

    //room node graph so'ya çift týklanýnca editörü aç
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
        //Herhangibir roomnodegraphso seçildiyse eðer iþleme devam edelim ve windowa çizdirelim.
        if(currentRoomNodeGraph != null)
        {
            DrawDraggedLine();

            ProcessEvents(Event.current);

            DrawRoomNodes();
        }

        if(GUI.changed)
            Repaint();
    }

    private void DrawDraggedLine()
    {
        if(currentRoomNodeGraph.linePosition != Vector2.zero)
        {
            Handles.DrawBezier(currentRoomNodeGraph.roomNodeToDrawLineFrom.rect.center, currentRoomNodeGraph.linePosition, 
                currentRoomNodeGraph.roomNodeToDrawLineFrom.rect.center, currentRoomNodeGraph.linePosition, Color.white, null, connectingLineWidth);
        }
    }

    private void ProcessEvents(Event currentEvent)
    {
        if(currentRoomNode == null || currentRoomNode.isLeftClickDragging == false)
        {
            currentRoomNode = IsMouseOverRoomNode(currentEvent); //mouse herhangi bir room nodeun üzerinde mi deðil mi kontrol edelim
        }
        if(currentRoomNode == null || currentRoomNodeGraph.roomNodeToDrawLineFrom != null)
        {
            ProcessRoomNodeGraphEvents(currentEvent);
        }
        else
        {
            currentRoomNode.ProcessEvents(currentEvent);
        }
    }

    private void ProcessRoomNodeGraphEvents(Event currentEvent)
    {
        switch (currentEvent.type)
        {
            case EventType.MouseDown:
                ProcessMouseDownEvent(currentEvent);
                break;

            case EventType.MouseUp:
                ProcessMouseUpEvent(currentEvent);
                break;

            case EventType.MouseDrag:
                ProcessMouseDragEvent(currentEvent);
                break;

            default:
                break;
        }
    }

    private RoomNodeSO IsMouseOverRoomNode(Event currentEvent)
    {
        for(int i = currentRoomNodeGraph.roomNodeList.Count - 1; i >= 0; i--)
        {
            if (currentRoomNodeGraph.roomNodeList[i].rect.Contains(currentEvent.mousePosition))
            {
                return currentRoomNodeGraph.roomNodeList[i];
            }
        }
        return null;
    }

    private void ProcessMouseDownEvent(Event currentEvent)
    {
        if(currentEvent.button == 1) //saðclick
        {
            ShowContextMenu(currentEvent.mousePosition);
        }
    }

    private void ShowContextMenu(Vector2 mousePosition)
    {
        GenericMenu menu = new GenericMenu(); //generic menuler özel dropdownlar oluþturmamýzý saðlýyor.
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

    private void ProcessMouseUpEvent(Event currentEvent)
    {
        if(currentEvent.button == 1 && currentRoomNodeGraph.roomNodeToDrawLineFrom != null)
        {
            ClearLineDrag();
        }
    }

    private void ProcessMouseDragEvent(Event currentEvent)
    {
        if(currentEvent.button == 1)
        {
            ProcessRightMouseDragEvent(currentEvent);
        }
    }

    private void ProcessRightMouseDragEvent(Event currentEvent)
    {
        if (currentRoomNodeGraph.roomNodeToDrawLineFrom != null)
        {
            DragConnectionLine(currentEvent.delta);
            GUI.changed = true;
        }
    }

    public void DragConnectionLine(Vector2 delta)
    {
        currentRoomNodeGraph.linePosition += delta;
    }

    private void ClearLineDrag()
    {
        currentRoomNodeGraph.roomNodeToDrawLineFrom = null;
        currentRoomNodeGraph.linePosition = Vector2.zero;
        GUI.changed = true;
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
