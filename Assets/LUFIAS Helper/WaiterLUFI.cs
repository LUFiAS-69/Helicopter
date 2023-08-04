using System.Collections;
using System;
using UnityEngine;
using Helper.Waiter;


public static class WaiterLUFI
{
    public static Coroutine Wait(float secondToWait, Action action)
    {
        return StaticCoroutine.StartCoroutine(ExecuteWaitSecond(secondToWait, action));
    }

    public static Coroutine Wait(int frameToWait, Action action)
    {
        return StaticCoroutine.StartCoroutine(ExecuteWait(frameToWait, action));
        
    }

    public static Coroutine WaitEndOffFrame(int frameToWait, Action action)
    {
        return StaticCoroutine.StartCoroutine(ExecuteWaitEndOffFrame(frameToWait, action));
    }


    static IEnumerator ExecuteWaitSecond(float secondToWait, Action action)
    {
        yield return new WaitForSecondsRealtime(secondToWait);
        action?.Invoke();
    }

    static IEnumerator ExecuteWait(int frameToWait, Action action)
    {
        for (int i = 0; i < frameToWait; i++)
        {
            yield return null;
        }
        action?.Invoke();
    }

    static IEnumerator ExecuteWaitEndOffFrame(int frameToWait, Action action)
    {
        for (int i = 0; i < frameToWait; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        action?.Invoke();
    }

}


namespace Helper.Waiter
{
    public static class StaticCoroutine
    {
        class CoroutineHolder : MonoBehaviour { }

        static CoroutineHolder _runner;
        static CoroutineHolder Runner
        {
            get
            {
                if (_runner == null)
                {
                    _runner = new GameObject("Helper Waiter RestClient")
                        .AddComponent<CoroutineHolder>();
                    UnityEngine.Object.DontDestroyOnLoad(_runner);
                }
                return _runner;
            }
        }

        public static Coroutine StartCoroutine(IEnumerator _coroutine)
        {
            return Runner.StartCoroutine(_coroutine);
        }
    }
}

