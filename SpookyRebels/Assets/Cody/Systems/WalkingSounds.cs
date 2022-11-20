using System.Collections;
using UnityEngine;

public class WalkingSounds : MonoBehaviour
{
    private Coroutine walkingLoop = null;
    private bool loopRunning = false;
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
            Debug.Log("Played Sound");
            Random.InitState(System.DateTime.Now.Millisecond);
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
        loopRunning = true;
    }

    public void StopWalkingLoop()
    {
        StopWalkingLoopHelper();
    }
    private void StopWalkingLoopHelper()
    {
        if(loopRunning)
        {
            StopCoroutine(walkingLoop);
            loopRunning = false;
        }
    }
}
