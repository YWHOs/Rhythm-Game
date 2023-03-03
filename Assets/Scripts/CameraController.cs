using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float speed = 15f;

    Vector3 playerDt = new Vector3();

    float hitDt;
    [SerializeField] float zoom = -1.25f;

    // Start is called before the first frame update
    void Start()
    {
        playerDt = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dtPos = player.position + playerDt + (transform.forward * hitDt);
        transform.position = Vector3.Lerp(transform.position, dtPos, speed * Time.deltaTime);
    }

    public IEnumerator ZoomCam()
    {
        hitDt = zoom;
        yield return new WaitForSeconds(0.15f);
        hitDt = 0;
    }
}
