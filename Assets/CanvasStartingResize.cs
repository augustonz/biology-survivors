using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class CanvasStartingResize : MonoBehaviour
{
    [SerializeField] RectTransform rect;
    Vector2 _startingScale;
    [SerializeField] AnimationCurve _openingCurve;
    [SerializeField] AnimationCurve _closingCurve;
    [SerializeField] AnimationCurve _hiddenCurve;
    [SerializeField] float _endScaleMultiply;

    [SerializeField] bool open;
    [SerializeField] bool close;
    [SerializeField] bool hidden;
    [SerializeField] float _duration;
    float _currentDuration;

    private void Start()
    {
        _startingScale = rect.sizeDelta;
    }

    // Update is called once per frame
    async void Update()
    {
        if (open)
        {
            rect.sizeDelta = Vector2.Lerp(_startingScale, _startingScale * _endScaleMultiply, _openingCurve.Evaluate(_currentDuration / _duration));
            if (_currentDuration >= _duration)
            {
                open = false;
                _currentDuration = 0;
                
            }
            else
                _currentDuration += Time.unscaledDeltaTime;
        }

        else if (close)
        {
            rect.sizeDelta = Vector2.Lerp(_startingScale * _endScaleMultiply, _startingScale, _closingCurve.Evaluate(_currentDuration / _duration));
            if (_currentDuration >= _duration)
            {
                close = false;
                _currentDuration = 0;
                await Task.Delay(1000);
                hidden = true;
            }
            else
                _currentDuration += Time.unscaledDeltaTime;
        }
        
        else if (hidden)
        {
            rect.sizeDelta = Vector2.Lerp(_startingScale, _startingScale * 0, _hiddenCurve.Evaluate(_currentDuration / _duration));
            if (_currentDuration >= _duration)
            {
                hidden = false;
                _currentDuration = 0;

            }
            else
                _currentDuration += Time.unscaledDeltaTime;
        }
    }

    public void OpenBall()
    {
        close = false;
        open = true;
        hidden = false;
        _currentDuration = 0;
    }

    public void CloseBall()
    {
        close = true;
        open = false;
        hidden = false;
        _currentDuration = 0;
    }

    public void HiddenBall()
    {
        hidden = true;
        close = false;
        open = false;
        _currentDuration = 0;
    }
}
