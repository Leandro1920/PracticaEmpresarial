using Core.utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Windows;
using System.Threading;
using MySqlHelper = MySqlConnector.MySqlHelper;
using System.Windows.Forms;

namespace Core.modules
{
    public class User
    {
        DataDictionary diction;
        DataDictionary userDataAux;
        public DataSet dataAux;
        public static DataDictionary loggedUser; // guarda los datos del usuario cuando accede
        public static List<DataDictionary> namePermissions; // guarda el nombre de los permisos que tiene ese usuario

        public Boolean verifyAdministrators(int idUser)
        {

            diction = null;

            String sql = "SELECT id_user FROM tbl_users WHERE id_role = '1' and id_user = '" + idUser + "'";

            diction = MysqlDB.fetchDataRow(sql);

            if (diction.Count > 0)
            {

                sql = "SELECT id_role FROM tbl_users WHERE id_role = '1' and id_user <> '" + idUser + "'";
                sql += "AND user_enabled  = 1 ";
                sql += "AND user_deleted  = 0 ";

                diction = MysqlDB.fetchDataRow(sql);
                if (diction.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }

        }

        public Boolean processUserInformation(int idRole, string Username, string userFullName, string passEncrypt)
        {
            try
            {
                String sql = "Insert into tbl_users (id_role,user_username,user_password,user_full_name)Values ";
                sql += "('" + idRole + "',";
                sql += "'" + MySqlHelper.EscapeString(Username) + "',";
                sql += "'" + MySqlHelper.EscapeString(passEncrypt) + "',";
                sql += "'" + MySqlHelper.EscapeString(userFullName) + "')";

                MysqlDB.Execute(sql);
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("El nombre de usuario ya existe.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

        }

        public void deleteUserInformation(int idUser)
        {

            if (verifyAdministrators(idUser))
            {
                try
                {
                    String sql = "Update tbl_users Set user_deleted = 1";
                    sql += " where id_user = '" + idUser + "'";

                    MysqlDB.Execute(sql);
                }
                catch (Exception)
                {

                }
            }
            else
            {
                MessageBox.Show("No se puede eliminar al último administrador del sistema.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        public void editUserInformation(int idUser, int idRole, string userFullName, string passEncrypt)
        {
            try
            {

                String sql = "Update tbl_users Set ";
                sql += "id_role ='" + idRole + "',";
                sql += "user_full_name = '" + MySqlHelper.EscapeString(userFullName) + "',";
                sql += "user_password = '" + MySqlHelper.EscapeString(passEncrypt) + "'";
                sql += " where id_user = '" + idUser + "'";

                MysqlDB.Execute(sql);
                MessageBox.Show("Se ha editado el usuario.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception)
            {

            }

        }

        public void editUserInformation(int idUser, string idRole, string userFullName)
        {
            try
            {

                String sql = "Update tbl_users Set ";
                sql += "id_role ='" + idRole + "',";
                sql += "user_full_name = '" + MySqlHelper.EscapeString(userFullName) + "'";
                sql += " where id_user = '" + idUser + "'";

                MysqlDB.Execute(sql);
                MessageBox.Show("Se ha editado el usuario.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception)
            {

            }

        }

        public void processKeyRest(int idUser, string passEncrypt)
        {
            try
            {
                String sql = "Update tbl_users Set ";
                sql += "user_password = '" + MySqlHelper.EscapeString(passEncrypt) + "',";
                sql += "user_temp_pwd = ''";
                sql += " where id_user = '" + idUser + "'";

                MysqlDB.Execute(sql);
            }
            catch (Exception)
            {

            }

        }

        public Boolean updateLoggedUser()
        {

            String sql = "SELECT id_user,id_roleuser_username,user_full_name,user_password,user_error_count,user_last_login,user_temp_pwd,user_locked,user_enabled,user_deleted " +
                " FROM tbl_users ";
            sql += "WHERE user_username = '" + loggedUser.Item("user_username") + "'";
            sql += "AND user_deleted  = 0 ";
            sql += "AND user_enabled  = 1 ";

            try
            {
                loggedUser = MysqlDB.fetchDataRow(sql);

                if (loggedUser.Count > 0)
                {
                    return true;
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
        }

        public Boolean unlockUser(int idUser)
        {
            try
            {
                String sql = "Update tbl_users Set ";
                sql += "user_locked = '0',";
                sql += "user_error_count = '0'";
                sql += " where id_user = '" + idUser + "'";

                MysqlDB.Execute(sql);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public Boolean changeTemporaryPassword(int idUser, string passEncrypt)
        {
            try
            {
                String sql = "Update tbl_users Set ";

                sql += "user_temp_pwd = '" + MySqlHelper.EscapeString(passEncrypt) + "'";
                sql += " where id_user = '" + idUser + "'";


                MysqlDB.Execute(sql);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public Boolean checkIfTheUserIsBlocked(int idUser)
        {
            try
            {
                String sql = "SELECT * FROM tbl_users ";
                sql += "WHERE id_user = '" + idUser + "' ";
                sql += "AND user_locked = 1";

                if (MysqlDB.fetchDataRow(sql).Count > 0)
                {
                    return true;

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


        }

        public void changeUserStatus(int idUser, int status)
        {

            String sql = "SELECT user_locked FROM tbl_users ";
            sql += "WHERE user_locked = '0'";
            sql += " AND id_user <> '" + idUser + "'";
            sql += "AND user_deleted  = 0 ";

            if (MysqlDB.fetchDataRow(sql).Count > 0)
            {
                try
                {
                    sql = "Update tbl_users Set ";
                    sql += "user_enabled = '" + status + "'";
                    sql += " where id_user = '" + idUser + "'";
                    sql += "AND user_deleted  = 0 ";

                    MysqlDB.Execute(sql);
                }
                catch (Exception)
                {

                }

            }
            else
            {
                MessageBox.Show("No se puede desactivar, es el unico activo");
            }

        }

        public DataSet listOfExistingUserNames()
        {
            dataAux = null;

            String Sql = "SELECT id_user, user_full_name FROM tbl_users";
            Sql += " WHERE user_deleted = '0' ";

            dataAux = MysqlDB.fetchDataSet(Sql);
            return dataAux;
        }

        public DataSet listOfExistingUsers()
        {
            dataAux = null;

            String Sql = "SELECT id_user,user_full_name,role_name,user_username,if (user_locked,'Si','No'),if(user_enabled,'Activo','Inactivo') FROM tbl_users";
            Sql += " INNER JOIN tbl_roles AS roles on tbl_users.id_role = roles.id_role";
            Sql += " WHERE user_deleted = '0'";

            dataAux = MysqlDB.fetchDataSet(Sql);
            return dataAux;
        }

        public DataSet listOfExistingUsers(string user)
        {
            dataAux = null;

            String Sql = "SELECT u.id_user,roles.role_name,u.user_username FROM tbl_users u";
            Sql += " LEFT JOIN tbl_roles AS roles on u.id_role = roles.id_role";
            Sql += " WHERE user_deleted = '0'";
            Sql += " AND user_username Like '%" + MySqlHelper.EscapeString(user) + "%'";

            dataAux = MysqlDB.fetchDataSet(Sql);
            return dataAux;
        }

        public DataDictionary getUserInformation(int idRole)
        {

            String Sql = "SELECT module_name FROM tbl_users WHERE id_module = " + idRole;

            diction = MysqlDB.fetchDataRow(Sql);

            return diction;
        }

        public DataDictionary userStatus(int idUser)
        {

            String Sql = "SELECT user_enabled FROM tbl_users WHERE id_user = " + idUser;

            diction = MysqlDB.fetchDataRow(Sql);

            return diction;
        }

        public Boolean userExists(string username)
        {

            String Sql = "SELECT id_user FROM tbl_users WHERE user_username = '" + MySqlHelper.EscapeString(username) + "' ";
            Sql += "AND user_deleted = '0'";
            diction = MysqlDB.fetchDataRow(Sql);

            if (diction.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public Boolean keyChange(int idUser, string passEncrypt)
        {

            try
            {
                String sql = "Update tbl_users Set ";
                sql += "user_password ='" + MySqlHelper.EscapeString(passEncrypt) + "' ";
                sql += "where id_user = '" + idUser + "' ";

                MysqlDB.Execute(sql);

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public Boolean changeUserStatusbtnName(int idUser)
        {
            try
            {
                String Sql = "SELECT user_enabled FROM tbl_users WHERE id_user = '" + idUser + "' ";
                diction = MysqlDB.fetchDataRow(Sql);

                if (diction.Item("user_enabled").Equals("1"))
                {
                    return true;
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

        }

        ////    login

        public Boolean verifyAccess(string pUsername, string pPassEncrypt)
        {

            string username = MySqlHelper.EscapeString(pUsername);
            string passEncrypt = MySqlHelper.EscapeString(pPassEncrypt);


            try
            {
                if (userInformationTryToLogion(username))
                {
                    verifyPassword(username, passEncrypt);

                    if (statusOfTheLoginAttempt(username))
                    {

                        userDataAux = null;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Datos incorrectos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        userDataAux = null;
                        loggedUser = null;
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("El usuario está bloqueado, inactivo o no existe.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hubo un error intentelo nuevamente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        public Boolean userInformationTryToLogion(string username)
        {

            String sql = "SELECT * FROM tbl_users ";
            sql += "WHERE user_username = '" + MySqlHelper.EscapeString(username) + "' ";
            sql += "AND user_locked  = 0 ";
            sql += "AND user_enabled  = 1 ";
            sql += "AND user_deleted  = 0 ";

            loggedUser = MysqlDB.fetchDataRow(sql);

            if (loggedUser.Count == 0)
            {
                return false;
            }

            return true;
        }

        public void verifyPassword(string pUsername, string pPassEncrypt)
        {

            string username = MySqlHelper.EscapeString(pUsername);
            string passEncrypt = MySqlHelper.EscapeString(pPassEncrypt);


            String sql = "SELECT * FROM tbl_users ";
            sql += "WHERE user_username = '" + username + "' ";
            sql += "AND user_locked  = 0 ";
            sql += "AND user_enabled  = 1 ";
            sql += "AND user_deleted  = 0 ";


            if (loggedUser.Item("user_temp_pwd").Equals(""))
            {
                sql += "AND user_password  = '" + passEncrypt + "' ";
            }
            else
            {
                sql += "AND user_temp_pwd  = '" + passEncrypt + "' ";
            }

            userDataAux = MysqlDB.fetchDataRow(sql);
        }

        public Boolean statusOfTheLoginAttempt(string pUsername)
        {
            string sql;
            string username = MySqlHelper.EscapeString(pUsername);

            if (userDataAux.Count > 0)
            {

                sql = "UPDATE tbl_users SET user_last_login = Now() ";
                sql += "WHERE user_username = '" + username + "' ";
                sql += "AND user_locked  = 0 ";
                sql += "AND user_enabled  = 1 ";
                sql += "AND user_deleted  = 0 ";

                MysqlDB.Execute(sql);
                return true;
            }
            else
            {
                if (loggedUser.Item("id_role").Equals("1"))
                {
                    sql = "UPDATE tbl_users SET user_error_count = 0 ";
                    sql += "WHERE user_username = '" + username + "' ";
                    sql += "AND user_locked  = 0 ";
                    sql += "AND user_enabled  = 1 ";
                    sql += "AND user_deleted  = 0 ";

                    MysqlDB.Execute(sql);
                    return false;
                }
                else
                {
                    sql = "UPDATE tbl_users SET user_error_count = user_error_count + 1 ";
                    sql += "WHERE user_username = '" + username + "' ";
                    sql += "AND user_locked  = 0 ";
                    sql += "AND user_enabled  = 1 ";
                    sql += "AND user_deleted  = 0 ";

                    MysqlDB.Execute(sql);
                    return false;
                }

            }
        }

        public Boolean errorCountSet(string username)
        {
            string sql;

            sql = "UPDATE tbl_users SET user_error_count = '" + 0 + "' ";
            sql += "WHERE user_username = '" + MySqlHelper.EscapeString(username) + "' ";
            sql += "AND user_locked  = 0 ";
            sql += "AND user_enabled  = 1 ";
            sql += "AND user_deleted  = 0 ";

            try
            {
                MysqlDB.Execute(sql);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public List<DataDictionary> checkPermissions(int idRole)
        {
            namePermissions = null;

            String Sql = "SELECT  m.id_module, m.module_name,m.module_parent FROM tbl_module_role as mr ";
            Sql += "INNER JOIN tbl_modules as m ON mr.id_module = m.id_module ";
            Sql += "where mr.id_role = '" + idRole + "' ";
            Sql += "AND m.id_module = mr.id_module";

            namePermissions = MysqlDB.fetchData(Sql);

            return namePermissions;

        }


    }
}