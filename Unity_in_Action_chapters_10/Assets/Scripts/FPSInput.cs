using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")] // https://docs.unity3d.com/ScriptReference/AddComponentMenu.html
public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;

    private CharacterController _characterController;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        // limitando movimentação diagonal.
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;

        movement *= Time.deltaTime;

        // transforma direção de coordenadas locais para globais
        movement = transform.TransformDirection(movement);

        _characterController.Move(movement);
    }
}
