using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//在每一个小场景内生成小物件
public class CreateInSamll : MonoBehaviour {
    public Transform Origin;              //每个小地点的相对原点
    public GameObject[] trees;            //树
    public GameObject[] Items;            //小物件
    public Transform[] BuildPos;         //可以建造的位置
    private List<Transform> CreatedPoint = new List<Transform>();      //已经建造的位置
    public int SceneNum;                 //场景编号
	// Use this for initialization
	void Awake () {
        switch (SceneNum)
        {
            case 0:
                BornPoint();
                break;
            case 1:
                BuildForest();
                break;
            case 2:
                BuildResidence();
                break;
        }
            
    }
	
    //建造出生点
	private void BornPoint()
    {
        
        if (Items.Length != 0)
        {
            int posRan = Random.Range(2, BuildPos.Length);
            for (int i = 1; i <= posRan; i++)
            {
                if (!IsInList(BuildPos[posRan]))
                {
                    int itemRan = Random.Range(0, Items.Length);
                    CreatItem(Items[itemRan], BuildPos[i], Quaternion.identity);
                }

            }
        }

        if (trees.Length != 0)
        {
            int posX = Random.Range(3, 4);
            int posZ = Random.Range(2, 4);
            for (int j = 0; j < posX * 9; j += 9)
            {
                int index = Random.Range(0, trees.Length);
                for (int k = 0; k < posZ * 9; k += 9)
                {
                    Vector3 treePos = new Vector3(Origin.position.x + j, Origin.position.y,
                    Origin.position.z + k);
                    CreatItem(trees[index], treePos, Quaternion.identity);
                }

            }
        }


    }

    //建造森林
    private void BuildForest()
    {
        if (trees.Length != 0)
        {
            for (int j = 0; j < 100; j += 15)
            {
                int RanNum = Random.Range(7, 10);
                
                for (int k = 0; k < 100; k += 15)
                {
                    int index = Random.Range(0, trees.Length);
                    Vector3 treePos = new Vector3(Origin.position.x + j+10-RanNum, Origin.position.y,
                    Origin.position.z + k + 10 - RanNum);
                    CreatItem(trees[index], treePos, Quaternion.identity);
                }

            }
        }
    }

    //建造居民区
    private void BuildResidence()
    {
        //居民区items[0]是防盗网，建造防盗网
        for (int i = 0; i < 100; i+=5)
        {
            //横排
            if (i != 45 && i != 50)
            {
                Vector3 Row1 = new Vector3(Origin.position.x + 2 + i, Origin.position.y, Origin.position.z);
                Vector3 Row2 = new Vector3(Origin.position.x + 2 + i, Origin.position.y, Origin.position.z + 100);
                CreatItem(Items[0], Row1, Quaternion.identity);
                CreatItem(Items[0], Row2, Quaternion.Euler(new Vector3(0, 180, 0)));
            }
            //竖排
            Vector3 Col1 = new Vector3(Origin.position.x-0.6f , Origin.position.y, Origin.position.z+2+i);
            Vector3 Col2 = new Vector3(Origin.position.x + 100-0.6f, Origin.position.y, Origin.position.z +2+ i);
            CreatItem(Items[0], Col1, Quaternion.Euler(new Vector3(0,90,0)));
            CreatItem(Items[0], Col2, Quaternion.Euler(new Vector3(0, -90, 0)));
        }
        //建筑
        for (int i = 5; i < 100; i+=15)
        {
            if (i<45)
            {
                for (int j = 10; j < 90; j += 17)
                {
                    int buildingRan = Random.Range(1, 4);
                    Vector3 buildingPos = new Vector3(Origin.position.x + i, Origin.position.y, Origin.position.z + j);
                    if (buildingRan==1)
                    {
                        CreatItem(Items[buildingRan], buildingPos, Quaternion.Euler(new Vector3(0,-90,0)));
                    }
                    else
                    {
                        CreatItem(Items[buildingRan], buildingPos, Quaternion.identity);
                    }
                    
                }
            }
            if (i>61)
            {
                for (int j = 13; j < 90; j += 30)
                {
                    int buildingRan = Random.Range(4, 7);
                    Vector3 buildingPos = new Vector3(Origin.position.x + i, Origin.position.y, Origin.position.z + j);
                    CreatItem(Items[buildingRan], buildingPos, Quaternion.identity);
                }
            }
        }
        for (int i = 2; i < 90; i+=10)
        {
            Vector3 treePos1 = new Vector3(Origin.position.x + 43, Origin.position.y, Origin.position.z + i);
            Vector3 treePos2 = new Vector3(Origin.position.x + 56, Origin.position.y, Origin.position.z + i);
            CreatItem(trees[0], treePos1, Quaternion.identity);
            CreatItem(trees[0], treePos2, Quaternion.identity);
        }
    }
    private void CreatItem(GameObject item, Transform createPos, Quaternion createRot)
    {
        GameObject ItemGo = Instantiate(item, createPos.position, createRot);
        ItemGo.transform.SetParent(gameObject.transform);
        CreatedPoint.Add(createPos);
    }
    
    private bool IsInList(Transform creatPos)
    {
        for(int i = 0; i < CreatedPoint.Count; i++)
        {
            if (creatPos==CreatedPoint[i])
            {
                return true;
            }
        }
        return false;
    }

    private void CreatItem(GameObject item, Vector3 createPos, Quaternion createRot)
    {
        GameObject ItemGo = Instantiate(item, createPos, createRot);
        ItemGo.transform.SetParent(gameObject.transform);
    }
}
