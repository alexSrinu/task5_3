using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using static task5.Models.Details;

namespace task5.Models
{
    public class Repository
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
        /*  public void Register(Details model)
          {
              using (var cmd = new SqlCommand("InsertTask", con))
              {
                  cmd.CommandType = CommandType.StoredProcedure;

                  cmd.Parameters.AddWithValue("@Name", model.Name);
                  cmd.Parameters.AddWithValue("@Mobile", model.Mobile);
                  cmd.Parameters.AddWithValue("@Email", model.Email);
                  cmd.Parameters.AddWithValue("@StateId", model.StateId);
                  cmd.Parameters.AddWithValue("@CityId", model.CityId);
                  cmd.Parameters.AddWithValue("@CreatedOn", model.CreatedOn);


                  string hobbyIds = string.Join(",", model.SelectedHobbies);
                  cmd.Parameters.AddWithValue("@Hid", hobbyIds);

                  con.Open();
                  cmd.ExecuteNonQuery();
                  con.Close();
              }
          }*/
        /*  public void Register(Details model)
          {

                  using (var cmd = new SqlCommand("InsertTask", con))
                  {
                      cmd.CommandType = CommandType.StoredProcedure;

                      // Add parameters for task details
                      cmd.Parameters.AddWithValue("@Name", model.Name);
                      cmd.Parameters.AddWithValue("@Mobile", model.Mobile);
                      cmd.Parameters.AddWithValue("@Email", model.Email);
                      cmd.Parameters.AddWithValue("@StateId", model.StateId);
                      cmd.Parameters.AddWithValue("@CityId", model.CityId);
                      cmd.Parameters.AddWithValue("@CreatedOn", model.CreatedOn); 


                      DataTable hobbyTable = new DataTable();
                      hobbyTable.Columns.Add("HobbyId", typeof(int)); 


                      foreach (var hobbyId in model.SelectedHobbies)
                      {
                          hobbyTable.Rows.Add(hobbyId);
                      }


                      SqlParameter tvpParam = cmd.Parameters.AddWithValue("@HobbyIds", hobbyTable);
                      tvpParam.SqlDbType = SqlDbType.Structured; 

                      con.Open();
                      cmd.ExecuteNonQuery();
                      con.Close();
                  }
              }*/
        public void Register(Details model)
        {
            using (var cmd = new SqlCommand("InsertTask", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Mobile", model.Mobile);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@StateId", model.StateId);
                cmd.Parameters.AddWithValue("@CityId", model.CityId);
                cmd.Parameters.AddWithValue("@CreatedOn", model.CreatedOn);

                // Create DataTable for HobbyIds
                DataTable hobbyTable = new DataTable();
                hobbyTable.Columns.Add("HobbyId", typeof(int)); // Ensure this matches your TVP definition

                // Populate DataTable with selected hobby IDs
                foreach (var hobby in model.Hobbies.Where(h => h.IsChecked))
                {
                    hobbyTable.Rows.Add(hobby.HobbyId);
                }

                // Add the DataTable as a parameter
                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@HobbyIds", hobbyTable);
                tvpParam.SqlDbType = SqlDbType.Structured;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }



        public List<Details> GetDetails1(string stat)
        {
            List<Details> obj = new List<Details>();

            SqlCommand cmd = new SqlCommand("PROC_CHECKBOX1", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StateName", stat);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                obj.Add(new Details
                {
                    Id = Convert.ToInt32(dr["TaskId"]),
                    Name = Convert.ToString(dr["Name"]),
                    Mobile = Convert.ToString(dr["Mobile"]),
                    Email = Convert.ToString(dr["Email"]),

                    CityName = Convert.ToString(dr["CityName"]),
                    StateName = Convert.ToString(dr["StateName"]),
                    CreatedOn = Convert.ToDateTime(dr["CretedOn"])


                }); ;
            }
            return obj;
        }
        public List<Details> GetDetails2(string city)
        {
            List<Details> obj = new List<Details>();

            SqlCommand cmd = new SqlCommand("PROC_CHECKBOX2", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CityName", city);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                obj.Add(new Details
                {
                    Id = Convert.ToInt32(dr["TaskId"]),
                    Name = Convert.ToString(dr["Name"]),
                    Mobile = Convert.ToString(dr["Mobile"]),
                    Email = Convert.ToString(dr["Email"]),

                    CityName = Convert.ToString(dr["CityName"]),
                    StateName = Convert.ToString(dr["StateName"]),
                    CreatedOn = Convert.ToDateTime(dr["CretedOn"])


                }); ;
            }
            return obj;
        }
        /*  public List<City> GetCitiesByState(string stateName)
          {
              List<City> cities = new List<City>();

              using (SqlCommand cmd = new SqlCommand("PROC_CHECKBOX5", con))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Parameters.AddWithValue("@StateName", stateName);

                  SqlDataAdapter da = new SqlDataAdapter(cmd);
                  DataTable dt = new DataTable();
                  con.Open();
                  da.Fill(dt);
                  con.Close();

                  foreach (DataRow dr in dt.Rows)
                  {
                      cities.Add(new City
                      {
                          CityName = Convert.ToString(dr["CityName"])
                      });
                  }
              }

              return cities;
          }*/
        public List<string> GetCitiesByState(string stateName)
        {
            List<string> cities = new List<string>(); // Change to List<string>

            using (SqlCommand cmd = new SqlCommand("PROC_CHECKBOX5", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StateName", stateName);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    cities.Add(Convert.ToString(dr["CityName"])); // Add city names directly
                }
            }

            return cities; // Return List<string> directly
        }
        public List<Details> GetDetails3(string statename)
        {
            List<Details> obj = new List<Details>();

            SqlCommand cmd = new SqlCommand("PROC_CHECKBOX2", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StateName", statename);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                obj.Add(new Details
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = Convert.ToString(dr["Name"]),
                    Mobile = Convert.ToString(dr["Mobile"]),
                    Email = Convert.ToString(dr["Email"]),

                    CityName = Convert.ToString(dr["CityName"]),
                    StateName = Convert.ToString(dr["StateName"]),
                    CreatedOn = Convert.ToDateTime(dr["CretedOn"])


                }); ;
            }
            return obj;
        }
        public List<Details> GetDetails4(string statename)
        {
            List<Details> obj = new List<Details>();

            SqlCommand cmd = new SqlCommand("PROC_CHECKBOX2", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StateName", statename);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                obj.Add(new Details
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = Convert.ToString(dr["Name"]),
                    Mobile = Convert.ToString(dr["Mobile"]),
                    Email = Convert.ToString(dr["Email"]),

                    CityName = Convert.ToString(dr["CityName"]),
                    StateName = Convert.ToString(dr["StateName"]),
                    CreatedOn = Convert.ToDateTime(dr["CretedOn"])


                }); ;
            }
            return obj;
        }
        public List<Details> GetDetailsCities(string cityNames)
        {
            List<Details> obj = new List<Details>();

            SqlCommand cmd = new SqlCommand("GetDetailsByCities", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CityNames", cityNames);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                obj.Add(new Details
                {
                    Id = Convert.ToInt32(dr["TaskId"]),
                    Name = Convert.ToString(dr["Name"]),
                    Mobile = Convert.ToString(dr["Mobile"]),
                    Email = Convert.ToString(dr["Email"]),

                    CityName = Convert.ToString(dr["CityName"]),
                    StateName = Convert.ToString(dr["StateName"]),
                    CreatedOn = Convert.ToDateTime(dr["CretedOn"])


                }); ;
            }
            return obj;
        }
        public List<State> GetStates()
        {
            List<State> states = new List<State>();

          
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT * FROM State";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            states.Add(new State
                            {
                                StateId = reader["StateId"] != DBNull.Value ? Convert.ToInt32(reader["StateId"]) : 0,
                                StateName = reader["StateName"].ToString()
                            });
                        }
                    }
                }
            } 

            return states;
        }
        public List<Hobby> GetHobbies()
        {
            List<Hobby> hobbies = new List<Hobby>();

            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT * FROM Hobby"; 

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            hobbies.Add(new Hobby
                            {
                                HobbyId = reader["HobbyId"] != DBNull.Value ? Convert.ToInt32(reader["HobbyId"]) : 0,
                                HobbyName = reader["HobbyName"].ToString()
                            });
                        }
                    }
                }
            }

            return hobbies;
        }




        public List<City> GetCities(int stateId)
        {
            List<City> cities = new List<City>();
            con.Open();
            string query = "SELECT * FROM City WHERE StateId = @StateId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@StateId", stateId);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cities.Add(new City
                {
                    CityId = Convert.ToInt32(reader["CityId"]),
                    CityName = reader["CityName"].ToString(),
                    StateId = Convert.ToInt32(reader["StateId"])
                });
            }
            con.Close();
            return cities;
        }
        public List<string> HobCheck1(int id)
        {
            List<string> hobbies = new List<string>();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("proc_HobCheck", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);

                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                               
                                string hobbyId = reader["Hid"].ToString();
                                if (!string.IsNullOrEmpty(hobbyId))
                                {
                                    hobbies.Add(hobbyId); 
                                }
                            }
                        }

                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                throw new ApplicationException("An error occurred while fetching hobbies.", ex);
            }

            return hobbies;
        }

        /*   public List<string> HobCheck(int id)
           {
               List<string> hobbies = new List<string>();
               try
               {
                   string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
                   using (SqlConnection con = new SqlConnection(connectionString))
                   {
                       using (SqlCommand cmd = new SqlCommand("GetTaskHobbyIds", con))
                       {
                           cmd.CommandType = CommandType.StoredProcedure;
                           cmd.Parameters.AddWithValue("@TaskId", id);

                           con.Open();

                           using (SqlDataReader reader = cmd.ExecuteReader())
                           {
                               while (reader.Read())
                               {
                                   //string hobbiestring = reader["Hid"].ToString();
                                   string hobbiesString = reader["HobbyID"].ToString();
                                   hobbies = hobbiesString.Split(',').ToList();
                               }
                           }

                           con.Close();
                       }
                   }
               }
               catch (Exception ex)
               {
                   // Log the exception or handle it accordingly
                   throw new ApplicationException("An error occurred while fetching hobbies.", ex);
               }

               return hobbies;
           }*/
        public List<int> HobCheck(int id)
        {
            List<int> hobbies = new List<int>();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetTaskHobbyIds", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TaskId", id);

                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                
                                hobbies.Add(reader.GetInt32(reader.GetOrdinal("HobbyID"))); 
                            }
                        }

                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
               
                throw new ApplicationException("An error occurred while fetching hobbies.", ex);
            }

            return hobbies;
        }




        public List<Details> GetDetails()
        {
            List<Details> obj = new List<Details>();

            SqlCommand cmd = new SqlCommand("selecttask1", con);
            cmd.CommandType = CommandType.StoredProcedure;
            // cmd.Parameters.AddWithValue("@id", id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                obj.Add(new Details
                {
                    Id = Convert.ToInt32(dr["TaskId"]),
                    Name = Convert.ToString(dr["Name"]),
                    Mobile = Convert.ToString(dr["Mobile"]),
                    Email = Convert.ToString(dr["Email"]),
                    CityId = Convert.ToInt32(dr["CityId"]),

                    StateId = Convert.ToInt32(dr["StateId"]),
                    CityName = Convert.ToString(dr["CityName"]),
                    StateName = Convert.ToString(dr["StateName"]),
                    CreatedOn = Convert.ToDateTime(dr["CretedOn"])

                }); ;
            }
            return obj;
        }
        public List<Details> GetDetails(int id)
        {
            List<Details> obj = new List<Details>();

            SqlCommand cmd = new SqlCommand("selecttask2", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                obj.Add(new Details
                {
                    Id = Convert.ToInt32(dr["TaskId"]),
                    Name = Convert.ToString(dr["Name"]),
                    Mobile = Convert.ToString(dr["Mobile"]),
                    Email = Convert.ToString(dr["Email"]),
                    CityId = Convert.ToInt32(dr["CityId"]),

                    StateId = Convert.ToInt32(dr["StateId"]),

                    CityName = Convert.ToString(dr["CityName"]),
                    StateName = Convert.ToString(dr["StateName"]),
                     

                }); ;
            }
            return obj;
        }

        /*  public void Edit(int id, Details r)
          {
              using (var cmd = new SqlCommand("UpdateTask", con))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Parameters.AddWithValue("@Id", id); 
                  cmd.Parameters.AddWithValue("@Name", r.Name);
                  cmd.Parameters.AddWithValue("@Mobile", r.Mobile);
                  cmd.Parameters.AddWithValue("@Email", r.Email);
                  cmd.Parameters.AddWithValue("@StateId", r.StateId);
                  cmd.Parameters.AddWithValue("@CityId", r.CityId);
                  cmd.Parameters.AddWithValue("@CreatedOn", r.CreatedOn);


                  if (r.SelectedHobbies != null && r.SelectedHobbies.Count > 0)
                  {

                      string hobbyIds = string.Join(",", r.SelectedHobbies.Select(h => h.ToString()));
                      cmd.Parameters.AddWithValue("@Hid", hobbyIds);
                  }
                  else
                  {

                      cmd.Parameters.AddWithValue("@Hid", DBNull.Value); 
                  }

                  con.Open();
                  cmd.ExecuteNonQuery();
                  con.Close();
              }
          }*/
        public void Edit(int id, Details r)
        {
            using (var cmd = new SqlCommand("UpdateTask", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", r.Name);
                cmd.Parameters.AddWithValue("@Mobile", r.Mobile);
                cmd.Parameters.AddWithValue("@Email", r.Email);
                cmd.Parameters.AddWithValue("@StateId", r.StateId);
                cmd.Parameters.AddWithValue("@CityId", r.CityId);
                cmd.Parameters.AddWithValue("@CreatedOn", r.CreatedOn);

               
                DataTable hobbyTable = new DataTable();
                hobbyTable.Columns.Add("HobbyId", typeof(int)); 

                
                if (r.SelectedHobbies != null && r.SelectedHobbies.Count > 0)
                {
                    foreach (var hobbyId in r.SelectedHobbies)
                    {
                        hobbyTable.Rows.Add(hobbyId);
                    }
                }

               
                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@HobbyIds", hobbyTable);
                tvpParam.SqlDbType = SqlDbType.Structured;

                
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }





        public void Delete(int id)
          {
              SqlCommand cmd = new SqlCommand("proc_delete", con);
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.Parameters.AddWithValue("@id", id);
              con.Open();
              cmd.ExecuteNonQuery();
              con.Close();
          }
      
        /*  public List<Details> GetPagedData(int pageSize, int pageNumber, string searchTerm, string cityNames, DateTime? startDate, DateTime? endDate, out int totalCount)
          {
              List<Details> obj1 = new List<Details>();

              using (SqlCommand cmd = new SqlCommand("PageTask", con))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Parameters.AddWithValue("@PageSize", pageSize);
                  cmd.Parameters.AddWithValue("@PageNumber", pageNumber);

                  SqlParameter totalCountParam = new SqlParameter("@TotalCount", SqlDbType.Int);
                  totalCountParam.Direction = ParameterDirection.Output;
                  cmd.Parameters.Add(totalCountParam);

                  cmd.Parameters.AddWithValue("@SearchTerm", string.IsNullOrEmpty(searchTerm) ? (object)DBNull.Value : searchTerm);
                  cmd.Parameters.AddWithValue("@CityNames", string.IsNullOrEmpty(cityNames) ? (object)DBNull.Value : cityNames);
                  cmd.Parameters.AddWithValue("@StartDate", startDate.HasValue ? (object)startDate.Value : DBNull.Value);
                  cmd.Parameters.AddWithValue("@EndDate", endDate.HasValue ? (object)endDate.Value : DBNull.Value);

                  SqlDataAdapter da = new SqlDataAdapter(cmd);
                  DataTable dt = new DataTable();
                  con.Open();
                  da.Fill(dt);
                  con.Close(); 

                  totalCount = (int)cmd.Parameters["@TotalCount"].Value;

                  foreach (DataRow dr in dt.Rows)
                  {
                     // var hobbiesString = Convert.ToString(dr["Hobbies"]);

                   //   var hobbiesList = string.IsNullOrEmpty(hobbiesString) ? new List<string>() : hobbiesString.Split(',').ToList();

                      obj1.Add(new Details
                      {
                          Id = Convert.ToInt32(dr["TaskId"]),
                          Name = Convert.ToString(dr["Name"]),
                          Mobile = Convert.ToString(dr["Mobile"]),
                          Email = Convert.ToString(dr["Email"]),
                          CityId = Convert.ToInt32(dr["CityId"]),
                          StateId = Convert.ToInt32(dr["StateId"]),
                          CityName = Convert.ToString(dr["CityName"]),
                          StateName = Convert.ToString(dr["StateName"]),
                          CreatedOn = Convert.IsDBNull(dr["CreatedOn"]) ? DateTime.MinValue : Convert.ToDateTime(dr["CreatedOn"]),
                          HobbyName = Convert.ToString(dr["Hname"]),
                         HobbyId =Convert.ToInt32(dr["Hid"])

                      });
                  }
              }

              return obj1;
          }*/
        public List<Details> GetPagedData(int pageSize, int pageNumber, string searchTerm, string cityNames, DateTime? startDate, DateTime? endDate, out int totalCount)
        {
            List<Details> detailsList = new List<Details>();

            using (SqlCommand cmd = new SqlCommand("PageTask", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Set parameters
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                cmd.Parameters.AddWithValue("@PageNumber", pageNumber);

                SqlParameter totalCountParam = new SqlParameter("@TotalCount", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(totalCountParam);

                cmd.Parameters.AddWithValue("@SearchTerm", string.IsNullOrEmpty(searchTerm) ? (object)DBNull.Value : searchTerm);
                cmd.Parameters.AddWithValue("@CityNames", string.IsNullOrEmpty(cityNames) ? (object)DBNull.Value : cityNames);
                cmd.Parameters.AddWithValue("@StartDate", startDate.HasValue ? (object)startDate.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@EndDate", endDate.HasValue ? (object)endDate.Value : DBNull.Value);

                // Fill the DataTable
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    con.Open();
                    da.Fill(dt);
                    con.Close();

                    // Get total count
                    totalCount = (int)cmd.Parameters["@TotalCount"].Value;

                    // Convert DataTable rows to List<Details>
                    foreach (DataRow dr in dt.Rows)
                    {
                        detailsList.Add(new Details
                        {
                            Id = dr["TaskId"] != DBNull.Value ? Convert.ToInt32(dr["TaskId"]) : 0, // Default value for Id
                            Name = dr["Name"] != DBNull.Value ? Convert.ToString(dr["Name"]) : string.Empty,
                            Mobile = dr["Mobile"] != DBNull.Value ? Convert.ToString(dr["Mobile"]) : string.Empty,
                            Email = dr["Email"] != DBNull.Value ? Convert.ToString(dr["Email"]) : string.Empty,
                            CityId = dr["CityId"] != DBNull.Value ? Convert.ToInt32(dr["CityId"]) : 0, // Default value for CityId
                            StateId = dr["StateId"] != DBNull.Value ? Convert.ToInt32(dr["StateId"]) : 0, // Default value for StateId
                            CityName = dr["CityName"] != DBNull.Value ? Convert.ToString(dr["CityName"]) : string.Empty,
                            StateName = dr["StateName"] != DBNull.Value ? Convert.ToString(dr["StateName"]) : string.Empty,
                            CreatedOn = dr["CreatedOn"] != DBNull.Value ? Convert.ToDateTime(dr["CreatedOn"]) : DateTime.MinValue,
                           
                        });
                    }
                }
            }

            return detailsList;
        }


        public List<Details> GetPagedData1(int pageSize, int pageNumber,  out int totalCount)
           {
               List<Details> obj1 = new List<Details>();

               using (SqlCommand cmd = new SqlCommand("PageTask1", con))
               {
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.Parameters.AddWithValue("@PageSize", pageSize);
                   cmd.Parameters.AddWithValue("@PageNumber", pageNumber);

                   SqlParameter totalCountParam = new SqlParameter("@TotalCount", SqlDbType.Int);
                   totalCountParam.Direction = ParameterDirection.Output;
                   cmd.Parameters.Add(totalCountParam);

              

                   SqlDataAdapter da = new SqlDataAdapter(cmd);
                   DataTable dt = new DataTable();
                   con.Open();
                   da.Fill(dt);
                   totalCount = (int)cmd.Parameters["@TotalCount"].Value;

                   foreach (DataRow dr in dt.Rows)
                   {
                       obj1.Add(new Details
                       {
                           Id = Convert.ToInt32(dr["TaskId"]),
                           Name = Convert.ToString(dr["Name"]),
                           Mobile = Convert.ToString(dr["Mobile"]),
                           Email = Convert.ToString(dr["Email"]),
                           CityId = Convert.ToInt32(dr["CityId"]),
                           StateId = Convert.ToInt32(dr["StateId"]),
                           CityName = Convert.ToString(dr["CityName"]),
                           StateName = Convert.ToString(dr["StateName"]),

                          // CreatedOn = Convert.IsDBNull(dr["CreatedOn"]) ? DateTime.MinValue : Convert.ToDateTime(dr["CreatedOn"]) 
                       });
                   }
               }

               return obj1;
           }



    }

}


