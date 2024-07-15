using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MyScope : MonoBehaviour
{
    private Camera _camera;

    private void Start() //блокаем курсор
    {   
        _camera = GetComponent<Camera>();
        Cursor.lockState =CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnGUI(){ //прицел, можно сделать по другому, я его в интернете посмотрел
        int size = 24;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "+");
    }
    private void Update(){ //взгляд
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            Vector3 screenCenter = new Vector3(Screen.width/2, Screen.height/2, 0);
            Ray ray = _camera.ScreenPointToRay(screenCenter);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit)){
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                
                
                if(target != null)
                {
                    target.ReactToHit();
                }
                else
                {
                    StartCoroutine(SphereInicatorCoroutine(hit.point));
                    Debug.DrawLine(this.transform.position, hit.point, Color.green, 6);
                
                }
            }
        }
    }
    private IEnumerator SphereInicatorCoroutine(Vector3 pos){ //след от пули
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(3);
        Destroy(sphere);
    }
}
