using UnityEngine;

public class MoveObjects : MonoBehaviour
{

    [SerializeField] Camera cam;

    Vector3 dis;
    float posX;
    float posY;

    bool touched = false;
    bool dragging = false;

    Transform toDrag;
    Rigidbody toDragRigidbody;
    Vector3 previousPosition;

    void Update()
    {
        if (Input.touchCount != 1)
        {
            dragging = false;
            touched = false;
            if (toDragRigidbody)
            {
                SetFreeProperties(toDragRigidbody);
            }
            return;
        }

        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        if (touch.phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(pos);

            if (Physics.Raycast(ray, out hit, 10f))
            {
                toDrag = hit.transform;
                previousPosition = toDrag.position;
                toDragRigidbody = toDrag.GetComponent<Rigidbody>();

                dis = cam.WorldToScreenPoint(previousPosition);
                posX = Input.GetTouch(0).position.x - dis.x;  // Позиция курсора относительно выбраного объекта на экране
                posY = Input.GetTouch(0).position.y - dis.y;

                SetDraggingProperties(toDragRigidbody);

                touched = true;
            }
        }

        if (touched && touch.phase == TouchPhase.Moved && toDragRigidbody != null)
        {
            dragging = true;

            float posXNow = Input.GetTouch(0).position.x - posX;
            float posYNow = Input.GetTouch(0).position.y - posY;  // Смещение курсора
            Vector3 curPos = new Vector3(posXNow, posYNow, dis.z);

            Vector3 worldPos = cam.ScreenToWorldPoint(curPos) - previousPosition;
            worldPos = new Vector3(worldPos.x, worldPos.y, 0.0f);

            toDragRigidbody.velocity = worldPos / (Time.deltaTime * 10);

            previousPosition = toDrag.position;
        }

        if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) && toDragRigidbody != null)
        {
            dragging = false;
            touched = false;
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
        touched = false;
        previousPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }
}