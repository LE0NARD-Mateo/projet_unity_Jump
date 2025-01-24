using UnityEngine;

public class tracking : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created    public GameObject player;
    public float interpolateSpeed = 0.8f;
    public GameObject player;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 interpolation = Vector2.Lerp(transform.position, player.transform.position, interpolateSpeed*Time.deltaTime);
        transform.position = new Vector3(interpolation.x, interpolation.y, -10f);
    }
}
