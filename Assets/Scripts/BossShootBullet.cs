using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootBullet : MonoBehaviour
{

    public GameObject bulletPrefab;
    public GameObject ThePlayer;
    public float timeInterval;

    private float crtTime;
    //private int[] patternCombination = {1,2,1,2,1,2,5,5,5,5,5,5,3,1,2,1,2,1,2,4,4,5,5,4,5,4,5,3,3,3};
    private int[] patternCombination = {3,2,1,2,1,2,5,5,5,5,5,5,3,1,2,1,2,1,2,4,4,5,5,4,5,4,5,1,2,1,3,3,3};
    private int crtPatternIndex;
    private float RealDeltaTimes;
    private bool patternRoutineFinished;
    private int patternNumber = 0;

    private void Start()
    {
        crtPatternIndex = 0;
        patternRoutineFinished = false;
        RealDeltaTimes = timeInterval;
    }

    void Update()
    {
        crtTime += Time.deltaTime;
        if (crtTime >= RealDeltaTimes)
        {
            if (crtPatternIndex >= patternCombination.Length-1) { crtPatternIndex = 0; }
            patternNumber = patternCombination[crtPatternIndex];
            if (patternNumber == 0) { }
            if (patternNumber == 1)
                {
                    PatternPlus();
                    crtPatternIndex++;
                }
            else if (patternNumber == 2)
                {
                    PatternCross();
                    crtPatternIndex++;
                }
            else if (patternNumber == 3)
            {
                StartCoroutine(PatternSpirale(10));
                if (patternRoutineFinished == true) { crtPatternIndex++; }
            }
            else if (patternNumber == 4)
            {
                PatternCircle(20, 360);
                crtPatternIndex++;
            }
            else if (patternNumber == 5)
            {
                Vector3 PosPlayer = (ThePlayer.transform.position - transform.position).normalized;
                PatternWave(5, 50,PosPlayer);
                crtPatternIndex++;
                RealDeltaTimes = timeInterval * 0.5f;
            }
            crtTime = 0;
        }
    }

    void PatternPlus()
    {
        GameObject bulletInstanceUp = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
        bulletInstanceUp.transform.parent = transform;
        bulletInstanceUp.GetComponent<Bullet>().direction = new Vector3(0, 1, 0);
        bulletInstanceUp.GetComponent<Bullet>().type = Bullet.Type.BOSS;
        GameObject bulletInstanceDown = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
        bulletInstanceDown.transform.parent = transform;
        bulletInstanceDown.GetComponent<Bullet>().direction = new Vector3(0, -1, 0);
        bulletInstanceDown.GetComponent<Bullet>().type = Bullet.Type.BOSS;
        GameObject bulletInstanceLeft = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
        bulletInstanceLeft.transform.parent = transform;
        bulletInstanceLeft.GetComponent<Bullet>().direction = new Vector3(-1, 0, 0);
        bulletInstanceLeft.GetComponent<Bullet>().type = Bullet.Type.BOSS;
        GameObject bulletInstanceRight = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
        bulletInstanceRight.transform.parent = transform;
        bulletInstanceRight.GetComponent<Bullet>().direction = new Vector3(1, 0, 0);
        bulletInstanceRight.GetComponent<Bullet>().type = Bullet.Type.BOSS;
    }
    void PatternCross()
    {
        GameObject bulletInstanceUp = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
        bulletInstanceUp.transform.parent = transform;
        bulletInstanceUp.GetComponent<Bullet>().direction = new Vector3(1, 1, 0);
        bulletInstanceUp.GetComponent<Bullet>().type = Bullet.Type.BOSS;
        GameObject bulletInstanceDown = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
        bulletInstanceDown.transform.parent = transform;
        bulletInstanceDown.GetComponent<Bullet>().direction = new Vector3(1, -1, 0);
        bulletInstanceDown.GetComponent<Bullet>().type = Bullet.Type.BOSS;
        GameObject bulletInstanceLeft = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
        bulletInstanceLeft.transform.parent = transform;
        bulletInstanceLeft.GetComponent<Bullet>().direction = new Vector3(-1, 1, 0);
        bulletInstanceLeft.GetComponent<Bullet>().type = Bullet.Type.BOSS;
        GameObject bulletInstanceRight = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
        bulletInstanceRight.transform.parent = transform;
        bulletInstanceRight.GetComponent<Bullet>().direction = new Vector3(-1, -1, 0);
        bulletInstanceRight.GetComponent<Bullet>().type = Bullet.Type.BOSS;
    }
    void PatternCircle(int NumBullet, float angle)
    {
        for (int i = 0; i < NumBullet; i++)
        {
            GameObject bulletInstance = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
            bulletInstance.transform.parent = transform;
            bulletInstance.GetComponent<Bullet>().direction = new Vector3(Mathf.Cos(2 * Mathf.PI / NumBullet * i * angle/360), Mathf.Sin(2 * Mathf.PI / NumBullet * i* angle/360), 0);
            bulletInstance.GetComponent<Bullet>().type = Bullet.Type.BOSS;
        }
    }
    void PatternWave(int NumBullet, float DeltaAngle, Vector3 Dir)
    {
        for (int i = 0; i < NumBullet; i++)
        {
            GameObject bulletInstance = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
            bulletInstance.transform.parent = transform;
            Vector3 Direction = (Quaternion.Euler(0, 0, DeltaAngle/NumBullet*(i-1f*NumBullet/2f)) * Dir).normalized;
            bulletInstance.GetComponent<Bullet>().direction = Direction;
            //new Vector3(Mathf.Cos(2 * Mathf.PI / NumBullet * i * DeltaAngle / 360+2 *Mathf.PI/360*angleInit), Mathf.Sin(2 * Mathf.PI / NumBullet * i * DeltaAngle / 360 + 2 * Mathf.PI / 360 * angleInit), 0);
            bulletInstance.GetComponent<Bullet>().type = Bullet.Type.BOSS;
        }
    }
    void PatternSolo(float Angle)
    {
        GameObject bulletInstance = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
        bulletInstance.transform.parent = transform;
        bulletInstance.GetComponent<Bullet>().direction = new Vector3(Mathf.Cos(2 * Mathf.PI / 360 * Angle), Mathf.Sin(2 * Mathf.PI / 360 * Angle), 0);
        bulletInstance.GetComponent<Bullet>().type = Bullet.Type.BOSS;
    }

    IEnumerator PatternSpirale(int bulletNumber)
    {
        //patternRoutineFinished = false;
        int crtBulletIndex = 0;
        patternRoutineFinished = true;
        while (crtBulletIndex < bulletNumber)
        {
            PatternSolo(360f/bulletNumber*crtBulletIndex);
            crtBulletIndex++;
            yield return new WaitForSeconds(timeInterval / bulletNumber);
        }
    }
}
