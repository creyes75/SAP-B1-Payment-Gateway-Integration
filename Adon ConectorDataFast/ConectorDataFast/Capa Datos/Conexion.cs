using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;
using SAPbouiCOM;
using System.Windows;

namespace ConectorDataFast.Capa_Datos
{
    class Conexion
    {
        public static SAPbobsCOM.Company oCompany;
        public static SAPbouiCOM.Application SapAplication;
        public static bool open()
        {
            bool result = false;
            try
            {
                SAPbouiCOM.SboGuiApi sboGuiApi = new SAPbouiCOM.SboGuiApi();
                sboGuiApi.Connect(Environment.GetCommandLineArgs().GetValue(1).ToString());
                SapAplication = sboGuiApi.GetApplication();
                sboGuiApi = null;
                oCompany = (SAPbobsCOM.Company)SapAplication.Company.GetDICompany();
                if (oCompany.Connected)
                {
                    result = true;
                    SapAplication.StatusBar.SetText("Conector DataFast Activo", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Success);
                }
                else
                {
                    SapAplication.StatusBar.SetText("Error al conectar Conector DataDast", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                }
                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        public static void Disconect()
        {
            if (oCompany != null && oCompany.Connected)
            {
                oCompany.Disconnect();
                // Liberar el objeto de la memoria COM de Windows
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oCompany);
                oCompany = null;
                GC.Collect(); // Forzar recolección de basura
            }
        }
    }
}
