using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class SqlAccess : MonoBehaviour {
    
    private static string database = "mygamedb";
    private static string host = "localhost";
    private static string id = "root";
    private static string pwd = "root";


    public static MySqlConnection GetConnection()
    {
        string connStr = "Database=" + database + ";Uid=" + id + ";Pwd=" + pwd + ";Host=" + host + ";";
        MySqlConnection connection = new MySqlConnection(connStr);
        connection.Open();
        return connection;
    }


    public static void InsertHelper(float posX, float posZ, float rotW, float rotY, int stoneNum, int lifeSpan)
    {
        using (MySqlConnection conn = GetConnection())
        {
            string insertStr = "insert into helper(helperPositionX,helperPositionZ,helperRotationW,helperRotationY," +
                "helperStoneNum,lifeSpan) values(@helperPositionX,@helperPositionZ,@helperRotationW,@helperRotationY," +
                "@lifeSpan,@helperStoneNum);";
            using (MySqlCommand cmd = new MySqlCommand(insertStr, conn))
            {
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@helperPositionX", posX);
                cmd.Parameters.AddWithValue("@helperPositionZ", posZ);
                cmd.Parameters.AddWithValue("@helperRotationW", rotW);
                cmd.Parameters.AddWithValue("@helperRotationY", rotY);
                cmd.Parameters.AddWithValue("@helperStoneNum", stoneNum);
                cmd.Parameters.AddWithValue("@lifeSpan", lifeSpan);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void UpdateHelperStoneNum(int id, int stoneNum)
    {
        using (MySqlConnection conn = GetConnection())
        {
            string updateStr = "update helper set helperStoneNum=@helperStoneNum where id_helper=" + id + ";";
            using (MySqlCommand cmd = new MySqlCommand(updateStr, conn))
            {
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@lifeSpan", stoneNum);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void UpdateHelperLifeSpan(int id, int lifeSpan)
    {
        using (MySqlConnection conn = GetConnection())
        {
            string updateStr = "update helper set lifeSpan=@lifeSpan where id_helper=" + id + ";";
            using (MySqlCommand cmd = new MySqlCommand(updateStr, conn))
            {
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@lifeSpan", lifeSpan);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void UpdateHelperPosition(int id, float posX, float posZ, float rotW, float rotY)
    {
        using (MySqlConnection conn = GetConnection())
        {
            string updateStr = "update helper set helperPositionX=@helperPositionX,helperPositionZ=@helperPositionZ," +
                "helperRotationW=@helperRotationW,helperRotationY=@helperRotationY where id_helper=" + id + ";";
            using (MySqlCommand cmd = new MySqlCommand(updateStr, conn))
            {
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@helperPositionX", posX);
                cmd.Parameters.AddWithValue("@helperPositionZ", posZ);
                cmd.Parameters.AddWithValue("@helperRotationW", rotW);
                cmd.Parameters.AddWithValue("@helperRotationY", rotY);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void DeleteHelper(int id)
    {
        using (MySqlConnection conn = GetConnection())
        {
            string deleteStr = "delete from helper where id_helper=" + id + ";";
            using (MySqlCommand cmd = new MySqlCommand(deleteStr, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void InsertEnemy(float posX, float posZ, float rotW, float rotY, int hp)
    {
        using (MySqlConnection conn = GetConnection())
        {
            string insertStr = "insert into helper(enemyPositionX,enemyPositionZ,enemyRotationW,enemyRotationY," +
                "enemyHp) values(@enemyPositionX,@enemyPositionZ,@enemyRotationW,@enemyRotationY,@enemyHp);";
            using (MySqlCommand cmd = new MySqlCommand(insertStr, conn))
            {
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@enemyPositionX", posX);
                cmd.Parameters.AddWithValue("@enemyPositionZ", posZ);
                cmd.Parameters.AddWithValue("@enemyRotationW", rotW);
                cmd.Parameters.AddWithValue("@enemyRotationY", rotY);
                cmd.Parameters.AddWithValue("@enemyHp", hp);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void UpdateEnemyPosition(int id, float posX, float posZ, float rotW, float rotY)
    {
        using (MySqlConnection conn = GetConnection())
        {
            string updateStr = "update enemy set enemyPositionX=@enemyPositionX,enemyPositionZ=@enemyPositionZ," +
                "enemyRotationW=@enemyRotationW,enemyRotationY=@enemyRotationY where id_enemy=" + id + ";";
            using (MySqlCommand cmd = new MySqlCommand(updateStr, conn))
            {
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@enemyPositionX", posX);
                cmd.Parameters.AddWithValue("@enemyPositionZ", posZ);
                cmd.Parameters.AddWithValue("@enemyRotationW", rotW);
                cmd.Parameters.AddWithValue("@enemyRotationY", rotY);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void UpdateEnemyHp(int id, int hp)
    {
        using (MySqlConnection conn = GetConnection())
        {
            string updateStr = "update enemy set enemyHp=@enemyHp where id_enemy=" + id + ";";
            using (MySqlCommand cmd = new MySqlCommand(updateStr, conn))
            {
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@enemyHp", hp);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void DeleteEnemy(int id)
    {
        using (MySqlConnection conn = GetConnection())
        {
            string deleteStr = "delete from enemy where id_enemy=" + id + ";";
            using (MySqlCommand cmd = new MySqlCommand(deleteStr, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void InsertPlayer(float posX, float posZ, float rotW, float rotY, int playerHp, int heartHp, 
        int remainingTime, int stoneNum)
    {
        using (MySqlConnection conn = GetConnection())
        {
            string insertStr = "insert into player(playerPositionX,playerPositionZ,playerRotationW,playerRotationY," +
                "playerStoneNum) values(@playerPositionX,@playerPositionZ,@playerRotationW,@playerRotationY," +
                "@playerHp,@heartHp,@remainingTime,@playerStoneNum);";
            using (MySqlCommand cmd = new MySqlCommand(insertStr, conn))
            {
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@playerPositionX", posX);
                cmd.Parameters.AddWithValue("@playerPositionZ", posZ);
                cmd.Parameters.AddWithValue("@playerRotationW", rotW);
                cmd.Parameters.AddWithValue("@playerRotationY", rotY);
                cmd.Parameters.AddWithValue("@playerHp", playerHp);
                cmd.Parameters.AddWithValue("@heartHp", heartHp);
                cmd.Parameters.AddWithValue("@remainingTime", remainingTime);
                cmd.Parameters.AddWithValue("@playerStoneNum", stoneNum);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void UpdatePlayerPosition(float posX, float posZ, float rotW, float rotY)
    {
        using (MySqlConnection conn = GetConnection())
        {
            string updateStr = "update player set playerPositionX=@playerPositionX,playerPositionZ=@playerPositionZ," +
                "playerRotationW=@playerRotationW,playerRotationY=@playerRotationY where id_player=1;";
            using (MySqlCommand cmd = new MySqlCommand(updateStr, conn))
            {
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@playerPositionX", posX);
                cmd.Parameters.AddWithValue("@playerPositionZ", posZ);
                cmd.Parameters.AddWithValue("@playerRotationW", rotW);
                cmd.Parameters.AddWithValue("@playerRotationY", rotY);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void UpdatePlayerHp(int hp)
    {
        using (MySqlConnection conn = GetConnection())
        {
            string updateStr = "update player set playerHp=@playerHp where id_player=1;";
            using (MySqlCommand cmd = new MySqlCommand(updateStr, conn))
            {
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@playerHp", hp);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void UpdateHeartHp(int hp)
    {
        using (MySqlConnection conn = GetConnection())
        {
            string updateStr = "update player set heartHp=@heartHp where id_player=1;";
            using (MySqlCommand cmd = new MySqlCommand(updateStr, conn))
            {
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@heartHp", hp);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void UpdateRemainingTime(int remainingTime)
    {
        using (MySqlConnection conn = GetConnection())
        {
            string updateStr = "update player set remainingTime=@remainingTime where id_player=1;";
            using (MySqlCommand cmd = new MySqlCommand(updateStr, conn))
            {
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@remainingTime", remainingTime);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void UpdatePlayerStoneNum(int stoneNum)
    {
        using (MySqlConnection conn = GetConnection())
        {
            string updateStr = "update player set playerStoneNum=@playerStoneNum where id_player=1;";
            using (MySqlCommand cmd = new MySqlCommand(updateStr, conn))
            {
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@playerStoneNum", stoneNum);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static float ResumePlayerPosX()
    {
        float posX;
        using (MySqlConnection conn = GetConnection())
        {
            string selectStr = "select playerPositionX from player;";
            using (MySqlCommand cmd = new MySqlCommand(selectStr, conn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    posX = reader.GetFloat(0);
                }
            }
        }
        return posX;
    }

    public static float ResumePlayerPosZ()
    {
        float posZ;
        using (MySqlConnection conn = GetConnection())
        {
            string selectStr = "select playerPositionZ from player;";
            using (MySqlCommand cmd = new MySqlCommand(selectStr, conn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    posZ = reader.GetFloat(0);
                }
            }
        }
        return posZ;
    }

    public static float ResumePlayerRotW()
    {
        float RotW;
        using (MySqlConnection conn = GetConnection())
        {
            string selectStr = "select playerRotationW from player;";
            using (MySqlCommand cmd = new MySqlCommand(selectStr, conn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    RotW = reader.GetFloat(0);
                }
            }
        }
        return RotW;
    }

    public static float ResumePlayerRotY()
    {
        float RotY;
        using (MySqlConnection conn = GetConnection())
        {
            string selectStr = "select playerRotationY from player;";
            using (MySqlCommand cmd = new MySqlCommand(selectStr, conn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    RotY = reader.GetFloat(0);
                }
            }
        }
        return RotY;
    }

    public static int ResumePlayerHp()
    {
        int hp;
        using (MySqlConnection conn = GetConnection())
        {
            string selectStr = "select playerHp from player;";
            using (MySqlCommand cmd = new MySqlCommand(selectStr, conn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    hp = reader.GetInt32(0);
                }
            }
        }
        return hp;
    }

    public static int ResumeHeartHp()
    {
        int hp;
        using (MySqlConnection conn = GetConnection())
        {
            string selectStr = "select heartHp from player;";
            using (MySqlCommand cmd = new MySqlCommand(selectStr, conn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    hp = reader.GetInt32(0);
                }
            }
        }
        return hp;
    }

    public static int ResumePlayerStoneNum()
    {
        int stoneNum;
        using (MySqlConnection conn = GetConnection())
        {
            string selectStr = "select playerStoneNum from player;";
            using (MySqlCommand cmd = new MySqlCommand(selectStr, conn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    stoneNum = reader.GetInt32(0);
                }
            }
        }
        return stoneNum;
    }

    public static int ResumeRemainingTime()
    {
        int remainingTime;
        using (MySqlConnection conn = GetConnection())
        {
            string selectStr = "select remainingTime from player;";
            using (MySqlCommand cmd = new MySqlCommand(selectStr, conn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    remainingTime = reader.GetInt32(0);
                }
            }
        }
        return remainingTime;
    }

    public static float ResumeHelperPosX(int id)
    {
        float posX;
        using (MySqlConnection conn = GetConnection())
        {
            string selectStr = "select helperPositionX from helper where id_helper=" + id + ";";
            using (MySqlCommand cmd = new MySqlCommand(selectStr, conn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    posX = reader.GetFloat(0);
                }
            }
        }
        return posX;
    }

    public static float ResumeHelperPosZ(int id)
    {
        float posZ;
        using (MySqlConnection conn = GetConnection())
        {
            string selectStr = "select helperPositionZ from helper where id_helper=" + id + ";";
            using (MySqlCommand cmd = new MySqlCommand(selectStr, conn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    posZ = reader.GetFloat(0);
                }
            }
        }
        return posZ;
    }

    public static float ResumeHelperRotW(int id)
    {
        float rotW;
        using (MySqlConnection conn = GetConnection())
        {
            string selectStr = "select helperRotationW from helper where id_helper=" + id + ";";
            using (MySqlCommand cmd = new MySqlCommand(selectStr, conn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    rotW = reader.GetFloat(0);
                }
            }
        }
        return rotW;
    }

    public static float ResumeHelperRotY()
    {
        float rotY;
        using (MySqlConnection conn = GetConnection())
        {
            string selectStr = "select helperRotationY from helper where id_helper=" + id + ";";
            using (MySqlCommand cmd = new MySqlCommand(selectStr, conn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    rotY = reader.GetFloat(0);
                }
            }
        }
        return rotY;
    }

    public static int ResumeHelperStoneNum()
    {
        int stoneNum;
        using (MySqlConnection conn = GetConnection())
        {
            string selectStr = "select helperStoneNum from helper where id_helper=" + id + ";";
            using (MySqlCommand cmd = new MySqlCommand(selectStr, conn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    stoneNum = reader.GetInt32(0);
                }
            }
        }
        return stoneNum;
    }

    public static int ResumeHelperLifeSpan()
    {
        int lifeSpan;
        using (MySqlConnection conn = GetConnection())
        {
            string selectStr = "select lifeSpan from helper where id_helper=" + id + ";";
            using (MySqlCommand cmd = new MySqlCommand(selectStr, conn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    lifeSpan = reader.GetInt32(0);
                }
            }
        }
        return lifeSpan;
    }

    public static float ResumeEnemyPosX(int id)
    {
        float posX;
        using (MySqlConnection conn = GetConnection())
        {
            string selectStr = "select enemyPositionX from enemy where id_enemy=" + id + ";";
            using (MySqlCommand cmd = new MySqlCommand(selectStr, conn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    posX = reader.GetFloat(0);
                }
            }
        }
        return posX;
    }

    public static float ResumeEnemyPosZ(int id)
    {
        float posZ;
        using (MySqlConnection conn = GetConnection())
        {
            string selectStr = "select enemyPositionZ from enemy where id_enemy=" + id + ";";
            using (MySqlCommand cmd = new MySqlCommand(selectStr, conn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    posZ = reader.GetFloat(0);
                }
            }
        }
        return posZ;
    }

    public static float ResumeEnemyRotW(int id)
    {
        float rotW;
        using (MySqlConnection conn = GetConnection())
        {
            string selectStr = "select enemyRotationW from enemy where id_enemy=" + id + ";";
            using (MySqlCommand cmd = new MySqlCommand(selectStr, conn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    rotW = reader.GetFloat(0);
                }
            }
        }
        return rotW;
    }

    public static float ResumeEnemyRowY(int id)
    {
        float rotY;
        using (MySqlConnection conn = GetConnection())
        {
            string selectStr = "select enemyRotationY from enemy where id_enemy=" + id + ";";
            using (MySqlCommand cmd = new MySqlCommand(selectStr, conn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    rotY = reader.GetFloat(0);
                }
            }
        }
        return rotY;
    }

    public static int ResumeEnemyHp(int id)
    {
        int hp;
        using (MySqlConnection conn = GetConnection())
        {
            string selectStr = "select enemyHp from enemy where id_enemy=" + id + ";";
            using (MySqlCommand cmd = new MySqlCommand(selectStr, conn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    hp = reader.GetInt32(0);
                }
            }
        }
        return hp;
    }
}
