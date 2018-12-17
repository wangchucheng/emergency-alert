using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation2 : MonoBehaviour {

    //用来装饰初始化地图所需物体
    public GameObject[] item;

    //已经有东西的位置列表
    private List<Vector3> itemPositionList = new List<Vector3>();

    private void Awake()
    {

        //箱子
        for (int i = 0; i < 50; i++)
        {
            CreatItem(item[0], CreateRandomPosition(), Quaternion.identity);
            CreatItem(item[1], CreateRandomPosition(), Quaternion.identity);
            CreatItem(item[2], CreateRandomPosition(), Quaternion.identity);
        }


    }

    private void CreatItem(GameObject creatGameObject, Vector3 creatPosition, Quaternion creatRotation)
    {
        GameObject itemGo = Instantiate(creatGameObject, creatPosition, creatRotation);
        itemGo.transform.SetParent(gameObject.transform);
        itemPositionList.Add(creatPosition);
    }

    //产生随机位置的方法
    private Vector3 CreateRandomPosition()
    {
        while (true)
        {
            int posX = Random.Range(-550, 550);
            int posZ = Random.Range(-550, 550);
            if ((posX < -100 || posX > 100) && (posZ < -100 || posZ > 100))
            {
                Vector3 creatPosition = new Vector3(posX, 0, posZ);
                if (!HasThePosition(creatPosition))
                {
                    return creatPosition;
                }
            }
        }
        
    }


    //判断位置列表中是否有这个位置
    private bool HasThePosition(Vector3 creatPos)
    {
        for (int i = 0; i < itemPositionList.Count; i++)
        {
            if (creatPos == itemPositionList[i])
            {
                return true;
            }
        }
        return false;
    }
}
