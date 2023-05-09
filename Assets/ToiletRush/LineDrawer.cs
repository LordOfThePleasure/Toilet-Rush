using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField] private float nextLinePositionTreshhold = 0.05f;

    private Coroutine drawing;

    [SerializeField] private string runnersTag;
    [SerializeField] private string gatesTag;

    private Runner currentRunner;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.transform != null && hit.transform.CompareTag(runnersTag))
            {
                currentRunner = hit.transform.GetComponent<Runner>();

                if (!currentRunner.hasPath)
                {
                    StartLine(currentRunner.Line);
                }
                else
                {
                    currentRunner = null;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0) && currentRunner != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.transform != null && hit.transform.CompareTag(gatesTag))
            {
                Gate chosenGate = hit.transform.GetComponent<Gate>();

                if (chosenGate.CompareType(currentRunner.type))
                {
                    currentRunner.PathReady();
                    GameManager.instance.CheckPathsComplition();
                }
                else
                {
                    currentRunner.Reset();
                }
            }
            else
            {
                currentRunner.Reset();
            }
            FinishLine();
        }
    }

    private void StartLine(LineRenderer line)
    {
        if (drawing != null)
        {
            StopCoroutine(drawing);
        }
        drawing = StartCoroutine(DrawLine(line));
    }

    private void FinishLine()
    {
        StopCoroutine(drawing);
        currentRunner = null;
    }

    private IEnumerator DrawLine(LineRenderer line)
    {
        line.positionCount = 1;
        line.SetPosition(line.positionCount - 1, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));

        while (true)
        {
            Vector3 inputPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector2.Distance(inputPosition, line.GetPosition(line.positionCount - 1)) > nextLinePositionTreshhold)
            {
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, inputPosition);
            }

            yield return null;
        }
    }
}
