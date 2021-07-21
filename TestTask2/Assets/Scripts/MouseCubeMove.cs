using UnityEngine;

public class MouseCubeMove : MonoBehaviour
{

    [SerializeField] Camera cam;

    Vector3 dis;
    float posX;
    float posY;

    bool mouseDown = false;
    bool dragging = false;

    Transform toDrag;
    Rigidbody toDragRigidbody;
    Vector3 previousPosition;

    void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            dragging = false;
            mouseDown = false;
            if (toDragRigidbody)
            {
                SetFreeProperties(toDragRigidbody);
            }
            return;
        }

        Vector3 pos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(pos);

            if (Physics.Raycast(ray, out hit, 10f))
            {
                toDrag = hit.transform;
                previousPosition = toDrag.position;
                toDragRigidbody = toDrag.GetComponent<Rigidbody>();

                dis = cam.WorldToScreenPoint(previousPosition);
                posX = Input.mousePosition.x - dis.x;
                posY = Input.mousePosition.y - dis.y;

                SetDraggingProperties(toDragRigidbody);

                mouseDown = true;
            }
        }

        if (mouseDown && Input.GetMouseButton(0) && toDragRigidbody != null)
        {
            dragging = true;

            float posXNow = Input.mousePosition.x - posX;
            float posYNow = Input.mousePosition.y - posY;
            Vector3 curPos = new Vector3(posXNow, posYNow, dis.z);

            Vector3 worldPos = cam.ScreenToWorldPoint(curPos) - previousPosition;
            worldPos = new Vector3(worldPos.x, worldPos.y, 0.0f);

            toDragRigidbody.velocity = worldPos / (Time.deltaTime * 10);

            previousPosition = toDrag.position;
        }

        if (dragging && Input.GetMouseButtonUp(0) && toDragRigidbody != null)
        {
            dragging = false;
            mouseDown = false;
            previousPosition = new Vector3(0.0f, 0.0f, 0.0f);
            SetFreeProperties(toDragRigidbody);
        }

    }

    private void SetDraggingProperties(Rigidbody rb)
    {
        rb.useGravity = false;
        //rb.drag = 20;
    }

    private void SetFreeProperties(Rigidbody rb)
    {
        rb.useGravity = true;
        //rb.drag = 5;
    }

    public void IsHit(Rigidbody rb)
    {
        SetFreeProperties(rb);
        dragging = false;
        mouseDown = false;
        previousPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }
}