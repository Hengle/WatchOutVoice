using UnityEngine;
using System.Collections;

public class ImudgesPlay : MonoBehaviour {
    private TweenPosition[] tweenPosition;
    private TweenRotation[] tweenRotation;
    public float[] delayTime1;
    public float[] delayTime2;
    private float timer = 0;
    private int i = 0;
    private int j = 0;
    void Start()
    {
        tweenPosition = transform.GetComponents<TweenPosition>();
        tweenRotation = transform.GetComponents<TweenRotation>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (i < delayTime1.Length)
        {
            if (timer > delayTime1[i])
            {
                tweenPosition[i].PlayForward();
                i++;
            }
        }
        if (j < delayTime2.Length)
        {
            if (timer > delayTime2[j])
            {
                tweenRotation[j].PlayForward();
                j++;
            }
        }
    }
}
