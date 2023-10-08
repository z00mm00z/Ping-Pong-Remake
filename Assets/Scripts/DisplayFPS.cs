using UnityEngine;
using UnityEngine.UI;

public class DisplayFPS : MonoBehaviour
{
    public Text FpsText;
    public bool displayFPS = true;

    private float pollingTime = 1f;
    private float time;
    private int frameCount;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        frameCount++;

        if(time >= pollingTime) {
            int frameRate = Mathf.RoundToInt(frameCount/time);
            if(displayFPS) { FpsText.text = frameRate.ToString() + " FPS"; }
            Debug.Log(frameRate);
            time -= pollingTime;
            frameCount = 0;
        }
    }
}
