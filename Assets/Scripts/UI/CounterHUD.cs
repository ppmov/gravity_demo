using UnityEngine;
using UnityEngine.UI;

public class CounterHUD : MonoBehaviour
{
    [SerializeField]
    private Text score;
    [SerializeField]
    private Text timer;

    public void Reset() => Counter.Reset();

    private void OnGUI()
    {
        score.text = Counter.Score.ToString();
        timer.text = Counter.Seconds.ToString();
    }
}
