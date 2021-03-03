using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainAttachment : MonoBehaviour
{
    public char alphabet { get; private set; } = 'a';

    public GameObject graphic;
    public TextMesh text;
    public ParticleSystem particleExplosion;

    public void Initial(char alphabet)
    {
        this.alphabet = alphabet;
        text.text = char.ToUpper(alphabet).ToString();
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
        graphic.SetActive(false);
        GetComponent<Collider2D>().enabled = false;
        particleExplosion.Play();

        StartCoroutine(DestroyLate());
    }

    IEnumerator DestroyLate()
    {
        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }
}
