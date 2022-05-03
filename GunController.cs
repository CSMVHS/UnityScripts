using System.Collections;
using UnityEngine.UI;
using UnityEngine;
 
public class GunController : MonoBehaviour
{
    [Header("Gun Settings")]
    public float fireRate = 0.1f;
    public int clipSize = 30;
    public int reservedAmmoCapactiy = 270;
 
    //Variables that change throughout the code
 
    bool _canShoot;
    int _currentAmmoInClip;
    int _ammoInReserve;
 
    //Muzzle Flash
    public Image muzzleFlashImage;
    public Sprite[] flashes;
 
    //Aiming
    public Vector3 normalLocalPosition;
    public Vector3 aimingLocalPosition;
 
    //Ammo Count
    public Text ammoText;
 
    public float aimSmoothing = 10;
 
    [Header("Mouse Settings")]
    public float mouseSensitivity = 5;
    Vector2 _currentRotation;
    public float weaponSwayAmount = -2;
 
    //Weapon Recoil
    public bool randomizeRecoil;
    public Vector2 randomRecoilConstraints;
    //You only need to assign this if randomize recoil is off
    public Vector2[] recoilPattern;
 
    private void Start()
    {
        _currentAmmoInClip = clipSize;
        _ammoInReserve = reservedAmmoCapactiy;
        _canShoot = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
 
    private void Update()
    {
        DetermineAim();
        DetermineRotation();
        AmmoCount();
 
        if(_ammoInReserve < 0)
        {
            _ammoInReserve = 0;
        }
 
        if(Input.GetMouseButton(0) && _canShoot && _currentAmmoInClip > 0)
        {
            _canShoot = false;
            _currentAmmoInClip--;
            StartCoroutine(ShootGun());
        } else if(Input.GetKeyDown(KeyCode.R) && _currentAmmoInClip < clipSize && _ammoInReserve > 0)
        {
            int amountNeeded = clipSize - _currentAmmoInClip;
            if(amountNeeded >= _ammoInReserve)
            {
                _currentAmmoInClip += _ammoInReserve;
                _ammoInReserve -=amountNeeded;
            }
            else
            {
                _currentAmmoInClip = clipSize;
                _ammoInReserve -= amountNeeded;
 
            }
        }
 
        void DetermineRotation()
        {
            Vector2 mouseAxis = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
 
            mouseAxis *= mouseSensitivity;
            _currentRotation += mouseAxis;
 
            _currentRotation.y = Mathf.Clamp(_currentRotation.y, -85, 85);
 
            transform.localPosition += (Vector3)mouseAxis * weaponSwayAmount / 1000;
 
            transform.root.localRotation = Quaternion.AngleAxis(_currentRotation.x, Vector3.up);
            transform.parent.localRotation = Quaternion.AngleAxis(-_currentRotation.y, Vector3.right);
        }
 
        void DetermineAim()
        {
            Vector3 target = normalLocalPosition;
            if(Input.GetMouseButton(1)) target = aimingLocalPosition;
 
            Vector3 desiredPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmoothing);
 
            transform.localPosition = desiredPosition;
        }
 
        void DetermineRecoil()
        {
            transform.localPosition -= Vector3.forward * 0.1f;
 
            if(randomizeRecoil)
            {
                float xRecoil = Random.Range (-randomRecoilConstraints.x, randomRecoilConstraints.x);
                float yRecoil = Random.Range (-randomRecoilConstraints.y, randomRecoilConstraints.y);
 
                Vector2 recoil = new Vector2(xRecoil, yRecoil);
 
                _currentRotation += recoil;
            }
            else
            {
                int currentStep = clipSize + - _currentAmmoInClip;
                currentStep = Mathf.Clamp(currentStep, 0, recoilPattern.Length - 1);
 
                _currentRotation += recoilPattern[currentStep];
            }
        }
 
        void RayCastForEnemy()
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.parent.position, transform.parent.forward, out hit, 1 << LayerMask.NameToLayer("Enemy")))
            {
                try 
                {
                    Debug.Log("Hit an Enemy");
                    Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                    rb.constraints = RigidbodyConstraints.None;
                    rb.AddForce(transform.parent.transform.forward * 500);
                }
                catch{ }
            }
        }
 
        void AmmoCount()
        {
            ammoText.enabled = true;
            ammoText.text = "Ammo: " + _currentAmmoInClip + "/" + _ammoInReserve;
        }
 
        IEnumerator ShootGun()
        {
            StartCoroutine(MuzzleFlash());
            DetermineRecoil();
 
            RayCastForEnemy();
 
            yield return new WaitForSeconds(fireRate);
            _canShoot = true;
        }
 
        IEnumerator MuzzleFlash()
        {
            muzzleFlashImage.sprite = flashes[Random.Range(0, flashes.Length)];
            muzzleFlashImage.color = Color.white;
            yield return new WaitForSeconds(0.05f);
            muzzleFlashImage.sprite = null;
            muzzleFlashImage.color = new Color(0, 0, 0, 0);
        }
    }
 
}
 

