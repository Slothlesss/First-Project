using UnityEngine;
public class CameraFL : MonoBehaviour
{
    private Transform Player;
    public float minX, maxX, minY, maxY;
    void Start() => Invoke(nameof(Find_player), 1);
    void Find_player() => Player = GameObject.Find("player_main").transform;
    void LateUpdate()
    {
        if (Player != null)
        {
            Vector3 tempx = transform.position;
            tempx.x = Player.position.x;

            if (tempx.x < minX) tempx.x = minX;
            if (tempx.x > maxX) tempx.x = maxX;
            transform.position = tempx;

            Vector3 tempy = transform.position;
            tempy.y = Player.position.y;

            if (tempy.y < minY) tempy.y = minY;
            if (tempy.y > maxY) tempy.y = maxY;
            transform.position = tempy;
        }
    }
}

