using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WordsDictionary
{
    public static List<string> words = new List<string> {
        "apple",
        "school",
        "book",
        "pencil",
        "home",
        "rubber",
        "bicycle",
        "swimming",
        "potatoes",
        "egg",
        "family",
        "friend",
        "happy",
        "pineapple",
        "clouds",
        "pedicab",
        "forest",
        "earth",
        "monkey",
        "kangaroo",
    };

    public static string GetRandomWord()
    {
        return words[Random.Range(0, words.Count)];
    }
}