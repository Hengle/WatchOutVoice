using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TreasureList {
    int x, y, z;
    public TreasureList(int x,int y,int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public List<GameObject> treasureList = new List<GameObject>();
    //新增宝藏到对应的列表中
    public void AddObject(GameObject go)
    {
        treasureList.Add(go);
        go.transform.position = new Vector3(Random.Range(-100+x, 100+x), Random.Range(-100+y, 100+y), Random.Range(-100+z, 100+z));
        new Treasure(go);
    }
    //将列表中所以宝藏设置到新的位置
    IEnumerator ChangeObject()
    {
        int num = treasureList.Count;
        for (int i = 0; i < num; i++)
        {
            treasureList[i].transform.position = new Vector3(Random.Range(-100 + x, 100 + x), Random.Range(-100 + y, 100 + y), Random.Range(-100 + z, 100 + z));
            yield return null;
        }
    }
    //改变列表的对应坐标
    bool isChange;
    public void ChangeXYZ(int x,int y,int z)
    {
        isChange = false;
        if (x - this.x >= 300)
        {
            isChange = true;
            this.x += 600;
        }
        if (x - this.x <= -300)
        {
            isChange = true;
            this.x -= 600;
        }
        if (y - this.y >= 300)
        {
            isChange = true;
            this.y += 600;
        }
        if (y - this.y <= -300)
        {
            isChange = true;
            this.y -= 600;
        }
        if (z - this.z >= 300)
        {
            isChange = true;
            this.z += 600;
        }
        if (z - this.z <= -300)
        {
            isChange = true;
            this.z -= 600;
        }
        if (isChange)
            GameKernel._instance.StartCoroutine(ChangeObject());
    }
    //若当前列表坐标为player所在地图，将自身返回
    public TreasureList getList(int x,int y,int z)
    {
        if (this.x == x && this.y == y && this.z == z)
        {
            return this;
        }
        return null;
    }
}
