using UnityEngine;
using System.Collections;

/// <summary>
/// AudioSourceの無音を削除してループ再生するサンプル
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class SoundController: MonoBehaviour
{

    AudioSource audioSource;
    float startTime;
    float endTime;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // クリップの全サンプルデータ取得
        AudioClip clip = audioSource.clip;
        if (null == clip)
        {
            Debug.Log("clip null");
            this.enabled = false;
            return;
        }

        int sampleLength = clip.samples * clip.channels;
        Debug.Log("clip length : " + sampleLength);
        float[] allSamples = new float[sampleLength];
        clip.GetData(allSamples, 0);

        int start, end;
        start = 0;
        end = sampleLength;

        // 音の開始位置を取得
        for (int i = 0; i < sampleLength; i += clip.channels)
        {
            if (allSamples[i] < 0.03f) continue;
            start = i;
            break;
        }
        // 音の終了位置を取得
        for (int i = sampleLength - 1; i >= 0; i -= clip.channels)
        {
            if (allSamples[i] < 0.03f) continue;
            end = i;
            break;
        }

        startTime = clip.length * (float)start / sampleLength;
        endTime = clip.length * (float)end / sampleLength;

        Debug.Log("音の長さ : " + clip.length);
        Debug.Log("音の開始位置 : " + startTime + "\n音の終了位置 : " + endTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // 通常ループ再生
            audioSource.loop = true;
            audioSource.Play();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            // 無音除去してループ再生
            StartCoroutine(audio_loop());
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            // 停止
            stop = true;
            audioSource.Stop();
        }
    }

    bool stop;
    IEnumerator audio_loop()
    {
        stop = false;
        audioSource.loop = false;
        while (!stop)
        {
            //			audioSource.Play((ulong)startTime * 1000);
            audioSource.PlayDelayed(startTime);

            float waitTime = Mathf.Abs(endTime - startTime);
            yield return new WaitForSeconds(waitTime);
        }
    }
}