
/**************************************************************************************
Econ App Name  : CPRS
Project Name   : CPRS Interactive Screens System
Program Name   : CprsDAL.SampleData.cs	    	
Programmer     : Christine Zhang
Creation Date  : 11/05/2015
Inputs         : id, sample record
Parameters     : None 
Outputs        : Sample data	
Description    : data layer to add, update sample
Detailed Design: None 
Other          : Called by: frmC700
 
Revision History:	
***************************************************************************************
Modified Date :  12/7/2020
Modified By   :  Christine Zhang
Keyword       :  
Change Request:  
Description   :  Save oadj and oflg fields in SaveSampleData()
****************************************************************************************
Modified Date :  01/25/2021
Modified By   :  Christine Zhang
Keyword       :  
Change Request:  
Description   : Read in Projlength from Sample table
****************************************************************************************/
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
    public class SampleData
    {
        
      /*Get sample Data */
      public Sample GetSampleData(string id)
      {
            /* get data table */
            string db_table = GetDBSampleTable(id);

            Sample samp = new Sample(id);
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT * FROM " + db_table + " WHERE ID = " + GeneralData.AddSqlQuotes(id);
            SqlCommand command = new SqlCommand(sql, connection);
            command.CommandTimeout = 0;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
               
                if (reader.Read())
                {
                    samp.Respid = reader["RESPID"].ToString();
                    samp.Masterid = (int)reader["MASTERID"];
                    samp.Projdesc = reader["PROJDESC"].ToString();
                    samp.Contract = reader["CONTRACT"].ToString();
                    samp.Projloc = reader["PROJLOC"].ToString();
                    samp.Pcityst = reader["PCITYST"].ToString();
                    samp.Fwgt = float.Parse(reader["FWGT"].ToString());
                    samp.Status = reader["STATUS"].ToString();
                    samp.Active = reader["ACTIVE"].ToString();
                    samp.Statdate = reader["STATDATE"].ToString().Trim();
                    samp.Strtdate = reader["STRTDATE"].ToString().Trim();
                    samp.Strtdater = reader["STRTDATER"].ToString().Trim();
                    samp.Repsdate = reader["REPSDATE"].ToString().Trim();
                    samp.Compdate = reader["COMPDATE"].ToString().Trim();
                    samp.Compdater = reader["COMPDATER"].ToString().Trim();
                    samp.Repcompd = reader["REPCOMPD"].ToString().Trim();
                    samp.Futcompd = reader["FUTCOMPD"].ToString().Trim();
                    samp.Futcompdr = reader["FUTCOMPDR"].ToString().Trim();
                    samp.Item5a = (int)reader["ITEM5A"];
                    samp.Item5ar = (int)reader["ITEM5AR"];
                    samp.Item5b = (int)reader["ITEM5B"];
                    samp.Item5c = (int)reader["ITEM5C"];
                    samp.Item5br = (int)reader["ITEM5BR"];
                    samp.Rvitm5c = (int)reader["RVITM5C"];
                    samp.Rvitm5cr = (int)reader["RVITM5CR"];
                    samp.Item6 = (int)reader["ITEM6"];
                    samp.Item6r = (int)reader["ITEM6R"];
                    samp.Capexp = (int)reader["CAPEXP"];
                    samp.Capexpr = (int)reader["CAPEXPR"];
                    samp.Contract = reader["CONTRACT"].ToString().Trim();
                    samp.Flag5a = reader["FLAG5A"].ToString();
                    samp.Flag5b = reader["FLAG5B"].ToString();
                    samp.Flag5c = reader["FLAG5C"].ToString();
                    samp.Flagr5c = reader["FLAGR5C"].ToString();
                    samp.Flagitm6 = reader["FLAGITM6"].ToString();
                    samp.Flagcap = reader["FLAGCAP"].ToString();
                    samp.Flagcompdate = reader["FLAGCOMPDATE"].ToString().Trim();
                    samp.Flagstrtdate = reader["FLAGSTRTDATE"].ToString().Trim();
                    samp.Flagfutcompd = reader["FLAGFUTCOMPD"].ToString().Trim();
                    samp.Flgmodel = reader["FLGMODEL"].ToString();
                    samp.Model = reader["MODEL"].ToString();
                    samp.Oadj = float.Parse(reader["OADJ"].ToString());
                    samp.Oflg = reader["OFLG"].ToString();
                    samp.Sampseq  = reader["SAMPSEQ"].ToString();
                    samp.Projlength = (int)reader["Projlength"];
                    samp.IsModified = false;
                }
                else
                {
                    return null;
                 }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally 
            {
                connection.Close();
            }
                
            return samp;

        }
      
      //Get data source 
      public TypeDBSource GetDatabaseSource(string id)
      {
          string table_name = GetDBSampleTable(id);

          if (table_name == "dbo.Sample")
              return TypeDBSource.Default;
          else 
              return TypeDBSource.Hold;
      }

      /* Get data table base on the db source  */
      private string GetDBSampleTable(string id)
      {
          string check_table = "dbo.Sample_hold";

          using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
          {
              string sql = "SELECT * FROM " + check_table + " WHERE ID = " + GeneralData.AddSqlQuotes(id);
              SqlCommand command = new SqlCommand(sql, connection);
              command.CommandTimeout = 0;

              connection.Open();
              using (SqlDataReader reader = command.ExecuteReader())
              {
                  if (!reader.HasRows)
                  {
                     check_table = "dbo.Sample";                    
                  }
              }
          }

          return check_table;
      }

      /*Save sample data base on table */
      public bool SaveSampleData(Sample so, TypeDBSource dbsource = TypeDBSource.Default)
      {
          string db_table = "dbo.Sample";
          if (dbsource == TypeDBSource.Hold)
              db_table = "dbo.Sample_Hold";

          using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
          {
              string usql = "UPDATE " + db_table + " SET " +
                              "STATUS = @STATUS, " +
                              "STRTDATE = @STRTDATE, " +
                              "STRTDATER = @STRTDATER, " +
                              "CONTRACT = @CONTRACT, " +
                              "PROJDESC = @PROJDESC, " +
                              "PROJLOC = @PROJLOC, " +
                              "FWGT = @FWGT, " +
                              "PCITYST = @PCITYST, " +
                              "ITEM5A = @ITEM5A, " +
                              "ITEM5B = @ITEM5B, " +
                              "ITEM5C = @ITEM5C, " +
                              "RVITM5C = @RVITM5C, " +
                              "ITEM6 = @ITEM6, " +
                              "CAPEXP = @CAPEXP, " +
                              "COMPDATE = @COMPDATE, " +
                              "FUTCOMPD = @FUTCOMPD," +
                              "ITEM5AR = @ITEM5AR, " +
                              "ITEM5BR = @ITEM5BR, " +
                              "RVITM5CR = @RVITM5CR, " +
                              "ITEM6R = @ITEM6R, " +
                              "CAPEXPR = @CAPEXPR, " +
                              "COMPDATER = @COMPDATER, " +
                              "FUTCOMPDR = @FUTCOMPDR," +
                              "FLAG5A = @FLAG5A, " +
                              "FLAG5B = @FLAG5B, " +
                              "FLAG5C = @FLAG5C, " +
                              "FLAGR5C = @FLAGR5C, " +
                              "FLAGITM6 = @FLAGITM6, " +
                              "FLAGCAP = @FLAGCAP, " +
                              "FLAGSTRTDATE = @FLAGSTRTDATE, " +
                              "FLAGCOMPDATE = @FLAGCOMPDATE, " +
                              "FLAGFUTCOMPD = @FLAGFUTCOMPD, " +
                              "REPSDATE = @RESPDATE, " +
                              "REPCOMPD = @REPCOMPD,  " +
                              "ACTIVE = @ACTIVE, " +
                              "OADJ = @OADJ, " +
                              "OFLG = @OFLG " +
                              "WHERE ID = @ID";

              SqlCommand update_command = new SqlCommand(usql, sql_connection);
              update_command.CommandTimeout = 0;

              update_command.Parameters.AddWithValue("@ID", so.Id);
              update_command.Parameters.AddWithValue("@STATUS", so.Status);
              update_command.Parameters.AddWithValue("@STRTDATE", so.Strtdate);
              update_command.Parameters.AddWithValue("@STRTDATER", so.Strtdater);
              update_command.Parameters.AddWithValue("@CONTRACT", so.Contract);
              update_command.Parameters.AddWithValue("@PROJDESC", so.Projdesc);
              update_command.Parameters.AddWithValue("@PROJLOC", so.Projloc);
              update_command.Parameters.AddWithValue("@FWGT", so.Fwgt);
              update_command.Parameters.AddWithValue("@PCITYST", so.Pcityst);
              update_command.Parameters.AddWithValue("@ITEM5A", so.Item5a);
              update_command.Parameters.AddWithValue("@ITEM5B", so.Item5b);
              update_command.Parameters.AddWithValue("@ITEM5C", so.Item5c);
              update_command.Parameters.AddWithValue("@RVITM5C", so.Rvitm5c);
              update_command.Parameters.AddWithValue("@ITEM6", so.Item6);
              update_command.Parameters.AddWithValue("@CAPEXP", so.Capexp);
              update_command.Parameters.AddWithValue("@COMPDATE", so.Compdate);
              update_command.Parameters.AddWithValue("@FUTCOMPD", so.Futcompd);
              update_command.Parameters.AddWithValue("@ITEM5AR", so.Item5ar);
              update_command.Parameters.AddWithValue("@ITEM5BR", so.Item5br);
              update_command.Parameters.AddWithValue("@RVITM5CR", so.Rvitm5cr);
              update_command.Parameters.AddWithValue("@ITEM6R", so.Item6r);
              update_command.Parameters.AddWithValue("@CAPEXPR", so.Capexpr);
              update_command.Parameters.AddWithValue("@COMPDATER", so.Compdater);
              update_command.Parameters.AddWithValue("@FUTCOMPDR", so.Futcompdr);
              update_command.Parameters.AddWithValue("@FLAG5A", so.Flag5a);
              update_command.Parameters.AddWithValue("@FLAG5B", so.Flag5b);
              update_command.Parameters.AddWithValue("@FLAG5C", so.Flag5c);
              update_command.Parameters.AddWithValue("@FLAGR5C", so.Flagr5c);
              update_command.Parameters.AddWithValue("@FLAGITM6", so.Flagitm6);
              update_command.Parameters.AddWithValue("@FLAGCAP", so.Flagcap);
              update_command.Parameters.AddWithValue("@FLAGSTRTDATE", so.Flagstrtdate);
              update_command.Parameters.AddWithValue("@FLAGCOMPDATE", so.Flagcompdate);
              update_command.Parameters.AddWithValue("@FLAGFUTCOMPD", so.Flagfutcompd);
              update_command.Parameters.AddWithValue("@RESPDATE", so.Repsdate);
              update_command.Parameters.AddWithValue("@REPCOMPD", so.Repcompd);
              update_command.Parameters.AddWithValue("@ACTIVE", so.Active);
              update_command.Parameters.AddWithValue("@OADJ", so.Oadj);
              update_command.Parameters.AddWithValue("@OFLG", so.Oflg);

                try
              {
                  sql_connection.Open();
                  int count = update_command.ExecuteNonQuery();
                  if (count > 0)
                      return true;
                  else
                      return false;
              }
              catch (SqlException ex)
              {
                  throw ex;
              }
              finally
              {
                  sql_connection.Close();
              }

          }
        
      }

      /*Add sample data */
      public void AddSampleData(Sample samp, TypeDBSource dbsource = TypeDBSource.Default)
      {
          string db_table = "dbo.Sample";
          if (dbsource == TypeDBSource.Hold)
              db_table = "dbo.Sample_Hold";

          SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

          string isql = "insert " + db_table 
                        + " (id, masterid, respid, projdesc, contract, projloc, pcityst, fwgt, active, status, statdate, strtdate, strtdater, "
                        + " repsdate, item5a, item5ar, item5b, item5br,item5c, rvitm5c, rvitm5cr, item6, item6r, capexp, capexpr, flag5a, flag5b, flag5c, "
                        + " flagr5c, flagitm6, flagcap, compdate, compdater, repcompd, futcompd, futcompdr, flagstrtdate, flagcompdate, flagfutcompd, "
                        + " flgmodel, model, oadj, oflg, sampseq, projlength)"
                  + "Values (@ID, @masterid, @respid, @projdesc, @contract, @projloc, @pcityst, @fwgt, @active, @status, @statdate, @strtdate, @strtdater, "
                         + "@repsdate, @item5a,@item5ar,@item5b,@item5br, @item5c,@rvitm5c,@rvitm5cr, @item6,@item6r, @capexp, @capexpr,@flag5a, @flag5b, @flag5c, "
                         + " @flagr5c, @flagitm6,@flagcap,@compdate,@compdater,@repcompd,@futcompd,@futcompdr,@flagstrtdate,@flagcompdate,@flagfutcompd, "
                         + "@flgmodel,@model,@oadj,@oflg,@sampseq,@projlength)";

          SqlCommand insert_command = new SqlCommand(isql, sql_connection);
          insert_command.CommandTimeout = 0;

          insert_command.Parameters.AddWithValue("@ID", samp.Id);
          insert_command.Parameters.AddWithValue("@MASTERID", samp.Masterid);
          insert_command.Parameters.AddWithValue("@RESPID", samp.Respid);
          insert_command.Parameters.AddWithValue("@PROJDESC", samp.Projdesc);
          insert_command.Parameters.AddWithValue("@CONTRACT", samp.Contract);
          insert_command.Parameters.AddWithValue("@PROJLOC", samp.Projloc);
          insert_command.Parameters.AddWithValue("@PCITYST", samp.Pcityst);
          insert_command.Parameters.AddWithValue("@FWGT", samp.Fwgt);
          insert_command.Parameters.AddWithValue("@ACTIVE", samp.Active);
          insert_command.Parameters.AddWithValue("@STATUS", samp.Status);
          insert_command.Parameters.AddWithValue("@STATDATE", samp.Statdate);
          insert_command.Parameters.AddWithValue("@STRTDATE", samp.Strtdate);
          insert_command.Parameters.AddWithValue("@STRTDATER", samp.Strtdater);
          insert_command.Parameters.AddWithValue("@REPSDATE", samp.Repsdate);         
          insert_command.Parameters.AddWithValue("@ITEM5A", samp.Item5a);
          insert_command.Parameters.AddWithValue("@ITEM5AR", samp.Item5ar);
          insert_command.Parameters.AddWithValue("@ITEM5B", samp.Item5b);
          insert_command.Parameters.AddWithValue("@ITEM5BR", samp.Item5br);
          insert_command.Parameters.AddWithValue("@ITEM5C", samp.Item5c);
          insert_command.Parameters.AddWithValue("@RVITM5C", samp.Rvitm5c);
          insert_command.Parameters.AddWithValue("@RVITM5CR", samp.Rvitm5cr);
          insert_command.Parameters.AddWithValue("@ITEM6", samp.Item6);
          insert_command.Parameters.AddWithValue("@ITEM6R", samp.Item6r);
          insert_command.Parameters.AddWithValue("@CAPEXP", samp.Capexp);
          insert_command.Parameters.AddWithValue("@CAPEXPR", samp.Capexpr);
          insert_command.Parameters.AddWithValue("@FLAG5A", samp.Flag5a);
          insert_command.Parameters.AddWithValue("@FLAG5B", samp.Flag5b);
          insert_command.Parameters.AddWithValue("@FLAG5C", samp.Flag5c);
          insert_command.Parameters.AddWithValue("@FLAGR5C", samp.Flagr5c);
          insert_command.Parameters.AddWithValue("@FLAGITM6", samp.Flagitm6);
          insert_command.Parameters.AddWithValue("@FLAGCAP", samp.Flagcap);
          insert_command.Parameters.AddWithValue("@COMPDATE", samp.Compdate);
          insert_command.Parameters.AddWithValue("@COMPDATER", samp.Compdater);
          insert_command.Parameters.AddWithValue("@REPCOMPD", samp.Repcompd);
          insert_command.Parameters.AddWithValue("@FUTCOMPD", samp.Futcompd);
          insert_command.Parameters.AddWithValue("@FUTCOMPDR", samp.Futcompdr);
          insert_command.Parameters.AddWithValue("@FLAGSTRTDATE", samp.Flagstrtdate);
          insert_command.Parameters.AddWithValue("@FLAGCOMPDATE", samp.Flagcompdate);
          insert_command.Parameters.AddWithValue("@FLAGFUTCOMPD", samp.Flagfutcompd);
          insert_command.Parameters.AddWithValue("@FLGMODEL", samp.Flgmodel);
          insert_command.Parameters.AddWithValue("@MODEL", samp.Model);
          insert_command.Parameters.AddWithValue("@OADJ", samp.Oadj);
          insert_command.Parameters.AddWithValue("@OFLG", samp.Oflg);
          insert_command.Parameters.AddWithValue("@SAMPSEQ", samp.Sampseq);
          insert_command.Parameters.AddWithValue("@PROJLENGTH", samp.Projlength);

            try
          {
              sql_connection.Open();
              insert_command.ExecuteNonQuery();
          }
          catch (SqlException ex)
          {
              throw ex;

          }
          finally
          {
              sql_connection.Close();
          }

      }

      /*Delete sample data */
      public bool DeleteSampleData(string id, TypeDBSource dbsource = TypeDBSource.Default)
      {
          string db_table = "dbo.Sample";
          if (dbsource == TypeDBSource.Hold)
              db_table = "dbo.Sample_Hold";

          SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

          string usql = "DELETE FROM " + db_table + " WHERE ID = @ID" ;

          SqlCommand delete_command = new SqlCommand(usql, sql_connection);
          delete_command.Parameters.AddWithValue("@ID", id);
          delete_command.CommandTimeout = 0;
          try
          {
              sql_connection.Open();
              int count = delete_command.ExecuteNonQuery();
              if (count > 0)
              {
                  return true;
              }
              else
              {
                  return false;
              }
          }
          catch (SqlException ex)
          {
              throw ex;
          }


      }
    }
}
