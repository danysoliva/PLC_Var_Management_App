using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using S7.Net;
using PLC_Var_Management_App.Classes;
using System.Data.SqlClient;

namespace PLC_Var_Management_App
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        #region Developer Defined Variables

        DataOperations dp = new DataOperations();
        DateTime log_date;

        #region PLC319

        Plc plc319;
        static CpuType plc319_CPUType = CpuType.S7300;
        static string plc319_IPAddress = "192.168.10.2";
        static Int16 plc319_Rack = 0;
        static Int16 plc319_Slot = 2;

        #endregion

        #region PLC315

        Plc plc315;
        static CpuType plc315_CPUType = CpuType.S7300;
        static string plc315_IPAddress = "192.168.10.3";
        static Int16 plc315_Rack = 0;
        static Int16 plc315_Slot = 2;

        string DoneMix1 = "DB509.DBX0.0";
        string DoneMix2 = "DB509.DBX0.1";

        string DoneTanqUp1 = "DB520.DBX12.0";
        string DoneTanqUp2 = "DB520.DBX26.0";
        string DoneTanqUp3 = "DB520.DBX40.0";
        string DoneTanqUp4 = "DB541.DBX4.0";

        string AlarmaMicro = "DB527.DBX0.0";

        Int64 Batch_idOrden = 0;
        string Batch_fullCode = "";
        int Batch_nBatchActual = 0;
        Int64 Batch_IdMix = 0;
        int Batch_Especie = 0;
        #endregion


        #endregion

        #region Developer Defined Methods

        private void Connect_PLC()
        {
            try
            {
                if (!plc319.IsConnected)
                {
                    plc319.Open();
                    txt_Status317.Text = "Connection Stablished";
                    txt_Status317.ForeColor = System.Drawing.Color.Green;
                }

                if (!plc315.IsConnected)
                {
                    plc315.Open();
                    txt_Status315.Text = "Connection Stablished";
                    txt_Status315.ForeColor = System.Drawing.Color.Green;
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Ubicacion error: Connect_PLC(); Detalle del Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Connect_PLC();
            }
        }

        private void Disconect_PLC()
        {
            try
            {
                if (plc319.IsConnected)
                {
                    plc319.Close();
                    txt_Status317.Text = "Disconnected";
                    txt_Status317.ForeColor = System.Drawing.Color.OrangeRed;
                }

                if (plc315.IsConnected)
                {
                    plc315.Close();
                    txt_Status315.Text = "Disconnected";
                    txt_Status315.ForeColor = System.Drawing.Color.OrangeRed;
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Detalle del Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Check_PLC_Availability()
        {
            try
            {
                if (plc319.IsAvailable)
                {
                    txt_Status317.Text = "Available";
                    txt_Status317.ForeColor = System.Drawing.Color.ForestGreen;
                }
                else
                {
                    txt_Status317.Text = "Unavailable";
                    txt_Status317.ForeColor = System.Drawing.Color.Red;
                }

                if (plc315.IsAvailable)
                {
                    txt_Status315.Text = "Available";
                    txt_Status315.ForeColor = System.Drawing.Color.ForestGreen;
                }
                else
                {
                    txt_Status315.Text = "Unavailable";
                    txt_Status315.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Detalle del Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Check_PLC_Connectivity()
        {
            try
            {
                if (plc319.IsConnected)
                {
                    txt_Status317.Text = "Connection Stablished";
                    txt_Status317.ForeColor = System.Drawing.Color.ForestGreen;
                }
                else
                {
                    txt_Status317.Text = "Disconnected";
                    txt_Status317.ForeColor = System.Drawing.Color.OrangeRed;
                }

                if (plc315.IsConnected)
                {
                    txt_Status315.Text = "Connection Stablished";
                    txt_Status315.ForeColor = System.Drawing.Color.ForestGreen;
                }
                else
                {
                    txt_Status315.Text = "Disconnected";
                    txt_Status315.ForeColor = System.Drawing.Color.OrangeRed;
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Detalle del Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Load_Log() 
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Detalle del Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void set_log_entry(string writen_plc, string writen_db, string writen_var, string writen_value, string process_index, string var_description, string custom_data_1, string custom_data_2, string custom_data_3, string custom_data_4, string custom_data_5) 
        {
            try
            {
                #region Parametros_SP_Entrada
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@writen_plc", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@writen_db", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@writen_var", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@writen_value", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@process_index", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@var_description", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@custom_data_1", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@custom_data_2", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@custom_data_3", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@custom_data_4", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@custom_data_5", SqlDbType.VarChar));

                cmd.Parameters["@writen_plc"].Value = writen_plc;
                cmd.Parameters["@writen_db"].Value = writen_db;
                cmd.Parameters["@writen_var"].Value = writen_var;
                cmd.Parameters["@writen_value"].Value = writen_value;
                cmd.Parameters["@process_index"].Value = process_index;
                cmd.Parameters["@var_description"].Value = var_description;
                cmd.Parameters["@custom_data_1"].Value = custom_data_1;
                cmd.Parameters["@custom_data_2"].Value = custom_data_2;
                cmd.Parameters["@custom_data_3"].Value = custom_data_3;
                cmd.Parameters["@custom_data_4"].Value = custom_data_4;
                cmd.Parameters["@custom_data_5"].Value = custom_data_5;
                #endregion

                dp.APMS_Exec_SP("SYS_Var_Write_Log_Entry", cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //MessageBox.Show("Detalle del Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Constructors

        public Form1()
        {
            InitializeComponent();
        }

        #endregion

        #region Form Events

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                plc319 = new Plc(plc319_CPUType, plc319_IPAddress, plc319_Rack, plc319_Slot);
                txt_Address317.Text = plc319_IPAddress;
                txt_rack317.Text = plc319_Rack.ToString();
                txt_Slot317.Text = plc319_Slot.ToString();

                plc315 = new Plc(plc315_CPUType, plc315_IPAddress, plc315_Rack, plc315_Slot);
                txt_Address315.Text = plc315_IPAddress;
                txt_Rack315.Text = plc315_Rack.ToString();
                txt_Slot315.Text = plc315_Slot.ToString();

                Check_PLC_Availability();

                txt_ServiceStatus.Caption = "Service Stopped";
                txt_ServiceStatus.Appearance.ForeColor = System.Drawing.Color.Red;
                btn_Stop_Service.Enabled = false;

                //conteoBatch.Start()

                log_date = Convert.ToDateTime(dp.APMS_GetSelectData(@"SELECT SYSDATETIME()").Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                //SEND E-MAIL Message on Service Failure.
                //MessageBox.Show("Detalle del Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Start_Service_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                MainTimer.Enabled = true;
                MainTimer.Start();

                VarReaderMonitor.Enabled = true;
                VarReaderMonitor.Start();

                Connect_PLC();
                txt_ServiceStatus.Caption = "Service Started";
                txt_ServiceStatus.Appearance.ForeColor = System.Drawing.Color.Green;

                btn_Start_Service.Enabled = false;
                btn_Stop_Service.Enabled = true;
                conteoBatch.Enabled = true;
                conteoBatch.Start();

                timerHorasMaquina.Enabled = true;
                timerHorasMaquina.Start();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Detalle del Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Stop_Service_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                MainTimer.Enabled = false;
                MainTimer.Stop();

                VarReaderMonitor.Enabled = false;
                VarReaderMonitor.Stop();
                conteoBatch.Stop();
                
                Disconect_PLC();
                txt_ServiceStatus.Caption = "Service Stopped";
                txt_ServiceStatus.Appearance.ForeColor = System.Drawing.Color.Red;

                btn_Start_Service.Enabled = true;
                btn_Stop_Service.Enabled = false;
                conteoBatch.Enabled = false;

                timerHorasMaquina.Enabled = false;
                timerHorasMaquina.Stop();

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Detalle del Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //
        private void btn_Test1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                short num = 1;
                plc319.Write("DB438.DBW112.0", num);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Detalle del Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Test2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                short num = 2;
                plc319.Write("DB438.DBW112.0", num);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Detalle del Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (plc319.IsAvailable && plc315.IsAvailable) 
                {
                    if (!plc319.IsConnected || !plc315.IsConnected)
                        Connect_PLC();


                    //Escribir los valores de FG1 y FG2
                    Funciones fn = new Funciones();
                    int id_rm_fg1 = fn.GetRM_ID_APMS_from_Bin_id(2);//DB551.DBW0;
                    int id_rm_fg2 = fn.GetRM_ID_APMS_from_Bin_id(3);//DB551.DBW2;

                    //Escribir en el plc los id de materia prima que estan en los bin
                    plc319.Write("DB551.DBW0", id_rm_fg1);//Bin FG1
                    plc319.Write("DB551.DBW2", id_rm_fg2);//Bin FG2

                    //Escribir si la formula incluye lecitina
                    if(fn.GetInclusionLecitinaEnFormula())
                        plc319.Write("DB488.DBX0.5", 1);//Indica que se aplica lecitina
                    else
                        plc319.Write("DB488.DBX0.5", 0);//Indica que No se aplica lecitina


                    //Escribir los nombre en pantalla de alimentacion
                    string db = "DB507.DBX";//Nombre de la DB que es fijo, solo cambia el bit del offset
                    int Multiplicador = 0;//Valor acumulado para formar el DB en el PLC
                    
                    int bin_idx = 4;//id de la base de datos
                    //Los id estan ordenados de forma ascendente de la fd1 = 4, fd2 = 5 ..... hasta fd12 = 15 
                    //Incrementaremos de 1 en 1 
                    
                    while (Multiplicador <= 132)//Es la coleccion proporcionada de PLC. Mantiene una secuencia de 12 char en el db por lo que concatenamos el nombre del db sumando +12
                    {
                        string name_db = db + Multiplicador;//creamos nuestro nombre de DB
                        Multiplicador += 12;//Incrementamos las 12 unidades para el sigueinte db
                        
                        //Get Nambe value
                        string name = fn.GetRM_ShortName_APMS_from_Bin_id(bin_idx);//Obtenemos de la base de datos el nombre corto del material en tolva.

                        if (name.Length >= 11)
                        {
                            name = name.Substring(0, 10);
                        }
                        plc319.Write(name_db, name);//Escribimos el string en el PLC319
                        bin_idx += 1;
                    }

                    //plc319.Write("DB576.DBX0","Cloruro de Coli");

                    //Vamos a leer el done de couter 1
                    //bool DoneCouter1 = plc319.Read("DB524.dbx");


                    int is_something_to_do = int.Parse(dp.APMS_GetSelectData(@"SELECT COUNT([task_id]) FROM [APMS].[dbo].[SYS_PLC_Var_Write_Queue]").Tables[0].Rows[0][0].ToString());

                    if (is_something_to_do > 0)//Si hay una variable para grabar
                    {
                        DataTable data = dp.APMS_Exec_SP_Get_Data("SYS_Var_Write_Queue", new SqlCommand());

                        foreach (DataRow row in data.Rows)
                        {
                            #region PLC_317 Validations

                            if (row["plc_name"].ToString() == "PLC_317")
                            {
                                if (row["plc_var_type"].ToString() == "INT" || row["plc_var_type"].ToString() == "WORD")
                                {
                                    short value = (short)int.Parse(row["plc_var_value_to_set"].ToString());
                                    plc319.Write(row["plc_var_full_name"].ToString(), value);

                                    SqlCommand command = new SqlCommand();
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@task_id", int.Parse(row["task_id"].ToString()));

                                    dp.APMS_Exec_SP("SYS_Var_Write_Delete_Record", command);
                                }
                                else if (row["plc_var_type"].ToString() == "DWORD")
                                {
                                    int value = int.Parse(row["plc_var_value_to_set"].ToString());
                                    plc319.Write(row["plc_var_full_name"].ToString(), value);

                                    SqlCommand command = new SqlCommand();
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@task_id", int.Parse(row["task_id"].ToString()));

                                    dp.APMS_Exec_SP("SYS_Var_Write_Delete_Record", command);
                                }
                                else if (row["plc_var_type"].ToString() == "REAL")
                                {
                                    double value = Convert.ToDouble(row["plc_var_value_to_set"].ToString());
                                    plc319.Write(row["plc_var_full_name"].ToString(), value);

                                    SqlCommand command = new SqlCommand();
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@task_id", int.Parse(row["task_id"].ToString()));

                                    dp.APMS_Exec_SP("SYS_Var_Write_Delete_Record", command);
                                }
                                else if (row["plc_var_type"].ToString() == "BOOL")
                                {
                                    bool value = Convert.ToBoolean(row["plc_var_value_to_set"].ToString());
                                    plc319.Write(row["plc_var_full_name"].ToString(), value);

                                    SqlCommand command = new SqlCommand();
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@task_id", int.Parse(row["task_id"].ToString()));

                                    dp.APMS_Exec_SP("SYS_Var_Write_Delete_Record", command);
                                }
                            }
                            #endregion

                            #region PLC_315 Validations

                            else if (row["plc_name"].ToString() == "PLC_315")
                            {
                                if (row["plc_var_type"].ToString() == "INT" || row["plc_var_type"].ToString() == "WORD")
                                {
                                    short value = (short)int.Parse(row["plc_var_value_to_set"].ToString());
                                    plc315.Write(row["plc_var_full_name"].ToString(), value);

                                    SqlCommand command = new SqlCommand();
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@task_id", int.Parse(row["task_id"].ToString()));

                                    dp.APMS_Exec_SP("SYS_Var_Write_Delete_Record", command);
                                }
                                else if (row["plc_var_type"].ToString() == "DWORD")
                                {
                                    int value = int.Parse(row["plc_var_value_to_set"].ToString());
                                    plc315.Write(row["plc_var_full_name"].ToString(), value);

                                    SqlCommand command = new SqlCommand();
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@task_id", int.Parse(row["task_id"].ToString()));

                                    dp.APMS_Exec_SP("SYS_Var_Write_Delete_Record", command);
                                }
                                else if (row["plc_var_type"].ToString() == "REAL")
                                {
                                    double value = Convert.ToDouble(row["plc_var_value_to_set"].ToString());
                                    plc315.Write(row["plc_var_full_name"].ToString(), value);

                                    SqlCommand command = new SqlCommand();
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@task_id", int.Parse(row["task_id"].ToString()));

                                    dp.APMS_Exec_SP("SYS_Var_Write_Delete_Record", command);
                                }
                                else if (row["plc_var_type"].ToString() == "BOOL")
                                {
                                    bool value = Convert.ToBoolean(row["plc_var_value_to_set"].ToString());
                                    plc315.Write(row["plc_var_full_name"].ToString(), value);

                                    SqlCommand command = new SqlCommand();
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@task_id", int.Parse(row["task_id"].ToString()));

                                    dp.APMS_Exec_SP("SYS_Var_Write_Delete_Record", command);
                                }
                            }

                            #endregion

                            set_log_entry(row["plc_name"].ToString(), row["plc_db_name"].ToString(), row["plc_var_name"].ToString(), row["plc_var_value_to_set"].ToString(), row["process_id"].ToString(), "", row["custom_data_1"].ToString(), row["custom_data_2"].ToString(), row["custom_data_3"].ToString(), row["custom_data_4"].ToString(), row["custom_data_5"].ToString());
                        }
                    }
                }
            }
            catch //(Exception ex)
            {
                //MessageBox.Show("Detalle del Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Connect_PLC();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Disconect_PLC();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Detalle del Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_hide_to_tray_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AppNotify.Visible = true;
            this.Hide();
        }

        private void AppNotify_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            AppNotify.Visible = false;
        }

        private void VarReaderMonitor_Tick(object sender, EventArgs e)
        {
            try
            {
                if (plc319.IsAvailable) 
                {
                    if (!plc319.IsConnected || !plc315.IsConnected)
                        Connect_PLC();

                    bool check_mix1 = (bool)plc319.Read("DB438.DBX116.0");
                    bool check_mix2 = (bool)plc319.Read("DB438.DBX116.1");

                    if (check_mix1 || check_mix2)
                    {
                        if (check_mix1)
                        {
                            double moisture_read = ((uint)plc319.Read("DB438.DBD118.0")).ConvertToDouble();

                            bool value = Convert.ToBoolean(0);
                            plc319.Write("DB438.DBX116.0", value);

                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@moisture", SqlDbType.Decimal));
                            cmd.Parameters.Add(new SqlParameter("@var_plc", SqlDbType.VarChar));

                            cmd.Parameters["@moisture"].Value = moisture_read;
                            cmd.Parameters["@var_plc"].Value = "DBX116.0";

                            dp.APMS_Exec_SP("SYS_MON_Set_Moisture_Check", cmd);
                        }
                        else if (check_mix2)
                        {
                            double moisture_read = ((uint)plc319.Read("DB438.DBD122.0")).ConvertToDouble();

                            bool value = Convert.ToBoolean(0);
                            plc319.Write("DB438.DBX116.1", value);

                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@moisture", SqlDbType.Decimal));
                            cmd.Parameters.Add(new SqlParameter("@var_plc", SqlDbType.VarChar));

                            cmd.Parameters["@moisture"].Value = moisture_read;
                            cmd.Parameters["@var_plc"].Value = "DBX116.1";

                            dp.APMS_Exec_SP("SYS_MON_Set_Moisture_Check", cmd);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Detalle del Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Connect_PLC();
            }
        }

        private void conteoBatch_Tick(object sender, EventArgs e)
        {
            string err = "";
            try
            {
                if (plc319.IsAvailable)
                {
                    if (!plc319.IsConnected)
                        Connect_PLC();

                    err = "Error en leer donemix1";
                    bool check_mix1 = (bool)plc319.Read(DoneMix1);
                    err = "Error en leer donemix2";
                    bool check_mix2 = (bool)plc319.Read(DoneMix2);
                    //object TotalBatchx = ((uint)plc317.Read("DB284.DBD106")); //plc317.read plc317.Read("DB284.DBD106");
                    //int lineaf = Convert.ToInt32(plc317.Read("db284.dbw108"));
                    //int result2 = ((uint)plc317.Read("DB284.DBD106")).ConvertToInt();
                    int x = Convert.ToInt32(plc319.Read("DB284.DBD106"));
                    if (check_mix1)
                    {
                        ///*****************************************//
                        ///***Guardar Batch en el Primer Mezclado***//
                        ///*****************************************//
                        if (GetOrdenActiva(1))
                        {


                            //Hay un batch en el primer mezclado
                            //InsertBatch(1);
                            //plc319.Write(DoneMix1, false);

                            
                            //Guardar Leicitina 


                            //Guardar Fylax



                        }
                    }
                    if (check_mix2)
                    {
                        ///*****************************************//
                        ///***Guardar Batch en elSegundo Mezclado***//
                        ///*****************************************//
                        if (GetOrdenActiva(2))
                        {
                            //Hay un batch en el Segundo mezclado
                            //InsertBatch(2);
                            //plc319.Write(DoneMix2, false);


                            bool DoneTq1Arriba = (bool)plc319.Read(DoneTanqUp1);
                            bool DoneTq2Arriba = (bool)plc319.Read(DoneTanqUp2);
                            bool DoneTq3Arriba = (bool)plc319.Read(DoneTanqUp3);
                            bool DoneTq4Arriba = (bool)plc319.Read(DoneTanqUp4);

                            if (DoneTq1Arriba)
                            {
                                err = "Error DONE tanque 1 arriba";
                                double TotalBatch = ((uint)plc319.Read("DB520.DBD8")).ConvertToDouble();
                                GuardarBatchAceite(1, TotalBatch);
                                plc319.Write(DoneTanqUp1, false);
                                plc319.Write(DataType.DataBlock, 520, 8, 0);
                            }

                            if (DoneTq2Arriba)
                            {
                                err = "Error DONE tanque 2 arriba";
                                double TotalBatch = ((uint)plc319.Read("DB520.DBD22")).ConvertToDouble();
                                GuardarBatchAceite(2, TotalBatch);
                                plc319.Write(DoneTanqUp2, false);
                                plc319.Write(DataType.DataBlock, 520, 22, 0);
                            }

                            if (DoneTq3Arriba)
                            {
                                err = "Error DONE tanque 3 arriba";
                                double TotalBatch = ((uint)plc319.Read("DB520.DBD36")).ConvertToDouble();
                                GuardarBatchAceite(3, TotalBatch);
                                plc319.Write(DoneTanqUp3, false);
                                plc319.Write(DataType.DataBlock, 520, 36, 0);
                            }

                            if (DoneTq4Arriba)
                            {
                                err = "Error DONE tanque 4 arriba";
                                double TotalBatch = ((uint)plc319.Read("DB541.DBD0")).ConvertToDouble();
                                GuardarBatchAceite(4, TotalBatch);
                                plc319.Write(DoneTanqUp4, false);
                                plc319.Write(DataType.DataBlock, 541, 0, 0);
                            }

                            //insert de valor teorico para postpellet
                            //Acumular valores teoricos por linea de produccion
                           int id = GetOrdenInPostPelletID();
                           if (id > 0)
                           {
                               SqlConnection conn = new SqlConnection(dp.ConnectionStringAPMS);
                               conn.Open();
                               string sql = @" SELECT --sum([plan_kg_batch])
                                                    sum((case when (coalesce(rr.[cant_postpellet],0))>0 then rr.[cant_postpellet] else rr.[plan_kg_batch] end) )
                                        FROM [APMS].[dbo].[OP_Production_Orders_Structure] rr join 
                                             [APMS].[dbo].[MD_Raw_Material] mm on
	                                         rr.rm_id = mm.id join 
	                                         [APMS].[dbo].[OP_Production_Orders_Main_Mix] mix on
	                                         rr.order_id = mix.order_id
                                        where mm.[apply_pp] = 1 and mix.id =  " + id;
                               SqlCommand cmd = new SqlCommand(sql, conn);
                               double teoric = Convert.ToDouble(cmd.ExecuteScalar());

                               if (teoric > 0)
                               {
                                   //db284.dbd106
                                   double TotalBatch = ((uint)plc319.Read("db284.dbd106")).ConvertToDouble();
                                   //string s = plc317.Read("db284.dbd106").ToString();
                                   //int linea = Convert.ToInt32(plc317.Read("db284.dbw108"));
                                   int linea = Convert.ToInt32(plc319.Read("db284.dbw108"));
                                   if (linea == 4)
                                       linea = 3;

                                   int linea_id = linea;
                                   
                                   string sql1 = string.Format(@"INSERT INTO [dbo].[op_apply_oil_counter]
                                                                            ([id_line]
                                                                            ,[id_mix_op]
                                                                            ,[cant])
                                                                        VALUES
                                                                            ({0},{1},{2})", linea_id, Batch_IdMix, teoric);
                                   SqlCommand cmd1 = new SqlCommand(sql1, conn);
                                   cmd1.ExecuteNonQuery();
                               }
                           }


                        }
                    }
   
                    //************************************************************//
                    //***Alarma de Especie en Adicion Manual Micro Ingredientes***//
                    //************************************************************//
                    if (GetOrdenActiva(1))
                    {
                        //get tipo de especie en el primer mezclado
                       
                            string sql = @"SELECT (SELECT case when ff.especie = 2 then 1
                                                               when ff.especie = 1 then 0 end 
	                                               FROM [AQFSVR003].[ACS].[dbo].[PP_Plan_Ordenes] po join 
		                                                [AQFSVR003].[ACS].[dbo].[FML_Formulas_v2] ff on 
		                                                po.id_formula = ff.id
	                                               where po.id = mm.acs_id)as especie
                                           FROM [APMS].[dbo].[OP_Production_Orders_Main_Mix] mix join
                                                [APMS].[dbo].[OP_Production_Orders_Main] mm on
	                                            mix.order_id = mm.id
                                           where mix.id = " + Batch_IdMix +
                                         " order by mix.id desc ";
                            SqlConnection conn = new SqlConnection(dp.ConnectionStringAPMS);
                            conn.Open();
                            SqlCommand cmd = new SqlCommand(sql, conn);
                            int id_especie = Convert.ToInt32(cmd.ExecuteScalar());

                            plc319.Write(AlarmaMicro, id_especie);
                            conn.Close();
                        
                    }


                    //Escribir teoricos en aplicacion de aceites3500
                    //Verificar si hay una orden en pellet
                    if (GetOrdenInPostPellet())
                    {
                        //Actualizar valor teorico
                        int id = GetOrdenInPostPelletID();
                        if (id > 0)
                        {
                            err = "Error en get Orden Activa (2)";
                            SqlConnection conn = new SqlConnection(dp.ConnectionStringAPMS);
                            conn.Open();

                            //Linea PELLET1
                            string sqla = @"SELECT coalesce(sum([cant]),0)
                                              FROM [APMS].[dbo].[op_apply_oil_counter] as val join
                                                   [APMS].[dbo].[OP_Production_Orders_Main_Mix] as mix on
	                                               val.id_mix_op = mix.id
                                            where id_line = 1 and mix.id =" + id;
                            SqlCommand cmd = new SqlCommand(sqla, conn);
                            double Pellet1 = Convert.ToDouble(cmd.ExecuteScalar());

                            plc319.Write("DB524.DBX24.0", true);
                            err = "Error writte batch teorico";
                            plc319.Write(DataType.DataBlock, 524, 20, Pellet1);

                            //Linea PELLET2
                            string sqlb = @"SELECT coalesce(sum([cant]),0)
                                              FROM [APMS].[dbo].[op_apply_oil_counter] as val join
                                                   [APMS].[dbo].[OP_Production_Orders_Main_Mix] as mix on
	                                               val.id_mix_op = mix.id
                                            where id_line = 2 and mix.id =" + id;
                            SqlCommand cmdb = new SqlCommand(sqlb, conn);
                            double Pellet2 = Convert.ToDouble(cmdb.ExecuteScalar());

                            plc319.Write("DB528.DBX24.0", true);
                            err = "Error writte batch teorico";
                            plc319.Write(DataType.DataBlock, 528, 20, Pellet2);

                            //Linea Extruder
                            string sqlc = @"SELECT coalesce(sum([cant]),0)
                                              FROM [APMS].[dbo].[op_apply_oil_counter] as val join
                                                   [APMS].[dbo].[OP_Production_Orders_Main_Mix] as mix on
	                                               val.id_mix_op = mix.id
                                            where id_line = 3 and mix.id =" + id;
                            SqlCommand cmdc = new SqlCommand(sqlc, conn);
                            double Extruder = Convert.ToDouble(cmdc.ExecuteScalar());

                            if (plc315.IsAvailable)
                            {
                                if (!plc315.IsConnected)
                                    Connect_PLC();

                                plc315.Write("DB528.DBX24.0", true);
                                err = "Error writte batch teorico";
                                plc315.Write(DataType.DataBlock, 528, 20, Extruder);

                            }
                            #region comentado
                            //                            string sql = @" SELECT --sum([plan_kg_batch]* mix.real_batch)
//                                                    sum((case when (coalesce(rr.[cant_postpellet],0))>0 then rr.[cant_postpellet] else rr.[plan_kg_batch] end) * mix.real_batch)
//                                        FROM [APMS].[dbo].[OP_Production_Orders_Structure] rr join 
//                                             [APMS].[dbo].[MD_Raw_Material] mm on
//	                                         rr.rm_id = mm.id join 
//	                                         [APMS].[dbo].[OP_Production_Orders_Main_Mix] mix on
//	                                         rr.order_id = mix.order_id
//                                        where mm.[apply_pp] = 1 and mix.id =  " + id;
                            //SqlCommand cmd = new SqlCommand(sql, conn);
                            //double teoric = Convert.ToDouble(cmd.ExecuteScalar());


                          

//                            //Update de la cantidad acumulada
//                            OrdenPostPellet OPP = new OrdenPostPellet() { id_mix = Batch_IdMix };
//                            if (OPP.RecuperarUltimaOrden())
//                            {
//                                if (conn.State != ConnectionState.Open)
//                                    conn.Open();

//                                if (!plc317.IsConnected)
//                                    Connect_PLC();

//                                err = "Error en obtener el valor dispensado";
//                                //Obtener el valor real dispensado
//                                double TotalAcumulado = ((uint)plc317.Read("DB524.DBD12")).ConvertToDouble();
//                                string sqlz = @"UPDATE [dbo].[oil_get_out]
//                                            SET [cant] = cast('" + TotalAcumulado + "' as decimal(10,2)) " +
//                                                " WHERE id = " + OPP.id;
//                                SqlCommand cc = new SqlCommand(sqlz, conn);
//                                cc.ExecuteNonQuery();
//                            }
//                            else
//                            {
//                                if (conn.State != ConnectionState.Open)
//                                    conn.Open();

//                                if (!plc317.IsConnected)
//                                    Connect_PLC();

//                                err = "Error en obtener el valor dispensado";
//                                //Obtener el valor real dispensado
//                                double TotalAcumulado = ((uint)plc317.Read("DB524.DBD12")).ConvertToDouble();
//                                string sqlz = @"insert into [dbo].[oil_get_out]
//                                                       ([id_mix]
//                                                       ,[cant])
//                                                 VALUES
//                                                       (" + Batch_IdMix + ", cast('" + TotalAcumulado + "' as decimal(10,2)))";
                                                
//                                SqlCommand cc = new SqlCommand(sqlz, conn);
//                                cc.ExecuteNonQuery();
                            //                            }
                            #endregion
                            conn.Close();
                        }
                    }
                    else
                    {
                        //poner la orden activa en pellet
                        plc319.Write("DB524.DBX24.0", false);

                        if (GetOrdenActiva(2))
                        {
                            SqlConnection conn = new SqlConnection(dp.ConnectionStringAPMS);
                            conn.Open();
                            plc319.Write("DB524.DBX24.0", true);
                            string sql1 = @"UPDATE [dbo].[OP_Production_Orders_Main_Mix]
                                                                   SET [activa_post_pellet] = 1
                                                                 WHERE id = " + Batch_IdMix;
                            SqlCommand cc1 = new SqlCommand(sql1, conn);
                            cc1.ExecuteNonQuery();
                            conn.Close();
                        }
                    }


                    
                    
                    //**********************************//
                    //***ORDEN FINALIZADA EN PELLET 1***//
                    //**********************************//
                    err = "Error leyendo el done de orden finalizada";
                    bool OrderFinalizada = (bool)plc319.Read("DB524.DBX24.2");
                    if (OrderFinalizada)
                    {
                        SqlConnection conn = new SqlConnection(dp.ConnectionStringAPMS);
                        conn.Open();
                        OrdenPostPellet OPP = new OrdenPostPellet() { id_mix = Batch_IdMix };
                        if (OPP.RecuperarUltimaOrden())
                        {
//                            string sqlv = @"UPDATE [dbo].[oil_get_out]
//                                            SET finish = 1
//                                        WHERE id = " + OPP.id;
//                            SqlCommand sxe = new SqlCommand(sqlv, conn);
//                            err = "Error update orden finalizada";
//                            sxe.ExecuteNonQuery();

                            //Guardar total por linea
                            double TotalAcumulado = ((uint)plc319.Read("DB524.DBD12")).ConvertToDouble();
                            GuardarTotalLinea(1, Batch_IdMix, TotalAcumulado);

                            //Reset de la señal de finalizar orden
                            plc319.Write("DB524.DBX24.2", false);

                            //Reset de Orden Activa
                            //plc317.Write("DB524.DBX24.0", false);

                            //Reset del valor teorico
                            plc319.Write(DataType.DataBlock, 524, 20, 0);

                            //reset del valor real
                            plc319.Write(DataType.DataBlock, 524, 12, 0);//DB524.DBD12

                            //Quitar de activa la orden de produccion
                            if (GetOrdenActiva(2))
                            {
                                string sql1 = @"UPDATE [dbo].[OP_Production_Orders_Main_Mix]
                                           SET [activa_post_pellet] = 0
                                         WHERE id = " + Batch_IdMix;
                                SqlCommand cc = new SqlCommand(sql1, conn);
                                err = "Error update orden finalizada";
                                cc.ExecuteNonQuery();
                            }

                            

                        }
                        conn.Close();
                    }



                    //**********************************//
                    //***ORDEN FINALIZADA EN PELLET 2***//
                    //**********************************//
                    err = "Error leyendo el done de orden finalizada";
                    bool OrderFinalizada2 = (bool)plc319.Read("DB528.DBX24.2");
                    if (OrderFinalizada2)
                    {
                        SqlConnection conn = new SqlConnection(dp.ConnectionStringAPMS);
                        conn.Open();
                        OrdenPostPellet OPP = new OrdenPostPellet() { id_mix = Batch_IdMix };
                        if (OPP.RecuperarUltimaOrden())
                        {
//                            string sqlv = @"UPDATE [dbo].[oil_get_out]
//                                            SET finish = 1
//                                        WHERE id = " + OPP.id;
//                            SqlCommand sxe = new SqlCommand(sqlv, conn);
//                            err = "Error update orden finalizada";
//                            sxe.ExecuteNonQuery();

                            //Reset de la señal de finalizar orden
                            plc319.Write("DB528.DBX24.2", false);

                            //Guardar total por linea
                            double TotalAcumulado = ((uint)plc319.Read("DB528.DBD12")).ConvertToDouble();
                            GuardarTotalLinea(2, Batch_IdMix, TotalAcumulado);

                            //Reset de Orden Activa
                            //plc317.Write("DB528.DBX24.0", false);

                            //Reset del valor teorico
                            plc319.Write(DataType.DataBlock, 528, 20, 0);

                            //reset del valor real
                            plc319.Write(DataType.DataBlock, 528, 12, 0);

                            //Quitar de activa la orden de produccion
                            if (GetOrdenActiva(2))
                            {
                                string sql1 = @"UPDATE [dbo].[OP_Production_Orders_Main_Mix]
                                           SET [activa_post_pellet] = 0
                                         WHERE id = " + Batch_IdMix;
                                SqlCommand cc = new SqlCommand(sql1, conn);
                                err = "Error update orden finalizada";
                                cc.ExecuteNonQuery();
                            }

                            
                        }
                        conn.Close();
                    }


                    //**********************************//
                    //***ORDEN FINALIZADA EN EXTRUDER***//
                    //**********************************//
                    if (plc315.IsAvailable)
                    {
                        if (!plc315.IsConnected)
                            Connect_PLC();

                        err = "Error leyendo el done de orden finalizada";
                        bool OrderFinalizada3 = (bool)plc315.Read("DB528.DBX24.2");
                        if (OrderFinalizada3)
                        {
                            SqlConnection conn = new SqlConnection(dp.ConnectionStringAPMS);
                            conn.Open();
                            OrdenPostPellet OPP = new OrdenPostPellet() { id_mix = Batch_IdMix };
                            if (OPP.RecuperarUltimaOrden())
                            {
//                                string sqlv = @"UPDATE [dbo].[oil_get_out]
//                                            SET finish = 1
//                                        WHERE id = " + OPP.id;
//                                SqlCommand sxe = new SqlCommand(sqlv, conn);
//                                err = "Error update orden finalizada";
//                                sxe.ExecuteNonQuery();

                                //Reset de la señal de finalizar orden
                                plc315.Write("DB528.DBX24.2", false);

                                //Guardar total por linea
                                double TotalAcumulado = ((uint)plc315.Read("DB528.DBD12")).ConvertToDouble();
                                GuardarTotalLinea(3, Batch_IdMix, TotalAcumulado);

                                //Reset de Orden Activa
                                //plc315.Write("DB528.DBX24.0", false);

                                //Reset del valor teorico
                                plc315.Write(DataType.DataBlock, 528, 20, 0);

                                //reset del valor real
                                plc315.Write(DataType.DataBlock, 528, 12, 0);

                                //Quitar de activa la orden de produccion
                                if (GetOrdenActiva(2))
                                {
                                    string sql1 = @"UPDATE [dbo].[OP_Production_Orders_Main_Mix]
                                           SET [activa_post_pellet] = 0
                                         WHERE id = " + Batch_IdMix;
                                    SqlCommand cc = new SqlCommand(sql1, conn);
                                    err = "Error update orden finalizada";
                                    cc.ExecuteNonQuery();
                                }


                                
                            }
                            conn.Close();
                        }
                    }


                }//End PLC Service Available

                
            }
            catch (Exception ec)
            {
                //MessageBox.Show(ec.Message + err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GuardarTotalLinea(int pLinea, Int64 pid_mix_op, double valor)
        {
            if (valor > 0)
            {
                try
                {
                    string sql = @"INSERT INTO [dbo].[op_apply_oil_counter_line]
                                       ([id_line]
                                       ,[id_mix_op]
                                       ,[cant])
                                 VALUES
                                       (" + pLinea +
                                           "," + pid_mix_op +
                                           ",cast('" + valor + "'as decimal(10,2)))";
                    SqlConnection conn = new SqlConnection(dp.ConnectionStringAPMS);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ec)
                {

                }
            }
        }

        private bool GetOrdenInPostPellet()
        {
            bool r = false;
            try
            {
                string sql = @"SELECT case when count(*)>0 then 1 else 0 end 
                               FROM [APMS].[dbo].[OP_Production_Orders_Main_Mix]
                               where [activa_post_pellet] = 1";
                SqlConnection conn = new SqlConnection(dp.ConnectionStringAPMS);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                r = Convert.ToBoolean(cmd.ExecuteScalar());
                conn.Close();
            }
            catch (Exception ec)
            {
                Console.WriteLine(ec.Message);
            }
            return r;
        }

        private int GetOrdenInPostPelletID()
        {
            int r = 0;
            try
            {
                string sql = @"SELECT top 1 id
                               FROM [APMS].[dbo].[OP_Production_Orders_Main_Mix]
                               where [activa_post_pellet] = 1 
                                order by 1 asc";
                SqlConnection conn = new SqlConnection(dp.ConnectionStringAPMS);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                r = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            catch (Exception ec)
            {
                Console.WriteLine(ec.Message);
            }
            return r;
        }

        private void GuardarBatchAceite(int v, double totalBatch)
        {
            //Codigo comentado el 28 de mayo de 2020 para evitar duplicados 
            //Estos se estaban guardando con el bincode TANQ
            //Danys Oliva

            //try
            //{
            //    int id_tanque = 0;
            //    switch (v)
            //    {
            //        case 1:
            //            id_tanque = 43;
            //            break;
            //        case 2:
            //            id_tanque = 44;
            //            break;
            //        case 3:
            //            id_tanque = 45;
            //            break;
            //        default:
            //            break;
            //    }

            //    if (id_tanque > 0)
            //    {
            //        OrdenActiva OrdenA = new OrdenActiva();
            //        if (OrdenA.RecuperarRegistroSegundoMix(id_tanque))
            //        {
            //            OrdenA.GuardarBatchAceite(0, id_tanque);
            //        }
            //    }
            //}
            //catch (Exception ec)
            //{
            //}
        }



   

        private void InsertBatch(int id_mix)
        {
            try
            {
                
                string sql = @"INSERT INTO [dbo].[OP_Log_Batch]
                                                ([id_orden]
                                                ,[code_orden]
                                                ,[mix]
                                                ,[batch]
                                                ,[date]
                                                ,[reg_teorico])
                                            VALUES
                                                (" + Batch_idOrden +",'"
                                                   + Batch_fullCode + "',"
                                                   + id_mix + ","
                                                   + Batch_nBatchActual+","
                                                   + "GETDATE(),0)";
                SqlConnection conn = new SqlConnection(dp.ConnectionStringAPMS);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ec)
            {
                //MessageBox.Show(ec.Message+ " Error en insert batch", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool GetOrdenActivaAPP()
        {
            bool rec = false;
            try
            {
                string sql = @"SELECT [order_id]
                                     ,[mix_fullCode]
	                                 ,(
	                                   SELECT case when count(*)>0 then count(*) else 1 end
	                                   FROM [APMS].[dbo].[OP_Log_Batch]
	                                   where id_orden = [order_id] and mix =mix_num
			                            )as total
                                     ,[id]
                                FROM [APMS].[dbo].[OP_Production_Orders_Main_Mix] 
                                where ((SELECT count(*)
                                          FROM [APMS].[dbo].[OP_Production_Orders_Main_Mix] mm
                                          where mm.[mix_num] = 2 and mm.[fin_post_pellet] = 0)=0) 
                                      AND status = 70 and mix_num = " + 2;

                SqlConnection conn = new SqlConnection(dp.ConnectionStringAPMS);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Batch_idOrden = dr.GetInt64(0);
                    Batch_fullCode = dr.GetString(1);
                    Batch_nBatchActual = dr.GetInt32(2);
                    Batch_IdMix = dr.GetInt64(3);
                    rec = true;
                }

                dr.Close();
                conn.Close();
            }
            catch (Exception ec)
            {
                //MessageBox.Show(ec.Message + " Error en get Orden Activa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return rec;
        }

        private bool GetOrdenActiva(int v)
        {
            bool rec = false;
            try
            {
                string sql = @"SELECT [order_id]
                                     ,[mix_fullCode]
	                                 ,(
	                                   SELECT case when count(*)>0 then count(*) else 1 end
	                                   FROM [APMS].[dbo].[OP_Log_Batch]
	                                   where id_orden = [order_id] and mix =mix_num
			                            )as total
                                     ,[id]
                                FROM [APMS].[dbo].[OP_Production_Orders_Main_Mix] 
                                where status = 70 and mix_num = " + v;

                SqlConnection conn = new SqlConnection(dp.ConnectionStringAPMS);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Batch_idOrden = dr.GetInt64(0);
                    Batch_fullCode = dr.GetString(1);
                    Batch_nBatchActual = dr.GetInt32(2);
                    Batch_IdMix = dr.GetInt64(3);
                    rec = true;
                }

                dr.Close();
                conn.Close();
            }
            catch (Exception ec)
            {
                //MessageBox.Show(ec.Message+" Error en get Orden Activa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return rec;
        }

        public decimal LecturaAnterior(int pid_maquina)
        {
            decimal val = 0;
            try
            {
                string sql = @"SELECT [ultima_lectura]
                                      ,[valor_maximo]
                                  FROM [dbo].[EQ_Maquinas_Horas] eq
                                  where eq.id_maquina = " + pid_maquina;
                SqlConnection conn = new SqlConnection(dp.ConnectionStringAPMS);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                val = Convert.ToDecimal(cmd.ExecuteScalar());
                conn.Close();
            }
            catch (Exception ec)
            {
                string a = ec.Message;
            }
            return val;
        }

        public bool UpdateLecturaActual(int pid_maquina,double pLectura_Actual)
        {
            bool a = false;
            try
            {
                DateTime HoraActual = dp.NowDateTime();

                string sql = @"UPDATE [dbo].[EQ_Maquinas_Horas]
                                   SET [lectura_actual] = " + pLectura_Actual +
                                   " ,[fecha_ultimo_update] = getdate() " +
                                 " WHERE [id_maquina] = " + pid_maquina;
                SqlConnection conn = new SqlConnection(dp.ConnectionStringAPMS);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                Convert.ToDecimal(cmd.ExecuteScalar());
                conn.Close();

                AcumularHoras(pid_maquina, HoraActual.Hour, pLectura_Actual);
                
            }
            catch (Exception ec)
            {
                
            }
            return a;
        }

        public bool AcumularHoras(int pIdMaquina_, int _Hour, double _LecturaActual)
        {
            bool a = false;
            try
            {
                int id_periodo= 0;
                string sql = "";
                if (PeriodoAbierto(pIdMaquina_))
                {
                    sql = @"SELECT pp.id
                                      FROM [APMS].[dbo].[EQ_Maquinas_Horas_Periodo] pp
                                      where pp.cerrado = 0 and pp.id_maquina = " + pIdMaquina_;
                    SqlConnection conn = new SqlConnection(dp.ConnectionStringAPMS);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    id_periodo = Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();
                }
                else
                {
                    sql = @"INSERT INTO [dbo].[EQ_Maquinas_Horas_Periodo]
                                                                           ([fechai]
                                                                           ,[cant]
                                                                           ,[cerrado]
                                                                           ,[ultima_lectura]
                                                                           ,[id_maquina])
                                                                     VALUES
                                                                           (getdate()
                                                                           ,0
                                                                           ,0
                                                                           ,@ultima_lectura
                                                                           ," + pIdMaquina_ + ") SELECT SCOPE_IDENTITY();";
                    SqlConnection conn = new SqlConnection(dp.ConnectionStringAPMS);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add("ultima_lectura", SqlDbType.Decimal).Value = _LecturaActual;
                    id_periodo = Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();
                }

                if (id_periodo > 0)
                {
                    #region codigo obsoleto
                    //                    switch (_Hour)
                    //                    {
                    //                        case 7://si son las 7 am
                    //                        case 19://si son las 7 pm
                    //                            //Cerrar el periodo
                    //                            sql = @"UPDATE [dbo].[EQ_Maquinas_Horas_Periodo]
                    //                                       SET [cerrado] = 1,
                    //                                           [fechaf] = getdate(),
                    //                                           [ultima_lectura] = @lectura
                    //                                     WHERE id = " + id_periodo + " and (select DATEDIFF(hour, [fechai], getdate()))>2";
                    //                                SqlConnection conn = new SqlConnection(dp.ConnectionStringAPMS);
                    //                                conn.Open();
                    //                                SqlCommand cmd = new SqlCommand(sql, conn);
                    //                                cmd.Parameters.Add("lectura", SqlDbType.Decimal).Value = _LecturaActual;
                    //                                id_periodo = Convert.ToInt32(cmd.ExecuteScalar());
                    //                                conn.Close();
                    //                            break;
                    //                        default:
                    //                            //Acumular horas 
                    //                            sql = @"SELECT [ultima_lectura]
                    //                                    FROM [APMS].[dbo].[EQ_Maquinas_Horas_Periodo] pp
                    //                                    where pp.cerrado = 0 and pp.id = " + id_periodo;
                    //                            SqlConnection conn1 = new SqlConnection(dp.ConnectionStringAPMS);
                    //                            conn1.Open();
                    //                            SqlCommand cmd1 = new SqlCommand(sql, conn1);
                    //                            double ultima_lectura = Convert.ToInt32(cmd1.ExecuteScalar());
                    //                            double valor_acumulado = _LecturaActual - ultima_lectura;
                    //                            conn1.Close();

                    //                            sql = @"UPDATE [dbo].[EQ_Maquinas_Horas_Periodo]
                    //                                       SET [cant] = @valor 
                    //                                           /*,[ultima_lectura] = @lectura*/
                    //                                      WHERE id = " + id_periodo;
                    //                            SqlConnection connx = new SqlConnection(dp.ConnectionStringAPMS);
                    //                            connx.Open();
                    //                            SqlCommand cmdx = new SqlCommand(sql, connx);
                    //                            cmdx.Parameters.Add("valor", SqlDbType.Decimal).Value = valor_acumulado;
                    //                            //cmdx.Parameters.Add("lectura", SqlDbType.Decimal).Value = _LecturaActual;
                    //                            cmdx.ExecuteNonQuery();
                    //                            connx.Close();
                    //                            break;
                    //                    }//end switch
#endregion


                    //Obtenemos el valor de la ultima lectura.
                    sql = @"SELECT [ultima_lectura], DATEDIFF(HOUR, pp.fechai, GETDATE())
                            FROM [APMS].[dbo].[EQ_Maquinas_Horas_Periodo] pp
                            where pp.cerrado = 0 and pp.id = " + id_periodo;
                    SqlConnection conn1 = new SqlConnection(dp.ConnectionStringAPMS);
                    conn1.Open();
                    SqlCommand cmd1 = new SqlCommand(sql, conn1);
                    SqlDataReader dr = cmd1.ExecuteReader();
                    double ultima_lectura = 0;
                    decimal horas_edad = 0;
                    if (dr.Read())
                    {
                        ultima_lectura = Convert.ToDouble(dr.GetDecimal(0));
                        horas_edad = dr.GetInt32(1);
                    }
                    dr.Close();
                    //double ultima_lectura = Convert.ToDouble(cmd1.ExecuteScalar());
                    conn1.Close();


                    switch (_Hour)
                    {
                        case 6://si son las 6 am
                        case 14://si son las 7 pm
                        case 22://si son las 10 pm
                            //Cerrar el periodo

                            bool EjecutoUpdate = false;
                            if (_LecturaActual > 0)
                            {
                                if (_LecturaActual >= ultima_lectura)
                                {
                                    sql = @"UPDATE [dbo].[EQ_Maquinas_Horas_Periodo]
                                                           SET [cerrado] = 1,
                                                               [fechaf] = getdate(),
                                                               [ultima_lectura] = @lectura
                                                         WHERE id = " + id_periodo + " and (select DATEDIFF(hour, [fechai], getdate()))>2";
                                    SqlConnection conn = new SqlConnection(dp.ConnectionStringAPMS);
                                    conn.Open();
                                    SqlCommand cmd = new SqlCommand(sql, conn);
                                    cmd.Parameters.Add("lectura", SqlDbType.Decimal).Value = _LecturaActual;
                                    EjecutoUpdate = true;
                                    //id_periodo = Convert.ToInt32(cmd.ExecuteScalar());
                                    cmd.ExecuteScalar();
                                    conn.Close();
                                }
                            }


                            //Si entra en el siguiente if, significa que la planta esta en paro.
                            //Ya se cumplio el turno pero no se acumularon horas
                            //vamos a cerrar el turno con cero horas para seguir las secuencias.
                            if (!EjecutoUpdate)
                            {
                                double dif = _LecturaActual - ultima_lectura;
                                if (horas_edad >= 8 && dif <= 0)
                                {
                                    sql = @"UPDATE [dbo].[EQ_Maquinas_Horas_Periodo]
                                                           SET [cerrado] = 1,
                                                               [fechaf] = getdate(),
                                                               [ultima_lectura] = @lectura
                                                         WHERE id = " + id_periodo;
                                    SqlConnection connz = new SqlConnection(dp.ConnectionStringAPMS);
                                    connz.Open();
                                    SqlCommand cmdz = new SqlCommand(sql, connz);
                                    cmdz.Parameters.Add("lectura", SqlDbType.Decimal).Value = _LecturaActual;
                                    cmdz.ExecuteScalar();
                                    connz.Close();
                                }
                            }
                            
                            break;
                        default:
                            #region comentado mejora
                            //Acumular horas 
//                            sql = @"SELECT [ultima_lectura]
//                                                        FROM [APMS].[dbo].[EQ_Maquinas_Horas_Periodo] pp
//                                                        where pp.cerrado = 0 and pp.id = " + id_periodo;
//                            SqlConnection conn1 = new SqlConnection(dp.ConnectionStringAPMS);
//                            conn1.Open();
//                            SqlCommand cmd1 = new SqlCommand(sql, conn1);
//                            double ultima_lectura = Convert.ToInt32(cmd1.ExecuteScalar());
//                            double valor_acumulado = _LecturaActual - ultima_lectura;
//                            conn1.Close();

//                            sql = @"UPDATE [dbo].[EQ_Maquinas_Horas_Periodo]
//                                                           SET [cant] = @valor 
//                                                               /*,[ultima_lectura] = @lectura*/
//                                                          WHERE id = " + id_periodo;
//                            SqlConnection connx = new SqlConnection(dp.ConnectionStringAPMS);
//                            connx.Open();
//                            SqlCommand cmdx = new SqlCommand(sql, connx);
//                            cmdx.Parameters.Add("valor", SqlDbType.Decimal).Value = valor_acumulado;
//                            //cmdx.Parameters.Add("lectura", SqlDbType.Decimal).Value = _LecturaActual;
//                            cmdx.ExecuteNonQuery();
                            //                            connx.Close();
                            #endregion
                            break;
                    }//end switch

                   

                    //Acumular horas
                    
                    double valor_acumulado = _LecturaActual - ultima_lectura;
                    

                    if (valor_acumulado < 0)
                        valor_acumulado = 0;

                    if (valor_acumulado > 0 && valor_acumulado <= 15)
                    {
                        sql = @"UPDATE [dbo].[EQ_Maquinas_Horas_Periodo]
                                                        SET [cant] = @valor 
                                                            /*,[ultima_lectura] = @lectura*/
                                                        WHERE id = " + id_periodo;
                        SqlConnection connx = new SqlConnection(dp.ConnectionStringAPMS);
                        connx.Open();
                        SqlCommand cmdx = new SqlCommand(sql, connx);
                        cmdx.Parameters.Add("valor", SqlDbType.Decimal).Value = valor_acumulado;
                        //cmdx.Parameters.Add("lectura", SqlDbType.Decimal).Value = _LecturaActual;
                        cmdx.ExecuteNonQuery();
                        connx.Close();
                    }
                   
                    
                    

                }
            }
            catch (Exception ec)
            {
                
            }
            return a;
        }

        public bool PeriodoAbierto(int pIdMaquina)
        {
            bool a = false;
            try
            {
                string sql = @"SELECT case when count(*)>0 then 1 else 0 end 
                               FROM [APMS].[dbo].[EQ_Maquinas_Horas_Periodo] pp
                               where pp.cerrado = 0 and pp.id_maquina = " + pIdMaquina;
                SqlConnection conn = new SqlConnection(dp.ConnectionStringAPMS);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                a = Convert.ToBoolean(cmd.ExecuteScalar());
                conn.Close();
            }
            catch (Exception ec)
            {
                
            }
            return a;
        }

        private void timerHorasMaquina_Tick(object sender, EventArgs e)
        {
            //Timer de recalculo para Horas Maquina
            if (plc319.IsAvailable)
            {
                if (!plc319.IsConnected)
                    Connect_PLC();
                try
                {
                    //Pellet 1
                    double HorasActual_Pellet1 = ((uint)plc319.Read("DB1.DBD19958")).ConvertToDouble();
                    //id maquina = 1
                    UpdateLecturaActual(1, HorasActual_Pellet1);

                    //Pellet 2
                    double HorasActual_Pellet2 = ((uint)plc319.Read("DB1.DBD14458")).ConvertToDouble();
                    UpdateLecturaActual(2, HorasActual_Pellet2);
                }
                catch 
                {
                }
                
            }


            //Timer de recalculo para Horas Maquina Extruder
            if (plc315.IsAvailable)
            {
                if (!plc315.IsConnected)
                    Connect_PLC();

                try
                {
                    //Extruder 1
                    double HorasActual_Extruder1 = ((uint)plc315.Read("DB2.DBD1808")).ConvertToDouble();
                    UpdateLecturaActual(3, HorasActual_Extruder1);
                }
                catch
                {
                }
                
            }
        }
    }

        #endregion
}
