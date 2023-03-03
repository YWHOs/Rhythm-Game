using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject stage;
    Transform[] stagePlates;

    [SerializeField] float offsetY = -5;
    [SerializeField] float speed = 10;

    int stepCount = 0;
    int totalPlate;

    // Start is called before the first frame update
    void Start()
    {
        stagePlates = stage.GetComponent<Stage>().plates;
        totalPlate = stagePlates.Length;

        for (int i = 0; i < totalPlate; i++)
        {
            stagePlates[i].position = new Vector3(stagePlates[i].position.x, stagePlates[i].position.y + offsetY, stagePlates[i].position.z);
        }
    }

    public void ShowNextPlate()
    {
        if (stepCount < totalPlate)
            StartCoroutine(MovePlateCoroutine(stepCount++));

    }

    IEnumerator MovePlateCoroutine(int _num)
    {
        stagePlates[_num].gameObject.SetActive(true);
        Vector3 dtPos = new Vector3(stagePlates[_num].position.x, stagePlates[_num].position.y - offsetY, stagePlates[_num].position.z);

        while(Vector3.SqrMagnitude(stagePlates[_num].position - dtPos) >= 0.001f)
        {
            stagePlates[_num].position = Vector3.Lerp(stagePlates[_num].position, dtPos, speed * Time.deltaTime);
            yield return null;
        }
        stagePlates[_num].position = dtPos;
    }
}
