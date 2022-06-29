using System;
using CustomGameEvent;
using UnityEngine;
using UnityEngine.UI;

public class SliderNoize : MonoBehaviour
{
    [SerializeField] [Range(0,5)] private float _noiseIncrease = 3f;
    [SerializeField] [Range(0,5)] private float _noiseReduction = 2f;
    [SerializeField] private float _minNoize = 0f;
    [SerializeField] private float _maxNoize = 10f;
    
    private Slider _slider;
    private float _currentNoiseValue = 0f;
    private float _offset = 0f;
    private bool _isNoiseExcess = false;
    private bool _isMovePlayer;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        GameEvent.OnMovePlayer.AddListener(IsMovePlayer);
        GameEvent.OnPrepare += OnGamePrepare;
    }

    private void OnDestroy()
    {
        GameEvent.OnPrepare -= OnGamePrepare;
    }

    private void Update()
    {
        if (!_isNoiseExcess)
        {
            HearToPlayerNoise();
        }
    }
    
    private void IsMovePlayer(bool isMovePlayer)
    {
        _isMovePlayer = isMovePlayer;
    }

    private void OnGamePrepare()
    {
        _offset = 0;
        _isNoiseExcess = false;
        _isMovePlayer = false;
        _slider.minValue = _minNoize;
        _slider.maxValue = _maxNoize;
        _slider.value = _currentNoiseValue = 0f;
    }
    
    private void ChangesNoise()
    {
        if (_isMovePlayer)
        {
            _offset = _noiseIncrease * Time.deltaTime;
            
            if (_maxNoize - _currentNoiseValue > _offset )
            {
                _currentNoiseValue += _offset;
            }
            else
            {
                _currentNoiseValue = _maxNoize;
                _isNoiseExcess = true;
            }
        }
        else
        {
            if (_currentNoiseValue > _minNoize)
            {
                _offset = _noiseReduction * Time.deltaTime;
                
                if (_currentNoiseValue - _minNoize > _offset)
                {
                    _currentNoiseValue -= _offset;
                }
                else
                {
                    _currentNoiseValue = _minNoize;
                }
            }
        }
        _slider.value = _currentNoiseValue;
    }

    private void HearToPlayerNoise()
    {
        ChangesNoise();
        if (_isNoiseExcess)
        {
            GameEvent.SendMaxNoize();
        }
    }
}
