using System;
using System.Data;
using System.Configuration;

public class ExecuteOperation
{
    IDBManager dbManager = null;
    string uid = "1";

    /// <summary>
    /// Intialise dbManager Connection
    /// </summary>
    public ExecuteOperation()
    {
        dbManager = new DBManager(DataProvider.SqlServer);
        dbManager.ConnectionString = ConfigurationManager.ConnectionStrings["inventoryConnection"].ToString();

        //Set user login id
        if (System.Web.HttpContext.Current.Session["userInfo"] != null)
        {
            cms_app.Models.LoginModalData dm = (cms_app.Models.LoginModalData)System.Web.HttpContext.Current.Session["userInfo"];
            uid = dm.userId;
        }
    }

    /// <summary>
    /// Dispose dbManager if connection is open
    /// </summary>
    public void DisposeConnection()
    {
        if (dbManager != null)
        {
            dbManager.Close();
            dbManager.Dispose();
        }
    }
    public DataTable checkUserLogin(String prmUserCode, String prmPassword)
    {
        DataTable DT = null;
        try
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@UserCode", prmUserCode);
            dbManager.AddParameters(1, "@UserPassword", prmPassword);
            DT = dbManager.ExecuteDataTable(CommandType.StoredProcedure, "[Admin].[checkUserAuthentication]");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            DisposeConnection();
        }
        return DT;
    }



    #region City
    public DataTable GetCityByStateId(string prmStateId)
    {
        DataTable DT = null;
        try
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@StateId", prmStateId);
            DT = dbManager.ExecuteDataTable(CommandType.StoredProcedure, "[Admin].[GetCityByStateId]");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            DisposeConnection();
        }
        return DT;
    }
    #endregion

    #region Dashboard
    public DataTable GetDashboardReport()
    {
        string query = "<Data><Employee EmployeeId=\"" + uid + "\"></Employee></Data>";
        DataTable DT = GetDataTableFromInputParameter(query, "[Admin].[Master_Dashboard]");
        return DT;
    }
    #endregion

    #region Designer
    public DataTable GetPendingSampleList(String prmProcName)
    {
        string query = "<Data><Mold CreatedBy=\"" + uid + "\"></Mold></Data>";
        DataTable DT = GetDataTableFromInputParameter(query, prmProcName);
        return DT;
    }
    public DataTable SaveSampleSubmition(String prmMoldId, String prmLastSubmitionDate, String prmAssignDate, String prmRemark,String prmIsApprove, out string strMsg)
    {
        string query = "<Data><Mold MoldId=\"" + prmMoldId + "\" LastSubmitionDate=\"" + prmLastSubmitionDate + "\" AssignDate=\"" + prmAssignDate +
                       "\" Remarks=\"" + prmRemark + "\" isApprove=\"" + prmIsApprove + "\" CreatedBy=\"" + uid +"\"></Mold></Data>";
        DataTable DT = GetDataTableFromInputParameterWithMessage(query, "[Admin].[Designer_SaveSampleSubmition]",out strMsg);
        return DT;
    }
    public DataSet SaveCourierSubmition(String prmMoldId, String prmCourierId, String prmTransporterId, String prmCourierNo, String prmSamplePhoto,
        String prmRemark, String prmStatus, String prmAssignRemarks, String prmAssignDate, String prmDesignerId, String prmAction, out string strMsg)
    {
        string query = "<Data><Courier MoldId=\"" + prmMoldId + "\" CourierId=\"" + prmCourierId + "\" TransporterId=\"" + prmTransporterId +
                       "\" CourierNo=\"" + prmCourierNo + "\" SamplePhoto=\"" + prmSamplePhoto + "\" Remarks=\"" + prmRemark +
                       "\" ACT=\"" + prmAction + "\" Status=\"" + prmStatus + "\" AssignRemarks=\"" + prmAssignRemarks + 
                       "\" AssignDate=\"" + prmAssignDate + "\" DesignerId=\"" + prmDesignerId + "\" CreatedBy=\"" + uid + "\"></Courier></Data>";
        DataSet DS = GetDataSetFromInputParameterWithMessage(query, "[Admin].[Manage_CourierSubmition]", out strMsg);
        return DS;
    }
    #endregion

    #region Employee
    public DataSet GetAllMasterDataByCompanyId(string prmCompanyCode, string prmMDHCode)
    {
        string query = "<Data><Category CompanyCode=\"" + prmCompanyCode + "\" MDHCode=\"" + prmMDHCode + "\" CreatedBy=\"" + uid + "\"></Category></Data>";
        DataSet DS = GetDataSetFromInputParameter(query, "[Admin].[GetAllMasterDataByCompanyId]");
        return DS;

        //DataSet DS = null;
        //try
        //{
        //    dbManager.Open();
        //    dbManager.CreateParameters(2);
        //    dbManager.AddParameters(0, "@CompanyId", prmCompanyCode);
        //    dbManager.AddParameters(1, "@CreatedBy", uid);
        //    DS = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "[Admin].[GetAllMasterDataByCompanyId]");
        //}
        //catch (Exception ex) { throw ex; }
        //finally
        //{
        //    DisposeConnection();
        //}
        //return DS;
    }
    #endregion

    #region ManageDataByXML
    public DataTable ManageData(String prmInputData, string prmProcName, out String PrmStrUpdatedDate)
    {
        DataTable DT = GetDataTableFromInputParameterWithMessage(prmInputData, prmProcName,out PrmStrUpdatedDate);
        return DT;
    }
    public DataTable GetMasterListForEdit(String prmId,String prmTableName)
    {
        DataTable DT = null;
        try
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@Id", prmId);
            dbManager.AddParameters(1, "@prmTableName", prmTableName);
            DT = dbManager.ExecuteDataTable(CommandType.StoredProcedure, "[Admin].[Master_GetForEditById]");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            DisposeConnection();
        }
        return DT;
    }
    public DataTable GetMasterDataList(String prmTableName, Int32 prmActive, Int32 prmIsDelete)
    {
        DataTable DT = null;
        try
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@prmTableName", prmTableName);
            dbManager.AddParameters(1, "@Active", prmActive);
            dbManager.AddParameters(2, "@isDelete", prmIsDelete);
            dbManager.AddParameters(3, "@LogedId", uid);
            DT = dbManager.ExecuteDataTable(CommandType.StoredProcedure, "[Admin].[Master_ListForEdit]");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            DisposeConnection();
        }
        return DT;
    }
    public DataSet GetDataWithCompanyForDropdown(Int32 SelectedId, String prmTableName)
    {
        DataSet DS = null;
        try
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@LogedId", uid);
            dbManager.AddParameters(1, "@SelectedId", SelectedId);
            dbManager.AddParameters(2, "@prmTableName", prmTableName);
            DS = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "[Admin].[GetDataWithCompanyForDropdown]");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            DisposeConnection();
        }
        return DS;
    }
    #endregion

    #region Mold
    public DataTable GetMoldDataList(string prmMoldType, Int32 prmActive, Int32 prmIsDelete)
    {
        DataTable DT = null;
        try
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@moldType", prmMoldType);
            dbManager.AddParameters(1, "@Active", prmActive);
            dbManager.AddParameters(2, "@isDelete", prmIsDelete);
            dbManager.AddParameters(3, "@LogedId", uid);
            DT = dbManager.ExecuteDataTable(CommandType.StoredProcedure, "[Admin].[Mold_GetList]");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            DisposeConnection();
        }
        return DT;
    }
    public DataTable GetMoldDataListForOrder(string prmProcName)
    {
        DataTable DT = null;
        try
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@LogedId", uid);
            DT = dbManager.ExecuteDataTable(CommandType.StoredProcedure, prmProcName);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            DisposeConnection();
        }
        return DT;
    }
    #endregion

    #region Article
    public DataTable GetArticleList(String prmAction, string prmProcName)
    {
        string query = "<Data><Article ActionName=\"" + prmAction + "\" CreatedBy=\"" + uid + "\"></Article></Data>";
        DataTable DT = GetDataTableFromInputParameter(query, prmProcName);
        return DT;
    }
    public DataTable SaveArticle(String prmMoldId, String prmBrand, String prmColor, String prmProcess, String prmSubProcess,
        String prmSize, String prmArticleDate, String prmIsBeforeProcess, String prmIsAfterProcess, String prmLang,
        String prmDescription, bool prmActive, String prmAction, String prmSubProcessInputs, out string strMsg)
    {
        string query = "<Data><Article MoldId=\"" + prmMoldId + "\" Brand=\"" + prmBrand + "\" Colour=\"" + prmColor +
                       /*"\" Process=\"" + prmProcess + "\" SubProcess=\"" + prmSubProcess +*/ "\" Size=\"" + prmSize + "\" ArticleDate=\"" + prmArticleDate +
                       "\" IsBeforeProcess=\"" + prmIsBeforeProcess + "\" IsAfterProcess =\"" + prmIsAfterProcess + "\" Lang=\"" + prmLang +
                       "\" Action=\"" + prmAction + "\" IsActive=\"" + prmActive + "\" CreatedBy=\"" + uid + "\"></Article></Data>";

        DataTable DT = null;
        try
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddxmlParameters(0, "@prmInputData", query);
            dbManager.AddParameters(1, "@prmSubProcessInputData", prmSubProcessInputs);
            dbManager.AddParameters(2, "@prmDescription", prmDescription);
            dbManager.AddOutParameters(3, "@OutMsg", DbType.String, 1000);
            DT = dbManager.ExecuteDataTable(CommandType.StoredProcedure, "[Admin].[Article_SaveData]");
            strMsg = dbManager.Parameters[3].Value.ToString();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            DisposeConnection();
        }
        return DT;
    }

    public DataTable SaveArticleRawMaterialInfo(String prmSubProcessInputs, out string strMsg)
    {
        DataTable DT = null;
        try
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@prmInputData", prmSubProcessInputs);
            dbManager.AddOutParameters(1, "@OutMsg", DbType.String, 1000);
            DT = dbManager.ExecuteDataTable(CommandType.StoredProcedure, "[Admin].[Article_SaveRawMaterialData]");
            strMsg = dbManager.Parameters[1].Value.ToString();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            DisposeConnection();
        }
        return DT;
    }
    public DataSet GetDataForCreateUpdateArticle(string SelectedId, string prmProcName)
    {
        string query = "<Data><Article SelectedId=\"" + SelectedId + "\" CreatedBy=\"" + uid + "\"></Article></Data>";
        DataSet DS = GetDataSetFromInputParameter(query, prmProcName);
        return DS;
    }
    #endregion


    #region NewlyAdded
    public DataTable GetListofAllAddedMasterData(String prmTableName, Int32 prmActive, Int32 prmIsDelete)
    {
        string query = "<Data><GetData TableName=\"" + prmTableName + "\" Active=\"" + prmActive + "\" isDelete=\"" + prmIsDelete + 
            "\" CreatedBy=\"" + uid + "\"></GetData></Data>";

        DataTable DT = GetDataTableFromInputParameter(query, "[Admin].[GetListofAllAddedMasterData]");
        return DT;
    }
    public DataSet GetListofAllAddedMasterDataForEdit(String prmId, String prmTableName)
    {
        DataSet DS = null;
        try
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@Id", prmId);
            dbManager.AddParameters(1, "@prmTableName", prmTableName);
            DS = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "[Admin].[GetListofAllAddedMasterDataForEdit]");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            DisposeConnection();
        }
        return DS;
    }
    public DataSet MasterDataByCategory(String prmCompanyCode, String prmMDDCode, String prmParrentMDDCode, String prmMDHCode, String prmMDDName,
            String prmRemarks, int prmActive, int prmIsDelete, String prmAction, out string strMsg)
    {
        string prmInputData = "<Data><Category CompanyCode=\"" + prmCompanyCode + "\" MDDCode=\"" + prmMDDCode + "\" MDHCode =\"" + prmMDHCode +
                           "\" MDDName=\"" + prmMDDName + "\" ParrentMDDCode=\"" + prmParrentMDDCode + "\" Remarks=\"" + prmRemarks + "\" Active=\"" + prmActive + "\" isDelete=\"" + prmIsDelete +
                           "\" Action=\"" + prmAction + "\" CreatedBy=\"" + uid + "\"></Category></Data>";

        DataSet DS = GetDataSetFromInputParameterWithMessage(prmInputData, "[Admin].[Master_ListByCategory]", out strMsg);
        return DS;
    }
    #endregion

    #region Common
    public DataSet GetDataSetFromInputParameter(String prmInputData, string prmProcName)
    {
        DataSet DS = null;
        try
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddxmlParameters(0, "@prmInputData", prmInputData);
            DS = dbManager.ExecuteDataSet(CommandType.StoredProcedure, prmProcName);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            DisposeConnection();
        }
        return DS;
    }
    public DataSet GetDataSetFromInputParameterWithMessage(String prmInputData, string prmProcName, out String PrmStrUpdatedDate)
    {
        DataSet DS = null;
        try
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddxmlParameters(0, "@prmInputData", prmInputData);
            dbManager.AddOutParameters(1, "@OutMsg", DbType.String, 1000);
            DS = dbManager.ExecuteDataSet(CommandType.StoredProcedure, prmProcName);
            PrmStrUpdatedDate = dbManager.Parameters[1].Value.ToString();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            DisposeConnection();
        }
        return DS;
    }
    public DataTable GetDataTableFromInputParameter(String prmInputData, string prmProcName)
    {
        DataTable DT = null;
        try
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddxmlParameters(0, "@prmInputData", prmInputData);
            DT = dbManager.ExecuteDataTable(CommandType.StoredProcedure, prmProcName);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            DisposeConnection();
        }
        return DT;
    }
    public DataTable GetDataTableFromInputParameterWithMessage(String prmInputData, string prmProcName, out String PrmStrUpdatedDate)
    {
        DataTable DT = null;
        try
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddxmlParameters(0, "@prmInputData", prmInputData);
            dbManager.AddOutParameters(1, "@OutMsg", DbType.String, 1000);
            DT = dbManager.ExecuteDataTable(CommandType.StoredProcedure, prmProcName);
            PrmStrUpdatedDate = dbManager.Parameters[1].Value.ToString();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            DisposeConnection();
        }
        return DT;
    }
    #endregion
}