using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGeneration
{
    List<string> firstNames = new List<string>()
    {
        "foo", "baz", "qux", "honk", "bonk", "flerb"
    };
    List<string> lastNames = new List<string>()
    {
        "foor", "bazz", "quxx", "honkk", "bonkk", "flerbb"
    };

    private static System.Random rnd = new System.Random();

    public string GenerateFirstName()
    {
        return GetRandomElementFromNamesList(firstNames);
    }
    
    public string GenerateLastName()
    {
        return GetRandomElementFromNamesList(lastNames);
    }

    private static string GetRandomElementFromNamesList(IList<string> names)
    {
        var r = rnd.Next(names.Count);
        return names[r];
    }
} 

