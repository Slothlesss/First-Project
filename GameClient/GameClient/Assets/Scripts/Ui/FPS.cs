using TMPro;
using UnityEngine;
public class FPS : MonoBehaviour
{
    public TextMeshProUGUI fpsTxt;
    private float time;
    private readonly float pollingTime = 1f;
    int frameCount;
    void Update()
    {
        time += Time.deltaTime;
        frameCount++;
        if (time > pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            fpsTxt.text = "<color=yellow> Fps: " + frameRate.ToString();
            time -= pollingTime;
            frameCount = 0;
        }
    }
}
