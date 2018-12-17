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

public class SceneManager1 : MonoBehaviour
{
    public GameObject Player;
    public GameObject Heart;
    public GameObject Enemy;
    public GameObject Turret;
    public GameObject MiniMap;
    public GameObject HPOfPlayerSlider;
    public GameObject HPOfHeartSlider;
    public GameObject CutDown;
    public GameObject WinImage;
    public GameObject FailImage;
    public GameObject PauseButton;
    public GameObject PauseMenu;
    public GameObject[] enemies;

    private Vector3[] enemyPos = { new Vector3(-35, 0, -398), new Vector3(-110, 0, -240), new Vector3(-95, 0, -78) };
    public int maxCount;
    public static int count;
    private float timeVal = 0;
    private string key = "yumenarabadorehodoyokattadeshoui";

    public bool isDead = false;
    public bool isDefeat = false;
    public bool timeOut = false;
    public bool gameStart = false;
    public bool gameOver = false;


    //单例
    private static SceneManager1 instance;

    public static SceneManager1 Instance
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
        Instantiate(Player, new Vector3(-492, 0, -298), Quaternion.Euler(0,90,0));
       
    }

    // Use this for initialization
    void Start()
    {
        count = 0;
        VolumeManager.Instance.PlayMusic("防守地图");
        VolumeManager.Instance.MusicPlayer.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Story1.storyEnd == true && gameStart == false)
        {
            GameStart();
            gameStart = true;
        }

        if(gameStart == true)
        {
            CreateEnemy();
        }

        if(gameOver == false)
        {
            JudgeVictory();
        }
        
    }

    public void JudgeVictory()
    {
        if (isDead == false && isDefeat == false && timeOut)
        {
            //胜利跳转
            VolumeManager.Instance.StopMusic();
            SoundManager.Instance.PlaySound("胜利");
            SaveCurLevel();
            gameOver = true;
            WinImage.SetActive(true);
            Invoke("Pause", 3f);
        }
        else if (isDead || isDefeat)
        {
            //失败跳转
            VolumeManager.Instance.StopMusic();
            SoundManager.Instance.PlaySound("失败");
            gameOver = true;
            FailImage.SetActive(true);
            Invoke("Pause", 1f);
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
        HPOfHeartSlider.SetActive(true);
        PauseButton.SetActive(true);
        CutDown.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            Instantiate(Enemy, enemyPos[i], Quaternion.identity);
            count++;
        }
    }

    //生成敌人
    public void CreateEnemy()
    {
        if (timeVal >= 10f && count < maxCount)
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
        Debug.Log("Pause");
        Time.timeScale = 0;
    }

    //创建Save
    private SaveScene1 CreateSave()
    {
        SaveScene1 save = new SaveScene1();
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
        GameObject heart = GameObject.FindGameObjectWithTag("home");
        double heartHp = heart.GetComponent<Heart>().Hp;
        save.heartHp = heartHp;
        double remainingTime = CutDown.GetComponent<CutDown>().remainingTime;
        save.remainingTime = remainingTime;
        return save;
    }

    //通过读档信息重置游戏状态
    public void SetGame(SaveScene1 save)
    {
        //先将所有的targrt里面的怪物清空，并重置所有的计时
        if(count != 0)
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
        heart.GetComponent<Heart>().Hp = (float)save.heartHp;
        float remainingTime = (float)save.remainingTime;
        CutDown.GetComponent<CutDown>().StopCoroutine("TimeCutDown");
        CutDown.GetComponent<CutDown>().StartCoroutine("TimeCutDown", remainingTime);

    }

    //JSON:存档和读档
    public void SaveByJson()
    {
        SaveScene1 save = CreateSave();
        string filePath = Application.dataPath + "/SaveScene1.json";
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
        string filePath = Application.dataPath + "/SaveScene1.json";
        if (File.Exists(filePath))
        {
            StreamReader sr = new StreamReader(filePath);
            string jsonStr = sr.ReadToEnd();
            jsonStr = RijndaelDecrypt(jsonStr, key);
            sr.Close();

            //将字符串jsonStr转换为Save对象
            SaveScene1 save = JsonMapper.ToObject<SaveScene1>(jsonStr);
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
        int maxLevel = 2;
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
            if(save.maxLevel <= maxLevel)
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
        save.maxLevel = 2;
        string filePath = Application.dataPath + "/SaveLevel.json";
        string saveJsonStr = JsonMapper.ToJson(save);
        saveJsonStr = RijndaelEncrypt(saveJsonStr, key);
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(saveJsonStr);
        sw.Close();
    }

    private void CleanTheText()
    {
        //PauseMenu.SendMessage("CleanMessage");
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

