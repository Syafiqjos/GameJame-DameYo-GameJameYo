using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainFragment : MonoBehaviour
{
    public TextMesh text;

    public char alphabet { get; private set; }

    public void Initial(char alphabet)
    {
        this.alphabet = alphabet;
        text.text = alphabet.ToString();
    }
}
