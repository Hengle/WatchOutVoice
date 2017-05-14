using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreasureManager:Manager {
    int x = 0, y = 0, z = 0;
    public List<TreasureList> ListList = new List<TreasureList>();
    Vector3 vec;
    bool isChange = false;
    public GameObject[] treasures;
    public override void Start()
    {
        base.Start();
        Load();
        //TreasureList tl = ListList[13];
        //ListList.Remove(ListList[13]);
       // ListList.Insert(0,tl);
    }
    public override void Update()
    {
        base.Update();
        ChangeMap();
    }
    public override void Destory()
    {
        base.Destory();
    }
    //加载宝藏
    public void Load()
    {
        treasures = new GameObject[35];
        GameObject go;
        TreasureList tl;
        tl = new TreasureList(0,0,0);
        ListList.Add(tl);
        for(int i = 0; i < 35; i++)
        {
            treasures[i]= Resources.Load("prefabs/" + (int)(i+1)) as GameObject;
        }
        for (int i = 1; i <= 10; i++)
        {
            //go = Resources.Load("prefabs/" + Random.Range(1, 36)) as GameObject;
            go = treasures[Random.Range(0,35)];
            GameObject gg = GameObject.Instantiate(go);
            tl.AddObject(gg);
        }
        for (int a1 = -1; a1 < 2; a1++)
        {
            for (int a2 = -1; a2 < 2; a2++)
            {
                for (int a3 = -1; a3 < 2; a3++)
                {
                    if (a1 == 0 && a2 == 0 && a3 == 0)
                        continue;
                    tl = new TreasureList(a1*200,a2*200,a3*200);
                    ListList.Add(tl);
                    for (int i = 1; i <= 10; i++)
                    {
                        //go = Resources.Load("prefabs/" + Random.Range(1, 36)) as GameObject;
                        go = treasures[Random.Range(0,35)];
                        GameObject gg = GameObject.Instantiate(go);
                        tl.AddObject(gg);
                    }
                }
            }
        }
    }
    //切换地图
    void ChangeMap()
    {
        vec = GameKernel._instance.playerManager.player.transform.position;
        if (vec.x - x >= 300)
        {
            isChange = true;
            x += 300;
        }
        if (vec.x - x <= -300)
        {
            isChange = true;
            x -= 300;
        }
        if (vec.y - y >= 300)
        {
            isChange = true;
            y += 300;
        }
        if (vec.y - y <= -300)
        {
            isChange = true;
            y -= 300;
        }
        if (vec.z - z >= 300)
        {
            isChange = true;
            z += 300;
        }
        if (vec.z - z <= -300)
        {
            isChange = true;
            z -= 300;
        }
        for (int i = 0; i < 27; i++)
        {
            ListList[i].ChangeXYZ((int)vec.x, (int)vec.y, (int)vec.z);
            //若player所在地图已经改变，找出player所在地图对应的列表，将其置于首位
            if (isChange)
                if (ListList[i].getList(x, y, z) != null)
                {
                    isChange = false;
                    TreasureList tl = ListList[i];
                    ListList.Remove(ListList[i]);
                    ListList.Insert(0, tl);
                    GameKernel._instance.voiceManager.ChangeAudio();
                }
        }
    }
}
