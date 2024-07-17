using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MyScope : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private int _maxAmmo = 30; // Максимальное количество патронов в обойме
    [SerializeField] private int _currentAmmo; // Текущее количество патронов
    [SerializeField] private int _totalAmmo = 90; // Общее количество патронов
    [SerializeField] private float _reloadTime = 2f; // Время перезарядки
    [SerializeField] private float _fireRate = 0.2f; // Скорострельность (5 выстрелов в секунду)
    public bool _isReloading = false;
    private bool _isShooting = false;
    public Texture2D crosshairImage;

    public int CurrentAmmo => _currentAmmo; // Свойство для получения текущего количества патронов
    public int TotalAmmo => _totalAmmo; // Свойство для получения общего количества патронов

    private void Start() // блокируем курсор
    {
        _camera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _currentAmmo = _maxAmmo; // Инициализация текущего количества патронов
    }

    private void OnGUI() // прицел, можно сделать по-другому, я его в интернете посмотрел
    {
        int size = 48;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), crosshairImage);
    }

    private void Update() // взгляд
    {
        if (_isReloading)
            return;

        if (_currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !_isShooting)
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        _isShooting = true;

        while (Input.GetKey(KeyCode.Mouse0) && _currentAmmo > 0)
        {
            _currentAmmo--;

            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Ray ray = _camera.ScreenPointToRay(screenCenter);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                if (target != null)
                {
                    target.ReactToHit(10); // передаем урон в 10 единиц
                }
                else
                {
                    StartCoroutine(SphereIndicatorCoroutine(hit.point));
                    Debug.DrawLine(this.transform.position, hit.point, Color.green, 6);
                }
            }

            Debug.Log("Ammo left: " + _currentAmmo);

            yield return new WaitForSeconds(_fireRate);
        }

        _isShooting = false;
    }

    private IEnumerator Reload()
    {
        _isReloading = true;
        Debug.Log("Reloading...");

        yield return new WaitForSeconds(_reloadTime);

        int ammoToReload = Mathf.Min(_maxAmmo, _totalAmmo);
        _currentAmmo = ammoToReload;
        _totalAmmo -= ammoToReload;

        _isReloading = false;
        Debug.Log("Reloaded. Ammo left: " + _currentAmmo + ", Total ammo left: " + _totalAmmo);
    }

    private IEnumerator SphereIndicatorCoroutine(Vector3 pos) // след от пули
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(3);
        Destroy(sphere);
    }
}
