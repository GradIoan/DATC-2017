using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System;
using KappaAPI.Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Linq;

namespace KappaAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        public static string _connectionString = "Server=tcp:kappaserver.database.windows.net;Database=kappa_database;User ID =IonutGrad;Password=GradIonut1;Trusted_Connection=False;Encrypt=True;";


        [HttpGet]
        public string Get() // get data from Date
        {
            SqlConnection DBConn = new SqlConnection(_connectionString);
            SqlCommand getCommand = null;
            SqlDataReader reader;
            List<Date> dataFromTable = new List<Date>();
            try
            {
                DBConn.Open();
                string getDataFromDateTable = "SELECT * FROM Date";
                getCommand = new SqlCommand(getDataFromDateTable, DBConn);
                reader = getCommand.ExecuteReader();
                if ( reader.HasRows)
                {
                    while(reader.Read())
                    {
                        dataFromTable.Add(new Date
                        {
                            Data = Convert.ToDateTime(reader["Data"]),
                            Id = Convert.ToInt32(reader["Id"]),
                            Valoare = Convert.ToDouble(reader["Valoare"]),
                            Zona = Convert.ToInt32(reader["Zona"])
                        });
                    }
                }
            }
            catch (Exception exp)
            {

            }
            var serializedJson = JsonConvert.SerializeObject(dataFromTable);

            return serializedJson;
        }

        // GET api/values/5
        [HttpGet("{zona}")]
        public string Get(string zona) // get data from Zona
        {
            SqlConnection DBConn = new SqlConnection(_connectionString);
            SqlCommand getCommand = null;
            SqlDataReader reader;
            List<Zone> dataFromZoneTable = new List<Zone>();
            try
            {
                DBConn.Open();
                string getDataFromDateTable = "SELECT * FROM Zone";
                getCommand = new SqlCommand(getDataFromDateTable, DBConn);
                reader = getCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dataFromZoneTable.Add(new Zone
                        {
                            X1 = Convert.ToInt32(reader["X1"]),
                            Y1= Convert.ToInt32(reader["Y1"]),
                            X2 = Convert.ToInt32(reader["X2"]),
                            Y2 = Convert.ToInt32(reader["Y2"]),
                            Zona = Convert.ToInt32(reader["Zona"])
                        });
                    }
                }
            }
            catch (Exception exp)
            {
            }
            var serializedJson = JsonConvert.SerializeObject(dataFromZoneTable);

            return serializedJson;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] dynamic fromBody)
        {
            IEnumerable<Zone> Izone;
            IEnumerable<DateDePrelucrat> IdateDePrelucrat;
            SqlConnection DBConn = null;
            SqlCommand insertCommand = null;
            var value = Convert.ToString(fromBody);
            try
            {
                //value = request.Content.ReadAsStringAsync().Result;
                // if is zone
                Izone = JsonConvert.DeserializeObject<IEnumerable<Zone>>(value);
                // post zone
                if (Izone.First().X1 == 0 && Izone.First().X2 == 0 && Izone.First().Y1 == 0 && Izone.First().Y2 == 0) // if it seria
                { }
                else
                {
                    DBConn = new SqlConnection(_connectionString);
                    DBConn.Open();
                    foreach (var item in Izone)
                    {
                        string insertCmd = string.Format
                            (
                                "INSERT INTO Zone VALUES({0},{1},{2},{3},{4})",
                                item.Zona, item.X1, item.Y1, item.X2, item.Y2
                            );
                        insertCommand = new SqlCommand(insertCmd, DBConn);
                        insertCommand.ExecuteNonQuery();
                    }
                    if (DBConn != null)
                        DBConn.Dispose();
                    return;
                }
            }
            catch { }
            try
            {
                //value = request.Content.ReadAsStringAsync().Result;
                IdateDePrelucrat = JsonConvert.DeserializeObject<IEnumerable<DateDePrelucrat>>(value);
                DBConn = new SqlConnection(_connectionString);
                DBConn.Open();
                if (IdateDePrelucrat.First().Id == 0 && IdateDePrelucrat.First().Valoare == 0 && IdateDePrelucrat.First().Zona == 0)
                { }
                else
                {
                    foreach (var item in IdateDePrelucrat)
                    {
                        string insertCmd = string.Format
                            (
                                "INSERT INTO DateDePrelucrat VALUES({0},{1},{2})",
                                item.Zona, item.Id, item.Valoare
                            );
                        insertCommand = new SqlCommand(insertCmd, DBConn);
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
            catch {  }
            finally
            {
                if (DBConn != null)
                    DBConn.Dispose();
            }

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
