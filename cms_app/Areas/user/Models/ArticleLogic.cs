using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace cms_app.Areas.user.Models
{
    public class ArticleLogic
    {
        public DataSet GetDataForCreateArticle(string SelectedId)
        {
            DataSet DS = new DataSet();
            ExecuteOperation op = new ExecuteOperation();
            DS = op.GetDataForCreateUpdateArticle(SelectedId, "[Admin].[Article_CreateArticle]");
            return DS;
        }

        public DataSet GetDataForUpdateArticle(string SelectedId)
        {
            DataSet DS = new DataSet();
            ExecuteOperation op = new ExecuteOperation();
            DS = op.GetDataForCreateUpdateArticle(SelectedId, "[Admin].[Article_UpdateArticleRawMaterialInfo]");
            return DS;
        }

        public DataTable GetArticleList(String prmAction, string prmProcName)
        {
            DataTable Dt = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            Dt = op.GetArticleList(prmAction, prmProcName);
            return Dt;
        }

        public DataTable SaveArticle(
            String prmMoldId, String prmBrand, String prmColor, String prmProcess, String prmSubProcess, String prmSize, String prmArticleDate,
            String prmIsBeforeProcess, String prmIsAfterProcess, String prmLang,
            String prmDescription, bool prmActive, String prmAction, String prmSubProcessInputs, out string strMsg
        )
        {
            DataTable Dt = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            Dt = op.SaveArticle(prmMoldId, prmBrand, prmColor, prmProcess, prmSubProcess, prmSize, prmArticleDate,
            prmIsBeforeProcess, prmIsAfterProcess, prmLang, prmDescription, prmActive, prmAction, prmSubProcessInputs, out strMsg);
            return Dt;
        }

        public DataTable SaveArticleRawMaterialInfo(String prmSubProcessInputs, out string strMsg)
        {
            return new ExecuteOperation().SaveArticleRawMaterialInfo(prmSubProcessInputs, out strMsg);
        }
    }
}