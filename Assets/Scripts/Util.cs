using System.Collections;
using UnityEngine;

public class Util
{
    public static IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
