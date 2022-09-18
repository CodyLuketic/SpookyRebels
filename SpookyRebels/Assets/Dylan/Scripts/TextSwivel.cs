using UnityEngine;

public class TextSwivel : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);
    }
}
