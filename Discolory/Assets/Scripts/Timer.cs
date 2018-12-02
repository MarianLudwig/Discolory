using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeLeft = 300.0f;

    public Text text;

    void Start()
    {
        timeLeft += Time.time;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;

        text.text = timeLeft.ToString("N0");
        //text.text = "" + Mathf.Round(timeLeft);
        if (timeLeft < 0)
        {
            LevelTransition.Instance.FadeToScene();
        }
    }
}
