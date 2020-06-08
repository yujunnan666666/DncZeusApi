using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Configuration;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using static System.Net.Mime.MediaTypeNames;
//using System.Collections;
//using System.Configuration;
//using System.Runtime.InteropServices;
namespace ZDClass
{
    public class CommonUtils
    {
        
        //private String CONFIGINI = Application.StartupPath + "\\ClientCfg.ini";
        private String CONFIGINI = "\\ClientCfg.ini";
        private String SECTION = "DataBase";

        private const int SELECTCOMMAND_COMMANDTIMEOUT_DEFAULT = 1000;   //等待命令执行的时间
        private String DATABASEOWNERNAME = "dbo.";

        private string GetConnString(string vs_section, string _path = null)
        {
            if (string.IsNullOrEmpty(_path))
            {
               // _path = CONFIGINI;
            }
            OperateIni ini = new OperateIni();
            string ls_connString = ini.ReadIni_Database(vs_section, _path);
            return ls_connString;
        }

        public void Close(SqlConnection sqlcon)
        {
            if (sqlcon != null)
            {
                sqlcon.Close();
            }
        }

        private SqlCommand CreateCommand(string procName, SqlParameter[] prams, SqlConnection sqlcon)
        {
            SqlCommand cmd = new SqlCommand(procName, sqlcon);
            cmd.CommandTimeout = SELECTCOMMAND_COMMANDTIMEOUT_DEFAULT;
            cmd.CommandType = CommandType.StoredProcedure;
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
            cmd.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return cmd;
        }

        public SqlDataAdapter dataAdapterQuery(string theSQL, string _section = null)
        {
            if (string.IsNullOrEmpty(_section))
            {
                _section = SECTION;
            }
            SqlConnection con = ConnOpen(_section);
            SqlCommand cmd = new SqlCommand(theSQL, con);
            cmd.CommandTimeout = SELECTCOMMAND_COMMANDTIMEOUT_DEFAULT;
            SqlDataAdapter myDA = new SqlDataAdapter();
            myDA.SelectCommand = cmd;
            Close(con);
            return myDA;
        }

        public DataSet dataSetQuery(string theSQL, string _section = null)
        {
            if (string.IsNullOrEmpty(_section))
            {
                _section = SECTION;
            }
            SqlConnection con = ConnOpen(_section);
            SqlCommand cmd = new SqlCommand(theSQL, con);
            cmd.CommandTimeout = SELECTCOMMAND_COMMANDTIMEOUT_DEFAULT;
            SqlDataAdapter myDA = new SqlDataAdapter();
            myDA.SelectCommand = cmd;
            DataSet myDS = new DataSet();
            myDA.Fill(myDS);
            Close(con);
            return myDS;
        }

        public void Dispose(SqlConnection sqlcon)
        {
            if (sqlcon != null)
            {
                sqlcon.Dispose();
                sqlcon = null;
            }
        }

        public DataSet dsRunProc(string procName, SqlParameter[] prams, string _section = null)
        {
            if (string.IsNullOrEmpty(_section))
            {
                _section = SECTION;
            }
            SqlConnection con = ConnOpen(_section);
            DataSet ds = new DataSet();
            SqlCommand cmd = this.CreateCommand(procName, prams, con);
            cmd.CommandTimeout = SELECTCOMMAND_COMMANDTIMEOUT_DEFAULT;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            Close(con);
            return ds;
        }


        public String GetDBFuncFullName(string pDBFuncName)
        {
            return (DATABASEOWNERNAME + pDBFuncName);
        }

        public SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
        {
            return this.MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        }

        public SqlParameter MakeOutParam(string ParamName, SqlDbType DbType, int Size)
        {
            return this.MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null);
        }

        public SqlParameter MakeParam(string ParamName, SqlDbType DbType, int Size, ParameterDirection Direction, object Value)
        {
            SqlParameter param;
            if (Size > 0)
            {
                param = new SqlParameter(ParamName, DbType, Size);
            }
            else
            {
                param = new SqlParameter(ParamName, DbType);
            }
            param.Direction = Direction;
            if ((Direction != ParameterDirection.Output) || (Value != null))
            {
                param.Value = Value;
            }
            return param;
        }

        public SqlParameter MakeReturnParam(string ParamName, SqlDbType DbType, int Size)
        {
            return this.MakeParam(ParamName, DbType, Size, ParameterDirection.ReturnValue, null);
        }

        private SqlConnection ConnOpen(string _section = null)
        {
            if (string.IsNullOrEmpty(_section))
            {
                _section = SECTION;
            }

            string ls_ConnString = GetConnString(_section);
            SqlConnection sqlcon = new SqlConnection(ls_ConnString);

            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            return sqlcon;
        }

        public int ProcedureNOQuery(string pProcedureName, string _section = null)
        {
            if (string.IsNullOrEmpty(_section))
            {
                _section = SECTION;
            }
            SqlConnection con = ConnOpen(_section);
            SqlCommand cmd = new SqlCommand(pProcedureName, con);
            cmd.CommandTimeout = SELECTCOMMAND_COMMANDTIMEOUT_DEFAULT;
            cmd.CommandType = CommandType.StoredProcedure;
            int theResult = cmd.ExecuteNonQuery();
            Close(con);
            return theResult;
        }

        public int ProcedureNOQuery(string pProcedureName, ref ArrayList Parameters, string _section = null)
        {
            if (string.IsNullOrEmpty(_section))
            {
                _section = SECTION;
            }
            SqlConnection con = ConnOpen(_section);
            SqlCommand cmd = new SqlCommand(pProcedureName, con);
            cmd.CommandTimeout = SELECTCOMMAND_COMMANDTIMEOUT_DEFAULT;
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < Parameters.Count; i++)
            {
                cmd.Parameters.Add((SqlParameter)Parameters[i]);
            }
            int theResult = cmd.ExecuteNonQuery();
            Close(con);
            return theResult;
        }

        public DataSet procedurePageQuery(string strSQL, string strIDColName, int CurPage, int PageSize, out int RecNums, out int RecPages)
        {
            ArrayList alParam = new ArrayList();
            int iResult = 0;
            alParam.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, false, 0, 0, "", DataRowVersion.Default, 0));
            alParam.Add(new SqlParameter("@pSQL", SqlDbType.VarChar, 0x1388, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, strSQL));
            alParam.Add(new SqlParameter("@pIDCol", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, strIDColName));
            alParam.Add(new SqlParameter("@pOrder", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, ""));
            alParam.Add(new SqlParameter("@pCurPage", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, CurPage));
            alParam.Add(new SqlParameter("@pPageSize", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, PageSize));
            alParam.Add(new SqlParameter("@pRecNums", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, 0));
            alParam.Add(new SqlParameter("@pRecPages", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, 0));
            DataSet objDS = ProcedureQueryDataSet(GetDBFuncFullName("sp_CommQueryProc"), ref alParam);
            iResult = int.Parse(((SqlParameter)alParam[0]).Value.ToString());
            RecNums = int.Parse(((SqlParameter)alParam[6]).Value.ToString());
            RecPages = int.Parse(((SqlParameter)alParam[7]).Value.ToString());
            return objDS;
        }

        public DataSet procedurePageQuery(string strSQL, string strIDColName, string strOrder, int CurPage, int PageSize, out int RecNums, out int RecPages)
        {
            ArrayList alParam = new ArrayList();
            int iResult = 0;
            alParam.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, false, 0, 0, "", DataRowVersion.Default, 0));
            alParam.Add(new SqlParameter("@pSQL", SqlDbType.VarChar, 0x1388, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, strSQL));
            alParam.Add(new SqlParameter("@pIDCol", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, strIDColName));
            alParam.Add(new SqlParameter("@pOrder", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, strOrder));
            alParam.Add(new SqlParameter("@pCurPage", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, CurPage));
            alParam.Add(new SqlParameter("@pPageSize", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, PageSize));
            alParam.Add(new SqlParameter("@pRecNums", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, 0));
            alParam.Add(new SqlParameter("@pRecPages", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, 0));
            DataSet objDS = ProcedureQueryDataSet(GetDBFuncFullName("sp_CommQueryProc"), ref alParam);
            iResult = int.Parse(((SqlParameter)alParam[0]).Value.ToString());
            RecNums = int.Parse(((SqlParameter)alParam[6]).Value.ToString());
            RecPages = int.Parse(((SqlParameter)alParam[7]).Value.ToString());
            return objDS;
        }

        public SqlDataReader ProcedureQuery(string pProcedureName, string _section = null)
        {
            if (string.IsNullOrEmpty(_section))
            {
                _section = SECTION;
            }
            SqlConnection con = ConnOpen(_section);
            SqlCommand cmd = new SqlCommand(pProcedureName, con);
            cmd.CommandTimeout = SELECTCOMMAND_COMMANDTIMEOUT_DEFAULT;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader theResult = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            Close(con);
            return theResult;
        }

        public SqlDataReader ProcedureQuery(string pProcedureName, ref ArrayList Parameters, string _section = null)
        {
            if (string.IsNullOrEmpty(_section))
            {
                _section = SECTION;
            }
            SqlConnection con = ConnOpen(_section);
            SqlCommand cmd = new SqlCommand(pProcedureName, con);
            cmd.CommandTimeout = SELECTCOMMAND_COMMANDTIMEOUT_DEFAULT;
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < Parameters.Count; i++)
            {
                cmd.Parameters.Add((SqlParameter)Parameters[i]);
            }
            SqlDataReader theResult = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            Close(con);
            return theResult;
        }

        public DataSet ProcedureQueryDataSet(string pProcedureName, ref ArrayList Parameters, string _section = null)
        {
            if (string.IsNullOrEmpty(_section))
            {
                _section = SECTION;
            }
            SqlConnection con = ConnOpen(_section);
            SqlCommand cmd = new SqlCommand(pProcedureName, con);
            cmd.CommandTimeout = SELECTCOMMAND_COMMANDTIMEOUT_DEFAULT;
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < Parameters.Count; i++)
            {
                cmd.Parameters.Add((SqlParameter)Parameters[i]);
            }
            DataSet objDS = new DataSet();
            new SqlDataAdapter(cmd).Fill(objDS);
            Close(con);
            return objDS;
        }

        public DataSet ProcPages(string SqlString, string sID, string SortField, int CurrentPageIndex, int PageSize, out int pRecNums, out int RecPages)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@pSQL", SqlDbType.VarChar, 9999999, SqlString), this.MakeInParam("@pIDCol", SqlDbType.VarChar, 500, sID), this.MakeInParam("@pOrder", SqlDbType.VarChar, 500, SortField), this.MakeInParam("@pCurPage", SqlDbType.Int, 4, CurrentPageIndex), this.MakeInParam("@pPageSize", SqlDbType.Int, 4, PageSize), this.MakeOutParam("@pRecNums", SqlDbType.Int, 4), this.MakeOutParam("@pRecPages", SqlDbType.Int, 4) };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("sp_CommQueryProc"), prams);
            pRecNums = Convert.ToInt32(prams[5].Value);
            RecPages = Convert.ToInt32(prams[6].Value);
            return objDS;
        }

        public DataSet ProcPagesb(string StrSelect, string StrFrom, string StrWhere, string StrOrder, int PageIndex, int PageSize, out int ItemCount, out int PageCount)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@StrSelect", SqlDbType.VarChar, 8000, StrSelect), this.MakeInParam("@StrFrom", SqlDbType.VarChar, 8000, StrFrom), this.MakeInParam("@StrWhere", SqlDbType.VarChar, 8000, StrWhere), this.MakeInParam("@StrOrder", SqlDbType.VarChar, 1000, StrOrder), this.MakeInParam("@PageIndex", SqlDbType.Int, 4, PageIndex), this.MakeInParam("@PageSize", SqlDbType.Int, 4, PageSize), this.MakeOutParam("@ItemCount", SqlDbType.Int, 4), this.MakeOutParam("@PageCount", SqlDbType.Int, 4), this.MakeOutParam("@SqlQuery", SqlDbType.VarChar, 8000) };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("sp_CommQueryProcb"), prams);
            ItemCount = Convert.ToInt32(prams[6].Value);
            PageCount = Convert.ToInt32(prams[7].Value);
            return objDS;
        }
        //用户权限部门
        public DataSet BudgetUserDepartmentJD(string userno)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@userno", SqlDbType.VarChar, 8000, userno),
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_UserDepartmentJD"), prams);
            return objDS;
        }
        //费用明细报表
        public DataSet BudgetCostDetail(string isBudAdmin, string budgetYear, string departmentId, string accountID, string userno, string Org)
        {
            SqlParameter[] prams = new SqlParameter[] {
                                                        this.MakeInParam("@isBudAdmin", SqlDbType.VarChar, 8000, isBudAdmin),
                                                        this.MakeInParam("@budgetYear", SqlDbType.VarChar, 8000, budgetYear),
                                                        this.MakeInParam("@departmentId",   SqlDbType.VarChar, 99999, departmentId),
                                                        this.MakeInParam("@accountID",   SqlDbType.VarChar, 8000, accountID),
                                                        this.MakeInParam("@userno", SqlDbType.VarChar, 8000, userno),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_CostDetail"), prams);
            return objDS;
        }
        //BG141费用明细报表（特殊）
        public DataSet BudgetCostDetailSpecialExport(string isBudAdmin, string budgetYear, string budgetOrg, string departmentId, string searchDepartment, string accountID, string userno, string Org)
        {
            SqlParameter[] prams = new SqlParameter[] {
                                                        this.MakeInParam("@isBudAdmin", SqlDbType.VarChar, 8000, isBudAdmin),
                                                        this.MakeInParam("@budgetYear", SqlDbType.VarChar, 8000, budgetYear),
                                                        this.MakeInParam("@budgetOrg", SqlDbType.VarChar, 8000, budgetOrg),
                                                        this.MakeInParam("@departmentId",   SqlDbType.VarChar, 99999, departmentId),
                                                        this.MakeInParam("@searchDepartment",   SqlDbType.VarChar, 99999, searchDepartment),
                                                        this.MakeInParam("@accountID",   SqlDbType.VarChar, 8000, accountID),
                                                        this.MakeInParam("@userno", SqlDbType.VarChar, 8000, userno),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_CostDetailSpecialExport"), prams);
            return objDS;
        }
        //费用明细报表（特殊）
        public DataSet BudgetCostDetailSpecial(string isBudAdmin, string budgetYear, string departmentId, string searchDepartment, string accountID, string userno, string Org)
        {
            SqlParameter[] prams = new SqlParameter[] {
                                                        this.MakeInParam("@isBudAdmin", SqlDbType.VarChar, 8000, isBudAdmin),
                                                        this.MakeInParam("@budgetYear", SqlDbType.VarChar, 8000, budgetYear),
                                                        this.MakeInParam("@departmentId",   SqlDbType.VarChar, 99999, departmentId),
                                                         this.MakeInParam("@searchDepartment",   SqlDbType.VarChar, 99999, searchDepartment),
                                                        this.MakeInParam("@accountID",   SqlDbType.VarChar, 8000, accountID),
                                                        this.MakeInParam("@userno", SqlDbType.VarChar, 8000, userno),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_CostDetailSpecial"), prams);
            return objDS;
        }
        //费用明细操作列表（特殊）
        public DataSet BudgetCostDetailSpecialOP(string isBudAdmin, string budgetYear, string budgetOrg, string departmentId, string LikedepartmentCode, string accountID, string userno, string Org, int CurrentPageIndex, int PageSize, out int pRecNums, out int RecPages)
        {
            SqlParameter[] prams = new SqlParameter[] {
                                                        this.MakeInParam("@isBudAdmin", SqlDbType.VarChar, 8000, isBudAdmin),
                                                        this.MakeInParam("@budgetYear", SqlDbType.VarChar, 8000, budgetYear),
                                                        this.MakeInParam("@budgetOrg", SqlDbType.VarChar, 8000, budgetOrg),
                                                        this.MakeInParam("@departmentId",   SqlDbType.VarChar, 99999, departmentId),
                                                        this.MakeInParam("@LikedepartmentCode",   SqlDbType.VarChar, 99999, LikedepartmentCode),
                                                        this.MakeInParam("@accountID",   SqlDbType.VarChar, 8000, accountID),
                                                        this.MakeInParam("@userno", SqlDbType.VarChar, 8000, userno),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@pCurPage", SqlDbType.Int, 4, CurrentPageIndex),
                                                        this.MakeInParam("@pPageSize", SqlDbType.Int, 4, PageSize),
                                                        this.MakeOutParam("@pRecNums", SqlDbType.Int, 4),
                                                        this.MakeOutParam("@pRecPages", SqlDbType.Int, 4)
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_CostDetailSpecialOP"), prams);
            pRecNums = Convert.ToInt32(prams[10].Value);
            RecPages = Convert.ToInt32(prams[11].Value);
            return objDS;
        }
        //费用明细操作列表预算科目(报表权限 导出)
        public DataSet BudgetUserAccountSpecialExport(string userisBudAdmin, string Org, string userno)
        {
            SqlParameter[] prams = new SqlParameter[] {
                                                        this.MakeInParam("@userisBudAdmin", SqlDbType.VarChar, 8000, userisBudAdmin),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@userno", SqlDbType.VarChar, 8000, userno),
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_UserAccountSpecialExport"), prams);
            return objDS;
        }
        //费用明细操作列表预算科目(报表权限)
        public DataSet BudgetUserAccountSpecial(string userisBudAdmin, string Org, string userno)
        {
            SqlParameter[] prams = new SqlParameter[] {
                                                        this.MakeInParam("@userisBudAdmin", SqlDbType.VarChar, 8000, userisBudAdmin),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@userno", SqlDbType.VarChar, 8000, userno),
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_UserAccountSpecial"), prams);
            return objDS;
        }
        //编制数据导出
        public DataSet BudgetCostExport(string isBudAdmin, string budgetYear, string departmentId, string userno, string Org)
        {
            SqlParameter[] prams = new SqlParameter[] {
                                                        this.MakeInParam("@isBudAdmin", SqlDbType.VarChar, 8000, isBudAdmin),
                                                        this.MakeInParam("@budgetYear", SqlDbType.VarChar, 8000, budgetYear),
                                                        this.MakeInParam("@departmentId",   SqlDbType.VarChar, 99999, departmentId),
                                                        this.MakeInParam("@userno", SqlDbType.VarChar, 8000, userno),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_CostExport"), prams);
            return objDS;
        }
        //损益表模板复制
        public DataSet Budget_InComeModleCopy(string incomeModleID, string username)
        {
            SqlParameter[] prams = new SqlParameter[] {
                                                        this.MakeInParam("@incomeModle", SqlDbType.VarChar, 8000, incomeModleID),
                                                        this.MakeInParam("@creuse", SqlDbType.VarChar, 8000, username),
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_InComeModleCopy"), prams);
            return objDS;
        }
        //损益表数据(按组织)
        public DataSet Budget_UserBusiness(string userno, string Org)
        {
            SqlParameter[] prams = new SqlParameter[] {
                                                        this.MakeInParam("@userno", SqlDbType.VarChar, 8000, userno),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_UserBusiness"), prams);
            return objDS;
        }
        //损益表数据
        public DataSet BudgetInComeSummary(string Year, string incomeModle, string Business, string budgetOrg, string DepartmentID, string LikedepartmentCode, string Org, string userno)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@Year", SqlDbType.VarChar, 8000, Year),
                                                        this.MakeInParam("@incomeModle", SqlDbType.VarChar, 10, incomeModle),
                                                        this.MakeInParam("@Business",   SqlDbType.VarChar, 10, Business),
                                                        this.MakeInParam("@budgetOrg", SqlDbType.VarChar, 10, budgetOrg),
                                                        this.MakeInParam("@budgetDepartment", SqlDbType.VarChar, 10, DepartmentID),
                                                        this.MakeInParam("@LikedepartmentCode", SqlDbType.VarChar, 20, LikedepartmentCode),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@userno", SqlDbType.VarChar, 8000, userno),
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_InComeSummary"), prams);
            return objDS;
        }
        //损益表数据导出
        public DataSet BudgetInComeSummaryExport(string Year, string incomeModle, string Business, string budgetOrg, string Org, string userno)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@Year", SqlDbType.VarChar, 8000, Year),
                                                        this.MakeInParam("@incomeModle", SqlDbType.VarChar, 10, incomeModle),
                                                        this.MakeInParam("@Business",   SqlDbType.VarChar, 10, Business),
                                                        this.MakeInParam("@budgetOrg", SqlDbType.VarChar, 10, budgetOrg),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@userno", SqlDbType.VarChar, 8000, userno),
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_InComeSummaryExport"), prams);
            return objDS;
        }
        //损益表数据(按组织)
        public DataSet BudgetInComeSummaryOrg(string Year, string incomeModle, string Business, string budgetOrg, string Org, string userno)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@Year", SqlDbType.VarChar, 8000, Year),
                                                        this.MakeInParam("@incomeModle", SqlDbType.VarChar, 10, incomeModle),
                                                        this.MakeInParam("@Business",   SqlDbType.VarChar, 10, Business),
                                                        this.MakeInParam("@budgetOrg", SqlDbType.VarChar, 10, budgetOrg),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@userno", SqlDbType.VarChar, 8000, userno),
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_InComeSummaryOrg"), prams);
            return objDS;
        }
        //预算对比分析报表
        public DataSet BudgetContrastiveAnalysis(string budgetYear, string Org, int departmentId, string costTypeId, string searchstr)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@budgetYear", SqlDbType.VarChar, 8000, budgetYear),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@departmentId", SqlDbType.VarChar, 500, departmentId),
                                                        this.MakeInParam("@costTypeId", SqlDbType.VarChar, 8000, costTypeId),
                                                        this.MakeInParam("@searchstr", SqlDbType.VarChar, 8000, searchstr)
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_ContrastiveAnalysis"), prams);
            return objDS;
        }
        //预算对比分析报表
        public DataSet BudgetContrastiveAnalysisExport(string budgetYear, string Org, int departmentId, string costTypeId, string searchstr)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@budgetYear", SqlDbType.VarChar, 8000, budgetYear),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@departmentId", SqlDbType.VarChar, 500, departmentId),
                                                        this.MakeInParam("@costTypeId", SqlDbType.VarChar, 8000, costTypeId),
                                                        this.MakeInParam("@searchstr", SqlDbType.VarChar, 8000, searchstr)
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_ContrastiveAnalysisExport"), prams);
            return objDS;
        }
        //预算末级部门对比分析报表
        public DataSet BudgetContrastiveAnalysisLast(string budgetYear, string Org, int departmentId, string costTypeId, string searchstr)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@budgetYear", SqlDbType.VarChar, 8000, budgetYear),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@departmentId", SqlDbType.VarChar, 500, departmentId),
                                                        this.MakeInParam("@costTypeId", SqlDbType.VarChar, 8000, costTypeId),
                                                        this.MakeInParam("@searchstr", SqlDbType.VarChar, 8000, searchstr)
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_ContrastiveAnalysisLast"), prams);
            return objDS;
        }
        //预算末级部门对比分析报表(导出)
        public DataSet BudgetContrastiveAnalysisLastExport(string budgetYear, string Org, int departmentId, string costTypeId, string searchstr)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@budgetYear", SqlDbType.VarChar, 8000, budgetYear),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@departmentId", SqlDbType.VarChar, 500, departmentId),
                                                        this.MakeInParam("@costTypeId", SqlDbType.VarChar, 8000, costTypeId),
                                                        this.MakeInParam("@searchstr", SqlDbType.VarChar, 8000, searchstr)
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_ContrastiveAnalysisLastExport"), prams);
            return objDS;
        }
        //预算汇总表
        public DataSet BudgetSummary(string budgetYear, string Org, int departmentId, string searchstr)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@budgetYear", SqlDbType.VarChar, 8000, budgetYear),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@departmentId", SqlDbType.VarChar, 500, departmentId),
                                                         this.MakeInParam("@searchstr", SqlDbType.VarChar, 8000, searchstr)
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_Summary"), prams);
            return objDS;
        }
        //预算汇总表(导出)
        public DataSet BudgetSummaryExport(string budgetYear, string Org, int departmentId, string searchstr)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@budgetYear", SqlDbType.VarChar, 8000, budgetYear),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@departmentId", SqlDbType.VarChar, 500, departmentId),
                                                         this.MakeInParam("@searchstr", SqlDbType.VarChar, 8000, searchstr)
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_SummaryExport"), prams);
            return objDS;
        }
        //部门预算汇总表(财务)
        public DataSet Budget_SummaryForAccountFinance(string budgetYear, string Org, int departmentId, string searchstr)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@budgetYear", SqlDbType.VarChar, 8000, budgetYear),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@departmentId", SqlDbType.VarChar, 500, departmentId),
                                                        this.MakeInParam("@searchstr", SqlDbType.VarChar, 8000, searchstr)
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_SummaryForAccountFinance"), prams);
            return objDS;
        }
        //部门属性对比
        public DataSet Budget_SummaryForDepartmentType(string budgetYear, string Org, int departmentId, string searchstr)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@budgetYear", SqlDbType.VarChar, 8000, budgetYear),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@departmentId", SqlDbType.VarChar, 500, departmentId),
                                                        this.MakeInParam("@searchstr", SqlDbType.VarChar, 8000, searchstr)
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_SummaryForDepartmentType"), prams);
            return objDS;
        }
        //部门科目属性汇总表
        public DataSet BudgetSummaryForAccountType(string isBudAdmin, string accountType, string Category, string CostType, string isAccount, string budgetYear, string budgetOrg, string departmentId, string LikedepartmentCode, string accountID, string userno, string Org, int CurrentPageIndex, int PageSize, out int pRecNums, out int RecPages)
        {
            SqlParameter[] prams = new SqlParameter[] {
                                                        this.MakeInParam("@isBudAdmin", SqlDbType.VarChar, 8000, isBudAdmin),
                                                        this.MakeInParam("@accountType", SqlDbType.VarChar, 8000, accountType),
                                                        this.MakeInParam("@Category", SqlDbType.VarChar, 8000, Category),
                                                        this.MakeInParam("@CostType", SqlDbType.VarChar, 8000, CostType),
                                                        this.MakeInParam("@isAccount", SqlDbType.VarChar, 8000, isAccount),
                                                        this.MakeInParam("@budgetYear", SqlDbType.VarChar, 8000, budgetYear),
                                                        this.MakeInParam("@budgetOrg", SqlDbType.VarChar, 8000, budgetOrg),
                                                        this.MakeInParam("@departmentId",   SqlDbType.VarChar, 99999, departmentId),
                                                        this.MakeInParam("@LikedepartmentCode",   SqlDbType.VarChar, 99999, LikedepartmentCode),
                                                        this.MakeInParam("@accountID",   SqlDbType.VarChar, 8000, accountID),
                                                        this.MakeInParam("@userno", SqlDbType.VarChar, 8000, userno),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@pCurPage", SqlDbType.Int, 4, CurrentPageIndex),
                                                        this.MakeInParam("@pPageSize", SqlDbType.Int, 4, PageSize),
                                                        this.MakeOutParam("@pRecNums", SqlDbType.Int, 4),
                                                        this.MakeOutParam("@pRecPages", SqlDbType.Int, 4)
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_SummaryForAccountType"), prams);
            pRecNums = Convert.ToInt32(prams[14].Value);
            RecPages = Convert.ToInt32(prams[15].Value);
            return objDS;
        }
        //多部门对比表
        public DataSet BudgetSummaryForAccount(string budgetYear, string Org, int departmentId, string searchstr)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@budgetYear", SqlDbType.VarChar, 8000, budgetYear),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@departmentId", SqlDbType.VarChar, 500, departmentId),
                                                         this.MakeInParam("@searchstr", SqlDbType.VarChar, 8000, searchstr)
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_SummaryForAccount"), prams);
            return objDS;
        }
        //多部门对比表Test
        public DataSet BudgetSummaryForAccountTest(string budgetYear, string Org, int departmentId, string costTypeId, string searchstr)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@budgetYear", SqlDbType.VarChar, 8000, budgetYear),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@departmentId", SqlDbType.VarChar, 500, departmentId),
                                                        this.MakeInParam("@costTypeId", SqlDbType.VarChar, 500, costTypeId),
                                                        this.MakeInParam("@searchstr", SqlDbType.VarChar, 8000, searchstr)
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_SummaryForAccount_Test"), prams);
            return objDS;
        }
        //预算部门树状图数据 (预算部门的)
        public DataSet BudgetDepartmentIsLastTree(string userisBudAdmin, string Org, string departmentId)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@userisBudAdmin", SqlDbType.VarChar, 8000, userisBudAdmin),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@departmentId", SqlDbType.VarChar, 9999999, departmentId)
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_DepartmentIsLastTree"), prams);
            return objDS;
        }
        //部门树状图数据 （全部部门）
        public DataSet BudgetDepartmentTree(string userisBudAdmin, string Org, string departmentId)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@userisBudAdmin", SqlDbType.VarChar, 8000, userisBudAdmin),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@departmentId", SqlDbType.VarChar, 500, departmentId)
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_DepartmentTree"), prams);
            return objDS;
        }
        //预算科目树状图数据 
        public DataSet BudgetAccountTree(string budgetYear, string userisBudAdmin, string Org, string userno, string departmentId, string orderbystr)
        {
            SqlParameter[] prams = new SqlParameter[] {
                                                        this.MakeInParam("@budgetYear", SqlDbType.VarChar, 8000, budgetYear),
                                                        this.MakeInParam("@userisBudAdmin", SqlDbType.VarChar, 8000, userisBudAdmin),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@userno", SqlDbType.VarChar, 8000, userno),
                                                        this.MakeInParam("@departmentId", SqlDbType.VarChar, 500, departmentId),
                                                        this.MakeInParam("@orderbystr", SqlDbType.VarChar, 8000, orderbystr),
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_AccountTree"), prams);
            return objDS;
        }
        //预算科目树状图数据 (预算部门可为空)
        public DataSet BudgetAccountTreeCost(string budgetYear, string userisBudAdmin, string Org, string userno, string departmentId, string orderbystr)
        {
            SqlParameter[] prams = new SqlParameter[] {
                                                        this.MakeInParam("@budgetYear", SqlDbType.VarChar, 8000, budgetYear),
                                                        this.MakeInParam("@userisBudAdmin", SqlDbType.VarChar, 8000, userisBudAdmin),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@userno", SqlDbType.VarChar, 8000, userno),
                                                        this.MakeInParam("@departmentId", SqlDbType.VarChar, 500, departmentId),
                                                        this.MakeInParam("@orderbystr", SqlDbType.VarChar, 8000, orderbystr),
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_AccountTreeCost"), prams);
            return objDS;
        }
        //预算科目树状图数据 (预算部门可为空)(报表权限)
        public DataSet BudgetAccountTreeCostSpecial(string budgetYear, string userisBudAdmin, string Org, string userno, string departmentId, string orderbystr)
        {
            SqlParameter[] prams = new SqlParameter[] {
                                                        this.MakeInParam("@budgetYear", SqlDbType.VarChar, 8000, budgetYear),
                                                        this.MakeInParam("@userisBudAdmin", SqlDbType.VarChar, 8000, userisBudAdmin),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@userno", SqlDbType.VarChar, 8000, userno),
                                                        this.MakeInParam("@departmentId", SqlDbType.VarChar, 500, departmentId),
                                                        this.MakeInParam("@orderbystr", SqlDbType.VarChar, 8000, orderbystr),
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_AccountTreeCostSpecial"), prams);
            return objDS;
        }
        //预算科目树状图数据 (全部科目不受权限表控制)
        public DataSet BudgetAllAccountTree(string budgetYear, string userisBudAdmin, string Org, string userno, string departmentId, string orderbystr)
        {
            SqlParameter[] prams = new SqlParameter[] {
                                                        this.MakeInParam("@budgetYear", SqlDbType.VarChar, 8000, budgetYear),
                                                        this.MakeInParam("@userisBudAdmin", SqlDbType.VarChar, 8000, userisBudAdmin),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@userno", SqlDbType.VarChar, 8000, userno),
                                                        this.MakeInParam("@departmentId", SqlDbType.VarChar, 500, departmentId),
                                                        this.MakeInParam("@orderbystr", SqlDbType.VarChar, 8000, orderbystr),
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_ALLAccountTree"), prams);
            return objDS;
        }
        //扩展字段明细
        public DataSet BudgetMiscodeexxMaterial(string ckindid, string departmentId, string strWhere)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@ckindid", SqlDbType.VarChar, 8000, ckindid),
                                                        this.MakeInParam("@departmentId", SqlDbType.VarChar, 8000, departmentId),
                                                        this.MakeInParam("@strWhere", SqlDbType.VarChar, 8000, strWhere),
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_MiscodeexxMaterial"), prams);
            return objDS;
        }
        //扩展字段明细
        public DataSet BudgetCheckMiscodeexxMaterial(string ckindid, string departmentId, string cdesc)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@ckindid", SqlDbType.VarChar, 8000, ckindid),
                                                        this.MakeInParam("@departmentId", SqlDbType.VarChar, 8000, departmentId),
                                                        this.MakeInParam("@cdesc", SqlDbType.VarChar, 8000, cdesc),
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_CheckMiscodeexxMaterial"), prams);
            return objDS;
        }
        //预算计划年结-反年结操作
        public DataSet BudgetPlanYearEnd(string Org, string Type, string creuser)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@Type", SqlDbType.VarChar, 8000, Type),
                                                        this.MakeInParam("@creuser", SqlDbType.VarChar, 500, creuser)
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_PlanYearEnd"), prams);
            return objDS;
        }

        //预算管控(销售预算达成率表保存)
        public DataSet BudgetSalesInRateSave(string hid, string Org, string budgetOrgID, string yyyymm, string reSaleIncome, string NreSaleIncome, string remark, string creuser)
        {
            SqlParameter[] prams = new SqlParameter[] {
                                                        this.MakeInParam("@hid", SqlDbType.VarChar, 8000, hid),
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@budgetOrgID", SqlDbType.VarChar, 8000, budgetOrgID),
                                                        this.MakeInParam("@yyyymm", SqlDbType.VarChar, 8000, yyyymm),
                                                        this.MakeInParam("@reSaleIncome", SqlDbType.VarChar, 8000, reSaleIncome),
                                                        this.MakeInParam("@NreSaleIncome", SqlDbType.VarChar, 8000, NreSaleIncome),
                                                        this.MakeInParam("@remark", SqlDbType.VarChar, 8000, remark),
                                                        this.MakeInParam("@creuser", SqlDbType.VarChar, 8000, creuser),
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_SalesInRateSave"), prams);
            return objDS;
        }

        //预算管控(结转)
        public DataSet BudgetCarryOver(string Org, string budgetOrgID, string yyyymm, string creuser)
        {
            SqlParameter[] prams = new SqlParameter[] {
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@budgetOrgID", SqlDbType.VarChar, 8000, budgetOrgID),
                                                        this.MakeInParam("@yyyymm", SqlDbType.VarChar, 8000, yyyymm),
                                                        this.MakeInParam("@creuser", SqlDbType.VarChar, 8000, creuser),
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_CarryOver"), prams);
            return objDS;
        }
        //预算使用（费用调整）
        public DataSet BudgetUseChangeDep(string Org, string @useHeadIds, string newBudgetOrgID, string newDepartmentID, string remark, string creuser)
        {
            SqlParameter[] prams = new SqlParameter[] {
                                                        this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@useHeadIds", SqlDbType.VarChar, 8000, useHeadIds),
                                                        this.MakeInParam("@newBudgetOrgID", SqlDbType.VarChar, 8000, newBudgetOrgID),
                                                        this.MakeInParam("@newDepartmentID", SqlDbType.VarChar, 8000, newDepartmentID),
                                                        this.MakeInParam("@remark", SqlDbType.VarChar, 8000, remark),
                                                        this.MakeInParam("@username", SqlDbType.VarChar, 8000, creuser),
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_UseChangeDep"), prams);
            return objDS;
        }

        //U9凭证转预算系统使用单
        public DataSet BudgetU9VoucherUse(string Org, string VoucherIds, string creuser, out string useDocnoS, out string Messgae)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@VoucherIds", SqlDbType.VarChar, 8000, VoucherIds),
                                                        this.MakeInParam("@username", SqlDbType.VarChar, 500, creuser),
                                                        this.MakeOutParam("@outUseHeadDocno", SqlDbType.VarChar, 8000),
                                                        this.MakeOutParam("@outMessage", SqlDbType.VarChar, 8000)
                                                        };
            //DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_U9VoucherUse"), prams);//MIS系统(存储过程放在)
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("sp_MisBudgetU9VoucherUse"), prams);//U9 (存储过程放在)
            useDocnoS = Convert.ToString(prams[3].Value);
            Messgae = Convert.ToString(prams[4].Value);
            return objDS;
        }

        public DataSet BudgetAccountDisCostList(string isBudAdmin, string userno, string Org, string SqlString, string sID, string SortField, int CurrentPageIndex, int PageSize, out int pRecNums, out int RecPages)
        {
            SqlParameter[] prams = new SqlParameter[] {
                                                       this.MakeInParam("@isBudAdmin", SqlDbType.VarChar, 9999999, isBudAdmin),
                                                        this.MakeInParam("@userno", SqlDbType.VarChar, 9999999, userno),
                                                         this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@pSQLWhere", SqlDbType.VarChar, 9999999, SqlString),
                                                        this.MakeInParam("@pIDCol", SqlDbType.VarChar, 500, sID),
                                                        this.MakeInParam("@pOrder", SqlDbType.VarChar, 500, SortField),
                                                        this.MakeInParam("@pCurPage", SqlDbType.Int, 4, CurrentPageIndex),
                                                        this.MakeInParam("@pPageSize", SqlDbType.Int, 4, PageSize),
                                                        this.MakeOutParam("@pRecNums", SqlDbType.Int, 4),
                                                        this.MakeOutParam("@pRecPages", SqlDbType.Int, 4)
                                                       };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_AccountDisCostList"), prams);
            pRecNums = Convert.ToInt32(prams[8].Value);
            RecPages = Convert.ToInt32(prams[9].Value);
            return objDS;
        }

        public DataSet BudgetAccountDisCostListExport(string isBudAdmin, string userno, string Org)
        {
            SqlParameter[] prams = new SqlParameter[] {
                                                       this.MakeInParam("@isBudAdmin", SqlDbType.VarChar, 9999999, isBudAdmin),
                                                        this.MakeInParam("@userno", SqlDbType.VarChar, 9999999, userno),
                                                         this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                       };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_AccountDisCostListExport"), prams);
            return objDS;
        }

        //预算调整单-审核弃审操作
        public DataSet BudgetChangeAuditing(string Org, string changedId, string type, string creuser)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@changedId", SqlDbType.VarChar, 8000, changedId),
                                                        this.MakeInParam("@type", SqlDbType.VarChar, 8000, type),
                                                        this.MakeInParam("@username", SqlDbType.VarChar, 500, creuser)
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_ChangeAuditing"), prams);
            return objDS;
        }
        //预算追加单-审核弃审操作
        public DataSet BudgetAddAuditing(string Org, string addHeadId, string type, string creuser)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@addHeadId", SqlDbType.VarChar, 8000, addHeadId),
                                                        this.MakeInParam("@type", SqlDbType.VarChar, 8000, type),
                                                        this.MakeInParam("@username", SqlDbType.VarChar, 500, creuser)
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_AddAuditing"), prams);
            return objDS;
        }
        //预算使用单-审核弃审操作
        public DataSet BudgetUseAuditing(string Org, string useHeadId, string type, string creuser)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org),
                                                        this.MakeInParam("@useHeadId", SqlDbType.VarChar, 8000, useHeadId),
                                                        this.MakeInParam("@type", SqlDbType.VarChar, 8000, type),
                                                        this.MakeInParam("@username", SqlDbType.VarChar, 500, creuser)
                                                        };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("Budget_UseAuditing"), prams);
            return objDS;
        }

        //指定要执行的存储过程
        public DataSet SingleProcPages(string StoprName, string SqlString, string sID, string SortField, int CurrentPageIndex, int PageSize, out int pRecNums, out int RecPages)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@pWhere", SqlDbType.VarChar, 8000, SqlString), this.MakeInParam("@pIDCol", SqlDbType.VarChar, 500, sID), this.MakeInParam("@pOrder", SqlDbType.VarChar, 500, SortField), this.MakeInParam("@pCurPage", SqlDbType.Int, 4, CurrentPageIndex), this.MakeInParam("@pPageSize", SqlDbType.Int, 4, PageSize), this.MakeOutParam("@pRecNums", SqlDbType.Int, 4), this.MakeOutParam("@pRecPages", SqlDbType.Int, 4) };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName(StoprName), prams);
            pRecNums = Convert.ToInt32(prams[5].Value);
            RecPages = Convert.ToInt32(prams[6].Value);
            return objDS;
        }

        //指定要执行的存储过程(PI250)
        public DataSet SingleProcPages_PI250(string StoprName, string orgno, DateTime thisDay, string EmpNo, string EmpName, string departno, string departname, string ids, int PageIndex, int PageSize, out int ItemCount, out int PageCount)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@orgno", SqlDbType.VarChar, 20, orgno),
                                                        this.MakeInParam("@thisDay", SqlDbType.DateTime, 20, thisDay),
                                                        this.MakeInParam("@EmpNo", SqlDbType.VarChar, 20, EmpNo),
                                                        this.MakeInParam("@EmpName", SqlDbType.VarChar, 20, EmpName),
                                                        this.MakeInParam("@departno", SqlDbType.VarChar, 20, departno),
                                                        this.MakeInParam("@departname", SqlDbType.VarChar, 500, departname),
                                                        this.MakeInParam("@ids", SqlDbType.VarChar, 500, ids),
                                                        this.MakeInParam("@PageIndex", SqlDbType.Int, 4, PageIndex),
                                                        this.MakeInParam("@PageSize", SqlDbType.Int, 4, PageSize),
                                                        this.MakeOutParam("@ItemCount", SqlDbType.Int, 4),
                                                        this.MakeOutParam("@PageCount", SqlDbType.Int, 4) };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName(StoprName), prams);
            ItemCount = Convert.ToInt32(prams[9].Value);
            PageCount = Convert.ToInt32(prams[10].Value);
            return objDS;
        }

        //指定要执行的存储过程(BOM220)
        public DataSet SingleProcPages_BOM220(string StoprName, string itemcode, long Org)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@strValue", SqlDbType.VarChar, 8000, itemcode), this.MakeInParam("@Org", SqlDbType.BigInt, 8, Org) };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName(StoprName), prams);
            return objDS;
        }
        //执行的存储过程(sp_ProduceRice)
        public DataSet SingleProcPages_ProduceRice(string monItemMaster, string itemCode, string bomtype, string Batchno, string Trseqno, string Docno, string listType, string Org, string username)
        {
            SqlParameter[] prams = new SqlParameter[] {
                                                        this.MakeInParam("@monItemMaster", SqlDbType.VarChar, 8000, monItemMaster),
                                                        this.MakeInParam("@itemCode", SqlDbType.VarChar, 8000, itemCode),
                                                        this.MakeInParam("@bomtype", SqlDbType.VarChar, 8000, bomtype),
                                                        this.MakeInParam("@Batchno", SqlDbType.VarChar, 8000, Batchno),
                                                        this.MakeInParam("@Trseqno", SqlDbType.VarChar, 8000, Trseqno),
                                                        this.MakeInParam("@Docno", SqlDbType.VarChar, 8000, Docno),
                                                        this.MakeInParam("@listType", SqlDbType.VarChar, 8000, listType),
                                                        this.MakeInParam("@Org", SqlDbType.BigInt, 8, Org),
                                                        this.MakeInParam("@username", SqlDbType.VarChar, 8000, username),
                                                    };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName("sp_ProduceRice"), prams);
            return objDS;
        }
        //指定要执行的存储过程(PRL130重获报价)
        public DataSet SingleProcPages_PRL130(string StoprName, string HID, string Org, string modUser)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@Hid", SqlDbType.VarChar, 8000, HID), this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org), this.MakeInParam("@modUser", SqlDbType.VarChar, 8000, modUser) };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName(StoprName), prams);
            return objDS;
        }
        //指定要执行的存储过程(PRL130全部重获报价)
        public DataSet SingleProcPages_PRL130ALL(string StoprName, string Org, string modUser)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@Org", SqlDbType.VarChar, 8000, Org), this.MakeInParam("@modUser", SqlDbType.VarChar, 8000, modUser) };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName(StoprName), prams);
            return objDS;
        }

        //指定要执行的存储过程(SO240)
        public DataSet SingleProcPages_SO240(string StoprName, string SqlString, string SortField, int PageIndex, int PageSize, out int ItemCount, out int PageCount)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@StrWhere", SqlDbType.VarChar, 8000, SqlString), this.MakeInParam("@StrOrder", SqlDbType.VarChar, 500, SortField), this.MakeInParam("@PageIndex", SqlDbType.Int, 4, PageIndex), this.MakeInParam("@PageSize", SqlDbType.Int, 4, PageSize), this.MakeOutParam("@ItemCount", SqlDbType.Int, 4), this.MakeOutParam("@PageCount", SqlDbType.Int, 4) };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName(StoprName), prams);
            ItemCount = Convert.ToInt32(prams[4].Value);
            PageCount = Convert.ToInt32(prams[5].Value);
            return objDS;
        } //指定要执行的存储过程(PRL150)
        public DataSet SingleProcPages_PRL150(string StoprName, string SqlString, string SortField, int PageIndex, int PageSize, out int ItemCount, out int PageCount)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@StrWhere", SqlDbType.VarChar, 8000, SqlString), this.MakeInParam("@StrOrder", SqlDbType.VarChar, 500, SortField), this.MakeInParam("@PageIndex", SqlDbType.Int, 4, PageIndex), this.MakeInParam("@PageSize", SqlDbType.Int, 4, PageSize), this.MakeOutParam("@ItemCount", SqlDbType.Int, 4), this.MakeOutParam("@PageCount", SqlDbType.Int, 4) };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName(StoprName), prams);
            ItemCount = Convert.ToInt32(prams[4].Value);
            PageCount = Convert.ToInt32(prams[5].Value);
            return objDS;
        }
        //指定要执行的存储过程(PRL150)
        public DataSet SingleProcPages_PRL1501(string StoprName, string SqlDate, string SqlString, string SortField, int PageIndex, int PageSize, out int ItemCount, out int PageCount)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@DateWhere", SqlDbType.VarChar, 8000, SqlDate), this.MakeInParam("@StrWhere", SqlDbType.VarChar, 8000, SqlString), this.MakeInParam("@StrOrder", SqlDbType.VarChar, 500, SortField), this.MakeInParam("@PageIndex", SqlDbType.Int, 4, PageIndex), this.MakeInParam("@PageSize", SqlDbType.Int, 4, PageSize), this.MakeOutParam("@ItemCount", SqlDbType.Int, 4), this.MakeOutParam("@PageCount", SqlDbType.Int, 4) };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName(StoprName), prams);
            ItemCount = Convert.ToInt32(prams[5].Value);
            PageCount = Convert.ToInt32(prams[6].Value);
            return objDS;
        }

        //指定要执行的存储过程(RP110)
        public DataSet SingleProcPages_RP110(string StoprName, string orgno, string SqlString, string sID, string SortField, int CurrentPageIndex, int PageSize, out int pRecNums, out int RecPages)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@orgno", SqlDbType.VarChar, 20, orgno), this.MakeInParam("@pWhere", SqlDbType.VarChar, 8000, SqlString), this.MakeInParam("@pIDCol", SqlDbType.VarChar, 500, sID), this.MakeInParam("@pOrder", SqlDbType.VarChar, 500, SortField), this.MakeInParam("@pCurPage", SqlDbType.Int, 4, CurrentPageIndex), this.MakeInParam("@pPageSize", SqlDbType.Int, 4, PageSize), this.MakeOutParam("@pRecNums", SqlDbType.Int, 4), this.MakeOutParam("@pRecPages", SqlDbType.Int, 4) };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName(StoprName), prams);
            pRecNums = Convert.ToInt32(prams[6].Value);
            RecPages = Convert.ToInt32(prams[7].Value);
            return objDS;
        }


        //指定要执行的存储过程(SOPIpa的接口信息)
        public DataSet SingleProcPages_SOPIpadMessage(string StoprName, string sCode, string lCode, string pCode)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@sCode", SqlDbType.VarChar, 8000, sCode), this.MakeInParam("@lCode", SqlDbType.VarChar, 8000, lCode), this.MakeInParam("@pCode", SqlDbType.VarChar, 8000, pCode) };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName(StoprName), prams);
            return objDS;
        }
        //指定要执行的存储过程(SOP线头看板的接口信息)
        public DataSet SingleProcPages_SOPLinerDataMessage(string StoprName, string sCode, string lCode)
        {
            SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@sCode", SqlDbType.VarChar, 8000, sCode), this.MakeInParam("@lCode", SqlDbType.VarChar, 8000, lCode) };
            DataSet objDS = this.dsRunProc(GetDBFuncFullName(StoprName), prams);
            return objDS;
        }
        //指定要执行的存储过程(PI240),积分核算
        //public DataSet PI240_Compute(string StoprName, string orgno, string trno, DateTime thisDay, string cType, string ConnCode = "mydns")
        //{
        //    SqlParameter[] prams = new SqlParameter[] { this.MakeInParam("@orgno", SqlDbType.VarChar, 20, orgno), this.MakeInParam("@trno", SqlDbType.VarChar, 20, trno), this.MakeInParam("@thisDay", SqlDbType.DateTime, 20, thisDay), this.MakeInParam("@cType", SqlDbType.Char, 1, cType) };
        //    DataSet objDS = this.dsRunProc2(GetDBFuncFullName(StoprName), prams);
        //    return objDS;
        //}

        public SqlDataReader Query(string theSQL, string _section = null)
        {
            if (string.IsNullOrEmpty(_section))
            {
                _section = SECTION;
            }
            SqlConnection con = ConnOpen(_section);
            SqlCommand cmd = new SqlCommand(theSQL, con);
            cmd.CommandTimeout = SELECTCOMMAND_COMMANDTIMEOUT_DEFAULT;
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public String QueryScalar(string theSQL, string _section = null)
        {
            if (string.IsNullOrEmpty(_section))
            {
                _section = SECTION;
            }
            SqlConnection con = ConnOpen(_section);
            SqlCommand cmd = new SqlCommand(theSQL, con);
            cmd.CommandTimeout = SELECTCOMMAND_COMMANDTIMEOUT_DEFAULT;
            string str = "";
            Object obj = cmd.ExecuteScalar();
            if (obj != null)
            {
                str = cmd.ExecuteScalar().ToString();

            }
            Close(con);
            return str;

        }

        public int RunProc(string procName, string _section = null)
        {
            if (string.IsNullOrEmpty(_section))
            {
                _section = SECTION;
            }
            SqlConnection con = ConnOpen(_section);
            SqlCommand cmd = this.CreateCommand(procName, null, con);
            cmd.CommandTimeout = SELECTCOMMAND_COMMANDTIMEOUT_DEFAULT;
            cmd.ExecuteNonQuery();
            Close(con);
            return (int)cmd.Parameters["ReturnValue"].Value;
        }

        public int RunProc(string procName, SqlParameter[] prams, string _section = null)
        {
            if (string.IsNullOrEmpty(_section))
            {
                _section = SECTION;
            }
            SqlConnection con = ConnOpen(_section);
            SqlCommand cmd = this.CreateCommand(procName, prams, con);
            cmd.CommandTimeout = SELECTCOMMAND_COMMANDTIMEOUT_DEFAULT;
            cmd.ExecuteNonQuery();
            Close(con);
            return (int)cmd.Parameters["ReturnValue"].Value;
        }

        public void RunProc(string procName, out SqlDataReader dataReader, string _section = null)
        {
            if (string.IsNullOrEmpty(_section))
            {
                _section = SECTION;
            }
            SqlConnection con = ConnOpen(_section);
            dataReader = this.CreateCommand(procName, null, con).ExecuteReader(CommandBehavior.CloseConnection);
            Close(con);
        }

        public void RunProc(string procName, SqlParameter[] prams, out SqlDataReader dataReader, string _section = null)
        {
            if (string.IsNullOrEmpty(_section))
            {
                _section = SECTION;
            }
            SqlConnection con = ConnOpen(_section);
            dataReader = this.CreateCommand(procName, prams, con).ExecuteReader(CommandBehavior.CloseConnection);
            Close(con);
        }

        public DataSet SelectDataBase(string strSQL, string _section = null)
        {
            if (string.IsNullOrEmpty(_section))
            {
                _section = SECTION;
            }
            SqlConnection con = ConnOpen(_section);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
            da.SelectCommand.CommandTimeout = SELECTCOMMAND_COMMANDTIMEOUT_DEFAULT;
            da.Fill(ds);
            Close(con);
            return ds;
        }

        public void SQLOperation(string theSQL, string _section = null)
        {
            if (string.IsNullOrEmpty(_section))
            {
                _section = SECTION;
            }
            SqlConnection con = ConnOpen(_section);

            SqlCommand cmd = new SqlCommand(theSQL, con);
            cmd.CommandTimeout = SELECTCOMMAND_COMMANDTIMEOUT_DEFAULT;
            cmd.ExecuteNonQuery();
            Close(con);
        }

        public int UpdateDataBase(string strSQL, string _section = null)
        {
            if (string.IsNullOrEmpty(_section))
            {
                _section = SECTION;
            }
            SqlConnection con = ConnOpen(_section);
            SqlCommand cmd = new SqlCommand(strSQL, con);
            cmd.CommandTimeout = SELECTCOMMAND_COMMANDTIMEOUT_DEFAULT;
            try
            {
                cmd.Transaction = con.BeginTransaction();
                int intNumber = cmd.ExecuteNonQuery();
                cmd.Transaction.Commit();
                Close(con);
                cmd.Dispose();
                this.Dispose(con);
                return intNumber;
            }
            catch
            {
                cmd.Transaction.Rollback();
                Close(con);
                cmd.Dispose();
                this.Dispose(con);
                return -1;
            }
        }
        //时间戳转日期
        public DateTime GetDateTime(string strLongTime)
        {
            Int64 begtime = Convert.ToInt64(strLongTime) * 10000000;//100毫微秒为单位,textBox1.text需要转化的int日期
            DateTime dt_1970 = new DateTime(1970, 1, 1, 8, 0, 0);
            long tricks_1970 = dt_1970.Ticks;//1970年1月1日刻度
            long time_tricks = tricks_1970 + begtime;//日志日期刻度
            DateTime dt = new DateTime(time_tricks);//转化为DateTim
            return dt;
        }





    }
}

