using UnityEngine;
using System.Collections;

public class GameKernel : MonoBehaviour {
    public GameObject[] imudges;
    public static GameKernel _instance;
    public delegate void OnStart();
    public event OnStart start;
    public delegate void OnUpdate();
    public event OnUpdate update;
    public delegate void OnDestory();
    public event OnDestory destory;
    public VoiceManager voiceManager = new VoiceManager();
    public TreasureManager treasureManager = new TreasureManager();
    public PlayerManager playerManager = new PlayerManager();
    public GameObject[] effects;
    bool isOver = true;
    float timee = 0;
    bool isPlay = false;
    void Awake()
    {
        Application.targetFrameRate = 100;
        _instance = this;
        start += voiceManager.Start;
        start += treasureManager.Start;
        start += playerManager.Start;
        update += treasureManager.Update;
        update += voiceManager.Update;
        update += playerManager.Update;
        destory += treasureManager.Destory;
        destory += voiceManager.Destory;
        destory += playerManager.Destory;
        start();
        voiceManager.ChangeAudio();
        StartCoroutine(AddEffect());
    }
    void Update()
    {
        timee += Time.deltaTime;
        if (timee >= 18&&!isPlay)
        {
            for(int i = 0; i < 8; i++)
            {
                Destroy(imudges[i]);
            }
            playerManager.SetState();
            voiceManager.SetAudio(true);
            isPlay = true;
            playerManager.gvrHead.trackRotation = true;
        }
        update();
        Reset();
    }
    //重新设置声源
    void Reset()
    {
        if(isOver)
        if (Vector3.Distance(playerManager.player.transform.position, voiceManager.voiceObject.transform.position) < 10)
        {
            StartCoroutine(GetTreasure(voiceManager.voiceObject));         
        }
    }
    Vector3 vec;
    GameObject go;
    GameObject gos;
    IEnumerator AddEffect()
    {
        effects = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            go= Resources.Load("Effect/" + (int)(i+1)) as GameObject;
            go.SetActive(false);
            effects[i] = GameObject.Instantiate(go);
        }
        yield return null;
    }
    //当找到宝藏时调用，生成一个随机的粒子特效
    IEnumerator GetTreasure(GameObject obj)
    {
        isOver = false;
        playerManager.playerStates = PlayerManager.states.other;
        Random.seed = ((int)Time.time);
        vec = Vector3.Normalize(obj.transform.position - playerManager.player.transform.position);
        gos= effects[Random.Range(0,4)];
        gos.SetActive(true);
        gos.transform.SetParent(obj.transform);
        gos.transform.localPosition = new Vector3(0, 0, 0);
        StartCoroutine(Move(gos, playerManager.player.transform.position- gos.transform.position, 1));
        voiceManager.SetWind(false);
        yield return new WaitForSeconds(3);
        playerManager.playerStates = PlayerManager.states.move;
        voiceManager.ChangeAudio();
        voiceManager.SetAudio(true);
        gos.SetActive(false);
        isOver = true;
    }
    float timer;
    IEnumerator Move(GameObject obj,Vector3 vec,float time)
    {
        timer = 0;
        while(timer<time)
        {
            timer += Time.deltaTime;
            obj.transform.Translate(vec * Time.deltaTime,Space.World );
            yield return null;
        }
    }
}
