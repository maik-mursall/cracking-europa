using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Utils
{
    /// <summary>
    /// Executes after amount of Seconds
    /// StartCoroutine( Xseconds, delayedAction(() => {
    /// WRITE YOUR CODE HERE
    /// }) );
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static IEnumerator ExecuteAfterSeconds(float seconds, UnityAction action)
    {
        yield return new WaitForSeconds(seconds); // Wait for the next frame
        action.Invoke(); // execute a delegate
    }
}
