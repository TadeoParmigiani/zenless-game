using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonaCamera : MonoBehaviour
{
    //personaje

    Transform playerTr;

    // Camara

    public Transform cameraAxis; // eje de la camra
    public Transform cameraTrack; // seguir al personaje 
    private Transform theCamera; // la camara xd

    
    // rotacion de los ejes de la camara 
    private float rotY = 0f;
    private float rotX = 0f;


    public float camRotSpeed = 200f;
    public float minAngle = -45f;
    public float maxAngle = 45f;
    public float cameraSpeed = 200f;


    // Start is called before the first frame update
    void Start()
    {
      playerTr = this.transform;

      theCamera = Camera.main.transform;

      
    }

    // Update is called once per frame
    void Update()
    {
        CameraLogic();
    }

    public void CameraLogic()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float theTime = Time.deltaTime;

//velocidad de rotacion de la camrara
        rotY += mouseY * theTime * camRotSpeed;
        rotX = mouseX * theTime * camRotSpeed;

        playerTr.Rotate(0, rotX, 0); //asi rota con la camra 

        rotY = Mathf.Clamp(rotY, minAngle, maxAngle);// valor minimo y maximo para la rotacion del eje Y

        Quaternion localRotation = Quaternion.Euler(-rotY, 0, 0);
        cameraAxis.localRotation = localRotation;

        theCamera.position = Vector3.Lerp(theCamera.position, cameraTrack.position, cameraSpeed * theTime); // posicion de la camara principal para pasarlo al camera track
        theCamera.rotation = Quaternion.Lerp(theCamera.rotation, cameraTrack.rotation, cameraSpeed * theTime); // rotacion de la camara principal para pasarlo al camera track
    }
}
