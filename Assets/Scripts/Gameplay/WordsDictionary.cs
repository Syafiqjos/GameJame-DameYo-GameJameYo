using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WordsDictionary
{
    public static List<string> words = new List<string> {
        "Kadal",
        "Kucing"
    };

    public static string GetRandomWord()
    {
        return words[Random.Range(0, words.Count)];
    }
}
