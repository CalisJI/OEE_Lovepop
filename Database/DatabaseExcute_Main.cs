using MySql.Data.MySqlClient;
using OEE_dotNET.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using Microsoft.AspNetCore.Identity;
using System.Data;
using Org.BouncyCastle.Asn1.X509.Qualified;
using OEE_dotNET.ViewModel;
using System.Diagnostics;

namespace OEE_dotNET.Database
{
    public static class DatabaseExcute_Main
    {
        private static string Password = "12345678";
        private static string Host = "localhost";
        private static string User = "root";
        private static string Database = "workstation";
        private static string acounts_tbl = "accounts_tbl";
        private static string lazer_tbl = "lazer_configuration";
        private static string Str_connection = "Server = " + Host + ";Database =" + Database + "; UId = " + User + "; Pwd = " + Password + "; Pooling = false; Character Set=utf8; SslMode=none";


        private static string Machine_rumtime_tbl = "machine_data_runtime";
        private static string Password_MCN = "12345678";
        private static string Host_MCN = "localhost";
        private static string User_MCN = "root";
        private static string Database_MCN = "workstation";
        private static string MCN_connection = "Server = " + Host_MCN + ";Database =" + Database_MCN + "; UId = " + User_MCN + "; Pwd = " + Password_MCN + "; Pooling = false; Character Set=utf8; SslMode=none";

        #region Login Databse action
        public static Accounts? Current_User = null;
        public static bool Check_Database()
        {
            MySqlConnection connection = new MySqlConnection(Str_connection);
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                    string query = "SELECT EXISTS ( " +
                        "SELECT 1 " +
                        "FROM INFORMATION_SCHEMA.TABLES " +
                        "WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = @tableName " +
                        ")";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@tableName", acounts_tbl);
                    var check = false;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        check = reader.GetInt32(0) == 1;
                    }

                    return check;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static void Login_User(string? username, string? password)
        {
            MySqlConnection connection = new MySqlConnection(Str_connection);
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                    var hashpass = HashPassword(password);
                    string query = "SELECT * FROM " + acounts_tbl + " WHERE username = @Username AND password = @Password";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", hashpass);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Accounts accounts = new Accounts()
                            {

                                Username = reader.GetString("username"),
                                Permission = reader.GetString("permission")
                            };
                            Current_User = accounts;
                        }
                        else
                        {
                            // Raise
                            // Username or Password is incorrect
                            throw new Exception("Username or Password is incorret");
                            //MessageBox.Show("Username or Password is incorret");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public static void User_register(string username, string password, string email, string permission)
        {
            MySqlConnection connection = new MySqlConnection(Str_connection);
            try
            {
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    var hasspw = HashPassword(password);
                    if (connection.State == System.Data.ConnectionState.Closed)
                    {
                        connection.Open();
                        string query = "INSERT INTO " + acounts_tbl + " (`username`,`password`,`email`,`permission`) VALUES (@username,@password,@email,@permission)";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", hasspw);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@permission", permission);
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public static void Change_password(string username, string password, string newpassword)
        {
            MySqlConnection connection = new MySqlConnection(Str_connection);
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                    string query = "SELECT FROM " + acounts_tbl + " WHERE username = @username";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@username", username);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (VerifyPassword(password, reader.GetString("password")))
                            {
                                string qry = $"UPDATE {acounts_tbl} SET password = @newpassword WHERE username = @username";
                                MySqlCommand cmd1 = new MySqlCommand(qry, connection);
                                cmd1.Parameters.AddWithValue("@newpassword", newpassword);
                                cmd1.Parameters.AddWithValue("@username", username);
                                cmd1.ExecuteNonQuery();
                                MessageBox.Show("Update password successfully");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Your old password is incorrect");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        static string HashPassword(string password)
        {
            using (var md5 = MD5.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = md5.ComputeHash(bytes);
                return Convert.ToHexString(hash);
            }
        }
        static bool VerifyPassword(string hashPass, string storePass)
        {
            if (HashPassword(hashPass) == storePass)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Plan Option Page databse action
        public static async Task InsertDataTableToMySQLExceptId(DataTable dataTable)
        {
            for (int j = 0; j < dataTable.Rows.Count; j++)
            {
                using (MySqlConnection connection = new MySqlConnection(Str_connection))
                {
                    await connection.OpenAsync();

                    // Build the SQL INSERT statement dynamically (excluding Id column)
                    StringBuilder sql = new StringBuilder("INSERT INTO total_plan (");
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        if (dataTable.Columns[i].ColumnName != "id")  // Skip the Id column
                        {
                            sql.Append(dataTable.Columns[i].ColumnName);
                            if (i < dataTable.Columns.Count - 1 && dataTable.Columns[i].ColumnName != "id") // Check again for Id
                            {
                                sql.Append(", ");
                            }
                        }
                    }
                    sql.Remove(sql.Length - 2, 2);
                    sql.Append(") VALUES (");
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        if (dataTable.Columns[i].ColumnName != "id")  // Skip the Id column
                        {
                            sql.Append("@param" + i);
                            if (i < dataTable.Columns.Count - 1 && dataTable.Columns[i].ColumnName != "id") // Check again for Id
                            {
                                sql.Append(", ");
                            }
                        }
                    }
                    sql.Remove(sql.Length - 2, 2);
                    sql.Append(")");

                    using (MySqlCommand command = new MySqlCommand(sql.ToString(), connection))
                    {
                        int paramIndex = 0; // Track parameter index (since we're skipping Id)
                        for (int i = 0; i < dataTable.Columns.Count; i++)
                        {
                            if (dataTable.Columns[i].ColumnName != "id")  // Skip the Id column
                            {
                                //command.Parameters.AddWithValue("@param" + paramIndex, dataTable.Columns[i].DataType == typeof(string) ? "'" + dataTable.Rows[j][i].ToString() + "'" : dataTable.Rows[0][i]);

                                if (dataTable.Columns[i].DataType == typeof(string))
                                {
                                    command.Parameters.AddWithValue("@param" + paramIndex, "" + dataTable.Rows[j][i].ToString() + "");
                                }
                                else if (dataTable.Columns[i].DataType == typeof(int))
                                {
                                    command.Parameters.AddWithValue("@param" + paramIndex, Convert.ToInt32(dataTable.Rows[j][i]));
                                }
                                else if (dataTable.Columns[i].DataType == typeof(DateTime))
                                {
                                    command.Parameters.AddWithValue("@param" + paramIndex, (DateTime)dataTable.Rows[j][i]);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@param" + paramIndex, "" + dataTable.Rows[j][i].ToString() + "");
                                }
                                paramIndex++;

                            }
                        }
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
        }
        #endregion

        #region Technican Department Database action

        public static DataTable LoadConfig() 
        {
            var datatable = new DataTable();
            
            MySqlConnection connection = new MySqlConnection(Str_connection);
            try
            {
                if(connection.State == ConnectionState.Closed) 
                {
                    connection.Open();
                    string? query = $"SELECT * FROM {lazer_tbl}";
                    using(MySqlCommand command = new MySqlCommand(query, connection)) 
                    {
                        using (MySqlDataReader reader = command.ExecuteReader()) 
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);
                                datatable.Columns.Add(columnName);
                            }

                            while (reader.Read()) 
                            {
                                object[] values = new object[reader.FieldCount];
                                reader.GetValues(values);
                                datatable.Rows.Add(values);
                            }
                        }
                    }
                    return datatable;
                }
                else 
                {
                    return datatable = new DataTable()
                    {
                        Columns =
                        {
                            new DataColumn("id",typeof(int)),
                            new DataColumn("color",typeof(string)),
                            new DataColumn("tanso",typeof(double)),
                            new DataColumn("nangluong",typeof(double)),
                            new DataColumn("step_size",typeof(double)),
                            new DataColumn("dotre_trunggian",typeof(double)),
                            new DataColumn("dotre_tat",typeof(double)),
                            new DataColumn("delay",typeof(double)),
                            new DataColumn("ucche_nangluong",typeof(double)),
                            new DataColumn("ucche_soluong",typeof (double)),
                            new DataColumn("thoigian_tamdung",typeof(double)),
                            new DataColumn("solan_laplai",typeof(int))
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return datatable = new DataTable()
                {
                    Columns =
                    {
                        new DataColumn("id",typeof(int)),
                        new DataColumn("color",typeof(string)),
                        new DataColumn("tanso",typeof(double)),
                        new DataColumn("nangluong",typeof(double)),
                        new DataColumn("step_size",typeof(double)),
                        new DataColumn("dotre_trunggian",typeof(double)),
                        new DataColumn("dotre_tat",typeof(double)),
                        new DataColumn("delay",typeof(double)),
                        new DataColumn("ucche_nangluong",typeof(double)),
                        new DataColumn("ucche_soluong",typeof (double)),
                        new DataColumn("thoigian_tamdung",typeof(double)),
                        new DataColumn("solan_laplai",typeof(int))
                    }
                };
                
                
            }
            finally 
            {
                connection.Close();
            }
        }
        public static void UpdateLazerconfig(List<Rowobject> rowobjects) 
        {
            MySqlConnection connection = new MySqlConnection(Str_connection);
            try
            {
                if(connection.State == ConnectionState.Closed) 
                {
                    connection.Open();
                    foreach(Rowobject rowobject in rowobjects) 
                    {
                        string query = $"UPDATE {lazer_tbl} SET color = @Color , " +
                            $"tanso = @Tanso , " +
                            $"nangluong = @Nangluong , " +
                            $"step_size = @Step_size , " +
                            $"dotre_trunggian = @Dotre_trunggian , " +
                            $"dotre_tat = @Dotre_tat , " +
                            $"delay = @Delay , " +
                            $"ucche_nangluong = @Ucche_nangluong , " +
                            $"ucche_soluong = @Ucche_soluong , " +
                            $"thoigian_tamdung = @Thoigian_tamdung , " +
                            $"solan_laplai = @Solan_laplai " +
                            $"WHERE id = @Id";
                        using (MySqlCommand command = new MySqlCommand(query, connection)) 
                        {
                            command.Parameters.AddWithValue("@Color", rowobject.color);
                            command.Parameters.AddWithValue("@Tanso", rowobject.tanso);
                            command.Parameters.AddWithValue("@Nangluong", rowobject.nangluong);
                            command.Parameters.AddWithValue("@Step_size", rowobject.step_size);
                            command.Parameters.AddWithValue("@Dotre_trunggian", rowobject.dotre_trunggian);
                            command.Parameters.AddWithValue("@Dotre_tat", rowobject.dotre_tat);
                            command.Parameters.AddWithValue("@Delay", rowobject.delay);
                            command.Parameters.AddWithValue("@Ucche_nangluong", rowobject.ucche_nangluong);
                            command.Parameters.AddWithValue("@Ucche_soluong", rowobject.ucche_soluong);
                            command.Parameters.AddWithValue("@Thoigian_tamdung", rowobject.thoigian_tamdung);
                            command.Parameters.AddWithValue("@Solan_laplai", rowobject.solan_laplai);
                            command.Parameters.AddWithValue("@Id", rowobject.id);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally 
            {
                connection.Close();
            }
        }
        #endregion

        #region Log Runtime machine status
        /// <summary>
        /// return (Stop, Running, Pause, Actual, Require) Unit:Second
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static (double,double,double,double,double) Get_status_runtime() 
        {

            MySqlConnection connection = new MySqlConnection(Str_connection);
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();

                    // string query = 
                    // $"SELECT "+
                    // "SUM(TIMESTAMPDIFF(SECOND,first_time,last_time)) AS total_pending_time "+
                    // $"FROM {Machine_rumtime_tbl} "+
                    // "WHERE status = 1";

                    string query = @"
                        SELECT 
                            SUM(CASE WHEN status = 0 THEN TIMESTAMPDIFF(SECOND, first_time, last_time) ELSE 0 END) AS total_time_status_0,
                            SUM(CASE WHEN status = 1 THEN TIMESTAMPDIFF(SECOND, first_time, last_time) ELSE 0 END) AS total_time_status_1,
                            SUM(CASE WHEN status = 2 THEN TIMESTAMPDIFF(SECOND, first_time, last_time) ELSE 0 END) AS total_time_status_2,
                            SUM(quantity_actual) AS actual,
                            AVG(quantity_require) AS average_require
                        FROM 
                            machine_data_runtime
                        WHERE 
                        status IN (0, 1, 2);";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int totalTimeStatus0Seconds = Convert.ToInt32(reader["total_time_status_0"]);
                                int totalTimeStatus1Seconds = Convert.ToInt32(reader["total_time_status_1"]);
                                int totalTimeStatus2Seconds = Convert.ToInt32(reader["total_time_status_2"]);
                                int actual = Convert.ToInt32(reader["actual"]);
                                double require = Convert.ToDouble(reader["average_require"]);
                                TimeSpan totalTimeStatus0 = TimeSpan.FromSeconds(totalTimeStatus0Seconds);
                                TimeSpan totalTimeStatus1 = TimeSpan.FromSeconds(totalTimeStatus1Seconds);
                                TimeSpan totalTimeStatus2 = TimeSpan.FromSeconds(totalTimeStatus2Seconds);

                                Debug.WriteLine($"Total time in status 0: {totalTimeStatus0}");
                                Debug.WriteLine($"Total time in status 1: {totalTimeStatus1}");
                                Debug.WriteLine($"Total time in status 2: {totalTimeStatus2}");
                                return (totalTimeStatus0Seconds,totalTimeStatus1Seconds,totalTimeStatus2Seconds,actual,require);
                            }
                            else 
                            {
                                return (0, 0, 0, 0, 0);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Error Connect");
                    return (0, 0, 0, 0, 0);
                }
            }
            catch (Exception ex)
            {
                return (0, 0, 0, 0, 0);
            }
            finally
            {
                connection.Close();
            }
        }


        public static void Write_Log(int last_status,int current_status, ref DateTime last_time)
        {
            MySqlConnection connection = new MySqlConnection(Str_connection);
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    string query = $"INSERT INTO {Machine_rumtime_tbl} " +
                    "(machine_id, first_time, last_time, status, quantity_actual, quantity_require, mo_code, op_id) VALUES " +
                    "(@Machine_id, @First_time, @Last_time, @Status, @Quantity_actual, @Quantity_require, @Mo_code, @Op_id)";
                    if (last_status == current_status)
                    {
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Machine_id", "STT1");
                            command.Parameters.AddWithValue("@First_time", last_time);
                            command.Parameters.AddWithValue("@Last_time", DateTime.Now);
                            command.Parameters.AddWithValue("@Status", current_status);
                            command.Parameters.AddWithValue("@Quantity_require", 100);
                            command.Parameters.AddWithValue("@Quantity_actual", 5);
                            command.Parameters.AddWithValue("@Mo_code", "MO105686");
                            command.Parameters.AddWithValue("@Op_id", "ABCD1244");
                            command.ExecuteNonQuery();
                        }
                        last_time = DateTime.Now;
                        last_status = current_status;
                    }
                    else
                    {
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Machine_id", "STT1");
                            command.Parameters.AddWithValue("@First_time", last_time);
                            command.Parameters.AddWithValue("@Last_time", DateTime.Now);
                            command.Parameters.AddWithValue("@Status", last_status);
                            command.Parameters.AddWithValue("@Quantity_require", 100);
                            command.Parameters.AddWithValue("@Quantity_actual", 5);
                            command.Parameters.AddWithValue("@Mo_code", "MO105686");
                            command.Parameters.AddWithValue("@Op_id", "ABCD1244");
                            command.ExecuteNonQuery();
                        }
                        last_time = DateTime.Now;
                        last_status = current_status;
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion
    }
}
