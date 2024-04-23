using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

using System.Configuration;

namespace Core.utils
{
    public class MysqlDB
    {
        private static MySqlConnection dbLocal = new MySqlConnection();

        public static int last_insert_id = 0;

        private static String localConnString;

        private static MySqlTransaction sqlTran;
        private OdbcConnection dbConn;

        public static void connectDB()
        {

            localConnString = "Database=testtesis;Data Source=" + ConfigurationManager.ConnectionStrings["localhost"].ConnectionString + ";User Id=root;Password=mi2018pueblo;Convert Zero Datetime=True;Default Command Timeout=60";

            Boolean connStatus = false;

            if (dbLocal.State == ConnectionState.Open)
            {
                try
                {
                    if (dbLocal.Ping())
                    {
                        connStatus = true;
                    }
                }
                catch (Exception ex)
                {
                    connStatus = false;
                }
            }

            try
            {
                if (!connStatus)
                {

                    dbLocal.ConnectionString = localConnString;
                    dbLocal.Open();
                }

            }
            catch (Exception ex)
            {
                //if (Misc_functions.isServer())
                //{
                //    System.Windows.Forms.MessageBox.Show("Error al conectar a la Base de datos, contacte a Soporte Técnico.", "Alerta", (System.Windows.Forms.MessageBoxButtons.OK), System.Windows.Forms.MessageBoxIcon.Error);
                //    System.Diagnostics.Process.Start("https://wa.me/50685532222?text=Solicitud%20de%20informaci%C3%B3n");
                //    throw new MysqlDBException("Error al conectar a la Base de datos, contacte a Soporte Técnico." + ex);

                //}
                //else
                //{
                //    System.Windows.Forms.MessageBox.Show("Error al conectar al equipo servidor, verifique su conexión.", "Alerta", (System.Windows.Forms.MessageBoxButtons.OK), System.Windows.Forms.MessageBoxIcon.Warning);
                //    System.Diagnostics.Process.Start("https://wa.me/50685532222?text=Solicitud%20de%20informaci%C3%B3n");
                //    throw new MysqlDBException("Error al conectar al equipo servidor, verifique su conexión.");
                //}
            }

        }

        public static void closeDB()
        {
            if (dbLocal.State == ConnectionState.Open)
            {
                dbLocal.Close();
            }
        }

        public static void beginTransaction()
        {
            sqlTran = dbLocal.BeginTransaction();
        }

        public static void commitTransaction()
        {
            sqlTran.Commit();
            sqlTran.Dispose();
            sqlTran = null;
        }
            public static void rollbackTransaction()
            {
                sqlTran.Rollback();
                sqlTran.Dispose();
                sqlTran = null;
            }

            public static void Execute(string p_sql, Boolean insert = false)
            {
                connectDB();

                if (dbLocal.State == ConnectionState.Open)
                {
                    MySqlCommand cmd = new MySqlCommand(p_sql, dbLocal);
                    if (sqlTran != null)
                        cmd.Transaction = sqlTran;

                    if (insert)
                    {
                        cmd.ExecuteScalar();
                        last_insert_id = (int)cmd.LastInsertedId;
                    }
                    else
                        cmd.ExecuteNonQuery();
                }

            }

            public static DataSet fetchDataSet(string p_sql)
            {
                connectDB();
                DataSet ld_results = new DataSet();

                if (dbLocal.State == ConnectionState.Open)
                {
                    MySqlDataAdapter adp = new MySqlDataAdapter(p_sql, dbLocal);
                    adp.Fill(ld_results, "dt");
                }

                return ld_results;
            }

            public static DataDictionary fetchDataRow(string p_sql)
            {
                connectDB();
                DataDictionary result = new DataDictionary();
                if (dbLocal.State == ConnectionState.Open)
                {
                    MySqlCommand cmd = new MySqlCommand(p_sql, dbLocal);
                    if (sqlTran != null)
                        cmd.Transaction = sqlTran;
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int cont = 0;

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            String num = "";
                            if (result.ContainsKey(reader.GetName(i)))
                            {
                                cont = cont + 1;
                                num = cont.ToString();
                            }

                            if (DBNull.Value.Equals(reader.GetValue(i)))
                            {
                                result.Add(reader.GetName(i) + num, "");
                            }
                            else
                            {
                                result.Add(reader.GetName(i) + num, reader.GetValue(i).ToString());
                            }

                        }


                    }
                    reader.Close();
                }

                return result;
            }

            public static List<DataDictionary> fetchData(string p_sql)
            {
                connectDB();

                List<DataDictionary> l_results = new List<DataDictionary>();

                if (dbLocal.State == ConnectionState.Open)
                {
                    MySqlCommand cmd = new MySqlCommand(p_sql, dbLocal);
                    if (sqlTran != null)
                        cmd.Transaction = sqlTran;
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        DataDictionary result = new DataDictionary();
                        int cont = 0;

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            String num = "";
                            if (result.ContainsKey(reader.GetName(i)))
                            {
                                cont = cont + 1;
                                num = cont.ToString();
                            }

                            if (DBNull.Value.Equals(reader.GetValue(i)))
                            {
                                result.Add(reader.GetName(i) + num, "");
                            }
                            else
                            {
                                result.Add(reader.GetName(i) + num, reader.GetValue(i).ToString());
                            }

                        }

                        l_results.Add(result);

                    }
                    reader.Close();
                }

                return l_results;
            }

            public static void keepAlive()
            {
                dbLocal.Ping();
            }

        }

        [Serializable()]
        public class MysqlDBException : System.Exception
        {
            public MysqlDBException() : base() { }
            public MysqlDBException(string message) : base(message) { }
            public MysqlDBException(string message, System.Exception inner) : base(message, inner) { }

            // A constructor is needed for serialization when an
            // exception propagates from a remoting server to the client.
            protected MysqlDBException(System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }

    } 

