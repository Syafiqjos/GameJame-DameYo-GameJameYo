using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainHead : MonoBehaviour
{
    public float trainSpeed;

    public GameObject trainFragment;

    public TrailRenderer trailRenderer;

    public float trailRatio = 0.5f;
    [Range(0,1)]public float trailAddon = 0.5f;
    public int trailIndexDelta = 5;

    public string formedWord { get { string x = ""; foreach (var p in fragments) x += p.alphabet; return x; } }

    private Camera cameraV;
    private Vector3 lastDirection;
    private List<TrainFragment> fragments = new List<TrainFragment>();

    private void Awake()
    {
        cameraV = Camera.main;
    }

    private void Update()
    {
        if (GameplayMaster.isPlaying)
        {
            MovementController();
            FragmentsController();

            DetachController();
        }
    }    

    void DetachController()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DetachLastFragment();
        }
    }

    private void DetachLastFragment()
    {
        if (fragments.Count > 0)
        {
            fragments.RemoveAt(fragments.Count - 1);
        }
    }

    void MovementController()
    {
        Vector2 point = Input.mousePosition;
        Vector2 centerScreen = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
        Vector2 direction = (point - centerScreen);

        lastDirection = direction;

        transform.position += lastDirection.normalized * trainSpeed;
    }

    public void AddFragment(char alphabet)
    {
        TrainFragment fragment = Instantiate(trainFragment).GetComponent<TrainFragment>();
        if (GameplayMaster.Instance)
        {
            fragment.transform.parent = GameplayMaster.Instance.trainsParent;
        }
        fragment.Initial(alphabet);

        if (fragments.Count > 0)
        {
            fragment.transform.position = fragments[fragments.Count - 1].transform.position;
        } else
        {
            fragment.transform.position = transform.position;
        }

        fragments.Add(fragment);

        health = true;

        GameplayMaster.AddPoints(1);
        CheckFormedWord();

        if (AttachmentSpawner.Instance) {
            AttachmentSpawner.Instance.SpawnAttachment(alphabet);
        }
    }

    private void FragmentsController()
    {
        trailRenderer.time = trailAddon + fragments.Count * trailRatio;

        for (int i = 0;i < fragments.Count;i++)
        {
            int ind = trailRenderer.positionCount - (i + 1) * trailIndexDelta;
            if (ind < trailRenderer.positionCount && ind >= 0)
            {
                fragments[i].transform.position = trailRenderer.GetPosition(ind);
            } else
            {
                if (i - 1 >= 0)
                {
                    fragments[i].transform.position = fragments[i - 1].transform.position;
                } else
                {
                    fragments[i].transform.position = transform.position;
                }
            }
        }
    }

    private bool health = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fragments")
        {
            if (fragments.Count > 1)
            {
                health = false;
            }

            if (health == false)
            {
                GameplayMaster.GameOver();
                Debug.Log("Kill Us");
            }
            health = false;
        } else if (other.tag == "Obstacle")
        {
            GameplayMaster.GameOver();
        }
    }

    private void CheckFormedWord()
    {
        if (formedWord != string.Empty)
        {
            Debug.Log(formedWord);
            if (GameplayMaster.CheckWord(formedWord) > 0)
            {
                //Success
                int n = fragments.Count;
                for (int i = 0;i < n; i++)
                {
                    DetachLastFragment();
                }
            }
        }
    }
}
