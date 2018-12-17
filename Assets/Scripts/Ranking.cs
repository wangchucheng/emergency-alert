using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;


public class Ranking : MonoBehaviour {

    public static List<Score> scoreList = new List<Score>();
    public GameObject[] rankingItems;
    private string key = "yumenarabadorehodoyokattadeshoui";

    public void LoadRanking()
    {
        string filePath = Application.dataPath + "/RankingList.json";
        if (File.Exists(filePath))
        {
            StreamReader sr = new StreamReader(filePath);
            string nextLine;
            while ((nextLine = sr.ReadLine()) != null)
            {
                scoreList.Add(JsonMapper.ToObject<Score>(nextLine));
            }
            sr.Close();//将所有存储的分数全部存到list中
        }  
    }

    public void SaveRanking()
    {
        scoreList.Sort();
        scoreList.Reverse();
        StreamWriter sw = new StreamWriter(Application.dataPath + "/RankingList.json");
        if (scoreList.Count > 9)
        {
            for (int i = 9; i <= scoreList.Count; i++) scoreList.RemoveAt(i);
        }
        for (int i = 0; i < scoreList.Count; i++)
        {
            string saveJsonStr = JsonMapper.ToJson(scoreList[i]);
            sw.WriteLine(saveJsonStr);
            Debug.Log(scoreList[i].ToString());
        }
        sw.Close();
        scoreList.Clear();
    }

    public void AddRanking()
    {
        Score save = new Score();
        string name = InputButtonController.playerName;
        save.name = name;
        double time = SceneManager3.time;
        save.time = time;
        scoreList.Add(save);
    }

    public void AddToPrefab()
    {
        for (int i = 0; i < scoreList.Count; i++)
        {
            GameObject rankingItem = rankingItems[i];
            rankingItem.transform.Find("Name").GetComponent<Text>().text = scoreList[i].name;
            rankingItem.transform.Find("Time").GetComponent<Text>().text = scoreList[i].time.ToString();
            scoreList.Clear();
        }

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
