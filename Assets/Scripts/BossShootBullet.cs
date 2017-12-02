using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootBullet : MonoBehaviour
{

    public GameObject bulletPrefab;
    public GameObject ThePlayer;
    public float timeInterval;

    private float crtTime;
    private int[] patternCombination = {5,5,5,5};
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
            if (patternNumber >= patternCombination.Length-1) { crtPatternIndex = 0; }
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
                PatternWave(5, 30,PosPlayer);
                crtPatternIndex++;
                RealDeltaTimes = timeInterval * 0.3f;
            }
            crtTime = 0;
        }
    }

    void PatternPlus()
    {
        GameObject bulletInstanceUp = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
        bulletInstanceUp.GetComponent<Bullet>().direction = new Vector3(0, 1, 0);
        bulletInstanceUp.GetComponent<Bullet>().type = Bullet.Type.BOSS;
        GameObject bulletInstanceDown = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
        bulletInstanceDown.GetComponent<Bullet>().direction = new Vector3(0, -1, 0);
        bulletInstanceDown.GetComponent<Bullet>().type = Bullet.Type.BOSS;
        GameObject bulletInstanceLeft = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
        bulletInstanceLeft.GetComponent<Bullet>().direction = new Vector3(-1, 0, 0);
        bulletInstanceLeft.GetComponent<Bullet>().type = Bullet.Type.BOSS;
        GameObject bulletInstanceRight = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
        bulletInstanceRight.GetComponent<Bullet>().direction = new Vector3(1, 0, 0);
        bulletInstanceRight.GetComponent<Bullet>().type = Bullet.Type.BOSS;
    }
    void PatternCross()
    {
        GameObject bulletInstanceUp = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
        bulletInstanceUp.GetComponent<Bullet>().direction = new Vector3(1, 1, 0);
        bulletInstanceUp.GetComponent<Bullet>().type = Bullet.Type.BOSS;
        GameObject bulletInstanceDown = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
        bulletInstanceDown.GetComponent<Bullet>().direction = new Vector3(1, -1, 0);
        bulletInstanceDown.GetComponent<Bullet>().type = Bullet.Type.BOSS;
        GameObject bulletInstanceLeft = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
        bulletInstanceLeft.GetComponent<Bullet>().direction = new Vector3(-1, 1, 0);
        bulletInstanceLeft.GetComponent<Bullet>().type = Bullet.Type.BOSS;
        GameObject bulletInstanceRight = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
        bulletInstanceRight.GetComponent<Bullet>().direction = new Vector3(-1, -1, 0);
        bulletInstanceRight.GetComponent<Bullet>().type = Bullet.Type.BOSS;
    }
    void PatternCircle(int NumBullet, float angle)
    {
        for (int i = 0; i < NumBullet; i++)
        {
            GameObject bulletInstanceUp = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
            bulletInstanceUp.GetComponent<Bullet>().direction = new Vector3(Mathf.Cos(2 * Mathf.PI / NumBullet * i * angle/360), Mathf.Sin(2 * Mathf.PI / NumBullet * i* angle/360), 0);
            bulletInstanceUp.GetComponent<Bullet>().type = Bullet.Type.BOSS;
        }
    }
    void PatternWave(int NumBullet, float DeltaAngle, Vector3 Dir)
    {
        for (int i = 0; i < NumBullet; i++)
        {
            GameObject bulletInstanceUp = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
            Vector3 Direction = (Quaternion.Euler(0, 0, DeltaAngle/NumBullet*(i-1f*NumBullet/2f)) * Dir).normalized;
            bulletInstanceUp.GetComponent<Bullet>().direction = Direction;
            //new Vector3(Mathf.Cos(2 * Mathf.PI / NumBullet * i * DeltaAngle / 360+2 *Mathf.PI/360*angleInit), Mathf.Sin(2 * Mathf.PI / NumBullet * i * DeltaAngle / 360 + 2 * Mathf.PI / 360 * angleInit), 0);
            bulletInstanceUp.GetComponent<Bullet>().type = Bullet.Type.BOSS;
        }
    }
    void PatternSolo(float Angle)
    {
        GameObject bulletInstanceUp = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
        bulletInstanceUp.GetComponent<Bullet>().direction = new Vector3(Mathf.Cos(2 * Mathf.PI / 360 * Angle), Mathf.Sin(2 * Mathf.PI / 360 * Angle), 0);
        bulletInstanceUp.GetComponent<Bullet>().type = Bullet.Type.BOSS;
    }

    IEnumerator PatternSpirale(int bulletNumber)
    {
        //patternRoutineFinished = false;
        int crtBulletIndex = 0;
        patternRoutineFinished = true;
        while (crtBulletIndex < bulletNumber)
        {
            Debug.Log("cacacaac   " + crtBulletIndex);
            PatternSolo(360f/bulletNumber*crtBulletIndex);
            crtBulletIndex++;
            Debug.Log("cacacaac   " + bulletNumber);
            yield return new WaitForSeconds(timeInterval / bulletNumber);
        }
    }
}
