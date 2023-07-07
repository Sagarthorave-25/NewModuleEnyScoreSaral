using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace ENYSchedular_new
{
    class helper
    {
        CommFun objComm = new CommFun();
        public DataSet GetEmptyEyData()
        {
            DataSet dt = new DataSet();
            try
            {
                dt = objComm.GetEmptyEyData();
                return dt;
            }
            catch (Exception ex)
            {
                new CommFun().SaveEYRiskScoreErrorLog("GetApplicationEy", "GetEmptyEyData", "helper", ex.Message, "Error", "NewEYModelShedular_SaralUW");
                string msg = ex.Message;
            }
            return dt;
        }
        public void GenerateEyScore_Saral(DataSet dst)
        {
            int SrNo = 0;
            string ApplicationNo = string.Empty;
            string Source = "NewEYModelShedular_SaralUW";
            string inputJson = string.Empty;
            string result = string.Empty;
            string apiUrl = ConfigurationManager.AppSettings["ENYScoreApiURL"].ToString(); 
            try
            {
                if (dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                    {
                        EYRiskScoreRequest objEYRiskScrReq = new EYRiskScoreRequest();
                        ApplicationNo = dst.Tables[0].Rows[i]["appNo"].ToString();
                        if (!string.IsNullOrEmpty(ApplicationNo))
                        {
                            DataSet ds = objComm.GetEyData(ApplicationNo, Source);
                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                SrNo = 0;
                                result = string.Empty;
                                objEYRiskScrReq.ProposalLoginMonth = ds.Tables[0].Rows[0]["ProposalLoginMonth"].ToString().Trim();
                                objEYRiskScrReq.ProposalLoginDay = ds.Tables[0].Rows[0]["ProposalLoginDay"].ToString().Trim();
                                objEYRiskScrReq.BILLFREQ = ds.Tables[0].Rows[0]["BILLFREQ"].ToString().Trim();
                                objEYRiskScrReq.BILLCHNL = ds.Tables[0].Rows[0]["BILLCHNL"].ToString().Trim();
                                objEYRiskScrReq.Owner_Add05 = ds.Tables[0].Rows[0]["Owner_Add05"].ToString().Trim();
                                objEYRiskScrReq.Is_Owner_LandLineProvided = ds.Tables[0].Rows[0]["Is_Owner_LandLineProvided"].ToString().Trim();
                                objEYRiskScrReq.Is_Owner_MobileProvided = ds.Tables[0].Rows[0]["Is_Owner_MobileProvided"].ToString().Trim();
                                objEYRiskScrReq.Is_Owner_EmailIDProvided = ds.Tables[0].Rows[0]["Is_Owner_EmailProvided"].ToString().Trim();
                                objEYRiskScrReq.Owner_Gender = ds.Tables[0].Rows[0]["Owner_GENDer"].ToString().Trim();
                                objEYRiskScrReq.Owner_Occ_Cat = ds.Tables[0].Rows[0]["Owner_Occ_Cat"].ToString().Trim();
                                objEYRiskScrReq.Owner_MaritalStatus = ds.Tables[0].Rows[0]["Owner_MaritalStatus"].ToString().Trim();
                                objEYRiskScrReq.LA_Add05 = ds.Tables[0].Rows[0]["LA_Add05"].ToString().Trim();
                                objEYRiskScrReq.Is_LA_LandLineProvided = ds.Tables[0].Rows[0]["Is_LA_LandLineProvided"].ToString().Trim();
                                objEYRiskScrReq.Is_LA_MobileProvided = ds.Tables[0].Rows[0]["Is_LA_MobileProvided"].ToString().Trim();
                                objEYRiskScrReq.Is_LA_EmailIDProvided = ds.Tables[0].Rows[0]["Is_LA_EmailProvided"].ToString().Trim();
                                objEYRiskScrReq.LA_Gender = ds.Tables[0].Rows[0]["LA_GENDer"].ToString().Trim();
                                objEYRiskScrReq.LA_Occ_Cat = ds.Tables[0].Rows[0]["LA_Occ_Cat"].ToString().Trim();
                                objEYRiskScrReq.LA_MaritalStatus = ds.Tables[0].Rows[0]["LA_MaritalStatus"].ToString().Trim();
                                string PhotoIdProof = GetPhotoIdProof(ds.Tables[0].Rows[0]["LA_PhotoIDPrf"].ToString().Trim());
                                objEYRiskScrReq.LA_PhotoIDPrf = PhotoIdProof;
                                string LA_AgePrf = GetLA_AgeProof(ds.Tables[0].Rows[0]["LA_AgePrf"].ToString().Trim());
                                objEYRiskScrReq.LA_AgePrf = LA_AgePrf;
                                string LA_IncomePrf = GetLA_IncomePrf(ds.Tables[0].Rows[0]["LA_IncomePrf"].ToString().Trim());
                                objEYRiskScrReq.LA_IncomePrf = LA_IncomePrf;
                                string LA_Nom = GetLaNominee(ds.Tables[0].Rows[0]["LA_Nominee"].ToString().Trim());
                                objEYRiskScrReq.LA_Nominee = LA_Nom;
                                string OwnerRelationship = GetOwnLARel(ds.Tables[0].Rows[0]["Owner_Relationship_LA"].ToString().Trim());
                                objEYRiskScrReq.Owner_Relationship_LA = OwnerRelationship;
                                objEYRiskScrReq.Payer_Add05 = ds.Tables[0].Rows[0]["Payer_Add05"].ToString().Trim();
                                objEYRiskScrReq.AgentCategory = ds.Tables[0].Rows[0]["AgentCategory"].ToString().Trim();
                                objEYRiskScrReq.Channel = ds.Tables[0].Rows[0]["Channel"].ToString().Trim();
                                objEYRiskScrReq.ProductType = ds.Tables[0].Rows[0]["ProductType"].ToString().Trim();
                                string ProductCat = GetProductCat(ds.Tables[0].Rows[0]["Product_Category"].ToString().Trim());
                                objEYRiskScrReq.Product_Category = ds.Tables[0].Rows[0]["Product_Category"].ToString().Trim();
                                objEYRiskScrReq.Par_NonPar = ds.Tables[0].Rows[0]["Par_NonPar"].ToString().Trim();
                                string FirstPaymentvalue = GetHonerdPayment(ds.Tables[0].Rows[0]["FirstHonouredPaymentType"].ToString().Trim());
                                objEYRiskScrReq.FirstHonouredPaymentType = FirstPaymentvalue;
                                objEYRiskScrReq.LA_noteql_Owner = ds.Tables[0].Rows[0]["LA_noteql_Owner"].ToString().Trim();
                                objEYRiskScrReq.Owner_noteql_Payer = ds.Tables[0].Rows[0]["Owner_noteql_Payer"].ToString().Trim();
                                objEYRiskScrReq.BMI_Labels = ds.Tables[0].Rows[0]["BMI_label"].ToString().Trim();
                                objEYRiskScrReq.LA_Education_Cat = ds.Tables[0].Rows[0]["LA_Education_Category"].ToString().Trim();
                                objEYRiskScrReq.HBT_isAlcoholic = ds.Tables[0].Rows[0]["HBT_isAlcoholic"].ToString().Trim();
                                objEYRiskScrReq.LA_IsSmoker = ds.Tables[0].Rows[0]["LA_IsSmoker"].ToString().Trim();
                                objEYRiskScrReq.AgntPCode_Labels = ds.Tables[0].Rows[0]["AgntPCode_Labels"].ToString().Trim();
                                objEYRiskScrReq.LAPCode_Labels = ds.Tables[0].Rows[0]["LA_PCode_Labels"].ToString().Trim();
                                objEYRiskScrReq.Branch_Labels = ds.Tables[0].Rows[0]["Branch_Labels"].ToString().Trim();
                                objEYRiskScrReq.Occupation_Tag = ds.Tables[0].Rows[0]["Occupation_Tag"].ToString().Trim();
                                objEYRiskScrReq.Owner_Income_Bucket = ds.Tables[0].Rows[0]["Owner_Income_Bucket"].ToString().Trim();
                                objEYRiskScrReq.Owner_Education_Cat = ds.Tables[0].Rows[0]["Owner_Eduaction_Cat"].ToString().Trim();
                                objEYRiskScrReq.LA_Income_Bucket = ds.Tables[0].Rows[0]["LA_Income_Bucket"].ToString().Trim();
                                objEYRiskScrReq.LA_PCode_noteql_Owner = ds.Tables[0].Rows[0]["LA_PCode_noteql_Owner"].ToString().Trim();
                                objEYRiskScrReq.LA_PCode_noteql_Payer = ds.Tables[0].Rows[0]["LA_PCode_noteql_Payer"].ToString().Trim();
                                objEYRiskScrReq.Payer_Income_Bucket = ds.Tables[0].Rows[0]["Payer_Income_Bucket"].ToString().Trim();
                                objEYRiskScrReq.Agnt_Tenure_Bucket = ds.Tables[0].Rows[0]["Agnt_Tenure_Bucket"].ToString().Trim();
                                objEYRiskScrReq.PT_PPT_Bucket = ds.Tables[0].Rows[0]["PT_PPT_Bucket"].ToString().Trim();
                                objEYRiskScrReq.SP_Bucket = ds.Tables[0].Rows[0]["SP_Bucket"].ToString().Trim();
                                objEYRiskScrReq.Age_Bucket = ds.Tables[0].Rows[0]["Age_Bucket"].ToString().Trim();
                                objEYRiskScrReq.Distance_btw_Branch_LAPCodes_Bucket = ds.Tables[0].Rows[0]["Distance_btw_Branch_LAPCodes_Bucket"].ToString().Trim();
                                objEYRiskScrReq.Distance_btw_Branch_AgntPCodes_Bucket = ds.Tables[0].Rows[0]["Distance_btw_Branch_AgntPCodes_Bucket"].ToString().Trim();
                                objEYRiskScrReq.SumAssured_Bucket = ds.Tables[0].Rows[0]["SumASsured_Bucket"].ToString().Trim();
                                objEYRiskScrReq.Premium_Bucket = ds.Tables[0].Rows[0]["Premium_Bucket"].ToString().Trim();
                                objEYRiskScrReq.CNTTYPE = ds.Tables[0].Rows[0]["CNTTYPE"].ToString().Trim();
                                objEYRiskScrReq.PPT_Bucket = ds.Tables[0].Rows[0]["PPT_BUCKET"].ToString().Trim();
                                objEYRiskScrReq.PolicyTerm_Bucket = ds.Tables[0].Rows[0]["PolicyTerm_Bucket"].ToString().Trim();
                                objEYRiskScrReq.Income_to_Premium_Bucket = ds.Tables[0].Rows[0]["Income_to_Premium_Bucket"].ToString().Trim();
                                string LastPaymentvalue = GetHonerdPayment(ds.Tables[0].Rows[0]["LAStHonouredPaymentType"].ToString().Trim());
                                objEYRiskScrReq.LastHonouredPaymentType = LastPaymentvalue;
                                objEYRiskScrReq.IIB_Score_Bucket = ds.Tables[0].Rows[0]["IIB_Score_Bucket"].ToString().Trim();
                                objEYRiskScrReq.CIBIL_Score_Bucket = ds.Tables[0].Rows[0]["CIBIL_Score_Bucket"].ToString().Trim();
                                string input = JsonConvert.SerializeObject(objEYRiskScrReq);
                                inputJson = input.Replace("LA_Nominee", "LA Nominee");
                                inputJson = inputJson.Replace("PT_PPT_Bucket", "PT-PPT_Bucket");
                                inputJson = inputJson.Replace("SP_Bucket", "S/P_Bucket");
                                SrNo = objComm.SaveEYRiskScoreInputOutputLog(inputJson, result, ApplicationNo, Source, result, "Create", 0);
                                result = SmartApi(apiUrl, inputJson, ApplicationNo, Source);
                                result = JsonConvert.DeserializeObject(result).ToString();
                                if (!string.IsNullOrEmpty(result))
                                {
                                    if (result == "Low Risk" || result == "High Risk" || result == "Medium Risk")
                                    {
                                        new CommFun().SaveEYRiskScoreInputOutputLog(inputJson, result, ApplicationNo, Source, result, "Update", SrNo);
                                        new CommFun().SaveENY(ApplicationNo, result, Source);
                                    }
                                    else
                                    {
                                        new CommFun().SaveEYRiskScoreInputOutputLog(inputJson, result, ApplicationNo, Source, result, "Update", SrNo);
                                        new CommFun().SaveEYRiskScoreErrorLog(ApplicationNo, "NewEYModelShedular_DEQC", "helper", "Result != Low,Medium,High", "Error", Source);
                                    }
                                }
                                else
                                {
                                    new CommFun().SaveEYRiskScoreErrorLog(ApplicationNo, "NewEYModelShedular_DEQC", "helper", "Result is blank ", "Error", Source);
                                }
                            }
                            else
                            {
                                new CommFun().SaveEYRiskScoreErrorLog(ApplicationNo, "GenerateEyScore_Saral", "helper", "Application Value not present in database", "Error", Source);
                            }
                        }
                    }
                }
                else {
                    new CommFun().SaveEYRiskScoreErrorLog(ApplicationNo, "GenerateEyScore_Saral", "helper", "No application present in database", "Error", Source);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                new CommFun().SaveEYRiskScoreInputOutputLog(inputJson, result, ApplicationNo, Source, result, "Update", SrNo);
                new CommFun().SaveEYRiskScoreErrorLog(ApplicationNo, "GenerateEyScore_Saral", "helper", ex.Message, "Error", Source);

            }
        }
        public string GetHonerdPayment(string type)
        {
            string result = string.Empty;
            if (type == "Waiver of Premium")
            {
                result = "ECS";
            }
            else if (type == "Receipt Cancellation")
            {
                result = "Other";
            }
            else if (type == "Journal")
            {
                result = "Other";
            }
            else if (type == "Direct Debit/ SI")
            {
                result = "ECS";
            }
            else if (type == "Payment Types")
            {
                result = "Missing";
            }
            else if (type == "Draft")
            {
                result = "Demand Draft";
            }
            else if (type == "Cash Payment")
            {
                result = "Cash";
            }
            else if (type == "Cheque Payment")
            {
                result = "Cheque";
            }
            else if (type == "IVR Payment")
            {
                result = "IVR";
            }
            else
            {
                result = type;
            }
            return result;

        }
        public string GetLaNominee(string type)
        {
            string result = string.Empty;
            if (type == "Self")
            {
                result = "AP";
            }
            else if (type == "Null")
            {
                result = "AP";
            }
            else if (type == "SON")
            {
                result = "SO";
            }
            else
            {
                result = type;
            }
            return result;
        }
        public string GetProductCat(string type) {
            string result = string.Empty;

            if (type == "Term")
            {
                result = "Annuity";
            }
            else {
                result = type;
            }
            return result;
        
        }
        public string GetOwnLARel(string type)
        {
            string OwnerRelationship = string.Empty;
            if (type == "Grand Father")
            {
                OwnerRelationship = "GF";
            }
            else if (type == "Self")
            {
                OwnerRelationship = "SELF";
            }
            else if (type == "Spouse")
            {
                OwnerRelationship = "WI";
            }
            else if (type == "0")
            {
                OwnerRelationship = "MISSING";
            }
            else
            {
                OwnerRelationship = type;
            }
            return OwnerRelationship;
        }
        public string GetPhotoIdProof(string PhotoId)
        {
            string Result = string.Empty;
            if (PhotoId == "ADHARCDN")
            {
                Result = "AADHARCD";
            }
            else if (PhotoId == "J&amp;KGVTID")
            {
                Result = "J&amp;KG";
            }
            else if (PhotoId == "AADHARFRONT")
            {
                Result = "AADHARCD";
            }
            else if (PhotoId == "Null")
            {
                Result = "NOP";
            }
            else if (PhotoId == "FRDMFGTR")
            {
                Result = "NOP";
            }
            else if (PhotoId == "GVTEMPID")
            {
                Result = "NOP";
            }
            else if (PhotoId == "PIOBOOKL")
            {
                Result = "NOP";
            }
            else if (PhotoId == "AADHARCDN")
            {
                Result = "NOP";
            }
            else if (PhotoId == "--Select--")
            {
                Result = "NOP";
            }
            else if (PhotoId == "MEDIASSC")
            {
                Result = "NOP";
            }
            else if (PhotoId == "0")
            {
                Result = "NOP";
            }
            else
            {
                Result = PhotoId;
            }
            return Result;

        }
        public string GetLA_AgeProof(string LA_AgeProof)
        {
            string result = string.Empty;
            if (LA_AgeProof == "0")
            {
                result = "NoDoc";
            }
            else if (LA_AgeProof == "AADHARCDN")
            {
                result = "ADHARCDN";
            }
            else if (LA_AgeProof == "AADHARCD")
            {
                result = "AADHARCD";
            }
            else if (LA_AgeProof == "AC")
            {
                result = "NoDoc";
            }
            else if (LA_AgeProof == "ECG")
            {
                result = "NoDoc";
            }
            else if (LA_AgeProof == "NULL")
            {
                result = "NoDoc";
            }
            else
            {
                result = LA_AgeProof;
            }
            return result;
        }
        public string GetLA_IncomePrf(string LA_income)
        {
            string result = string.Empty;
            if (LA_income == "NULL")
            {
                result = "NotProvided";
            }
            else if (LA_income == "TPRTYKYC")
            {
                result = "NotProvided";
            }
            else if (LA_income == "Please Select")
            {
                result = "NotProvided";
            }
            else if (LA_income == "0")
            {
                result = "NotProvided";
            }
            else
            {
                result = LA_income;
            }
            return result;
        }
        public static string SmartApi(string apiUrl, string inputJson, string ApplicationNo,  string Source)
        {
            string Response = string.Empty;

            try
            {
                System.Net.WebClient client = new System.Net.WebClient();
                System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
                System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;
                //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                client.Headers["Content-type"] = "application/json";
                client.Headers["Ocp-Apim-Subscription-Key"] = "7b68b0514d2e4e3dba0e74e4aaa995dd";
                //client.Headers["Authorization"] = Authorization;
                client.Encoding = Encoding.UTF8;
                Response = client.UploadString(apiUrl, inputJson);
            }
            catch (Exception ex)
            {
                Response = ex.Message;
                new CommFun().SaveEYRiskScoreErrorLog(ApplicationNo, "SmartApi", "helper", ex.Message, "Error", Source);
            }
            return Response;
        }


        //private string CallENYScore(EYRiskScoreRequest ey)
        //{
        //    string result = string.Empty;
        //    try
        //    {
        //        string rs = string.Empty;
        //        string inputJson = string.Empty;

        //        string Authorization = string.Empty;
        //        string apiUrl = ConfigurationManager.AppSettings["ENYScoreApiURL"].ToString();
        //            inputJson = (new JavaScriptSerializer()).Serialize(ey);
        //            result = SmartApi(apiUrl, inputJson, Authorization,ey.ApplicationNo);
        //    }
        //    catch (Exception ex)
        //    {
        //        objComm.Insert_Logs_RiskAndEY(ey.ApplicationNo, ex.Message, "NewEYModelSaral", "CallENYScore", "Fail");
        //    }
        //    return result;
        //}
        //private void generateToken(ref string token)
        //{
        //    token = string.Empty;
        //    try
        //    {
        //        using (var client1 = new HttpClient())
        //        {
        //            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        //            var postData = new List<KeyValuePair<string, string>>();

        //            postData.Add(new KeyValuePair<string, string>("ClientID", ConfigurationManager.AppSettings["ClientID"].ToString()));
        //            postData.Add(new KeyValuePair<string, string>("ClientSecret", ConfigurationManager.AppSettings["ClientSecret"].ToString()));
        //            postData.Add(new KeyValuePair<string, string>("Source", ConfigurationManager.AppSettings["Source"].ToString()));
        //            postData.Add(new KeyValuePair<string, string>("PartnerID", ConfigurationManager.AppSettings["PartnerID"].ToString()));

        //            client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            client1.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings["TOKENSubscriptionKey"].ToString());

        //            HttpContent content = new FormUrlEncodedContent(postData);

        //            var responseResult = client1.PostAsync(ConfigurationManager.AppSettings["TOKENURL"].ToString(), content).Result;


        //            string Result = string.Empty;
        //            if (responseResult.IsSuccessStatusCode)
        //            {
        //                Result = responseResult.Content.ReadAsStringAsync().Result;
        //                JObject json = JObject.Parse(Result);

        //                token = json["access_token"].ToString();
        //                //token = json["token_type"].ToString() + " " + json["access_token"].ToString();
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }

        //}
        //public string SmartApi(string apiUrl, string inputJson, string Authorization, string Appno)
        //{

        //    try
        //    {
        //        System.Net.WebClient client = new System.Net.WebClient();
        //        System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
        //        System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;
        //        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

        //        client.Headers["Content-type"] = "application/json";
        //        client.Headers["Ocp-Apim-Subscription-Key"] = ConfigurationManager.AppSettings["Ocp-Apim-Subscription-Key"].ToString();
        //        //client.Headers["Authorization"] = Authorization;
        //        client.Encoding = Encoding.UTF8;
        //        string Response = client.UploadString(apiUrl , inputJson);

        //        return Response;
        //    }
        //    catch (Exception ex)
        //    {
        //        objComm.Insert_Logs_RiskAndEY(Appno, ex.Message, "NewEYModelSaral", "CallENYScore", "Fail");
        //        return null;
        //    }
        //}
        //public void InsertEnyVal(string appno, string score, string early_claim_risk_level)
        //{
        //    objComm.Insert_EnyFlag_Details(appno, score, early_claim_risk_level);
        //}

    }
}
