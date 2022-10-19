using UnityEngine;

public class MainBehaviour : MonoBehaviour
{
    protected virtual void Awake() => this.AddComponent();
    protected virtual void Reset() => this.AddComponent();

    protected virtual void AddComponent()
    {
        // Override there
    }
}
