using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Genesis.Gtimes.Common;
using Genesis.Gtimes.WIP;
using Genesis.Gtimes.ADM;
using Genesis.Gtimes.MTR;
using Genesis.Gtimes.Transaction;
using Genesis.Gtimes.Transaction.EQP;
using Genesis.Gtimes.Transaction.MTR;
using Genesis.Gtimes.Transaction.WIP;
using Genesis.Gtimes.GTiCompoents;
using TaiFlexMES.ComFunction;

namespace TaiFlexMES.TW_WIP
{
    public partial class TW_WipLocationH01 : PageBase
    {
        #region Property

        private EquipmentUtility.EquipmentInfo EqpInfo
        {
            get { return (EquipmentUtility.EquipmentInfo)this["EqpInfo"]; }
            set { this["EqpInfo"] = value; }
        }
        private RouteUtility.RouteVerOperationInfo RouteVerOperInfo
        {
            get { return (RouteUtility.RouteVerOperationInfo)this["RouteVerOperInfo"]; }
            set { this["RouteVerOperInfo"] = value; }
        }
        private List<RouteUtility.RouteVersionOperationInfo> MainRouteOperList
        {
            get { return (List<RouteUtility.RouteVersionOperationInfo>)this["MainRouteOperList"]; }
            set { this["MainRouteOperList"] = value; }
        }
        private WOUtility.WOInfo WOInfo
        {
            get { return (WOUtility.WOInfo)this["WOInfo"]; }
            set { this["WOInfo"] = value; }
        }
        private DataTable dtMLot
        {
            get
            {
                return (DataTable)ViewState["dtMLot"];
            }
            set { ViewState["dtMLot"] = value; }
        }
        private DataView dvData
        {
            get
            {
                return (DataView)this["dvData"];
            }
            set { this["dvData"] = value; }
        }

        ///// <summary>
        ///// 批號報廢資訊
        ///// </summary>
        //private List<LotUtility.LotScrapCreateInfo> lotScrapCreateInfo
        //{
        //    get
        //    {
        //        if (this["lotScrapCreateInfo"] == null)
        //        {
        //            lotScrapCreateInfo = new List<LotUtility.LotScrapCreateInfo>();
        //        }
        //        return (List<LotUtility.LotScrapCreateInfo>)this["lotScrapCreateInfo"];
        //    }
        //    set { this["lotScrapCreateInfo"] = value; }
        //}

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //檢查使用者權限與閒置時間.
                //CheckRightAndTimeOut();

                //if (!CheckUserAccessRight()) return;

                if (!IsPostBack)
                {

                    InitialControl();
                }
                CalData1();
                Radio2.Checked = true;
            }
            catch (Exception ex)
            {
                WriteClientMessage(this.Page, MessageType.Exception, ex.Message);
            }
        }
        private void InitialControl()
        {
            this.cbDetail.FunctionTitleText = GetFunName();
            ClearField();
        }

        private void ClearField()
        {

            Text_Lot.Text = "";

            this.cbDetail.OKButton.Enabled = false;

        }

        #region Event




        #region GridView
        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected void gvMLot_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected void gvMLotRowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
        #endregion

        #endregion

        #region Button

        protected override void OnInit(EventArgs e)
        {
            this.cbDetail.OKButton.Click += new ImageClickEventHandler(OKButton_Click);
            this.cbDetail.ExitButton.Visible = false;
            this.cbDetail.HelpButton.Visible = false;
            this.cbDetail.OKButton.Visible = false;
            this.cbDetail.OKButton.Enabled = false;
            base.OnInit(e);
        }

        void OKButton_Click(object sender, ImageClickEventArgs e)
        {
            //try
            //{
            //    ConditionCheck();

            //    string linkSID = this.DBC.GetSID();
            //    DateTime txnTime = this.DBC.GetDBTime();
            //    string user = this.User.Identity.Name;

            //    TransactionUtility.TransactionBase txnBase = new TransactionUtility.TransactionBase(linkSID, this.User.Identity.Name, txnTime, FunctionRightName);
            //    TransactionUtility.GtimesTxn gtimesTxn = new TransactionUtility.GtimesTxn(this.DBC, txnBase);
            //    TransactionUtility.AddSQLCommandTxn sqlcmd = new TransactionUtility.AddSQLCommandTxn();
            //    List<IDbCommand> commands = new List<IDbCommand>(); 

            //    using (IDbTransaction tx = DBC.GetTransaction())
            //    {
            //        try
            //        {
            //            CommFunction.CommFunction.Common func = new TaiFlexMES.CommFunction.CommFunction.Common(DBC);
            //            CommFunction.CommFunction.TW_Common func_TW = new TaiFlexMES.CommFunction.CommFunction.TW_Common(DBC);
            //            PartUtility.PartInfo partInfo = new PartUtility.PartInfo(DBC, WOInfo.PARTNO, PartUtility.IndexType.No);

            //        }
            //        catch (Exception txnEX)
            //        {
            //            tx.Rollback();
            //            throw txnEX;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    WriteClientMessage(this.Page, MessageType.Exception, ex.Message);
            //}

        }

        private void ConditionCheck()
        {

            if (Radio1.Checked == false && Radio2.Checked == false)
            {
                AlertDialogMessage(this.Page, "請先選擇一批號帶入方式");
                return;
            }

            if (Drop_FabFrom.SelectedIndex == 0)
            {
                AlertDialogMessage(this.Page, "請先選擇運送地點(起)");
                return;
            }

            if (Drop_FabTo.SelectedIndex == 0)
            {
                AlertDialogMessage(this.Page, "請先選擇運送地點(迄)");
                return;
            }

            if (Drop_Dept.SelectedIndex == 0)
            {
                AlertDialogMessage(this.Page, "請先選擇收貨單位");
                return;
            }

            if (Text_Group.Text == String.Empty)
            {
                AlertDialogMessage(this.Page, "自定群組欄位未輸入");
                return;
            }
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {

            string linkSID = this.DBC.GetSID();
            DateTime txnTime = this.DBC.GetDBTime();
            TransactionUtility.TransactionBase txnBase = new TransactionUtility.TransactionBase(linkSID, this.User.Identity.Name, txnTime, "TW_WipLocationH01");
            TransactionUtility.GtimesTxn gtimesTxn = new TransactionUtility.GtimesTxn(this.DBC, txnBase);

            TransactionUtility.AddSQLCommandTxn commandTxn = new TransactionUtility.AddSQLCommandTxn();
            List<IDbCommand> commands = new List<IDbCommand>();


            ConditionCheck();

            String RECEIPT_NO = this.DBC.GetSID();
            if (dvData == null || dvData.Count == 0) return;

            string sqlA = String.Empty;
            sqlA = "select USER_NAME FROM AD_USER WHERE ACCOUNT_NO = '" + this.User.Identity.Name + "'";
            DataTable dtA = DBC.Select(sqlA);
            string user_name = String.Empty;

            if (dtA == null || dtA.Rows == null || dtA.Rows.Count == 0)
            {

            }
            else
            {
                user_name = dtA.Rows[0]["USER_NAME"].ToString();
            }

            String fab_from_M = string.Empty;
            String fab_to_M = string.Empty;
            String dept_M = string.Empty;

            using (IDbTransaction tx = DBC.GetTransaction())
            {

                for (int i = 0; i <= gvData.Rows.Count - 1; i++)
                {
                    string SID = this.DBC.GetSID();
                    String lotno = gvData.Rows[i].Cells[1].Text;
                    String fab_from = gvData.Rows[i].Cells[2].Text;
                    String fab_to = gvData.Rows[i].Cells[3].Text;
                    String dept = gvData.Rows[i].Cells[4].Text;
                    String package_no = String.Empty;
                    String machine = String.Empty;

                    if (string.IsNullOrEmpty(fab_from_M))
                    {
                        fab_from_M = fab_from;
                    }
                    if (string.IsNullOrEmpty(fab_to_M))
                    {
                        fab_to_M = fab_to;
                    }
                    if (string.IsNullOrEmpty(dept_M))
                    {
                        dept_M = dept;
                    }

                    package_no = gvData.Rows[i].Cells[5].Text.Trim();

                    if (package_no == "&nbsp;")
                    {
                        package_no = String.Empty;
                    }

                    machine = gvData.Rows[i].Cells[6].Text.Trim();

                    if (machine == "&nbsp;")
                    {
                        machine = String.Empty;
                    }

                    string sql = String.Empty;
                    sql = "select * from XX_WIP_LOCATION where LOT = '" + lotno + "'  AND STATUS IN (10 , 20, 30) ";
                    DataTable dt = DBC.Select(sql);

                    if (dt == null || dt.Rows == null || dt.Rows.Count == 0)
                    {
                        InsertCommandBuilder icb = new InsertCommandBuilder(DBC, "XX_WIP_LOCATION");
                        if (fab_from == "二廠" && fab_to == "二廠" && dept != "合成一課危險倉" && dept != "合成二課危險倉")
                        {
                            icb.InsertColumn("SID", SID);
                            icb.InsertColumn("LOT", lotno);
                            icb.InsertColumn("FAB_FROM", fab_from);
                            icb.InsertColumn("FAB_TO", fab_to);
                            icb.InsertColumn("RECEIPT_NO", RECEIPT_NO);
                            icb.InsertColumn("PACKAGE_NO", package_no);
                            icb.InsertColumn("H01_USER", this.User.Identity.Name + "/" + user_name);
                            icb.InsertColumn("H01_DATE", txnTime);
                            icb.InsertColumn("H02_USER", this.User.Identity.Name + "/" + user_name);
                            icb.InsertColumn("H02_DATE", txnTime);
                            icb.InsertColumn("H02_MIN", 0);
                            icb.InsertColumn("H03_USER", this.User.Identity.Name + "/" + user_name);
                            icb.InsertColumn("H03_DATE", txnTime);
                            icb.InsertColumn("H03_MIN", 0);
                            icb.InsertColumn("STATUS", 30);
                            icb.InsertColumn("H04_DEPT", dept);
                            icb.InsertColumn("MACHINE", machine);
                        }
                        else if (dept == "合成一課危險倉" || dept == "合成二課危險倉" || Text_Group.Text.ToString().StartsWith("B"))
                        {
                            icb.InsertColumn("SID", SID);
                            icb.InsertColumn("LOT", lotno);
                            icb.InsertColumn("FAB_FROM", fab_from);
                            icb.InsertColumn("FAB_TO", fab_to);
                            icb.InsertColumn("RECEIPT_NO", RECEIPT_NO);
                            icb.InsertColumn("PACKAGE_NO", package_no);
                            icb.InsertColumn("H01_USER", this.User.Identity.Name + "/" + user_name);
                            icb.InsertColumn("H01_DATE", txnTime);
                            icb.InsertColumn("H02_USER", this.User.Identity.Name + "/" + user_name);
                            icb.InsertColumn("H02_DATE", txnTime);
                            icb.InsertColumn("H02_MIN", 0);
                            icb.InsertColumn("STATUS", 20);
                            icb.InsertColumn("H04_DEPT", dept);
                            icb.InsertColumn("MACHINE", machine);
                        }
                        else
                        {
                            icb.InsertColumn("SID", SID);
                            icb.InsertColumn("LOT", lotno);
                            icb.InsertColumn("FAB_FROM", fab_from);
                            icb.InsertColumn("FAB_TO", fab_to);
                            icb.InsertColumn("RECEIPT_NO", RECEIPT_NO);
                            icb.InsertColumn("PACKAGE_NO", package_no);
                            icb.InsertColumn("H01_USER", this.User.Identity.Name + "/" + user_name);
                            icb.InsertColumn("H01_DATE", txnTime);
                            icb.InsertColumn("H04_DEPT", dept);
                            icb.InsertColumn("MACHINE", machine);
                        }


                        commands.AddRange(icb.GetCommands());


                    }

                }
                commandTxn.Commands.Clear();
                commandTxn.Commands.AddRange(commands);
                gtimesTxn.Clear();
                gtimesTxn.Add(commandTxn);
                gtimesTxn.DoTransaction(gtimesTxn.GetTransactionCommands(), tx);
                gtimesTxn.Clear();

                tx.Commit();

                if (Drop_Dept.SelectedItem.ToString().Substring(0, 2) == "後製")
                {


                    for (int i = 0; i <= gvData.Rows.Count - 1; i++)
                    {
                        String lot = gvData.Rows[i].Cells[1].Text;
                        string sqlB = "select L.LOT, L.WO, L.PARTNO, P.PART_NAME, L.PRODUCT, L.OPER_SEQ, O.OPERATION_NO, L.OPERATION from WP_LOT L, PF_PARTNO P, PF_OPERATION O where L.LOT = '" + lot + "' and  L.PARTNO = P.PARTNO and L.OPER_SID = O.OPER_SID order by  L.LOT, L.WO, L.PARTNO";
                        DataTable dtB = DBC.Select(sqlB);
                        if (dtB == null || dtB.Rows == null || dtB.Rows.Count == 0)
                        {

                        }
                        else
                        {
                            InsertCommandBuilder inserBuilder1 = new InsertCommandBuilder(DBC, "TC_XFK_FILE");
                            inserBuilder1.InsertColumn("TC_XFK001", dtB.Rows[0]["LOT"].ToString());
                            inserBuilder1.InsertColumn("TC_XFK003", dtB.Rows[0]["WO"].ToString());
                            inserBuilder1.InsertColumn("TC_XFK004", dtB.Rows[0]["PARTNO"].ToString());
                            inserBuilder1.InsertColumn("TC_XFK005", dtB.Rows[0]["PART_NAME"].ToString());
                            inserBuilder1.InsertColumn("TC_XFK006", dtB.Rows[0]["PRODUCT"].ToString());
                            inserBuilder1.InsertColumn("TC_XFK007", dtB.Rows[0]["OPER_SEQ"].ToString());
                            inserBuilder1.InsertColumn("TC_XFK008", dtB.Rows[0]["OPERATION_NO"].ToString());
                            inserBuilder1.InsertColumn("TC_XFK009", dtB.Rows[0]["OPERATION"].ToString());
                            inserBuilder1.InsertColumn("TC_XFKUSER1", this.User.Identity.Name);
                            inserBuilder1.InsertColumn("TC_XFKDATE1", txnTime);
                            inserBuilder1.InsertColumn("TC_XFKTIME1", string.Format("{0:HH:mm:ss}", txnTime));
                            inserBuilder1.InsertColumn("TC_XFKCHECK", "2");
                            inserBuilder1.InsertColumn("TC_APNAME", "TW_WipLocationH01");

                            inserBuilder1.DoTransaction();
                        }

                    }
                }
            }

            AlertDialogMessage(this.Page, "H01運送申請成功");

            Taiflex.Mplus.MPLUS mplus = new Taiflex.Mplus.MPLUS();
            mplus.PushNotificationText("MES", "kbBRssPC", "8", "T", "", "", "20230612038231331686554439654777", string.Format("{0} H01 運送申請，群組運輸條碼：{1}，起點：{2} ~ 迄點：{3}，收貨單位：{4}", this.User.Identity.Name + "/" + user_name, Text_Group.Text, fab_from_M, fab_to_M, dept_M));

            Text_Lot.Text = String.Empty;
            Text_Group.Text = String.Empty;
            dvData = null;
            gvData.DataSource = dvData;
            gvData.DataBind();
            CalData1();
            Label_phone.Text = "";            
        }


        #endregion

        protected void gvData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (dvData == null || dvData.Count == 0) return;

                dvData.Table.Rows.RemoveAt(e.RowIndex);
                dvData.Table.AcceptChanges();
                gvData.DataSource = dvData;
                gvData.DataBind();
            }
            catch (Exception ex)
            {
                WriteClientMessage(this.Page, MessageType.Exception, ex.Message);
            }
        }


        public void CalData1()
        {
            int sum = 0;
            for (int i = 0; i < gvData.Rows.Count; i++)
            {

                sum++;

            }
            if (sum > 0)
            {
                Label_Sum.Text = "共" + sum + "筆";

            }
            else
            {
                Label_Sum.Text = "共0筆";
            }
        }

        protected void Drop_Dept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Drop_Dept.SelectedItem.ToString() == "合成一課危險倉" || Drop_Dept.SelectedItem.ToString() == "合成二課危險倉")
            {
                String fnum = String.Empty;
                while (true)
                {
                    Random myObject = new Random();
                    int ranNum = myObject.Next(1, 9999);
                    fnum = "B" + String.Format("{0:0000}", ranNum);

                    string sql = String.Empty;
                    sql = "select * from XX_WIP_LOCATION where PACKAGE_NO = '" + fnum + "'  AND STATUS IN (10 , 20, 30) ";
                    DataTable dt = DBC.Select(sql);

                    if (dt == null || dt.Rows == null || dt.Rows.Count == 0)
                    {
                        break;
                    }
                }


                Text_Group.Text = fnum;
            }
        }

        protected void Drop_Machine_SelectIndexChanged(object sender, EventArgs e)
        {
            //if (Drop_Machine.SelectedValue == "T1")
            //{
            //    Label_phone.Text = "一廠 T1 71911";
            //}
            switch (Drop_Machine.SelectedValue)
            {
                case "T1":
                    Label_phone.Text = "一廠 T1 71911";
                    break;
                case "T2":
                    Label_phone.Text = "一廠 T2 71911";
                    break;
                case "T3":
                    Label_phone.Text = "二廠 T3 72912";
                    break;
                case "T5":
                    Label_phone.Text = "二廠 T5 72915";
                    break;
                case "T6":
                    Label_phone.Text = "二廠 T6 72918";
                    break;
                case "T7":
                    Label_phone.Text = "二廠 T7 72920";
                    break;
                case "T8":
                    Label_phone.Text = "三廠 T8 73926";
                    break;
                case "T9":
                    Label_phone.Text = "三廠 T9 73958";
                    break;
                case "T10":
                    Label_phone.Text = "三廠 T10 73970";
                    break;
                case "T11":
                    Label_phone.Text = "二廠 T11 72932";
                    break;
                case "T12":
                    Label_phone.Text = "三廠 T12 73857";
                    break;
                case "C1":
                    Label_phone.Text = "二廠 C1 72915";
                    break;
                case "C2":
                    Label_phone.Text = "三廠 C2 73958";
                    break;
                case "C3":
                    Label_phone.Text = "三廠 C3 73938";
                    break;
                case "C5":
                    Label_phone.Text = "三廠 C5 73970";
                    break;
                case "L1":
                    Label_phone.Text = "二廠 L1 72918";
                    break;
                case "L2":
                    Label_phone.Text = "一廠 L2 71912";
                    break;
                case "L3":
                    Label_phone.Text = "一廠 L3 71912";
                    break;
                case "L5":
                    Label_phone.Text = "三廠 L5 73933";
                    break;
                case "L6":
                    Label_phone.Text = "三廠 L6 73933";
                    break;
                case "L7":
                    Label_phone.Text = "三廠 L7 73933";
                    break;
                case "L8":
                    Label_phone.Text = "三廠 L8 73933";
                    break;
                case "L9":
                    Label_phone.Text = "三廠 L9 73933";
                    break;
                default:
                    Label_phone.Text = "";
                    break;
            }
        }

        protected void Text_ItemNo_TextChanged(object sender, EventArgs e)
        {

            if (Drop_FabFrom.SelectedIndex == 0)
            {
                AlertDialogMessage(this.Page, "請先選擇運送地點(起)");
                Text_ItemNo.Text = "";
                return;
            }

            if (Drop_FabTo.SelectedIndex == 0)
            {
                AlertDialogMessage(this.Page, "請先選擇運送地點(迄)");
                Text_ItemNo.Text = "";
                return;
            }

            if (Drop_Dept.SelectedIndex == 0)
            {
                AlertDialogMessage(this.Page, "請先選擇收貨單位");
                Text_ItemNo.Text = "";
                return;
            }

            if (Text_Group.Text == String.Empty)
            {
                AlertDialogMessage(this.Page, "自定群組欄位未輸入");
                Text_ItemNo.Text = "";
                return;
            }

            if (Drop_Dept.SelectedIndex == 1 && Drop_Machine.SelectedIndex == 0)
            {
                AlertDialogMessage(this.Page, "收貨單位為前製,線別欄位不能為空");
                Text_ItemNo.Text = "";

                return;
            }

            string sql = String.Empty;
            //sql = "select * from tc_sfa_file where ( tc_sfa001,tc_sfa002,tc_sfa003 ) in  (select tc_sfa001,tc_sfa002,tc_sfa003 from tc_sfa_file where tc_sfa006 = '" + Text_Lot.Text + "')";
            //sql = "select a.inv_no,a.lot from zz_inv_detail a left join zz_inv_master b on a.inv_no = b.inv_no where a.inv_no in (select inv_no from zz_inv_detail where lot = '" + Text_Lot.Text + "') and b.enable_flag = 'T' and b.INV_TYPE = '1'";
            sql = "select IT06 AS LOT from wipinv_transaction where factory = 'TAIFLEX' and it01 = 0 and it02 = 2 and substr(it03,5,length(it03)-4) = '" + Text_ItemNo.Text + "'";
            DataTable dt = DBC.Select(sql);
            DataView dv;
            if (dt == null || dt.Rows == null || dt.Rows.Count == 0)
            {
                dv = null;
                gvData.DataSource = dv;
                gvData.DataBind();
                AlertDialogMessage(this.Page, "輸入的發料單號查無發料批號");



            }
            else
            {
                if (dvData == null)
                {
                    DataTable dtA = new DataTable();
                    dtA.Columns.Add("LOT", typeof(string));
                    dtA.Columns.Add("FAB_FROM", typeof(string));
                    dtA.Columns.Add("FAB_TO", typeof(string));
                    dtA.Columns.Add("H04_DEPT", typeof(string));
                    dtA.Columns.Add("PACKAGE_NO", typeof(string));
                    dtA.Columns.Add("MACHINE", typeof(string));

                    dvData = new DataView(dtA);
                }

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {


                    DataRow dr = dvData.Table.NewRow();
                    dr["LOT"] = dt.Rows[i]["LOT"].ToString();
                    dr["FAB_FROM"] = Drop_FabFrom.SelectedValue;
                    dr["FAB_TO"] = Drop_FabTo.SelectedValue;
                    dr["H04_DEPT"] = Drop_Dept.SelectedValue;
                    dr["PACKAGE_NO"] = Text_Group.Text;

                    dvData.Table.Rows.Add(dr);
                    dvData.Table.AcceptChanges();
                }

                gvData.DataSource = dvData;
                gvData.DataBind();
            }
        }

        protected void Text_Lot_TextChanged(object sender, EventArgs e)
        {
            //if (Radio1.Checked == false && Radio2.Checked == false)
            //{
            //    AlertDialogMessage(this.Page, "請先選擇一批號帶入方式");
            //    Text_Lot.Text = "";
            //    return;
            //}

            if (Drop_FabFrom.SelectedIndex == 0)
            {
                AlertDialogMessage(this.Page, "請先選擇運送地點(起)");
                Text_Lot.Text = "";
                return;
            }

            if (Drop_FabTo.SelectedIndex == 0)
            {
                AlertDialogMessage(this.Page, "請先選擇運送地點(迄)");
                Text_Lot.Text = "";
                return;
            }

            if (Drop_Dept.SelectedIndex == 0)
            {
                AlertDialogMessage(this.Page, "請先選擇收貨單位");
                Text_Lot.Text = "";
                return;
            }

            //<是否為膠批號>
            if (!Text_Group.Text.ToString().StartsWith("B"))
            {
                if (Text_Lot.Text.Trim().Length > 0)
                {
                    string sqlG = String.Empty;
                    sqlG = "select distinct UNIT_1 from ZZ_WO_MTL_ISSUE where PARTNO in (select PARTNO from mt_lot where mtr_lot='" + Text_Lot.Text.Trim() + "')";
                    DataTable dtG = DBC.Select(sqlG);

                    if (dtG != null && dtG.Rows.Count > 0)
                    {
                        if (dtG.Rows[0]["UNIT_1"].ToString() == "KG")
                        {
                            String fnum = String.Empty;
                            while (true)
                            {
                                Random myObject = new Random();
                                int ranNum = myObject.Next(1, 9999);
                                fnum = "B" + String.Format("{0:0000}", ranNum);

                                string sql = String.Empty;
                                sql = "select * from XX_WIP_LOCATION where PACKAGE_NO = '" + fnum + "'  AND STATUS IN (10 , 20, 30) ";
                                DataTable dt = DBC.Select(sql);

                                if (dt == null || dt.Rows == null || dt.Rows.Count == 0)
                                {
                                    break;
                                }
                            }

                            Text_Group.Text = fnum;
                        }
                    }
                }
            }
            //</是否為膠批號>

            if (Text_Group.Text == String.Empty)
            {
                AlertDialogMessage(this.Page, "自定群組欄位未輸入");
                Text_Lot.Text = "";
                return;
            }

            if (Drop_Dept.SelectedIndex == 1 && Drop_Machine.SelectedIndex == 0)
            {
                AlertDialogMessage(this.Page, "收貨單位為前製,線別欄位不能為空");
                Text_Lot.Text = "";

                return;
            }

            try
            {
                if (Text_Lot.Text.Trim().Length <= 0) return;


                string sqlA = String.Empty;
                sqlA = "select * from XX_WIP_LOCATION where LOT = '" + Text_Lot.Text.Trim() + "'  AND STATUS IN (10 , 20, 30) ";
                DataTable dtA = DBC.Select(sqlA);

                if (dtA == null || dtA.Rows == null || dtA.Rows.Count == 0)
                {

                }
                else
                {
                    AlertDialogMessage(this.Page, "輸入的批號已有存在資料");
                    Text_Lot.Text = "";
                    return;
                }


                if (dvData == null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("LOT", typeof(string));
                    dt.Columns.Add("FAB_FROM", typeof(string));
                    dt.Columns.Add("FAB_TO", typeof(string));
                    dt.Columns.Add("H04_DEPT", typeof(string));
                    dt.Columns.Add("PACKAGE_NO", typeof(string));
                    dt.Columns.Add("MACHINE", typeof(string));

                    dvData = new DataView(dt);
                }

                if (Radio1.Checked == true)   //批號逐筆帶入
                {
                    DataRow dr = dvData.Table.NewRow();
                    dr["LOT"] = Text_Lot.Text;
                    dr["FAB_FROM"] = Drop_FabFrom.SelectedValue;
                    dr["FAB_TO"] = Drop_FabTo.SelectedValue;
                    dr["H04_DEPT"] = Drop_Dept.SelectedValue;
                    dr["PACKAGE_NO"] = Text_Group.Text;
                    dr["MACHINE"] = Drop_Machine.SelectedValue;

                    dvData.Table.Rows.Add(dr);
                    dvData.Table.AcceptChanges();

                    gvData.DataSource = dvData;
                    gvData.DataBind();
                }
                else   //依包裝單號帶入整批批號
                {
                    string sql = String.Empty;
                    //sql = "select * from tc_sfa_file where ( tc_sfa001,tc_sfa002,tc_sfa003 ) in  (select tc_sfa001,tc_sfa002,tc_sfa003 from tc_sfa_file where tc_sfa006 = '" + Text_Lot.Text + "')";
                    sql = "select a.inv_no,a.lot from zz_inv_detail a left join zz_inv_master b on a.inv_no = b.inv_no where a.inv_no in (select inv_no from zz_inv_detail where lot = '" + Text_Lot.Text + "') and b.enable_flag = 'T' and b.INV_TYPE = '1'";
                    DataTable dt = DBC.Select(sql);
                    DataView dv;
                    if (dt == null || dt.Rows == null || dt.Rows.Count == 0)
                    {
                        //dv = null;
                        //gvData.DataSource = dv;
                        //gvData.DataBind();
                        //AlertDialogMessage(this.Page, "輸入的批號查無包裝單號");

                        Boolean item = false;
                        for (int i = 0; i < gvData.Rows.Count; i++)
                        {
                            if (gvData.Rows[i].Cells[1].Text == Text_Lot.Text)
                            {
                                item = true;
                            }
                        }

                        if (item == true)
                        {
                            AlertDialogMessage(this.Page, "所輸批號已存在於下方資料列表中");
                        }
                        else
                        {
                            DataRow dr = dvData.Table.NewRow();
                            dr["LOT"] = Text_Lot.Text;
                            dr["FAB_FROM"] = Drop_FabFrom.SelectedValue;
                            dr["FAB_TO"] = Drop_FabTo.SelectedValue;
                            dr["H04_DEPT"] = Drop_Dept.SelectedValue;
                            dr["PACKAGE_NO"] = Text_Group.Text;
                            dr["MACHINE"] = Drop_Machine.SelectedValue;

                            dvData.Table.Rows.Add(dr);
                            dvData.Table.AcceptChanges();

                            gvData.DataSource = dvData;
                            gvData.DataBind();
                        }

                    }
                    else
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {

                            //string d = dt.Rows[i]["TC_SFA001"].ToString();
                            //string w = dt.Rows[i]["TC_SFA003"].ToString();
                            //string s = dt.Rows[i]["TC_SFA002"].ToString();
                            //string package_no = d.Substring(0, 10).Trim() + "_" + w + "_" + s;

                            DataRow dr = dvData.Table.NewRow();
                            dr["LOT"] = dt.Rows[i]["LOT"].ToString();
                            dr["FAB_FROM"] = Drop_FabFrom.SelectedValue;
                            dr["FAB_TO"] = Drop_FabTo.SelectedValue;
                            dr["H04_DEPT"] = Drop_Dept.SelectedValue;
                            dr["PACKAGE_NO"] = Text_Group.Text;

                            dvData.Table.Rows.Add(dr);
                            dvData.Table.AcceptChanges();
                        }

                        gvData.DataSource = dvData;
                        gvData.DataBind();
                    }
                }

                Text_Lot.Text = String.Empty;
                CalData1();
                Text_Lot.Focus();
            }
            catch (Exception ex)
            {
                Text_Lot.Text = "";
                Text_Lot.Focus();
                WriteClientMessage(this.Page, MessageType.Exception, ex.Message);
            }
        }

    }
}
