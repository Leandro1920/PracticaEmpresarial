using Exceptionless.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Core.utils
{
    public class Misc_functions
    {
        DataDictionary diction;
        public DataSet dataAux;
        public static List<int> ConfUserPermissions = new List<int>();

        public static Boolean isServer()
        {
            Boolean ProgramaEjecutado = false;

            try
            {
                ServiceController controller = new ServiceController("MySQL57");
                var status = controller.Status;
                ProgramaEjecutado = true;
            }
            catch (Exception ex)
            {
            }

            try
            {
                ServiceController controller = new ServiceController("MySQL80");
                var status = controller.Status;
                ProgramaEjecutado = true;
            }
            catch (Exception ex)
            {
            }

            if (Process.GetProcessesByName("mysqld").GetUpperBound(0) > 0)
            {
                ProgramaEjecutado = true;
            }


            return ProgramaEjecutado;

        }
    }
}
