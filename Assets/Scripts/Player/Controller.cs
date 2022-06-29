using System;
using CustomGameEvent;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Controller : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;

    private Rigidbody _rigidbody;
    private Camera _camera;
    private Vector3 _velocity;

    private bool _isMove;
    public event Action<bool> PlayerMove = delegate {};

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
        _isMove = false;
    }

    private void Update()
    {
        
        Vector3 mosePos = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,_camera.transform.position.y));
        transform.LookAt(mosePos + Vector3.up * transform.position.y);

        
        _velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * _moveSpeed;

        if (Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f)
        {
            if (_isMove)
            {
                _isMove = false;
                PlayerMove?.Invoke(false);
            }
        }
        else
        {
            if (!_isMove)
            {
                _isMove = true;
                PlayerMove?.Invoke(false);
            }
           
        }
        
        // if (Input.GetKey(KeyCode.Mouse0))
        // {
        //     GameEvent.Current = GameStage.WIN;
        // }
        //  
        // if (Input.GetKey(KeyCode.Mouse1))
        // {
        //     GameEvent.Current = GameStage.LOSE;
        // }
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _velocity * Time.fixedDeltaTime);
    }

    public void Destroy()
    {
        PlayerMove?.Invoke(true);
        Destroy(gameObject);
    }
}
