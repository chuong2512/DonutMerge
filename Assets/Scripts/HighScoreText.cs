using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class HighScoreText : MonoBehaviour
    {
        private TextMeshProUGUI _highScore;
        
        private void Start()
        {
            _highScore = GetComponent<TextMeshProUGUI>();
            _highScore.text = PlayerPrefs.GetInt("highScore").ToString();
        }
    }
}