using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;
using SAPbouiCOM;
using System.Globalization;
using ConectorDataFast.Capa_Datos;
using System.Windows;
namespace ConectorDataFast.Procesos
{
    class EnlaceDataFast
    {
        public EnlaceDataFast()
        {
            Conexion.open();
            //Conexion.Disconect();
            Conexion.SapAplication.ItemEvent += new _IApplicationEvents_ItemEventEventHandler(SBO_Application_ItemEvent); //manda a revisar los eventos de form
            //Conexion.SapAplication.AppEvent += new _IApplicationEvents_AppEventEventHandler(SBO_Application_AppEvent); // capturo los eventos tipo Application
            //Conexion.SapAplication.FormDataEvent += new _IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);
            //Conexion.SapAplication.MenuEvent += new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler(SBO_Application_MenuEvent);

        }
        private void SBO_Application_ItemEvent(string formUID,ref ItemEvent pval, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                if(pval.FormTypeEx== "146" && pval.EventType==BoEventTypes.et_FORM_LOAD && pval.BeforeAction == false)
                {
                    Form oForm = Conexion.SapAplication.Forms.Item(formUID);
                    //Conexion.SapAplication.MessageBox("Estoy Aca");
                    AgregarBoton(oForm);
                    //EvaluaBoton(oForm,pval.Row);
                }
                //if (pval.FormTypeEx=="146" && pval.ItemUID=="112" && pval.ColUID=="42" && pval.EventType==BoEventTypes.et_VALIDATE && pval.BeforeAction == false) // valido cada cambio del campo Cuenta Mayor
                //{
                //    //if (pval.Row <= 0)
                //    //    return;
                //    if(pval.ActionSuccess)
                //    {
                //        Form oForm = Conexion.SapAplication.Forms.Item(formUID);
                //        EvaluaBoton(oForm, pval.Row);
                //    }
                   
                //}
                if(pval.FormTypeEx=="146" && pval.ItemUID=="BtnDf" && pval.EventType==BoEventTypes.et_ITEM_PRESSED && pval.BeforeAction == false)
                {
                    Form oForm = Conexion.SapAplication.Forms.Item(formUID);
                    Matrix oMatrix = (Matrix)oForm.Items.Item("112").Specific;
                    EditText txtCuenta = (EditText)oMatrix.Columns.Item("42").Cells.Item(1).Specific;
                    string valor = txtCuenta.Value.Trim();
                    if(valor == "1110300100-000-00")
                    {
                        Conexion.SapAplication.MessageBox(valor);
                    }
                    else
                    {
                        Conexion.SapAplication.MessageBox("No se ha seleccionado Codigo de Tarjeta");
                    }
                    
                    //en esta parte de aca va el codigo que enlaza con el datafast
                }
            }
            catch(Exception ex)
            {
                Conexion.SapAplication.StatusBar.SetText(ex.Message);
            }
            

        }
        private void AgregarBoton(Form oForm)
        {
            try
            {
                oForm.Freeze(true); // evita que el formulario parpadee
                try
                {
                    oForm.Items.Item("BtnDf");
                    return;
                }
                catch { }
                Item btnRef = oForm.Items.Item("2"); //selecciono el boton cancelar para tomarlo como referencia
                Item oBtnDataFast = oForm.Items.Add("BtnDf", BoFormItemTypes.it_BUTTON); //creo el nuevo boton
                oBtnDataFast.Top = btnRef.Top;
                oBtnDataFast.Left = btnRef.Left + btnRef.Width + 10;
                oBtnDataFast.Width = 100;
                oBtnDataFast.Height = btnRef.Height;
                Button oBotton = (Button)oBtnDataFast.Specific;
                oBotton.Caption = "Activa DataFast";
                //oBtnDataFast.Enabled = false;
                
                
            }
            catch(Exception ex)
            {
                oForm.Freeze(false);
                Conexion.SapAplication.StatusBar.SetText(ex.Message)
;            }
            
        }
        private void EvaluaBoton(Form oForm, int row)
        {
            try
            {
                if (row <= 0) return;
                Matrix oMatrix = (Matrix)oForm.Items.Item("112").Specific;
                if (row > oMatrix.RowCount) return;
                EditText txtCuenta = (EditText)oMatrix.Columns.Item("42").Cells.Item(row).Specific;
                string valor = txtCuenta.Value.Trim();
                Item ObtnItem = oForm.Items.Item("BtnDf");
                ObtnItem.Enabled = (valor == "1110300100-000-00");
                //if (valor == "1110300100-000-00")
                //{
                //    ObtnItem.Enabled = true;
                //}
                //else
                //{
                //    ObtnItem.Enabled = false;
                //}
            }
            catch(Exception ex)
            {
                Conexion.SapAplication.StatusBar.SetText(ex.Message);
            }
        }
    }
    
       

    
}
