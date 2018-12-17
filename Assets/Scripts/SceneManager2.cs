using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using LitJson;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;

public class SceneManager2 : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;
    public GameObject Turret;
    public GameObject enemyHeart1;
    public GameObject enemyHeart2;
    public GameObject enemyHeart3;
    public GameObject numberPra;
    public GameObject MiniMap;
    public GameObject HPOfPlayerSlider;
    public GameObject CutDown;
    public GameObject WinImage;
    public GameObject FailImage;
    public GameObject PauseButton;
    public GameObject PauseMenu;
    public GameObject Reminder;
    public GameObject[] enemies;

    private Vector3[] enemyPos = { new Vector3(466, 0, 22), new Vector3(-468, 0, 245), new Vector3(-502, 0, -65) };
    public int maxCount;
    public static int count;
    private float timeVal = 0;
    private string key = "yumenarabadorehodoyokattadeshoui";

    public bool isDead;
    public bool getPoints;
    public bool timeOut;
    public bool gameStart = false;
    public bool gameOver = false;


    //单例
    private static SceneManager2 instance;

    public static SceneManager2 Instance
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
        Instantiate(Player, new Vector3(13, 0, 0), Quaternion.Euler(0,90,0) );
    }

    // Use this for initialization
    void Start()
    {
        count = 0;
        VolumeManager.Instance.PlayMusic("收集地图");
        VolumeManager.Instance.MusicPlayer.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Story2.storyEnd == true && gameStart == false)
        {
            GameStart();
            gameStart = true;
        }

        if (gameStart == true)
        {
            CreateEnemy();
        }

        if (gameOver == false)
        {
            JudgeVictory();
        }
        
    }

    public void JudgeVictory()
    {
        if (isDead == false && timeOut == false && getPoints)
        {
            //胜利跳转
            VolumeManager.Instance.StopMusic();
            SoundManager.Instance.PlaySound("胜利");
            SaveCurLevel();
            gameOver = true;
            WinImage.SetActive(true);
            Invoke("Pause", 3f);
        }
        else if (isDead || timeOut)
        {
            //失败跳转
            VolumeManager.Instance.StopMusic();
            SoundManager.Instance.PlaySound("失败");
            gameOver = true;
            FailImage.SetActive(true);
            Invoke("Pause", 4f);
        }
    }

    public static void NumDecrease()
    {
        count--;
    }

    //游戏开始
    public void GameStart()
    {
        MiniMap.SetActive(true);
        HPOfPlayerSlider.SetActive(true);
        PauseButton.SetActive(true);
        CutDown.SetActive(true);
        Reminder.SetActive(true);
        numberPra.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            Instantiate(Enemy, enemyPos[i], Quaternion.identity);
            count++;
        }
    }

    //生成敌人
    public void CreateEnemy()
    {
        if (timeVal >= 8f && count < maxCount)
        {
            int posIndex = UnityEngine.Random.Range(0, 3);
            Instantiate(Enemy, enemyPos[posIndex], Quaternion.identity);
            count++;
            timeVal = 0;
        }
        else
        {
            timeVal += Time.deltaTime;
        }
    }
    //暂停
    public void Pause()
    {
        Time.timeScale = 0;
    }

    //创建Save
    private SaveScene2 CreateSave()
    {
        SaveScene2 save = new SaveScene2();
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

        //EnemyHeart1的判断
        if(enemyHeart1 == null)
        {
            save.enemyHeartHp1 = 0;
        }
        else
        {
            double enemyHeartHp1 = enemyHeart1.GetComponent<EnemyHeartInPoint_2>().HeartHp;
            save.enemyHeartHp1 = enemyHeartHp1;
        }

        //EnemyHeart2的判断
        if (enemyHeart2 == null)
        {
            save.enemyHeartHp2 = 0;
        }
        else
        {
            double enemyHeartHp2 = enemyHeart2.GetComponent<EnemyHeartInPoint_2>().HeartHp;
            save.enemyHeartHp2 = enemyHeartHp2;
        }

        //EnemyHeart3的判断
        if (enemyHeart3 == null)
        {
            save.enemyHeartHp3 = 0;
        }
        else
        {
            double enemyHeartHp3 = enemyHeart3.GetComponent<EnemyHeartInPoint_2>().HeartHp;
            save.enemyHeartHp3 = enemyHeartHp3;
        }
        double remainingTime = CutDown.GetComponent<CutDown>().remainingTime;
        save.remainingTime = remainingTime;
        int awareCount = numberPra.GetComponent<AwareUIInPoint_2>().awareCount;
        save.awareCount = awareCount;
        return save;
    }

    //通过读档信息重置游戏状态
    public void SetGame(SaveScene2 save)
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
        player.GetComponent<Player>().Hp = (float)save.playerHp;
        GameObject heart = GameObject.FindGameObjectWithTag("home");

        //EnemyHeart1的判断
        if (enemyHeart1 == null && save.enemyHeartHp1 != 0)
        {
            Instantiate(enemyHeart1);
            enemyHeart1.GetComponent<EnemyHeartInPoint_2>().HeartHp = (float)save.enemyHeartHp1;
        }
        else if(enemyHeart1 != null)
        {
            if(save.enemyHeartHp1 == 0)
            {
                Destroy(enemyHeart1);
            }
            else
            {
                enemyHeart1.GetComponent<EnemyHeartInPoint_2>().HeartHp = (float)save.enemyHeartHp1;
            }
        }

        //EnemyHeart2的判断
        if (enemyHeart2 == null && save.enemyHeartHp2 != 0)
        {
            Instantiate(enemyHeart2);
            enemyHeart2.GetComponent<EnemyHeartInPoint_2>().HeartHp = (float)save.enemyHeartHp2;
        }
        else if (enemyHeart2 != null)
        {
            if (save.enemyHeartHp2 == 0)
            {
                Destroy(enemyHeart2);
            }
            else
            {
                enemyHeart2.GetComponent<EnemyHeartInPoint_2>().HeartHp = (float)save.enemyHeartHp2;
            }
        }

        //EnemyHeart3的判断
        if (enemyHeart3 == null && save.enemyHeartHp3 != 0)
        {
            Instantiate(enemyHeart3);
            enemyHeart3.GetComponent<EnemyHeartInPoint_2>().HeartHp = (float)save.enemyHeartHp3;
        }
        else if (enemyHeart3 != null)
        {
            if (save.enemyHeartHp3 == 0)
            {
                Destroy(enemyHeart3);
            }
            else
            {
                enemyHeart3.GetComponent<EnemyHeartInPoint_2>().HeartHp = (float)save.enemyHeartHp3;
            }
        }

        numberPra.GetComponent<AwareUIInPoint_2>().awareCount = save.awareCount;
        float remainingTime = (float)save.remainingTime;
        CutDown.GetComponent<CutDown>().StopCoroutine("TimeCutDown");
        CutDown.GetComponent<CutDown>().StartCoroutine("TimeCutDown", remainingTime);
    }

    //JSON:存档和读档
    public void SaveByJson()
    {
        SaveScene2 save = CreateSave();
        string filePath = Application.dataPath + "/SaveScene2.json";
        string saveJsonStr = JsonMapper.ToJson(save);
        saveJsonStr = RijndaelEncrypt(saveJsonStr, key);
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(saveJsonStr);
        sw.Close();
        PauseMenu.SendMessage("ShowMessage", "保存成功");
        Invoke("CleanTheText", 1f);
    }

    public void LoadByJson()
    {
        string filePath = Application.dataPath + "/SaveScene2.json";
        if (File.Exists(filePath))
        {
            StreamReader sr = new StreamReader(filePath);
            string jsonStr = sr.ReadToEnd();
            jsonStr = RijndaelDecrypt(jsonStr, key);
            sr.Close();

            //将字符串jsonStr转换为Save对象
            SaveScene2 save = JsonMapper.ToObject<SaveScene2>(jsonStr);
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

    //关卡记录
    private void SaveCurLevel()
    {
        int maxLevel = 3;
        SaveLevel save = new SaveLevel();
        string filePath = Application.dataPath + "/SaveLevel.json";
        if (File.Exists(filePath))
        {
            StreamReader sr = new StreamReader(filePath);
            string jsonStr = sr.ReadToEnd();
            jsonStr = RijndaelDecrypt(jsonStr, key);
            sr.Close();

            //将字符串jsonStr转换为Save对象
            save = JsonMapper.ToObject<SaveLevel>(jsonStr);
            if (save.maxLevel <= maxLevel)
            {
                ChangeMaxLevel(save);
            }
        }
        else
        {
            ChangeMaxLevel(save);
        }

    }

    //更改最高关卡
    private void ChangeMaxLevel(SaveLevel save)
    {
        save.maxLevel = 3;
        string filePath = Application.dataPath + "/SaveLevel.json";
        string saveJsonStr = JsonMapper.ToJson(save);
        saveJsonStr = RijndaelEncrypt(saveJsonStr, key);
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(saveJsonStr);
        sw.Close();
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
}


