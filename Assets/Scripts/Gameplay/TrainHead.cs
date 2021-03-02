using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainHead : MonoBehaviour
{
    public float trainSpeed;
    public float minimumMouseDelta = 1.0f;

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
        FollowCursor();
        FragmentsController();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddFragment('A');
        }
    }

    void FollowCursor()
    {
        Vector3 point = cameraV.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (point - transform.position);

        direction.z = 0;

        if (direction.magnitude > minimumMouseDelta)
        {
            lastDirection = direction;
        }

        transform.position += lastDirection.normalized * trainSpeed;
    }

    public void AddFragment(char alphabet)
    {
        TrainFragment fragment = Instantiate(trainFragment).GetComponent<TrainFragment>();
        fragment.Initial(alphabet);

        if (fragments.Count > 0)
        {
            fragment.transform.position = fragments[fragments.Count - 1].transform.position;
        } else
        {
            fragment.transform.position = transform.position;
        }

        fragments.Add(fragment);

        CheckFormedWord();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fragments")
        {
            Debug.Log("Kill Us");
        }
    }

    private void CheckFormedWord()
    {
        if (formedWord != string.Empty)
        {

        }
    }
}
