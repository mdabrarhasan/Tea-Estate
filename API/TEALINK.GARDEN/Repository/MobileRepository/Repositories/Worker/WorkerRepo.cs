using System.Data;
using System.Dynamic;
using System.Net;
using Azure.Core.Serialization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TEALINK.GARDEN;
using TEALINK.GARDEN.Interface.Mobile;
using TEALINK.GARDEN.Model.Mobile.Models.Worker;
using TEALINK.Models.Worker;

namespace TEALINK.Repositories.Worker
{
    public class WorkerRepo:IWorkerRepo
    {
        public Context _context;
        SqlConnection _connection;
        IConfiguration _configuration;
        public WorkerRepo(Context contextt, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = contextt;
            _connection = (SqlConnection)_context.Database.GetDbConnection();
        }

        public WorkerRegistrationResponseDetails Registration(WorkerRegistrationRequest workerRegistrationRequest)
        {
            WorkerRegistrationResponseDetails registrationResponse = new WorkerRegistrationResponseDetails();
            try
            {
                _connection.Open();
                using (SqlCommand cmd = new SqlCommand("SprWorkerRegistration", _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    _ = cmd.Parameters.AddWithValue("@NAME", workerRegistrationRequest.NAME);
                    _ = cmd.Parameters.AddWithValue("@CODE", workerRegistrationRequest.CODE);
                    _ = cmd.Parameters.AddWithValue("@PFNUMBER", workerRegistrationRequest.PFNUMBER);
                    _ = cmd.Parameters.AddWithValue("@DIVISIONID", workerRegistrationRequest.DIVISIONID);
                    _ = cmd.Parameters.AddWithValue("@EMPNO", workerRegistrationRequest.EMPNO);
                    _ = cmd.Parameters.AddWithValue("@GENDERID", workerRegistrationRequest.GENDERID);
                    _ = cmd.Parameters.AddWithValue("@WORKERTYPEID", workerRegistrationRequest.WORKERTYPEID);
                    _ = cmd.Parameters.AddWithValue("@WORKERSUBTYPEID", workerRegistrationRequest.WORKERSUBTYPEID);
                    _ = cmd.Parameters.AddWithValue("@BOOKID", workerRegistrationRequest.BOOKID);
                    _ = cmd.Parameters.AddWithValue("@DISABLEDDISINCENTIVEID", workerRegistrationRequest.DISABLEDDISINCENTIVEID);
                    _ = cmd.Parameters.AddWithValue("@MONTHLYRATED", workerRegistrationRequest.MONTHLYRATED);
                    _ = cmd.Parameters.AddWithValue("@ESSENTIALWORKER", workerRegistrationRequest.ESSENTIALWORKER);
                    _ = cmd.Parameters.AddWithValue("@RETIREDID", workerRegistrationRequest.RETIREDID);
                    _ = cmd.Parameters.AddWithValue("@PLUCKINGBATCHID", workerRegistrationRequest.PLUCKINGBATCHID);
                    _ = cmd.Parameters.AddWithValue("@BATCHID", workerRegistrationRequest.BATCHID);
                    _ = cmd.Parameters.AddWithValue("@KAMJARIID", workerRegistrationRequest.KAMJARIID);
                    _ = cmd.Parameters.AddWithValue("@DATEOFBIRTH", workerRegistrationRequest.DATEOFBIRTH);
                    _ = cmd.Parameters.AddWithValue("@ADDRESS", workerRegistrationRequest.ADDRESS);
                    _ = cmd.Parameters.AddWithValue("@CONTACTNUMBER", workerRegistrationRequest.CONTACTNUMBER);
                    _ = cmd.Parameters.AddWithValue("@ENROLL", workerRegistrationRequest.ENROLL);
                    _ = cmd.Parameters.AddWithValue("@UNITID", workerRegistrationRequest.UNITID);
                   
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                  registrationResponse.MSG = reader.GetString("MSG");
                                  registrationResponse.CODE = reader.GetString("CODE");
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }
            _connection.Close();
            return registrationResponse;
        }

        public List<dynamic> GetWorkerListDynamic(WorkerFetchRequest workerFetchRequest)
        {
            List<dynamic> dynamicDt = new List<dynamic>();
            try
            {
                _connection.Open();
                using (SqlCommand cmd = new SqlCommand("sprGetWorkerlist", _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    _ = cmd.Parameters.AddWithValue("@type", workerFetchRequest.TYPE);
                    _ = cmd.Parameters.AddWithValue("@id", workerFetchRequest.WorkerID);

                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    sda.Fill(ds, "dynamic");
                    DataTable dt = ds.Tables["dynamic"];
                    if (dt != null) if (dt.Rows.Count > 0)
                            foreach (DataRow row in dt.Rows)
                            {
                                dynamic dyn = new ExpandoObject();
                                dynamicDt.Add(dyn);
                                foreach (DataColumn column in dt.Columns)
                                {
                                    var dic = (IDictionary<string, object>)dyn;
                                    dic[column.ColumnName] = row[column];
                                }
                            }
                }
            }
            catch (Exception ex) { }
            _connection.Close();
            return dynamicDt;
        }

        public List<WorkerFetchResponseDetails> GetWorker(WorkerFetchRequest workerFetchRequest)
        {
            List<WorkerFetchResponseDetails> workerResponselist = new List<WorkerFetchResponseDetails>();
            try
            {
                _connection.Open();
                using (SqlCommand cmd = new SqlCommand("sprGetWorkerlist", _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    _ = cmd.Parameters.AddWithValue("@type", workerFetchRequest.TYPE);
                    _ = cmd.Parameters.AddWithValue("@id", workerFetchRequest.WorkerID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                WorkerFetchResponseDetails workerResponse = new WorkerFetchResponseDetails();
                                workerResponse.ID = reader.GetInt32("ID");
                                workerResponse.NAME = reader.GetString("NAME");
                                workerResponse.CODE = reader.GetString("CODE");
                                workerResponse.PFNUMBER = reader.GetString("PFNUMBER");
                                workerResponse.Division = reader.GetString("Division");
                                workerResponse.EMPNO = reader.GetString("EMPNO");
                                workerResponse.Gender = reader.GetString("Gender");
                                workerResponse.Workertype = reader.GetString("Workertype");
                                workerResponse.Book = reader.GetString("Book");
                                workerResponse.Pluck = reader.GetString("Pluck");
                                workerResponse.Batch = reader.GetString("Batch");
                                workerResponse.Kamjari = reader.GetString("Kamjari");
                                workerResponse.DATEOFBIRTH = reader.GetDateTime("DATEOFBIRTH");
                                workerResponse.ADDRESS = reader.GetString("ADDRESS");
                                workerResponse.CONTACTNUMBER = reader.GetString("CONTACTNUMBER");
                                workerResponse.ERPENROLL = reader.GetInt32("ERPENROLL");
                                workerResponse.UNITID = reader.GetInt32("UNITID");
                                workerResponse.WORKERSUBTYPEID = reader.GetInt32("WORKERSUBTYPEID");
                                workerResponse.DISABLEDEDISINCENTIVEID = reader.GetBoolean("DISABLEDDISINCENTIVEID");
                                workerResponse.MONTHLYRATED = reader.GetBoolean("MONTHLYRATED");
                                workerResponse.RETIREDID = reader.GetBoolean("RETIREDID");
                                workerResponselist.Add(workerResponse);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }
            _connection.Close();
            return workerResponselist;
        }

        public List<dynamic> GetBookByWorker(int BookId)
        {
            List<dynamic> dynamicDt = new List<dynamic>();
            try
            {
                _connection.Open();
                using (SqlCommand cmd = new SqlCommand("sprGetBookByEMP", _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    _ = cmd.Parameters.AddWithValue("@BookId", BookId);

                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    sda.Fill(ds, "dynamic");
                    DataTable dt = ds.Tables["dynamic"];
                    if (dt != null) if (dt.Rows.Count > 0)
                            foreach (DataRow row in dt.Rows)
                            {
                                dynamic dyn = new ExpandoObject();
                                dynamicDt.Add(dyn);
                                foreach (DataColumn column in dt.Columns)
                                {
                                    var dic = (IDictionary<string, object>)dyn;
                                    dic[column.ColumnName] = row[column];
                                }
                            }
                }
            }
            catch (Exception ex) { }
            _connection.Close();
            return dynamicDt;
        }

        public List<dynamic> WorkerImageManipulation(WorkerImageRequest request)
        {
            List<dynamic> dynamicDt = new List<dynamic>();
            try
            {
                _connection.Open();
                using (SqlCommand cmd = new SqlCommand("sprEmpFaceManipulation", _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    int ls = int.Parse(request.name); 

                    _ = cmd.Parameters.AddWithValue("@empCode", ls);
                    _ = cmd.Parameters.AddWithValue("@bookId", request.bookId);
                    _ = cmd.Parameters.AddWithValue("@binFaceJpg", request.faceJpg);
                    _ = cmd.Parameters.AddWithValue("@binTemp", request.templates);

                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    sda.Fill(ds, "dynamic");
                    DataTable dt = ds.Tables["dynamic"];
                    if (dt != null) if (dt.Rows.Count > 0)
                            foreach (DataRow row in dt.Rows)
                            {
                                dynamic dyn = new ExpandoObject();
                                dynamicDt.Add(dyn);
                                foreach (DataColumn column in dt.Columns)
                                {
                                    var dic = (IDictionary<string, object>)dyn;
                                    dic[column.ColumnName] = row[column];
                                }
                            }
                }
            }
            catch (Exception ex) { }
            _connection.Close();
            return dynamicDt;
        }

        public List<dynamic> FetchWorkerImage(int bookId)
        {
            List<dynamic> dynamicDt = new List<dynamic>();
            try
            {
                _connection.Open();
                using (SqlCommand cmd = new SqlCommand("sprGetAllWorkerImage", _connection))
                {
                   
                    cmd.CommandType = CommandType.StoredProcedure;
                    _ = cmd.Parameters.AddWithValue("@BookId", bookId);


                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    sda.Fill(ds, "dynamic");
                    DataTable dt = ds.Tables["dynamic"];
                    if (dt != null) if (dt.Rows.Count > 0)
                            foreach (DataRow row in dt.Rows)
                            {
                                dynamic dyn = new ExpandoObject();
                                dynamicDt.Add(dyn);
                                foreach (DataColumn column in dt.Columns)
                                {
                                    var dic = (IDictionary<string, object>)dyn;
                                    dic[column.ColumnName] = row[column];
                                }
                            }
                }
            }
            catch (Exception ex) { }
            _connection.Close();
            return dynamicDt;
        }

    }
}
