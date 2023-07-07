using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENYSchedular_new
{
    class Program
    {
        static void Main(string[] args)
        {
            helper help = new helper();
            try
            {
                DataSet dt = help.GetEmptyEyData();
                
                help.GenerateEyScore_Saral(dt);
            }
            catch (Exception ex)
            {
                new CommFun().SaveEYRiskScoreErrorLog("MainMethod", "Main", "Program", ex.Message, "Error", "NewEYModelShedular_SaralUW");
            }
            //this.Close();
        }
    }
}
