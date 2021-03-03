using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentSpawner : MonoBehaviour
{
    public Rect spawnArea;

    public Transform attachmentParent;
    public GameObject attachmentPrefab;

    public Transform tracker;

    public static AttachmentSpawner Instance { get; private set; }

    public List<TrainAttachment> attachments = new List<TrainAttachment>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < 26; i++)
        {
            SpawnAttachment((char)('a' + i));
        }
    }

    float GetRandomPosX()
    {
        return Random.Range(spawnArea.x, spawnArea.x + spawnArea.width);
    }

    float GetRandomPosY()
    {
        return Random.Range(spawnArea.y, spawnArea.y + spawnArea.height);
    }

    public void SpawnAttachment(char alphabet)
    {
        Vector2 pos = new Vector2(GetRandomPosX(), GetRandomPosY());

        GameObject ne = Instantiate(attachmentPrefab, pos, Quaternion.identity);
        TrainAttachment x = ne.GetComponent<TrainAttachment>();
        ne.transform.parent = attachmentParent;

        x.Initial(alphabet);
        attachments.Add(x);
    }

    public void TrackController(char alphabet)
    {
        Transform target = null;

        foreach (var x in attachments)
        {
            if (x.alphabet == alphabet)
            {
                target = x.transform;
                break;
            }
        }

        if (GameplayMaster.Instance)
        {
            tracker.position = GameplayMaster.Instance.trainHead.transform.position;
        }

        if (target)
        {
            tracker.LookAt(target);
        }
    }
}
