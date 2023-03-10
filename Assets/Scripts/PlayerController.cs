using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool canPress = true;
    [Header("이동")]
    [SerializeField] float moveSpeed = 3;
    Vector3 dir = new Vector3();
    public Vector3 destination = new Vector3();
    Vector3 origin = new Vector3();

    [Header("회전")]
    [SerializeField] float turnSpeed = 270;
    Vector3 rotation = new Vector3();
    Quaternion dtRotate = new Quaternion();

    [Header("반동")]
    [SerializeField] float recoilY = 0.25f;
    [SerializeField] float recoilSpeed = 1.5f;

    [SerializeField] Transform fakeCube;
    [SerializeField] Transform realCube;

    bool canMove = true;
    bool isFall;

    TimeManager timeManager;
    CameraController cameraController;
    Rigidbody rigid;
    StatusManager statusManager;

    // Start is called before the first frame update
    void Start()
    {
        timeManager = FindObjectOfType<TimeManager>();
        cameraController = FindObjectOfType<CameraController>();
        rigid = GetComponentInChildren<Rigidbody>();
        statusManager = FindObjectOfType<StatusManager>();
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isStart)
        {
            CheckFall();
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
            {
                if (canMove && canPress && !isFall)
                {
                    CalculateMove();
                    if (timeManager.CheckTiming())
                    {
                        StartAction();
                    }
                }
            }
        }

    }
    public void Initialized()
    {
        transform.position = Vector3.zero;
        destination = Vector3.zero;
        realCube.localPosition = Vector3.zero;
        canMove = true;
        canPress = true;
        isFall = false;
        rigid.useGravity = false;
        rigid.isKinematic = true;
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
        // 제곱근
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

    void CheckFall()
    {
        if (!isFall && canMove)
        {
            if (!Physics.Raycast(transform.position, Vector3.down, 1.1f))
            {
                Fall();
            }
        }
    }
    void Fall()
    {
        isFall = true;
        rigid.useGravity = true;
        rigid.isKinematic = false;
    }

    public void ResetFall()
    {
        statusManager.DecreaseHP(1);
        AudioManager.instance.PlaySFX("Fall");
        if (!statusManager.IsDead())
        {
            isFall = false;
            rigid.useGravity = false;
            rigid.isKinematic = true;
            transform.position = origin;
            realCube.localPosition = Vector3.zero;
        }
    }
}
