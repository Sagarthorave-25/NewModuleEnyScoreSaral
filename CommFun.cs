using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENYSchedular_new
{
    class CommFun
    {
        Sql sqlClass = new Sql();
        public DataSet GetEmptyEyData()
        {
            DataSet ds = new DataSet();
            try
            {
                sqlClass.Sql_GetEmptyEyData("Usp_NewEYScoreData_SaralUW", ref ds,1);
                return ds;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                SaveEYRiskScoreErrorLog("GetEmptyEyData", "GetEmptyEyData", "CommFun", ex.Message, "Error", "NewEYModelShedular_SaralUW");
            }
            return ds;
        }
        public DataSet GetEyData(string ApplicationNo, string Source)
        {
            DataSet ds = new DataSet();
            try
            {
               ds= sqlClass.GetEyData(ApplicationNo, Source);
            }
            catch (Exception ex)
            {
                SaveEYRiskScoreErrorLog("GetEyData", "GetEyData", "CommFun", ex.Message, "Error", Source);
            }
            return ds;
        }
        public int SaveEYRiskScoreInputOutputLog(string Request, string Response,  string ApplicationNo, string Source, string Score, string Mode, int srNo)
        {
            
            try
            {
                srNo=sqlClass.SaveEYRiskScoreInputOutputLog(Request, Response, ApplicationNo, Source, Score, Mode, srNo);
            }
            catch (Exception ex) {
              SaveEYRiskScoreErrorLog(ApplicationNo, "SaveEYRiskScoreInputOutputLog", "CommFun", ex.Message, "Error", Source);
            }
            return srNo;
        }
        public int SaveEYRiskScoreErrorLog(string ApplicationNo, string Method, string ClassName, string ErrorMsg, string Error,  string Source)
        {
            int RowNum = 0;
            SqlParameter[] sqlparams = new SqlParameter[6];
            try
            {
                sqlparams[0] = new SqlParameter("@AppNo", ApplicationNo);
                sqlparams[1] = new SqlParameter("@Method", Method);
                sqlparams[2] = new SqlParameter("@Class", ClassName);
                sqlparams[3] = new SqlParameter("@ErrorMsg", ErrorMsg);
                sqlparams[4] = new SqlParameter("@Error", Error);
                sqlparams[5] = new SqlParameter("@Source", Source);
                RowNum = sqlClass.Insertrecord("Usp_NEWEYModel_ErrorLogs", sqlparams);
            }
            catch (Exception ex)
            {
            }
            return RowNum;
        }
        public int SaveENY(string ApplicationNo, string result,  string Source)
        {
            SqlParameter[] sqlparams = new SqlParameter[3];
            int RowNum = 0;
            try
            {
                sqlparams[0] = new SqlParameter("@AppNo", ApplicationNo);
                sqlparams[1] = new SqlParameter("@EnyScore", result);
                sqlparams[2] = new SqlParameter("@Source", Source);
                RowNum = sqlClass.Insertrecord("Usp_NEWEYModel_Details",sqlparams);
            }
            catch (Exception ex)
            {
                SaveEYRiskScoreErrorLog(ApplicationNo, "SaveENY", "CommFun", ex.Message, "Error", Source);
            }
            return RowNum;
        }
        
        //public DataSet Featch_SMARTAPIENYFLAG_Details(string Appno)
        //{
        //    DataSet Dt = new DataSet();
        //    SqlParameter[] sqlparams = new SqlParameter[1];
        //    try
        //    {
        //        sqlparams[0] = new SqlParameter("@ApplicationNo", Appno);
        //        Dt = sqlClass.RetrieveDataset("USP_Fetch_EY_UWModelData", sqlparams);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return Dt;
        //}
        //public int Insert_EnyFlag_Details(string appNo, string ENYValue, string Actions)
        //{
        //    SqlParameter[] sqlparams = new SqlParameter[4];
        //    try
        //    {
        //        sqlparams[0] = new SqlParameter("@AppNo", appNo);
        //        sqlparams[1] = new SqlParameter("@score", ENYValue);
        //        sqlparams[2] = new SqlParameter("@early_claim_risk_level", Actions);
        //        sqlparams[3] = new SqlParameter("@source", "UW Saral");
        //        int i = sqlClass.Insertrecord("USP_INSERT_EnyFlagScore_UWSaral", sqlparams);
        //        return i;
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;

        //    }
        //}
        //public int Insert_Logs_RiskAndEY(string ApplicationNo, string Msg, string Source, string APIDetails, string StatusLog)
        //{
        //    SqlParameter[] sqlparams = new SqlParameter[5];
        //    try
        //    {
        //        sqlparams[0] = new SqlParameter("@AppNo", ApplicationNo);
        //        sqlparams[1] = new SqlParameter("@Msg", Msg);
        //        sqlparams[2] = new SqlParameter("@Source", Source);
        //        sqlparams[3] = new SqlParameter("@ApiDetails", APIDetails);
        //        sqlparams[4] = new SqlParameter("@StatusLog", StatusLog);
        //        int i = sqlClass.Insertrecord("Sp_Insert_EnyAndRiskScore", sqlparams);
        //        return i;
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }
        //}


    }
}
