using UnityEngine;

public class Loading : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1), 500 * Time.deltaTime);
    }
}
