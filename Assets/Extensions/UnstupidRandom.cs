using System;

public static class UnstupidRandom {

    /// <summary>
    /// Equivalent to UnityEngine.Random.Range() except not stupid.
    /// </summary>
    public static int Range(this int startInclusive, int endInclusive) {
        return UnityEngine.Random.Range(startInclusive, endInclusive + 1);
    }

}