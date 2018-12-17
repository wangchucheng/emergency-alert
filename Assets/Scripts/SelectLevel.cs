using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System.Text;
using System;

public class SelectLevel : MonoBehaviour {
    private int level;
    public Button level2;
    public Button level3;
    public GameObject lock2;
    public GameObject lock3;
    public GameObject ChooseToLoad;
    private string key = "yumenarabadorehodoyokattadeshoui";

    // Use this for initialization
    void Start () {
        UnLock();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EnterLevel(int index)
    {
        string path = Application.dataPath + "/SaveScene" + index + ".json";
        if (File.Exists(path))
        {
            level = index;
            ChooseToLoad.SetActive(true);
        }
        else
        {
            Loading.loadingScene = index;
            SceneManager.LoadScene(2);
        }
    }

    private void UnLock()
    {
        string filePath = Application.dataPath + "/SaveLevel.json";
        if (File.Exists(filePath))
        {
            StreamReader sr = new StreamReader(filePath);
            string jsonStr = sr.ReadToEnd();
            jsonStr = RijndaelDecrypt(jsonStr, key);
            sr.Close();
            //将字符串jsonStr转换为Save对象
            SaveLevel save = JsonMapper.ToObject<SaveLevel>(jsonStr);
            switch (save.maxLevel)
            {
                case 1:
                    level2.interactable = false;
                    level3.interactable = false;
                    break;
                case 2:
                    lock2.SetActive(false);
                    level3.interactable = true;
                    break;
                case 3:
                    lock2.SetActive(false);
                    lock3.SetActive(false);
                    break;
            }
        }
        else
        {
            level2.interactable = false;
            level3.interactable = false;
        }
    }

    public void ChooseToReload()
    {
        switch(level)
        {
            case 1:
                Story1.reload = true;
                break;
            case 2:
                Story2.reload = true;
                break;
            case 3:
                Story3.reload = true;
                break;
        }
        Loading.loadingScene = level;
        SceneManager.LoadScene(4);
    }

    public void NotToReload()
    {
        Loading.loadingScene = level;
        SceneManager.LoadScene(4);
    }

    public void Exit()
    {
        ChooseToLoad.SetActive(false);
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
