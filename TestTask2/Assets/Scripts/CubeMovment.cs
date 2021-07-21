using UnityEngine;

public class CubeMovment : MonoBehaviour
{
    [SerializeField] float _power = 1;
    Rigidbody _cube;
    bool _isDrag;
    Vector3 _direction;

    void Update()
    {
        if (Input.touchCount > 0)
            return;

        _direction = Vector3.zero;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
        {
            _cube = hit.collider.GetComponent<Rigidbody>();
            _isDrag = true;
            _cube.useGravity = false;
            Debug.Log(_cube.name);
        }
        
        if (_cube != null)
        {
            if (_isDrag && Input.GetMouseButton(0))
            {
                _direction = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
                _cube.position = Vector3.Lerp(_cube.position, _cube.position + _direction.normalized, Time.deltaTime * _power);
            }

            if (Input.GetMouseButtonUp(0))
            {
                _direction = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
                _isDrag = false;
                _cube.useGravity = true;
                _cube.AddForce(_direction * _cube.mass, ForceMode.Impulse);
            }
        }
    }

    public void IsHit()
    {
        _isDrag = false;
        _cube.useGravity = true;
    }
}
