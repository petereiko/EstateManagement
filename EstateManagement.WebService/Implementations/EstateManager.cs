using EstateManagement.WebService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace EstateManagement.WebService.Implementations
{
    public class EstateManager
    {
        public CreateEstateResponse CreateEstate(CreateEstateRequest request)
        {
            CreateEstateResponse response = new CreateEstateResponse();
            int rowsAffected = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
                {
                    string query = $"insert into Estates values ('{request.Name}', 1, {request.lgaId})";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        rowsAffected = cmd.ExecuteNonQuery();
                        if (conn.State == ConnectionState.Open)
                            conn.Close();
                        if (rowsAffected > 0)
                        {
                            response.Status = true;
                            response.StatusCode = 201;
                            response.Message = "Estate created successfully";
                        }
                        else
                        {
                            response.Status = false;
                            response.StatusCode = 200;
                            response.Message = "Estate was not created";
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                response.Message = "An error occurred in the remote server.";
                response.Status = false;
                response.StatusCode = 500;
                LogError(ex);
            }


            return response;
        }

        public List<StateModel> GetAllStates()
        {
            List<StateModel> response = new List<StateModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
                {
                    string query = $"select Id, Name from States";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            StateModel model = new StateModel
                            {
                                Id = Convert.ToInt32(rdr["Id"]),
                                Name = rdr["Name"].ToString()
                            };
                            response.Add(model);
                        }
                        if (conn.State == ConnectionState.Open)
                            conn.Close();
                    }

                }

            }
            catch (Exception ex)
            {
                LogError(ex);
            }


            return response;
        }

        public List<LgaModel> GetAllLgaByStateId(int stateId)
        {
            List<LgaModel> response = new List<LgaModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
                {
                    string query = $"select Id, Name from Lgas where stateId={stateId.ToString()}";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            LgaModel model = new LgaModel
                            {
                                Id = Convert.ToInt32(rdr["Id"]),
                                LgaName = rdr["Name"].ToString(),
                                StateId = stateId
                            };
                            response.Add(model);
                        }
                        if (conn.State == ConnectionState.Open)
                            conn.Close();
                    }

                }

            }
            catch (Exception ex)
            {
                LogError(ex);
            }


            return response;
        }


        public static void LogError(Exception ex)
        {
            string logPath= ConfigurationManager.AppSettings["LogPath"].ToString();
            if(!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);

            string fileName = DateTime.Now.ToString("dd-MM-yyyy") + ".txt";

            string fullPath = Path.Combine(logPath, fileName);
            if(!File.Exists(fullPath))
            {
                FileStream stream = File.Create(fullPath);
                stream.Dispose();
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"-------------------------------{DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:tt")}-----------------------------");
            sb.AppendLine(ex.Message);
            sb.AppendLine(ex.StackTrace);

            string content = sb.ToString();

            File.AppendAllText(content, fullPath);
        }
    }
}