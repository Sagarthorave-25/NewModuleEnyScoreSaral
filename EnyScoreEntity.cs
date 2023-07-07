using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENYSchedular_new
{
    class EnyScoreEntity
    {

    }
    public class EYRiskScoreRequest
    {
        public string ProposalLoginMonth { get; set; } = string.Empty;
        public string ProposalLoginDay { get; set; } = string.Empty;
        public string BILLFREQ { get; set; } = string.Empty;
        public string BILLCHNL { get; set; } = string.Empty;
        public string Owner_Add05 { get; set; } = string.Empty;
        public string Is_Owner_LandLineProvided { get; set; } = string.Empty;
        public string Is_Owner_MobileProvided { get; set; } = string.Empty;
        public string Is_Owner_EmailIDProvided { get; set; } = string.Empty;
        public string Owner_Gender { get; set; } = string.Empty;
        public string Owner_Occ_Cat { get; set; } = string.Empty;
        public string Owner_MaritalStatus { get; set; } = string.Empty;
        public string LA_Add05 { get; set; } = string.Empty;
        public string Is_LA_LandLineProvided { get; set; } = string.Empty;
        public string Is_LA_MobileProvided { get; set; } = string.Empty;
        public string Is_LA_EmailIDProvided { get; set; } = string.Empty;
        public string LA_Gender { get; set; } = string.Empty;
        public string LA_Occ_Cat { get; set; } = string.Empty;
        public string LA_MaritalStatus { get; set; } = string.Empty;
        public string LA_PhotoIDPrf { get; set; } = string.Empty;
        public string LA_AgePrf { get; set; } = string.Empty;
        public string LA_IncomePrf { get; set; } = string.Empty;
        public string LA_Nominee { get; set; } = string.Empty;
        public string Owner_Relationship_LA { get; set; } = string.Empty;
        public string Payer_Add05 { get; set; } = string.Empty;
        public string AgentCategory { get; set; } = string.Empty;
        public string Channel { get; set; } = string.Empty;
        public string ProductType { get; set; } = string.Empty;
        public string Product_Category { get; set; } = string.Empty;
        public string Par_NonPar { get; set; } = string.Empty;
        public string FirstHonouredPaymentType { get; set; } = string.Empty;
        public string LA_noteql_Owner { get; set; } = string.Empty;
        public string Owner_noteql_Payer { get; set; } = string.Empty;
        public string BMI_Labels { get; set; } = string.Empty;
        public string LA_Education_Cat { get; set; } = string.Empty;
        public string HBT_isAlcoholic { get; set; } = string.Empty;
        public string LA_IsSmoker { get; set; } = string.Empty;
        public string AgntPCode_Labels { get; set; } = string.Empty;
        public string LAPCode_Labels { get; set; } = string.Empty;
        public string Branch_Labels { get; set; } = string.Empty;
        public string Occupation_Tag { get; set; } = string.Empty;
        public string Owner_Income_Bucket { get; set; } = string.Empty;
        public string Owner_Education_Cat { get; set; } = string.Empty;
        public string LA_Income_Bucket { get; set; } = string.Empty;
        public string LA_PCode_noteql_Owner { get; set; } = string.Empty;
        public string LA_PCode_noteql_Payer { get; set; } = string.Empty;
        public string Payer_Income_Bucket { get; set; } = string.Empty;
        public string Agnt_Tenure_Bucket { get; set; } = string.Empty;

        public string PT_PPT_Bucket { get; set; } = string.Empty;
        public string SP_Bucket { get; set; } = string.Empty;
        public string Age_Bucket { get; set; } = string.Empty;
        public string Distance_btw_Branch_LAPCodes_Bucket { get; set; } = string.Empty;
        public string Distance_btw_Branch_AgntPCodes_Bucket { get; set; } = string.Empty;
        public string SumAssured_Bucket { get; set; } = string.Empty;
        public string Premium_Bucket { get; set; } = string.Empty;
        public string CNTTYPE { get; set; } = string.Empty;
        public string PPT_Bucket { get; set; } = string.Empty;
        public string PolicyTerm_Bucket { get; set; } = string.Empty;
        public string Income_to_Premium_Bucket { get; set; } = string.Empty;
        public string LastHonouredPaymentType { get; set; } = string.Empty;
        public string IIB_Score_Bucket { get; set; } = string.Empty;
        public string CIBIL_Score_Bucket { get; set; } = string.Empty;



    }
}

