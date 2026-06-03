using TMPro;
using UnityEngine;

public class WaveDisplayController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemySpawner.Wavestarted.AddListener(UpdateWaveCount);
    }

    // Update is called once per frame
    void UpdateWaveCount(int newWave){
        GetComponent<TextMeshProUGUI>().text = newWave.ToString("000");
    }
}
