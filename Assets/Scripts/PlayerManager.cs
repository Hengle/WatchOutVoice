using UnityEngine;
using System.Collections;

public class PlayerManager:Manager {
    public enum states
    {
        move,
        run,
        other
    }
    public states playerStates = states.move;
    public GameObject player;
    public float speed = 10;
    private Camera []cameras;
    private float timer = 0;
    private Vector3 startForward;
    private Vector3 nowForward;
    public GvrHead gvrHead;
    public GvrViewer gvrViewer;
    public override void Start()
    {
        base.Start();
        startForward = nowForward = Vector3.zero;
        player = GameKernel._instance.gameObject;
        cameras = player.GetComponentsInChildren<Camera>();
        gvrViewer = player.GetComponent<GvrViewer>();
        gvrHead = player.GetComponentInChildren<GvrHead>();
        playerStates = states.other;
        gvrHead.trackRotation = false;
    }
    //控制player向前方运动
    public override void Update()
    {
        base.Update();
        speed = 10;
        if (playerStates == states.move)
        {
            nowForward = gvrHead.transform.forward;
            if (nowForward != startForward)
                speed = 3;
            startForward = nowForward;
            ChoiceTreasure();
            player.transform.Translate(gvrHead.transform.forward * speed * Time.deltaTime, Space.World);
        }
        if(playerStates == states.run)
        {
            if (GameKernel._instance.voiceManager.windSource.isPlaying == false)
            {
                GameKernel._instance.voiceManager.SetWind(true);
            }
            player.transform.Translate(Vector3.Normalize(treasurePos- player.transform.position) * speed*3 * Time.deltaTime, Space.World);
        }
    }
    public override void Destory()
    {
        base.Destory();
    }
    public void SetState()
    {
        playerStates = states.move;
        for (int i = 0; i < 3; i++)
        {
            cameras[i].clearFlags = CameraClearFlags.Skybox;
            cameras[i].cullingMask = 1;
        }
    }
    Vector3 treasurePos;
    Vector3 dir;
    float angle;
    void ChoiceTreasure()
    {
        treasurePos = GameKernel._instance.voiceManager.voiceObject.transform.parent.position;
        dir = Vector3.Normalize(player.transform.position - treasurePos);
        angle = Vector3.Angle(gvrHead.transform.forward, dir);
        if (angle > 172)
        {
            timer += Time.deltaTime;
        }
        else
            timer = 0;
        if (timer >= 1)
        {
            playerStates = states.run;
        }
    }
}
