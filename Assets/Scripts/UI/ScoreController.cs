using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class ScoreController : MonoBehaviour
{
    private const string BEST_SCORE = "BestScore";

    [SerializeField]
    private TextMeshProUGUI _currentScoreLabel;
    [SerializeField]
    private int _scorePerSquare;
    [SerializeField]
    private AudioSource _bestScoreSound;
    [SerializeField]
    private float _scaleDuration; //время изменения скейла

    [SerializeField]
    private float _scaleFactor; //коэфицент изменения скейла

    private int _currentScore;
    private int _bestScore; 
    
    private void Awake()
    {
        _bestScore = PlayerPrefs.GetInt(BEST_SCORE);
    }
    public void AddScore()
    {
        _currentScore+= _scorePerSquare;

        _currentScoreLabel.text = _currentScore.ToString();
        
        _currentScoreLabel.transform
            .DOPunchScale(Vector3.one * _scaleFactor, _scaleDuration, 0)
            .OnComplete(() => _currentScoreLabel.transform.DOScale(Vector3.one, 0));
    }

    public int GetCurrentScore()
    {
        return _currentScore;
    }
    
    public int GetBestScore()
    {
        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;
            PlayerPrefs.SetInt(BEST_SCORE, _bestScore); 
            PlayerPrefs.Save();
            _bestScoreSound.Play();
        }

        return _bestScore;
    }
}
