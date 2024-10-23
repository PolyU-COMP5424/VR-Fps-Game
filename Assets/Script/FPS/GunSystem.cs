using UnityEngine;

public class GunSystem : MonoBehaviour
{
    // ��������
    public int damage; // �����˺�
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots; // ��������ɢ�䡢��̡���װʱ�䡢ÿ��������
    public int bulletsLeft, magazineSize, bulletsPerTap, currentAmmo, maxAmmoSize; // ʣ���ӵ�����ϻ������ÿ�η�����ӵ�������ǰ��ҩ�����ҩ����
    public bool allowButtonHold; // �Ƿ�����ס���
    int bulletsShot; // ��ǰ������ӵ�����

    // ����ֵ 
    bool shooting, readyToShoot, reloading; // ���״̬���Ƿ�׼����������Ƿ�������װ

    // �ο�����
    public Transform attackPoint; // ������
    public RaycastHit rayHit; // ������ײ��Ϣ
    public LayerMask whatIsEnemy; // ���˵Ĳ�

    // ͼ��Ч��
    public GameObject muzzleFlash, bulletHoleGraphic; // ����Ч�����ӵ���ͼ��

    private void Awake()
    {
        readyToShoot = true; // ��ʼ��Ϊ׼�������
    }

    private void Update()
    {
        MyInput(); // ��������

        // ���µ�ҩ�����ı����������Ҫ�Ļ���������� UI �����
        // text.SetText("Ammo: " + bulletsLeft + " / " + magazineSize); // ���µ�ҩ�����ı�
    }

    private void MyInput()
    {
        // ��������ȷ���������
        if (allowButtonHold) shooting = Input.GetButton("Fire1"); // ��ס���
        else shooting = Input.GetButtonDown("Fire1"); // ������

        // ��װ����
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload(); // ��R����װ

        // ���
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0) // ���׼�������������������
        {
            bulletsShot = bulletsPerTap; // ���õ�ǰ������ӵ�����
            Shoot_(); // ִ�����
        }
    }

    public void ����()
    {
        // ���
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0) // ���׼�������������������
        {
            bulletsShot = bulletsPerTap; // ���õ�ǰ������ӵ�����
            Shoot_(); // ִ�����
        }
    }
    public void װ��()
    {
        if (bulletsLeft < magazineSize && !reloading) Reload(); // ��R����װ
    }
    private void Shoot_()
    {
        readyToShoot = false; // ����Ϊ��׼�����

        // ����ɢ��
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // �������ɢ��ķ���
        Vector3 direction = attackPoint.forward + new Vector3(x, y, 0); // ʹ�ù������ǰ�������ɢ��

        // ���߼��
        if (Physics.Raycast(attackPoint.position, direction, out rayHit, range, whatIsEnemy)) // �ӹ�����λ�÷�������
        {
            Debug.Log(rayHit.collider.name); // ��ӡ��ײ������

            // �����ײ���ǵ��ˣ�������˺�
            if (rayHit.collider.CompareTag("Enemy"))
                rayHit.collider.GetComponent<EnemyDamage>().TakeDamage(damage); // �Ե�������˺�

            Zombie1 zombie1 = rayHit.collider.GetComponent<Zombie1>(); // ��ȡ��ײ���ϵ� Zombie1 ���
            if (zombie1 != null) // ����Ƿ��� Zombie1
            {
                zombie1.zombieHitDamage(damage); // �Խ�ʬ����˺�
            }
        }
        if(rayHit.point==Vector3.zero)rayHit.point = new Vector3(999,999,999);
        // ͼ��Ч��
        GameObject impact = Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0)); // �����ӵ���ͼ��
        GameObject muzzle = Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity); // ��������Ч��
        Destroy(impact, 1f); // 1��������ӵ���
        Destroy(muzzle, 1f); // 1������ٻ���Ч��

        bulletsLeft--; // ����ʣ���ӵ�
        bulletsShot--; // ���ٵ�ǰ������ӵ�����

        Invoke("ResetShot", timeBetweenShooting); // �ӳٵ����������״̬�ķ���

        if (bulletsShot > 0 && bulletsLeft > 0) // ��������ӵ��������ʣ���ӵ�����0
            Invoke("Shoot", timeBetweenShots); // �ӳ��ٴ����
    }

    private void ResetShot()
    {
        readyToShoot = true; // �������״̬Ϊ׼����
    }

    private void Reload()
    {
        reloading = true; // ����Ϊ������װ
        Invoke("ReloadFinished", reloadTime); // �ӳٵ�����װ��ɵķ���
    }

    private void ReloadFinished()
    {
        // ��װ�߼�
        bulletsLeft = magazineSize; // ��ʣ���ӵ�������Ϊ��ϻ����
        reloading = false; // ��װ���
    }

    public void AddAmmo(int x)
    {
        // ��ӵ�ҩ�߼�
        // currentAmmo += x; // ���������Ӿ���ʵ��
    }
}
