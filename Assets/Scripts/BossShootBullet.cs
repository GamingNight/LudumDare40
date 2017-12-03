using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootBullet : MonoBehaviour
{

    public GameObject bulletPrefab;
    public GameObject ThePlayer;
    public float defaultTimeInterval;
	public float defaultBulletSpeed;

    private float crtTime;
	private int[] patternCombination = {12,4,12,4,12,4,12,4,0,0,0,0,5,5};
	private float[] patternSpeeds = {-1f, -1f, -1f, -1f, -1f, -1f, -1f, -1f, -1f, -1f, -1f, -1f, 1f, 1f};
	//private int[] patternCombination = {12, 0, 5};
	//private float[] patternSpeeds = {-1f, -1f, 1.5f};
    private int crtPatternIndex;
    private float realDeltaTimes;
    private bool patternRoutineFinished;
    private int patternNumber = 0;

    private void Start()
    {
        crtPatternIndex = 0;
        patternRoutineFinished = false;
		realDeltaTimes = defaultTimeInterval;
    }

    void Update()
    {
        crtTime += Time.deltaTime;
        if (crtTime >= realDeltaTimes)
        {
            realDeltaTimes = defaultTimeInterval;
            if (crtPatternIndex > patternCombination.Length-1) 
			{ 
				crtPatternIndex = 0;
			}

			float crtSpeed = defaultBulletSpeed;
			if (patternSpeeds [crtPatternIndex] != -1)
				crtSpeed = patternSpeeds [crtPatternIndex];

            patternNumber = patternCombination[crtPatternIndex];
			if (patternNumber == 0) {
				//Empty pattern (pause)
				crtPatternIndex++;
			}
			if (patternNumber == 1) {
				PatternPlus (crtSpeed);
				crtPatternIndex++;
			} else if (patternNumber == 2) {
				PatternCross (crtSpeed);
				crtPatternIndex++;
			} else if (patternNumber == 12) {
				PatternPlus (crtSpeed);
				PatternCross (crtSpeed);
				crtPatternIndex++;
			}
            else if (patternNumber == 3)
            {
				StartCoroutine(PatternSpirale(10, crtSpeed));
                if (patternRoutineFinished)
				{
					crtPatternIndex++;
				}
            }
            else if (patternNumber == 4)
            {
				PatternCircle(20, 0, crtSpeed);
                crtPatternIndex++;
            }
            else if (patternNumber == 5)
            {
                Vector3 PosPlayer = (ThePlayer.transform.position - transform.position).normalized;
				PatternWave(5, 50, PosPlayer, crtSpeed);
                crtPatternIndex++;
            }
            crtTime = 0;
        }
    }

	void PatternPlus(float speed)
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

		bulletInstanceUp.GetComponent<Bullet>().initSpeed = speed;
		bulletInstanceDown.GetComponent<Bullet>().initSpeed = speed;
		bulletInstanceLeft.GetComponent<Bullet>().initSpeed = speed;
		bulletInstanceRight.GetComponent<Bullet>().initSpeed = speed;
    }
	void PatternCross(float speed)
    {
        GameObject bulletInstanceUp = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
        bulletInstanceUp.transform.parent = transform;
		bulletInstanceUp.GetComponent<Bullet>().direction = new Vector3(1, 1, 0).normalized;
        bulletInstanceUp.GetComponent<Bullet>().type = Bullet.Type.BOSS;
        GameObject bulletInstanceDown = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
        bulletInstanceDown.transform.parent = transform;
		bulletInstanceDown.GetComponent<Bullet>().direction = new Vector3(1, -1, 0).normalized;
        bulletInstanceDown.GetComponent<Bullet>().type = Bullet.Type.BOSS;
        GameObject bulletInstanceLeft = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
        bulletInstanceLeft.transform.parent = transform;
		bulletInstanceLeft.GetComponent<Bullet>().direction = new Vector3(-1, 1, 0).normalized;
        bulletInstanceLeft.GetComponent<Bullet>().type = Bullet.Type.BOSS;
        GameObject bulletInstanceRight = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
        bulletInstanceRight.transform.parent = transform;
		bulletInstanceRight.GetComponent<Bullet>().direction = new Vector3(-1, -1, 0).normalized;
        bulletInstanceRight.GetComponent<Bullet>().type = Bullet.Type.BOSS;

		bulletInstanceUp.GetComponent<Bullet>().initSpeed = speed;
		bulletInstanceDown.GetComponent<Bullet>().initSpeed = speed;
		bulletInstanceLeft.GetComponent<Bullet>().initSpeed = speed;
		bulletInstanceRight.GetComponent<Bullet>().initSpeed = speed;
    }

	void PatternCircle(int NumBullet, float offset, float speed)
    {
        for (int i = 0; i < NumBullet; i++)
        {
            GameObject bulletInstance = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
            bulletInstance.transform.parent = transform;
			bulletInstance.GetComponent<Bullet>().direction = new Vector3(Mathf.Cos(offset + 2 * Mathf.PI * i / NumBullet), Mathf.Sin(offset + 2 * Mathf.PI * i / NumBullet), 0).normalized;
			bulletInstance.GetComponent<Bullet>().type = Bullet.Type.BOSS;
			bulletInstance.GetComponent<Bullet>().initSpeed = speed;
        }
    }
	void PatternWave(int NumBullet, float DeltaAngle, Vector3 Dir, float speed)
    {
        for (int i = 0; i < NumBullet; i++)
        {
            GameObject bulletInstance = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
            bulletInstance.transform.parent = transform;
            Vector3 Direction = (Quaternion.Euler(0, 0, DeltaAngle/NumBullet*(i-1f*NumBullet/2f)) * Dir).normalized;
            bulletInstance.GetComponent<Bullet>().direction = Direction;
            //new Vector3(Mathf.Cos(2 * Mathf.PI / NumBullet * i * DeltaAngle / 360+2 *Mathf.PI/360*angleInit), Mathf.Sin(2 * Mathf.PI / NumBullet * i * DeltaAngle / 360 + 2 * Mathf.PI / 360 * angleInit), 0);
            bulletInstance.GetComponent<Bullet>().type = Bullet.Type.BOSS;
			bulletInstance.GetComponent<Bullet>().initSpeed = speed;
        }
    }
	void PatternSolo(float Angle, float speed)
    {
        GameObject bulletInstance = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
        bulletInstance.transform.parent = transform;
		bulletInstance.GetComponent<Bullet>().direction = new Vector3(Mathf.Cos(2 * Mathf.PI / 360 * Angle), Mathf.Sin(2 * Mathf.PI / 360 * Angle), 0).normalized;
        bulletInstance.GetComponent<Bullet>().type = Bullet.Type.BOSS;
		bulletInstance.GetComponent<Bullet>().initSpeed = speed;
    }

    IEnumerator PatternSpirale(int bulletNumber, float speed)
    {
        //patternRoutineFinished = false;
        int crtBulletIndex = 0;
        patternRoutineFinished = true;
        while (crtBulletIndex < bulletNumber)
        {
            PatternSolo(360f/bulletNumber*crtBulletIndex, speed);
            crtBulletIndex++;
            yield return new WaitForSeconds(defaultTimeInterval / bulletNumber);
        }
    }
}
