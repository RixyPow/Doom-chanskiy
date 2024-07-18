using System.Collections;
using UnityEngine;

public class MyScope : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] public int _maxAmmo = 30;
    [SerializeField] public int _currentAmmo;
    [SerializeField] public int _totalAmmo = 90;
    [SerializeField] private float _reloadTime = 2f;
    [SerializeField] private float _fireRate = 0.2f;
    public bool _isReloading = false;
    private bool _isShooting = false;
    public Texture2D crosshairImage;
    public ParticleSystem flamethrowerParticleSystem;
    public int flamethrowerDamage = 20;
    public float damageInterval = 0.5f;
    [SerializeField] private float _flamethrowerFuel = 100f; // Новая переменная для топлива огнемета
    [SerializeField] private float _flamethrowerFuelConsumptionRate = 1f; // Скорость расхода топлива

    public float FlamethrowerFuel => _flamethrowerFuel; // Публичное свойство для доступа к топливу огнемета

    private void Start()
    {
        _camera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _currentAmmo = _maxAmmo;
        flamethrowerParticleSystem.Stop();
    }

    private void OnGUI()
    {
        int size = 48;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), crosshairImage);
    }

    private void Update()
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

        if (Input.GetKey(KeyCode.Mouse1) && _flamethrowerFuel > 0)
        {
            StartFlamethrower();
        }
        else
        {
            StopFlamethrower();
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
                    target.ReactToHit(10);
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

        int unusedAmmo = _currentAmmo;
        _totalAmmo += unusedAmmo;

        int ammoToReload = Mathf.Min(_maxAmmo, _totalAmmo);
        _currentAmmo = ammoToReload;
        _totalAmmo -= ammoToReload;

        _isReloading = false;
        Debug.Log("Reloaded. Ammo left: " + _currentAmmo + ", Total ammo left: " + _totalAmmo);
    }

    private void StartFlamethrower()
    {
        if (!flamethrowerParticleSystem.isPlaying)
        {
            flamethrowerParticleSystem.Play();
            StartCoroutine(DealFlamethrowerDamage());
        }
    }

    private void StopFlamethrower()
    {
        if (flamethrowerParticleSystem.isPlaying)
        {
            flamethrowerParticleSystem.Stop();
            StopCoroutine(DealFlamethrowerDamage());
        }
    }

    private IEnumerator DealFlamethrowerDamage()
    {
        while (flamethrowerParticleSystem.isPlaying && _flamethrowerFuel > 0)
        {
            _flamethrowerFuel -= _flamethrowerFuelConsumptionRate * damageInterval;
            Collider[] hitColliders = Physics.OverlapSphere(flamethrowerParticleSystem.transform.position, 5f);
            foreach (var hitCollider in hitColliders)
            {
                ReactiveTarget target = hitCollider.GetComponent<ReactiveTarget>();
                if (target != null)
                {
                    target.ReactToHit(flamethrowerDamage);
                }
            }
            yield return new WaitForSeconds(damageInterval);
        }
        StopFlamethrower();
    }

    private IEnumerator SphereIndicatorCoroutine(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(3);
        Destroy(sphere);
    }
}

