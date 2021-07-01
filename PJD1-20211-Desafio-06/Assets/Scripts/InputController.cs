using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private float Horizontal;
    [SerializeField]
    private float Vertical;
    [SerializeField]
    private bool Fire;
    [SerializeField]
    private bool Reload;
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private int SelectWeapon;

    [SerializeField]
    private Vector3 MousePosition;

    public Joystick JoystickLeft;
    public Joystick JoystickRight;

    private void Awake()
    {
        cam = Camera.allCameras.ToList().Find(_camera => {
            return _camera.name.Contains("Main");
        });
        player = GameObject.FindObjectOfType<PlayerController>();
    }

    private float GetHorizontal()
    {
        float h = 0f;
        float jh = JoystickLeft.Horizontal;
        float kh = Input.GetAxis("Horizontal");

        if(jh != 0f)
        {
            h = jh;
        }
        else if(kh != 0f)
        {
            h = kh;
        }

        return h;
    }

    private float GetVertical()
    {
        float v = 0f;
        float jv = JoystickLeft.Vertical;
        float kv = Input.GetAxis("Vertical");

        if (jv != 0f)
        {
            v = jv;
        }
        else if (kv != 0f)
        {
            v = kv;
        }

        return v;
    }

    private Vector2 GetAngle()
    {
        Vector2 angle = new Vector2();
        MousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        if (JoystickRight.Direction != Vector2.zero)
        {
            angle = JoystickRight.Direction;
        }
        else if(MousePosition != Vector3.zero)
        {
            angle = MousePosition - player.transform.position;
        }
        
        return angle;
    }


    // Update is called once per frame
    void Update()
    {
        Horizontal = GetHorizontal();
        Vertical = GetVertical();

        Fire = Input.GetMouseButton(0);
        Reload = Input.GetMouseButton(1);

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectWeapon = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectWeapon = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectWeapon = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectWeapon = 4;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            SelectWeapon++;
        }
        else if(Input.GetKeyDown(KeyCode.Q))
        {
            SelectWeapon--;
        }

        if(Input.GetKey(KeyCode.Space))
        {
            GameController.RespawnEnemy();
        }

        Vector2 angle = GetAngle();

        player.SetInput(Horizontal, Vertical, angle, SelectWeapon, Fire, Reload);

    }
}
