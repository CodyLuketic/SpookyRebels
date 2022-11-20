using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSounds : MonoBehaviour
{
    private Coroutine walkingLoop = null;
    [SerializeField]
    private AudioClip[] audioClipArray;
    [SerializeField]
    private float stepDelay = 0.1f;
    [SerializeField]
    private float volume = 1f;
    
    private void Start()
    {
        StartWalkingLoopHelper();
    }

    private IEnumerator WalkingLoop()
    {
        while(true)
        {
            Vector3 position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.y + 10);

            Debug.Log("Played Sound");
            AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length)], gameObject.transform.position, volume);
            yield return new WaitForSeconds(stepDelay);
        }
    }

    public void StartWalkingLoop()
    {
        StartWalkingLoopHelper();
    }
    private void StartWalkingLoopHelper()
    {
        walkingLoop = StartCoroutine(WalkingLoop());
    }

    public void StopWalkingLoop()
    {
        StopWalkingLoopHelper();
    }
    private void StopWalkingLoopHelper()
    {
        StopCoroutine(walkingLoop);
    }
}
