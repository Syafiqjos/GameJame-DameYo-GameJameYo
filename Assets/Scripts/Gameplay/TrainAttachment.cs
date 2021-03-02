using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainAttachment : MonoBehaviour
{
    public char alphabet { get; private set; } = 'A';

    public TextMesh text;

    private void Start()
    {
        text.text = alphabet.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            TrainHead head = other.GetComponent<TrainHead>();
            head.AddFragment(alphabet);

            DestroyAttachment(); 
        }
    }

    public void DestroyAttachment()
    {
        Destroy(gameObject);
    }
}
