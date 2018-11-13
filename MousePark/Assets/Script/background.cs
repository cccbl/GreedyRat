using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour {

    
    //房间
	public GameObject[] availableRooms;
	public List<GameObject> currentRooms;
	
    //陷阱 金币
	public GameObject[] availableObjects;    
	public List<GameObject> objects;

    float screenWidthInPoints;

    public float objectsMinDistance = 5.0f;    
	public float objectsMaxDistance = 10.0f;
	
	public float objectsMinY = -1.4f;
	public float objectsMaxY = 1.4f;
	
	public float objectsMinRotation = -45.0f;
	public float objectsMaxRotation = 45.0f; 

	void Start () {
        //获得摄像机正交值
        float height = 2.0f * Camera.main.orthographicSize;
        //获取或设置Camera视口的宽高比例值
         screenWidthInPoints = height * Camera.main.aspect;

        Invoke("Update", 3f);
    }
	
	// Update is called once per frame
	void Update () {
        ShowRoom();
        ShowObject();

	}

    //房间
    void AddRoom(float RoomEndX)
    {
        //获取3个room
        int randomRoom = Random.Range(0, availableRooms.Length);
        //实例化3个房间
        GameObject room = Instantiate(availableRooms[randomRoom]);
        //房间中心
        float roomCenter = RoomEndX + ((room.transform.Find("floor").localScale.x) * 0.5f);
        //最后让房间固定在中间出现
        room.transform.position = new Vector2(roomCenter,0);

        // List<GameObject> currentRooms;
        currentRooms.Add(room);
    }
    void ShowRoom()
    {
        //要移除的房间
        List<GameObject> roomToRemove = new List<GameObject>();
        bool addRooms = true;

        float playerTransform = transform.position.x;
        //玩家当前位置到达摄像机最右边
        float playerInRight = playerTransform - screenWidthInPoints;
        //玩家前方添加一个摄像机距离
        float PlayerInLeft = playerTransform + screenWidthInPoints;

        float RoomEndX = 0f;
        foreach (var room in currentRooms)
        {
            //romm width
            float roomWidth = room.transform.Find("floor").localScale.x;
            //room width/2
            float roomStartX = room.transform.position.x - (roomWidth * 0.5f);
            //room *1.5
            float roomEndX = roomStartX + roomWidth;

            // 人物在摄像机视野内
            if (roomStartX > PlayerInLeft)
                addRooms = false;

            //人物越过房间最右边
            if (roomEndX < playerInRight)
                roomToRemove.Add(room);

            RoomEndX = roomEndX;
        }
        if (addRooms)
        {
             AddRoom(RoomEndX);
        }
        //roomRemove_List，并销毁
        foreach (var room in roomToRemove)
        {
            currentRooms.Remove(room);
            Destroy(room);
           // DestroyImmediate(room);
        }

    }

    //金币 障碍物
    void AddObject(float lastObject)
    {

        //获取物体 实例化
        int randomIndex = Random.Range(0, availableObjects.Length);
        GameObject obj = Instantiate(availableObjects[randomIndex]);

        //position (X,Y)
        //加上从ShowObject()中的距离计算 
        float randomX = lastObject+Random.Range(objectsMinDistance, objectsMaxDistance); ;
        float randomY = Random.Range(objectsMinY, objectsMaxY);
        float randomRotation = Random.Range(objectsMinRotation, objectsMaxRotation);

        obj.transform.position = new Vector2(randomX, randomY);
        obj.transform.rotation = Quaternion.Euler(Vector2.right*randomRotation);

        //public List<GameObject> objects;
        objects.Add(obj);
    }
    void ShowObject()
    {
        List<GameObject> objectToRemove = new List<GameObject>();

        float PlayerTransform = transform.position.x;
        //right
        float objectRemove = PlayerTransform - screenWidthInPoints;
        //left
        float objectAdd = PlayerTransform + screenWidthInPoints;

        float objectEndX =0;
        //List<GameObject> objects
        foreach (var obj in objects)
        {
            float objX = obj.transform.position.x;
            //超出屏幕范围的添加到要移除的list
            if (objX < objectRemove)
                objectToRemove.Add(obj);

            //重新刷新 obj实例化的位置
            objectEndX = objX;
        }
        //destory objectToRemove<>;
        foreach (var obj in objectToRemove)
        {
            objects.Remove(obj);
            Destroy(obj);
        }
        //Add
        if (objectEndX < objectAdd) {
            AddObject(objectEndX);
        }
    }


}
