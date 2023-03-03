using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool canPress = true;
    [Header("�̵�")]
    [SerializeField] float moveSpeed = 3;
    Vector3 dir = new Vector3();
    public Vector3 destination = new Vector3();

    [Header("ȸ��")]
    [SerializeField] float turnSpeed = 270;
    Vector3 rotation = new Vector3();
    Quaternion dtRotate = new Quaternion();

    [Header("�ݵ�")]
    [SerializeField] float recoilY = 0.25f;
    [SerializeField] float recoilSpeed = 1.5f;

    [SerializeField] Transform fakeCube;
    [SerializeField] Transform realCube;

    bool canMove = true;

    TimeManager timeManager;
    CameraController cameraController;

    // Start is called before the first frame update
    void Start()
    {
        timeManager = FindObjectOfType<TimeManager>();
        cameraController = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
        {
            if (canMove && canPress)
            {
                CalculateMove();
                if (timeManager.CheckTiming())
                {
                    StartAction();
                }
            }

        }
    }

    void CalculateMove()
    {
        dir.Set(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));

        destination = transform.position + new Vector3(-dir.x, 0, dir.z);

        rotation = new Vector3(-dir.z, 0, -dir.x);
        fakeCube.RotateAround(transform.position, rotation, turnSpeed);
        dtRotate = fakeCube.rotation;
    }
    void StartAction()
    {
        StartCoroutine(MoveCoroutine());
        StartCoroutine(TurnCoroutine());
        StartCoroutine(RecoilCoroutine());
        StartCoroutine(cameraController.ZoomCam());
    }

    IEnumerator MoveCoroutine()
    {
        canMove = false;
        // ������
        while(Vector3.SqrMagnitude(transform.position - destination) >= 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = destination;
        canMove = true;
    }

    IEnumerator TurnCoroutine()
    {
        while(Quaternion.Angle(realCube.rotation, dtRotate) > 0.5f)
        {
            realCube.rotation = Quaternion.RotateTowards(realCube.rotation, dtRotate, turnSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator RecoilCoroutine()
    {
        while(realCube.position.y < recoilY)
        {
            realCube.position += new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }
        while(realCube.position.y > 0)
        {
            realCube.position -= new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }
        realCube.localPosition = Vector3.zero;
    }
}
