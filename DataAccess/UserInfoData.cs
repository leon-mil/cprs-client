/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : UserInfoData.cs
Programmer    : Christine Zhang
Creation Date : April 29 2015
Parameters    : N/A
Inputs        : dbo.SCHED_ID
Outputs       : N/A
Description   : Get user info data from env and SCHED_ID table
Change Request: 
Detailed Design: N/A
Rev History   : See Below
Other         : N/A
 ***********************************************************************
Modified Date : 06/09/2016
Modified By   : Cestine Gill
Keyword       : cg060916
Change Request: na
Description   : added line to obtain Grade from sched_id table
***********************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using CprsBLL;

namespace CprsDAL
{
    public class UserInfoData
    {
        /*Get userinfo from sched_id table, if get the info return true, otherwise return false */
        public bool GetUserInfo()
        {
            bool user_exist = false;

            //get user name from environment variable
            string user_name = (Environment.GetEnvironmentVariable("UserName"));

            //get userid, groupcode and printq from sched_id table
            if (!String.IsNullOrEmpty(user_name))
            {
                UserInfo.UserName = user_name.ToLower();
                using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
                {
                    string sql = "SELECT GRPCDE, PRINTQ, GRADE, INITFD, INITSL, INITNR, INITMF, CONTFD, CONTSL, CONTNR, CONTMF FROM dbo.SCHED_ID WHERE USRNME = " + GeneralData.AddSqlQuotes(user_name);
                    SqlCommand command = new SqlCommand(sql, connection);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                UserInfo.GroupCode = (EnumGroups)Enum.Parse(typeof(EnumGroups), reader["GRPCDE"].ToString());
                                UserInfo.PrinterQ = reader["PRINTQ"].ToString();
                                UserInfo.Grade = reader["GRADE"].ToString();

                                UserInfo.InitFD = reader["INITFD"].ToString();
                                UserInfo.InitSL = reader["INITSL"].ToString();
                                UserInfo.InitNR = reader["INITNR"].ToString();
                                UserInfo.InitMF = reader["INITMF"].ToString();
                                UserInfo.ContFD = reader["CONTFD"].ToString();
                                UserInfo.ContSL = reader["CONTSL"].ToString();
                                UserInfo.ContNR = reader["CONTNR"].ToString();
                                UserInfo.ContMF = reader["CONTMF"].ToString();

                                user_exist = true;
                            }
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
            }

            return user_exist;

        }

        //Check avialable from sysstatus table
        public bool CheckUserAvailable(EnumGroups groupcode)
        {
            bool available = false;
            string role;
            if (groupcode == EnumGroups.NPCInterviewer || groupcode == EnumGroups.NPCManager || groupcode == EnumGroups.NPCLead)
                role = "CLERK";
            else if (groupcode == EnumGroups.HQAnalyst || groupcode == EnumGroups.HQManager || groupcode == EnumGroups.HQMathStat || groupcode == EnumGroups.HQTester || groupcode == EnumGroups.HQSupport)
                role = "ANALYST";
            else
                role = "PROGRAMMER";
                    
             using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT " + role + " from dbo.SYSSTATUS";
                SqlCommand command = new SqlCommand(sql, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                    if (reader.Read())
                    {

                        if (reader[role].ToString().Trim() == "Y")
                            available = true;
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }


            return available;
        }
    }

}
