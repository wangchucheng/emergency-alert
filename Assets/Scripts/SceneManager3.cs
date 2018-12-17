using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LitJson;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;

public class SceneManager3 : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;
    public GameObject Boss;
    public GameObject Turret;
    public GameObject timer;
    public GameObject BossSlider;
    //public GameObject WinImage;
    public GameObject FailImage;
    public GameObject PauseButton;
    public GameObject PauseMenu;
    public GameObject MiniMap;
    public GameObject PlayerHpSlider;
    public GameObject storyEnd;
    public GameObject[] enemies;
    public GameObject TimeMachine;
    public GameObject barrier;

    private Vector3[] enemyPos = { new Vector3(-75, 0, 112), new Vector3(115, 0, 112), new Vector3(0, 0, -5) };
    public int maxCount;
    public static int count;
    public static int totalCount = 0;
    public static float time;
    private float timeVal = 0;
    private string key = "yumenarabadorehodoyokattadeshoui";

    public bool isDead;
    public bool bossDead;
    public bool gameStart = false;
    public bool gameOver = false;
    private bool annoyingUpdate = false;
    public bool loadAndHaveBoss = false;

    //单例
    private static SceneManager3 instance;

    public static SceneManager3 Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        Instance = this;
        Instantiate(Player, new Vector3(0, 0, -553), Quaternion.identity);
    }

    // Use this for initialization
    void Start()
    {
        count = 0;
        totalCount = 0;
        VolumeManager.Instance.PlayMusic("攻击地图");
        VolumeManager.Instance.MusicPlayer.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Story3.storyEnd && !gameStart)
        {
            GameStart();
            gameStart = true;
        }

        if (gameStart)
        {
            CreateEnemy();
        }
        if (totalCount > 4 && count <= 0 && annoyingUpdate == false)
        {
            OpenTheBarrier();
            annoyingUpdate = true;
        }

        if (gameOver == false)
        {
            JudgeVictory();
        }

        
    }


    private void GameStart()
    {
        MiniMap.SetActive(true);
        PlayerHpSlider.SetActive(true);
        PauseButton.SetActive(true);
        TimeMachine.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            Instantiate(Enemy, enemyPos[i], Quaternion.identity);
            count++;
            totalCount++;
        }
    }

    private void CreateEnemy()
    {
        if (timeVal >= 8f && count < maxCount && totalCount <= 4)
        {
            int posIndex = UnityEngine.Random.Range(0, 3);
            Instantiate(Enemy, enemyPos[posIndex], Quaternion.identity);
            count++;
            totalCount++;
            timeVal = 0;
        }
        else
        {
            timeVal += Time.deltaTime;
        }
    }

    private void JudgeVictory()
    {
        if (bossDead)
        {
            //胜利跳转
            time = timer.GetComponent<Timer>().remainingTime;
            VolumeManager.Instance.StopMusic();
            SoundManager.Instance.PlaySound("FinalVictory");
            gameOver = true;
            storyEnd.SetActive(true);
            MiniMap.SetActive(false);
            PlayerHpSlider.SetActive(false);
            PauseButton.SetActive(false);
            BossSlider.SetActive(false);
            TimeMachine.SetActive(false);
            //Invoke("Pause", 3f);
        }

        if (isDead)
        {
            //失败跳转
            VolumeManager.Instance.StopMusic();
            SoundManager.Instance.PlaySound("失败");
            gameOver = true;
            FailImage.SetActive(true);
            Invoke("Pause", 1f);
        }
    }

    //生成Boss
    public void CreateBoss()
    {
        Instantiate(Boss, new Vector3(0, 0, 468), Quaternion.identity);
        BossSlider.SetActive(true);
        MiniMap.SetActive(true);
        PlayerHpSlider.SetActive(true);
        PauseButton.SetActive(true);

    }

    public static void NumDecrease()
    {
        count--;
    }

    //暂停
    public void Pause()
    {
        Time.timeScale = 0;
    }

    //创建Save
    private SaveScene3 CreateSave()
    {
        SaveScene3 save = new SaveScene3();
        GameObject player = GameObject.FindGameObjectWithTag("player");
        save.playerPositionX = player.transform.position.x;
        save.playerPositionZ = player.transform.position.z;
        save.playerRotationW = player.transform.rotation.w;
        save.playerRotationY = player.transform.rotation.y;
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject gameObject in enemies)
        {
            Enemy enemy = gameObject.GetComponent<Enemy>();
            double enemyPositionX = enemy.transform.position.x;
            double enemyPositionZ = enemy.transform.position.z;
            double enemyRotationW = enemy.transform.rotation.w;
            double enemyRotationY = enemy.transform.rotation.y;
            save.enemyPositionX.Add(enemyPositionX);
            save.enemyPositionZ.Add(enemyPositionZ);
            save.enemyRotationW.Add(enemyRotationW);
            save.enemyRotationY.Add(enemyRotationY);
        }
        double playerHp = player.GetComponent<Player>().Hp;
        save.playerHp = playerHp;

        //Boss存在状态的判断
        GameObject boss = GameObject.FindGameObjectWithTag("boss");
        if (boss == null)
        {
            save.playerPositionX = 0;
            save.playerPositionZ = 0;
            save.playerRotationW = 0;
            save.playerRotationY = 0;
            save.bossHp = 0;
        }
        else
        {
            save.bossPositionX = boss.transform.position.x;
            save.bossPositionZ = boss.transform.position.z;
            save.bossRotationW = boss.transform.rotation.w;
            save.bossRotationY = boss.transform.rotation.y;
            save.bossHp = boss.GetComponent<Boss>().Hp;
        }

        save.totalCount = totalCount;
        save.time = timer.GetComponent<Timer>().remainingTime;
        return save;
    }

    //通过读档信息重置游戏状态
    public void SetGame(SaveScene3 save)
    {
        //先将所有的targrt里面的怪物清空，并重置所有的计时
        if (count != 0)
        {
            //摧毁所有
            GameObject[] enemy = GameObject.FindGameObjectsWithTag("enemy");
            for (int i = 0; i < count; i++)
            {
                Destroy(enemy[i]);
            }
            count = 0;
        }
        //反序列化得到的Save对象中存储的信息
        float playerPositionX = (float)save.playerPositionX;
        float playerPositionZ = (float)save.playerPositionZ;
        float playerRotationW = (float)save.playerRotationW;
        float playerRotationY = (float)save.playerRotationY;
        Vector3 playerPosition = new Vector3(playerPositionX, 0, playerPositionZ);
        Quaternion playerRotation = new Quaternion(0, playerRotationY, 0, playerRotationW);
        GameObject player = GameObject.FindGameObjectWithTag("player");
        player.transform.position = playerPosition;
        player.transform.rotation = playerRotation;
        player.GetComponent<Player>().Hp = (float)save.playerHp;
        totalCount = save.totalCount;
        for (int i = 0; i < save.enemyPositionX.Count; i++)
        {
            float positionX = (float)save.enemyPositionX[i];
            float positionZ = (float)save.enemyPositionZ[i];
            float rotationW = (float)save.enemyRotationW[i];
            float rotationY = (float)save.enemyRotationY[i];
            Vector3 position = new Vector3(positionX, 0, positionZ);
            Quaternion rotation = new Quaternion(0, rotationY, 0, rotationW);
            Instantiate(Enemy, position, rotation);
            count++;
        }
        //Boss状态的判断
        if (save.bossHp == 0)
            return;
        loadAndHaveBoss = true;
        GameObject boss = GameObject.FindGameObjectWithTag("boss");
        float bossPositionX = (float)save.bossPositionX;
        float bossPositionZ = (float)save.bossPositionZ;
        float bossRotationW = (float)save.bossRotationW;
        float bossRotationY = (float)save.bossRotationY;
        Vector3 bossPosition = new Vector3(bossPositionX, 0, bossPositionZ);
        Quaternion bossRotation = new Quaternion(0, bossRotationY, 0, bossRotationW);
        if (boss != null)
        {
            Debug.Log("有boss");
            boss.transform.position = bossPosition;
            boss.transform.rotation = bossRotation;
            boss.GetComponent<Boss>().Hp = (float)save.bossHp;
        }
        else
        {
            boss = Instantiate(Boss, bossPosition, bossRotation);
            BossSlider.SetActive(true);
            Debug.Log("生成boss");
            StartCoroutine(LoadHp(boss, save.bossHp));
            annoyingUpdate = true;
        }
        float remainingTime = (float)save.time;
        timer.GetComponent<Timer>().StopCoroutine("TimeCutDown");
        timer.GetComponent<Timer>().StartCoroutine("TimeCutDown", remainingTime);

    }

    //延时调用，加载存档的血量
    IEnumerator LoadHp(GameObject boss,double hp)
    {
        yield return new WaitForSeconds(0.05f);
        boss.GetComponent<Boss>().Hp = (float)hp;
    }

    //JSON:存档和读档
    public void SaveByJson()
    {
        SaveScene3 save = CreateSave();
        string filePath = Application.dataPath + "/SaveScene3.json";
        string saveJsonStr = JsonMapper.ToJson(save);
        //saveJsonStr = RijndaelEncrypt(saveJsonStr, key);
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(saveJsonStr);
        sw.Close();
        PauseMenu.SendMessage("ShowMessage", "保存成功");
        Invoke("CleanTheText", 1f);
    }

    public void LoadByJson()
    {
        string filePath = Application.dataPath + "/SaveScene3.json";
        if (File.Exists(filePath))
        {
            StreamReader sr = new StreamReader(filePath);
            string jsonStr = sr.ReadToEnd();
            //jsonStr = RijndaelDecrypt(jsonStr, key);
            sr.Close();

            //将字符串jsonStr转换为Save对象
            SaveScene3 save = JsonMapper.ToObject<SaveScene3>(jsonStr);
            SetGame(save);
            PauseMenu.SendMessage("ShowMessage", "加载成功");
            Invoke("CleanTheText", 1f);
        }
        else
        {
            PauseMenu.SendMessage("ShowMessage", "加载失败");
            Invoke("CleanTheText", 1f);
        }
    }

    private void CleanTheText()
    {
        PauseMenu.GetComponent<PauseMenuCanvas>().CleanMessage();
    }

    private static string RijndaelEncrypt(string pString, string pKey)
    {
        //密钥
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes(pKey);
        //待加密明文数组
        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(pString);

        //Rijndael解密算法
        RijndaelManaged rDel = new RijndaelManaged();
        rDel.Key = keyArray;
        rDel.Mode = CipherMode.ECB;
        rDel.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = rDel.CreateEncryptor();

        //返回加密后的密文
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    private static String RijndaelDecrypt(string pString, string pKey)
    {
        //解密密钥
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes(pKey);
        //待解密密文数组
        byte[] toEncryptArray = Convert.FromBase64String(pString);

        //Rijndael解密算法
        RijndaelManaged rDel = new RijndaelManaged();
        rDel.Key = keyArray;
        rDel.Mode = CipherMode.ECB;
        rDel.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = rDel.CreateDecryptor();

        //返回解密后的明文
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        return UTF8Encoding.UTF8.GetString(resultArray);
    }


    private void OpenTheBarrier()
    {
        MiniMap.SetActive(false);
        PlayerHpSlider.SetActive(false);
        PauseButton.SetActive(false);
        barrier.GetComponent<OpenAndClose>().Open();
        Invoke("CreateBoss", 2.5f);
    }
}
