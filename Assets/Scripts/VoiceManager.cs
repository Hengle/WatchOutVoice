using UnityEngine;
using System.Collections;

public class VoiceManager : Manager
{
    public GameObject voiceObject;
    public GvrAudioSource audioSource;
    public GvrAudioListener audioListener;
    public GvrAudioListener Listener
    {
        get
        {
            if (audioListener == null)
            {
                audioListener = GameKernel._instance.playerManager.player.GetComponentInChildren<GvrAudioListener>();
            }
            return audioListener;
        }
    }
    public GameObject gold;
    public AudioSource windSource;
    public AudioSource imudgesSource;
    private int level = 10;
    private float timer = 0;
    private bool isPlaying = false;
    public override void Start()
    {
        base.Start();
        AddAudio();
        Load();
        windSource = GameKernel._instance.transform.GetComponent<AudioSource>();
        imudgesSource = GameKernel._instance.imudges[7].GetComponent<AudioSource>();
    }
    public override void Update()
    {
        base.Update();
        if (!isPlaying)
        {
            if (Time.fixedTime >= 10.5f)
            {
                imudgesSource.Play();
                isPlaying = true;
            }
        }
        ResetAudio();
    }
    public override void Destory()
    {
        base.Destory();
    }
    //装载声源对象
    GameObject go;
    void Load()
    {
        //go = Resources.Load("prefabs/GVR") as GameObject;
        //voiceObject = GameObject.Instantiate(go);
        voiceObject = GameObject.Find("GVR");
        gold = voiceObject.transform.FindChild("gold").gameObject;
        audioSource = voiceObject.GetComponent<GvrAudioSource>();
    }
    AudioClip[] audios;
    void AddAudio()
    {
        audios = new AudioClip[3];
        for(int i = 0; i < 3; i++)
        {
            audios[i]= Resources.Load("music/" +(int)(i+1))as AudioClip;
        }
    }
    //切换声源
    public void ChangeAudio()
    {
        if (level > 1)
            level--;
        Random.seed = ((int)Time.time);
        //audioSource.clip = Resources.Load("music/" + Random.Range(1, 4)) as AudioClip; 
        audioSource.clip = audios[Random.Range(0, 3)];
        //SetAudio(true);
        voiceObject.transform.SetParent(GameKernel._instance.treasureManager.ListList[0].treasureList[Random.Range(0, 10)].transform);
        voiceObject.transform.localPosition = new Vector3(0, 0, 0);
    }
    AudioClip ac;
    //根据距离设置声音大小以及粒子特效播放情况
    public void ResetAudio()
    {
        timer += Time.deltaTime;
        if (timer > 10)
            timer = 0;
        if (timer < level)
        {
            gold.SetActive(true);
        }
        else
        {
            gold.SetActive(false);
        }
        Listener.worldScale = Mathf.Max(8000 / (Vector3.Distance(voiceObject.transform.position, GameKernel._instance.playerManager.player.transform.position)), 80);
    }
    //设置声音播放状态
    public void SetAudio(bool isPlay)
    {
        if (isPlay)
            audioSource.Play();
        else
            audioSource.Stop();
    }
    public void SetWind(bool isPlay)
    {
        if (isPlay)
            windSource.Play();
        else
            windSource.Stop();
    }
}
