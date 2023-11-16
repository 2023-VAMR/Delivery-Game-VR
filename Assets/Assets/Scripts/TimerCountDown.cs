using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimerCountDown : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textObject;

    [SerializeField] int startSeconds = 100;

    float timer = 0.0f;
    int remainingSeconds;

    // Start is called before the first frame update
    void Start()
    {
        remainingSeconds = startSeconds;
        textObject.text = remainingSeconds.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingSeconds > 0)
        {
            timer += Time.deltaTime;
            remainingSeconds = startSeconds - (int)(timer % 60);
            textObject.text = remainingSeconds.ToString();
        }
        else
        {
            // GameOver
        }
    }
}
