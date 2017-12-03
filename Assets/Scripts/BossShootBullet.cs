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
	private int[] patternCombination;
	private int[] patternCombinationP0 = {12,0,6,6,6,4,0,4,4,6,6,6,6,6,6,6,6,6,0,4,4,4,6,6,6,6,6,6,0,4,4,4,4,6,6,6,6,6,6,0,4,4,6,6,6,4,6,6,6,4,6,6,6,6,6,6,0,0};
	private int[] patternCombinationP21 = {3,0,3,0,0,5,5,0,5,5,5,0,3,3,5,5,3,3,5,5,0,0};
    private int[] patternCombinationP22 = {4,4,5,5,5,0,4,5,5,4,5,4,0,3,6,6,6,3,3,6,6,6,3,5,6,6,6,5,5,6,6,6,3,5,4,4,4,5,6,5,6,6,5,5,0,0};
    private int[] patternCombinationP2 = { 3, 0, 3, 0, 0, 5, 5, 0, 5, 5, 5, 0, 3, 3, 5, 5, 3, 3, 5, 5, 0, 0, 4, 4, 5, 5, 5, 0, 4, 5, 5, 4, 5, 4, 0, 3, 6, 6, 6, 3, 3, 6, 6, 6, 3, 5, 6, 6, 6, 5, 5, 6, 6, 6, 3, 5, 4, 4, 4, 5, 6, 5, 6, 6, 5, 5, 0, 0 };
    private int[] patternCombinationP1 = {0};
    //private float[] patternSpeeds = {-1f, -1f, -1f, 1f, -1f, -1f, -1f, 1f, 1f ,-1f, -1f, -1f, -1f, 1f, 1f, -1f, -1f, -1f, 1f, -1f, -1f, -1f, -1f, -1f, -1f, 1f, 1f, -1f, -1f, -1f, -1f, 1f, 1f, -1f, -1f, -1f, -1f, 1f, 1f, -1f, -1f, -1f };
	//private int[] patternCombination = {12, 0, 5};
	//private float[] patternSpeeds = {-1f, -1f, 1.5f};
    private int crtPatternIndex;
    private float realDeltaTimes;
    private bool patternRoutineFinished;
    private int patternNumber = 0;
    private int Igat;


    private void Start()
    {
        crtPatternIndex = 0;
        patternRoutineFinished = false;
		realDeltaTimes = defaultTimeInterval;
        Igat = -1;
    }

    void Update()
    {
		int phase = GetComponent<BossStatus> ().phase;
		if (phase == 0) {patternCombination = patternCombinationP0;}
		else if (phase==1) {patternCombination = patternCombinationP1;}
		else if (phase==2) {patternCombination = patternCombinationP2;}
        crtTime += Time.deltaTime;

        if (crtTime >= realDeltaTimes)
        {
            realDeltaTimes = defaultTimeInterval;
            if (crtPatternIndex > patternCombination.Length-1) 
			{ 
				crtPatternIndex = 0;
			}

			float crtSpeed = defaultBulletSpeed;
			//if (patternSpeeds [crtPatternIndex] != -1)
			//	crtSpeed = patternSpeeds [crtPatternIndex];

            patternNumber = patternCombination[crtPatternIndex];
			if (patternNumber == 0) {
				//Empty pattern (pause)
				crtPatternIndex++;
                realDeltaTimes = defaultTimeInterval * 3;
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
				StartCoroutine(PatternSpirale(15, crtSpeed));
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
                Debug.Log("lkseflksjdf   " + Igat);
                Igat = Igat + 1;
                Vector3 PosPlayer = (ThePlayer.transform.position - transform.position).normalized;
				PatternWave(5, 75, PosPlayer, crtSpeed,4f-4f*Mathf.Pow(-1,-Igat));
                crtPatternIndex++;
                if (Igat > 0) { Igat = -1; }
                realDeltaTimes = defaultTimeInterval / 3;
            }
            else if (patternNumber == 6)
            {
                crtSpeed = crtSpeed * Mathf.Pow(2, 1-Mathf.Abs(Igat));
                crtSpeed = crtSpeed * Mathf.Pow(1.5f, Mathf.Abs(Igat));
                realDeltaTimes = defaultTimeInterval / 2;
                Vector3 PosPlayer = (ThePlayer.transform.position - transform.position).normalized;
                StartCoroutine(PatternGatling(4, PosPlayer, crtSpeed,30*Igat));
                crtPatternIndex++;
                Igat = Igat + 1;
                if (Igat > 1) { Igat = -1; }
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
	void PatternWave(int NumBullet, float DeltaAngle, Vector3 Dir, float speed,float Offset)
    {
        for (int i = 0; i < NumBullet; i++)
        {
            GameObject bulletInstance = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
            bulletInstance.transform.parent = transform;
            Vector3 Direction = (Quaternion.Euler(0, 0, DeltaAngle/NumBullet*(i-1f*NumBullet/2f)+Offset) * Dir).normalized;
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
    IEnumerator PatternGatling(float bulletNumber, Vector3 Dir, float speed,int Offset)
    {
        int crtBulletIndex = 0;
        while (crtBulletIndex < bulletNumber)
        {
            GameObject bulletInstance = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
            bulletInstance.transform.parent = transform;
            Vector3 Direction = (Quaternion.Euler(0, 0, Mathf.Pow(-1,crtBulletIndex)*crtBulletIndex+Offset) * Dir).normalized;
            bulletInstance.GetComponent<Bullet>().direction = Direction;
            bulletInstance.GetComponent<Bullet>().type = Bullet.Type.BOSS;
            bulletInstance.GetComponent<Bullet>().initSpeed = speed;
            crtBulletIndex++;
            yield return new WaitForSeconds(defaultTimeInterval / bulletNumber);
        }
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
