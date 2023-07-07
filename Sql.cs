using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENYSchedular_new
{
    class Sql
    {
        //static string TrnxCommRisk = ConfigurationManager.AppSettings["TrnxCommRisk"].ToString().Trim();
        static string TrnxComm = ConfigurationManager.AppSettings["TrnxComm"].ToString().Trim();
        public void Sql_GetEmptyEyData(String spName, ref DataSet _dt,int db)
        {
            string finalConn = string.Empty;
            try
            {
                SqlConnection connection = new SqlConnection(TrnxComm);

                SqlCommand cmd = new SqlCommand(spName, connection);
                cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["SqlCommandTimeOut"]);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(_dt);
                connection.Close();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }

        public DataSet GetEyData(string ApplicationNo, string Source)
        {
            DataSet ds = new DataSet();
            SqlConnection connection = new SqlConnection(TrnxComm);
            SqlCommand cmd = new SqlCommand("USP_Fetch_EY_UWModelData", connection);
            try
            {
                cmd.CommandTimeout = Convert.ToInt32(3000);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ApplicationNo", ApplicationNo);
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {

            }
            finally {
                connection.Close();
                connection.Dispose();
                connection.Dispose();
            }
            return ds;
        }

        public DataSet RetrieveDataset(string spName, SqlParameter[] sqlParam)
        {
            DataSet _ds;
            _ds = SqlHelper.ExecuteDataset(TrnxComm, CommandType.StoredProcedure, spName, sqlParam);
            return _ds;
        }

        public int Insertrecord(string spName, SqlParameter[] sqlParam)
        {
            int result;
            result = SqlHelper.ExecuteNonQuery(TrnxComm, CommandType.StoredProcedure, spName, sqlParam);
            return result;
        }

        public int SaveEYRiskScoreInputOutputLog(string Request, string Response,  string ApplicationNo, string Source, string Score, string Mode, int srNo)
        {
            SqlConnection conn = new SqlConnection(TrnxComm);
            SqlCommand cmd = new SqlCommand();
            int RowNum = 0;
            try
            {
                conn.Open();
                cmd = new SqlCommand("Usp_NEWEYModel_ResReqLogs", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AppNo", ApplicationNo);
                cmd.Parameters.AddWithValue("@Request", Request);
                cmd.Parameters.AddWithValue("@Response", Response);
                cmd.Parameters.AddWithValue("@Source", Source);
                cmd.Parameters.AddWithValue("@EnyScore", Score);
                cmd.Parameters.AddWithValue("@Mode", Mode);
                cmd.Parameters.AddWithValue("@SrNo", srNo);
                RowNum = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return RowNum;
        }

        //public int InsertrecordScalar(string spName, SqlParameter[] sqlParam)
        //{
        //    int result;
        //    result = SqlHelper.ExecuteScalar(TrnxComm,Comm);
        //    return result;
        //}

    }
}
