using UnityEngine;
using System.Collections;

public class Treasure {
    public GameObject treasure;
    public int rate = 5;
    public int pos = 3;
    //初始化宝藏对象
    public Treasure(GameObject trea)
    {
        treasure = trea;
        treasure.transform.Rotate(new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
        treasure.transform.localScale = new Vector3(12,12,12);
    }
}
