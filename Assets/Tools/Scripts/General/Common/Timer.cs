using System;
using System.Collections;
using UnityEngine;

public class Timer:MonoBehaviour
{
    Coroutine _coroutine;
    public event Action OnTimerComplete;
    
    public Timer(float minutes,float seconds)
    {
        float time = (minutes * 60) + seconds;
        _coroutine = StartCoroutine(StartTimer(time));
    }
    
    public Timer(float seconds)
    {
        _coroutine = StartCoroutine(StartTimer(seconds));
    }

    private IEnumerator StartTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        OnTimerComplete?.Invoke();
    }
    
    public void StopTimer() => StopCoroutine(_coroutine);
}
