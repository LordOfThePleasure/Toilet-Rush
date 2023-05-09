using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum RunnerType { Angel, Demon, Both }

public class Runner : MonoBehaviour
{
    [SerializeField] private float speedPerLinePosition;
    private float actualSpeed;
    public RunnerType type;

    [SerializeField] private GameObject linePrefub;
    public LineRenderer Line { get; private set; }
    public bool hasPath;

    public bool moving;

    private void Start()
    {
        Line = Instantiate(linePrefub, transform.parent).GetComponent<LineRenderer>();
    }

    public void PathReady()
    {
        hasPath = true;

        float lineLengh = 0;
        for (int i = 1; i < Line.positionCount; i++)
        {
            lineLengh += (Line.GetPosition(i) - Line.GetPosition(i - 1)).magnitude;
        }

        actualSpeed = lineLengh * speedPerLinePosition;
    }

    private void Update()
    {
        if (!moving)
        {
            return;
        }

        if (Line.positionCount - 1 < 0)
        {
            print("At the distanation!");
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, Line.GetPosition(0), actualSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, Line.GetPosition(0)) <= actualSpeed * Time.deltaTime)
        {
            Vector3[] positions = new Vector3[Line.positionCount];
            Line.GetPositions(positions);
            Line.SetPositions(positions.Skip(1).ToArray());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Runner") || collision.CompareTag("Obsticle"))
        {
            GameManager.instance.GameOver();
        }
        else if (collision.CompareTag("Gate") && collision.GetComponent<Gate>().CompareType(type))
        {
            moving = false;
            GameManager.instance.GetToGate();
        }
    }

    public void Reset()
    {
        Line.positionCount = 0;
    }
}
