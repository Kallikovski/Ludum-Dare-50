using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Variables

    [SerializeField] private float gapSize;
    [SerializeField] private float spawnfrequency;

    private float timePassed;
    private Vector3 boundSize;
    private int segmentCount = 4;

    //References
    [SerializeField] private GameObject barrierPrefab;
    [SerializeField] private GameObject spawnArea;
    void Start()
    {
        boundSize = spawnArea.GetComponent<Collider>().bounds.size;
        timePassed = spawnfrequency/2;
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed >= spawnfrequency)
        {
            SpawnBarrierWave();
            timePassed = 0f;
        }
    }

    private void SpawnBarrierWave()
    {
        int gapSegmentIndex = Random.Range(0, segmentCount);
        float segmentLevel = boundSize.y/segmentCount;
        for (int i = 0; i < segmentCount; i++)
        {
            if(gapSegmentIndex == i)
            {
                SpawnGapBarrier(segmentLevel,i);
            }
            else
            {
                GameObject barrier = Instantiate(barrierPrefab, transform);
                barrier.transform.position = new Vector3(spawnArea.transform.position.x, segmentLevel * i + segmentLevel / 2, spawnArea.transform.position.z);          
            }
        }
    }

    private void SpawnGapBarrier(float segmentLevel, int index)
    {
        GameObject barrierPart_1 = Instantiate(barrierPrefab, transform);
        GameObject barrierPart_2 = Instantiate(barrierPrefab, transform);
        float maxWidth = barrierPart_1.GetComponent<Collider>().bounds.size.x;
        float firstSegmentEndPoint = Random.Range(0, maxWidth - gapSize);
        float secondSegmentEndPoint = maxWidth - (firstSegmentEndPoint + gapSize);

        newScale(barrierPart_1, firstSegmentEndPoint);
        barrierPart_1.transform.position = new Vector3(spawnArea.transform.position.x - (maxWidth - barrierPart_1.transform.localScale.x)/2, segmentLevel * index + segmentLevel / 2, spawnArea.transform.position.z);


        newScale(barrierPart_2, secondSegmentEndPoint);
        barrierPart_2.transform.position = new Vector3(spawnArea.transform.position.x + (maxWidth - secondSegmentEndPoint) / 2, segmentLevel * index + segmentLevel / 2, spawnArea.transform.position.z);
    }

    private void newScale(GameObject GameObject, float newSize)
    {

        float size = GameObject.GetComponent<Renderer>().bounds.size.x;

        Vector3 rescale = GameObject.transform.localScale;

        rescale.x = newSize * rescale.x / size;

        GameObject.transform.localScale = rescale;

    }
}
