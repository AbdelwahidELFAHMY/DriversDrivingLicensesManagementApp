using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccessLayer
{
    public class clsDataAccessLayer
    {
        public static DataTable LoadCountries()
        {
            DataTable table = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select CountryName from Countries";

            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    table.Load(reader);
                }

                reader.Close();
            }catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally {
                connection.Close(); 
            }

            return table;
        }

        public static int AddNewPerson(string CIN,string FirstName, string SecondName, string ThirdName,
            string LastName, DateTime DateOfBirth, byte Gendor, string Address, string Phone, string Email,
            int NationalityID, string ImagePath)
        {
            int PersonID=-1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO [dbo].[People]
           ([NationalNo]
           ,[FirstName]
           ,[SecondName]
           ,[ThirdName]
           ,[LastName]
           ,[DateOfBirth]
           ,[Gendor]
           ,[Address]
           ,[Phone]
           ,[Email]
           ,[NationalityCountryID]
           ,[ImagePath])

           VALUES(@cin,@firstname,@secondname,@thirdname,@lastname,@dateofbirth,@gendor,@address,@phone,@email,
             @nationalityID,@imagePath);
              SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@cin", CIN);
            command.Parameters.AddWithValue("@firstname", FirstName);
            command.Parameters.AddWithValue("@secondname", SecondName);
            command.Parameters.AddWithValue("@thirdname", ThirdName);
            command.Parameters.AddWithValue("@lastname", LastName);
            command.Parameters.AddWithValue("@dateofbirth", DateOfBirth);
            command.Parameters.AddWithValue("@gendor", Gendor);
            command.Parameters.AddWithValue("@address", Address);
            command.Parameters.AddWithValue("@phone", Phone);
            command.Parameters.AddWithValue("@email", Email);
            command.Parameters.AddWithValue("@nationalityID", NationalityID);

            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@imagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@imagePath", System.DBNull.Value);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    PersonID = insertedID;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return PersonID;
            }

            finally
            {
                connection.Close();
            }
            return PersonID;

        }

        public static bool deletePerson(int PersonID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete People where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);

        }

        public static bool GetPersonByID(int PersonID,ref string NationalNo, ref string FirstName, ref string LastName,
            ref string SecondName, ref string ThirdName, ref DateTime DateOfBirth,ref byte Gendor, ref string Address,
            ref string Phone, ref string Email, ref int NationalityID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;

                    NationalNo = (string)reader["NationalNo"];
                    NationalityID = (int)reader["NationalityCountryID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];

                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }
                    Gendor = (byte)reader["Gendor"];

                }
                else
                {
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }


        public static bool GetPersonByNationalNo(string NationalNo,ref int PersonID, ref string FirstName, ref string LastName,
            ref string SecondName, ref string ThirdName, ref DateTime DateOfBirth, ref byte Gendor, ref string Address,
            ref string Phone, ref string Email, ref int NationalityID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;

                    PersonID = (int)reader["PersonID"];
                    NationalityID = (int)reader["NationalityCountryID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];

                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }
                    Gendor = (byte)reader["Gendor"];

                }
                else
                {
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName, string LastName,
             string SecondName, string ThirdName, DateTime DateOfBirth, byte Gendor,  string Address,
             string Phone, string Email, int NationalityID, string ImagePath)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[People]
                          SET [NationalNo] = @NationalNo
                             ,[FirstName] = @FirstName
                             ,[SecondName] = @SecondName
                             ,[ThirdName] = @ThirdName
                             ,[LastName] = @LastName
                             ,[DateOfBirth] = @DateOfBirth
                             ,[Gendor] = @Gendor
                             ,[Address] = @Address
                             ,[Phone] = @Phone
                             ,[Email] = @Email
                             ,[NationalityCountryID] = @NationalityCountryID
                             ,[ImagePath] = @ImagePath
                        WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@ThirdName", ThirdName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityID);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            if(ImagePath != "" && ImagePath != string.Empty)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);


            try
            {
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating person: " + ex.Message);
                return false;
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public static string Login(string username, string password)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
             SELECT PersonID
             FROM [dbo].[Users] u
             WHERE u.UserName = @UserName AND u.Password = @Password AND u.IsActive = 1";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                command.Parameters.AddWithValue("@UserName", username);
                command.Parameters.AddWithValue("@Password", password);

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    return result.ToString();
                }
                else
                {
                    return "Username ou Password Incorrect";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "An error occurred while processing your request.";
            }
            finally { connection.Close(); }
        }

        public static DataTable GetPeople()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection( clsDataAccessSettings.ConnectionString);

            string query = @"SELECT [PersonID]
                                             ,[NationalNo]
                                             ,[FirstName]
                                             ,[SecondName]
                                             ,[ThirdName]
                                             ,[LastName]
                                             ,[DateOfBirth]
                                             , CASE 
                                                   WHEN [Gendor] = 0 THEN 'Male'
                                                   WHEN [Gendor] = 1 THEN 'Female'
                                                   ELSE 'Unknown'
                                             END AS [Gendor]
                                             ,[Address]
                                             ,[Phone]
                                             ,[Email]
                                             ,c.[CountryName]
            FROM [dbo].[People]as p INNER JOIN Countries as c ON c.[CountryID] = p.[NationalityCountryID]";

            SqlCommand command = new SqlCommand(query,connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static string GetCountryById(int id)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select CountryName from Countries where CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryID", id);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if(result.ToString() != null)
                {
                    return result.ToString();
                } 
            }catch (Exception ex)
            {
                throw new Exception("Error retrieving country name: " + ex.Message);
                
            }
            finally
            {
                connection.Close();
            }
            return string.Empty;
        }
        static void Main(string[] args)
        {
        }
    }

    public class clsUserAccess
    {
        public static int AddNewUser(int PersonID,string UserName, string Password, byte IsActive)
        {
            int UserID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO [dbo].[Users]
           ([PersonID]
           ,[UserName]
           ,[Password]
           ,[IsActive])
            VALUES(@PersonID,@UserName,@Password,@IsActive );
              SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(@query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    UserID = insertedID;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return UserID;
            }

            finally
            {
                connection.Close();
            }
            return UserID;
        }

        public static bool IsUserExist(int PersonID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select found = 1 from Users where PersonID = @PersonID";

            SqlCommand cmd = new SqlCommand(@query, connection);

            cmd.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    return true;
                } else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return true;
            }

            finally
            {
                connection.Close();
            }
            return true;
        }

        public static bool IsPasswordCorrect(string Password, int PersonID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select found = 1 from Users where PersonID = @PersonID AND Password = @Password";

            SqlCommand cmd = new SqlCommand(@query, connection);

            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.Parameters.AddWithValue("@Password", Password);

            try
            {
                connection.Open();

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return true;
            }

            finally
            {
                connection.Close();
            }
        }

        public static bool ChangePassword(string Password, int PersonID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Users set Password = @Password where PersonID = @PersonID";

            SqlCommand cmd = new SqlCommand(@query, connection);

            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.Parameters.AddWithValue("@Password", Password);

            try
            {
                connection.Open();

                int AffectedRows = cmd.ExecuteNonQuery();

                if (AffectedRows == 1)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }

            finally
            {
                connection.Close();
            }
        }

        public static bool GetUserByPersonID(int PersonID, ref int UserId, ref string UserName,ref byte IsActive)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT [UserID]
                                    ,[UserName]
                                    ,[IsActive]
                                FROM [dbo].[Users] where PersonID = @PersonID";
                              
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    UserId = (int)reader["UserID"];
                    UserName = (string)reader["UserName"];
                    IsActive = Convert.ToByte(reader["IsActive"]);

                    return true;
                }
                else
                {
                    return false;
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }

            return false;
        }
        public static DataTable GetUsers()
            {
                DataTable dt = new DataTable();

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = @"SELECT [UserID]
                        ,U.[PersonID]
                        ,[UserName]
                        ,[FullName] = [FirstName] +' '+[SecondName]+' '+[ThirdName]+' '+[LastName]
                        ,[IsActive]
                    FROM [dbo].[Users] as U INNER JOIN [dbo].[People] as P ON P.PersonID = U.PersonID";
               
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)

                    {
                        dt.Load(reader);
                    }

                    reader.Close();
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);

                }
                finally
                {
                    connection.Close();
                }

                return dt;
            }
    }

    public class LicenseClasseAccess 
    {
        public static DataTable GetLicenseClasses()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT [ClassName],[ClassFees]  FROM [dbo].[LicenseClasses]";
                               
            SqlCommand command = new SqlCommand(query,connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);    

                }
                reader.Close();
                return dt;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                connection.Close();
            }
            return dt;

        }
    }

    public class ApplicationsAccess
    {
        public static float GetAppTypeFees(int AppTypeID) {
            float Fees = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select [ApplicationFees] FROM [dbo].[ApplicationTypes] where [ApplicationTypeID] = @AppTypeID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@AppTypeID", AppTypeID);

            try
            {
                connection.Open();

                object result = cmd.ExecuteScalar();
                if (result != null) 
                    Fees = float.Parse(result.ToString());
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
            return Fees;    
        } 

        public static bool IsPersonHasAlreadyAPP(int PersonID,int AppTypeID, int LicenseClassID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select found = 1 from Applications A

     JOIN LocalDrivingLicenseApplications LDLA
	     ON LDLA.ApplicationID = A.ApplicationID
     JOIN LicenseClasses LC
	    ON LDLA.LicenseClassID = LC.LicenseClassID
   where A.ApplicantPersonID = @PersonID  and ApplicationTypeID = 1 and LC.LicenseClassID = @LicenseClassID;";

            SqlCommand cmd = new SqlCommand(@query, connection);

            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.Parameters.AddWithValue("@ApplicationTypeID", AppTypeID);
            cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return true;
            }

            finally
            {
                connection.Close();
            }
        }

        public static int AddNewLocalApplication(int AppPersonID, int AppUserID, float PaidFees, int LicenseClasseID)
        {
            int AppID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO [dbo].[Applications] ([ApplicantPersonID]
                                                               ,[ApplicationDate]
                                                               ,[ApplicationTypeID]
                                                               ,[ApplicationStatus]
                                                               ,[LastStatusDate]
                                                               ,[PaidFees]
                                                               ,[CreatedByUserID])
                              VALUES(@AppPersonID, @ApplicationDate, @ApplicationTypeID, @ApplicationStatus,
                                     @LastStatusDate, @PaidFees, @AppUserID);
                              SELECT SCOPE_IDENTITY();";

            string query2 = @"INSERT INTO [dbo].[LocalDrivingLicenseApplications]
                       ([ApplicationID]   ,[LicenseClassID])  VALUES (@ApplicationID, @LicenseClassID)";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppPersonID", AppPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", DateTime.Now);
            command.Parameters.AddWithValue("@ApplicationTypeID", 1);
            command.Parameters.AddWithValue("@ApplicationStatus", 1);
            command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@AppUserID", AppUserID);  
            
            SqlCommand cmd = new SqlCommand(query2,connection);
            
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    AppID = insertedID;
                    cmd.Parameters.AddWithValue("@ApplicationID", AppID);
                    cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClasseID);

                    int AffectedRows = cmd.ExecuteNonQuery();

                    if (AffectedRows == 1)
                        return AppID;
                    else
                        return -1;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return AppID;
            }

            finally
            {
                connection.Close();
            }
            return AppID;

        }

        public static bool UpdateLocalApplication(int LDlAppID, int AppUserID, int LicenseClasseID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[Applications]
                     SET [ApplicationStatus] = @ApplicationStatus,
                         [LastStatusDate] = @LastStatusDate,
                         [CreatedByUserID] = @AppUserID
                     WHERE [ApplicationID] = (SELECT [ApplicationID] 
                                              FROM [dbo].[LocalDrivingLicenseApplications] 
                                              WHERE [LocalDrivingLicenseApplicationID] = @LDlAppID)";

            string query2 = @"UPDATE [dbo].[LocalDrivingLicenseApplications]
                      SET [LicenseClassID] = @LicenseClassID
                      WHERE [LocalDrivingLicenseApplicationID] = @LDlAppID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationStatus", 1);
            command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
            command.Parameters.AddWithValue("@AppUserID", AppUserID);
            command.Parameters.AddWithValue("@LDlAppID", LDlAppID);

            SqlCommand cmd = new SqlCommand(query2, connection);
            cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClasseID);
            cmd.Parameters.AddWithValue("@LDlAppID", LDlAppID);

            try
            {
                connection.Open();

                int affectedRowsApp = command.ExecuteNonQuery();

                int affectedRowsLicense = cmd.ExecuteNonQuery();

                if (affectedRowsApp > 0 && affectedRowsLicense > 0)
                {
                    return true; 
                }
                else
                {
                    return false; 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool DeleteLocalApplication(int LDlAppID)
        {
            bool isDeleted = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            // Requête pour supprimer les rendez-vous de test associés (TestAppointments)
            string deleteTestAppointments = @"DELETE FROM [dbo].[TestAppointments]
                                      WHERE LocalDrivingLicenseApplicationID = @LDLAppID";

            // Requête pour supprimer les résultats de test associés (Tests)
            string deleteTests = @"DELETE FROM [dbo].[Tests]
                           WHERE TestAppointmentID IN 
                           (SELECT TestAppointmentID FROM [dbo].[TestAppointments]
                           WHERE LocalDrivingLicenseApplicationID = @LDLAppID)";

            // Requête pour supprimer l'enregistrement dans LocalDrivingLicenseApplications
            string deleteLocalApplication = @"DELETE FROM [dbo].[LocalDrivingLicenseApplications]
                                      WHERE LocalDrivingLicenseApplicationID = @LDLAppID";

            // Requête pour supprimer l'enregistrement dans Applications
            string deleteApplication = @"DELETE FROM [dbo].[Applications]
                                 WHERE ApplicationID = (SELECT ApplicationID 
                                                        FROM [dbo].[LocalDrivingLicenseApplications]
                                                        WHERE LocalDrivingLicenseApplicationID = @LDLAppID)";

            SqlCommand cmdDeleteTestAppointments = new SqlCommand(deleteTestAppointments, connection);
            SqlCommand cmdDeleteTests = new SqlCommand(deleteTests, connection);
            SqlCommand cmdDeleteLocalApplication = new SqlCommand(deleteLocalApplication, connection);
            SqlCommand cmdDeleteApplication = new SqlCommand(deleteApplication, connection);

            cmdDeleteTestAppointments.Parameters.AddWithValue("@LDLAppID", LDlAppID);
            cmdDeleteTests.Parameters.AddWithValue("@LDLAppID", LDlAppID);
            cmdDeleteLocalApplication.Parameters.AddWithValue("@LDLAppID", LDlAppID);
            cmdDeleteApplication.Parameters.AddWithValue("@LDLAppID", LDlAppID);

            try
            {
                connection.Open();

                cmdDeleteTests.ExecuteNonQuery();

                cmdDeleteTestAppointments.ExecuteNonQuery();

                int affectedRowsLocalApp = cmdDeleteLocalApplication.ExecuteNonQuery();

                if (affectedRowsLocalApp > 0)
                {
                    cmdDeleteApplication.ExecuteNonQuery();
                    isDeleted = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return isDeleted;
        }

        public static bool CancelLocalApplication(int LDlAppID)
        {
            bool isCancelled = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string updateStatusQuery = @"UPDATE [dbo].[Applications]
                                 SET ApplicationStatus = @CancelledStatus, LastStatusDate = @LastStatusDate
                                 WHERE ApplicationID = (SELECT ApplicationID 
                                                        FROM [dbo].[LocalDrivingLicenseApplications]
                                                        WHERE LocalDrivingLicenseApplicationID = @LDLAppID)";

            SqlCommand cmd = new SqlCommand(updateStatusQuery, connection);

            cmd.Parameters.AddWithValue("@CancelledStatus", 2);  
            cmd.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@LDLAppID", LDlAppID);

            try
            {
                connection.Open();

                int affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                {
                    isCancelled = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return isCancelled;
        }

        public static string LicenseIssuedReason(int LDlAppID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT 
                              L.IssueReason
                          FROM 
                              Licenses L
                          INNER JOIN 
                              Applications A 
                              ON L.ApplicationID = A.ApplicationID
                          
                          	INNER JOIN 
                          	LocalDrivingLicenseApplications LDLA
                          	ON LDLA.ApplicationID = A.ApplicationID
                          WHERE 
                              LDLA.LocalDrivingLicenseApplicationID = @LDLAppID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAppID", LDlAppID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();


                if (result != null)
                {
                    int issueReason = Convert.ToInt32(result);
                    switch (issueReason)
                    {
                        case 1:
                            return "FirstTime";
                        case 2:
                            return "Renew";
                        case 3:
                            return "Replacement for Damaged";
                        case 4:
                            return "Replacement for Lost";
                    }
                }
                else
                    return "";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            finally
            {
                connection.Close();
            }
            return "ERROR";
        }

        public static DataTable GetLocalApps()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT 
                             LDLA.LocalDrivingLicenseApplicationID AS LDLAppID,
                             LC.ClassName AS DrivingClass,
                             P.NationalNo,                 
                             CONCAT(P.FirstName, ' ', P.SecondName,' ' ,P.ThirdName,' ',P.LastName ) AS FullName, 
                             A.ApplicationDate,         
                             (
                                 SELECT COUNT(T.TestID) 
                                 FROM Tests T
                                 JOIN TestAppointments TA ON T.TestAppointmentID = TA.TestAppointmentID
                                 WHERE TA.LocalDrivingLicenseApplicationID = LDLA.LocalDrivingLicenseApplicationID
                                 AND T.TestResult = 1  
                             ) AS PassedTests, 
                         	case
                         	   when A.ApplicationStatus = 1 then 'New'
                         	   when A.ApplicationStatus = 2 then 'Canceled'
                         	   when A.ApplicationStatus = 3 then 'Completed'
                         	   else 'Unknown'
                              end AS Status  
                         FROM 
                             Applications A
                         JOIN 
                             LocalDrivingLicenseApplications LDLA ON A.ApplicationID = LDLA.ApplicationID
                         JOIN 
                             LicenseClasses LC ON LDLA.LicenseClassID = LC.LicenseClassID
                         JOIN 
                             People P ON A.ApplicantPersonID = P.PersonID
                         ORDER BY 
                             A.ApplicationDate DESC; ";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

        public static DataTable GetInternationalApps()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT [ApplicationID] as InterAppID
                                    ,CONCAT(P.FirstName, ' ', P.SecondName,' ' ,P.ThirdName,' ',P.LastName ) AS FullName
	                                ,P.NationalNo
                                    ,[ApplicationDate]
                                    ,[LastStatusDate]
                                    ,[PaidFees]
	                                ,case
                                         when App.ApplicationStatus = 1 then 'New'
                                         when App.ApplicationStatus = 2 then 'Canceled'
                                         when App.ApplicationStatus = 3 then 'Completed'
                                         else 'Unknown'
                                    end AS Status  
                            FROM [dbo].[Applications] App JOIN People P ON P.PersonID=App.ApplicantPersonID
                            where  ApplicationTypeID=6;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static DataTable GetApplicationsByLocalDrivingLicenseApplicationId(int localDrivingLicenseApplicationId)
        {
            DataTable dtApplications = new DataTable();

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT 
                                A.ApplicationID, AppType.ApplicationFees AS Fees, AppType.ApplicationTypeTitle AS Type,
                                LC.ClassName AS AppliedForLicense, P.PersonID,                 
                                CONCAT(P.FirstName, ' ', P.SecondName, ' ', P.ThirdName, ' ', P.LastName) AS Applicant, 
                                U.UserName AS CreatedBy,
                                A.ApplicationDate AS Date, A.LastStatusDate AS StatusDate,
                                (
                                    SELECT COUNT(T.TestID) 
                                    FROM Tests T
                                    JOIN TestAppointments TA ON T.TestAppointmentID = TA.TestAppointmentID
                                    WHERE TA.LocalDrivingLicenseApplicationID = LDLA.LocalDrivingLicenseApplicationID
                                    AND T.TestResult = 1
                                ) AS PassedTests, 
                                CASE
                                    WHEN A.ApplicationStatus = 1 THEN 'New'
                                    WHEN A.ApplicationStatus = 2 THEN 'Canceled'
                                    WHEN A.ApplicationStatus = 3 THEN 'Completed'
                                    ELSE 'Unknown'
                                END AS Status  
                             FROM ApplicationTypes AS AppType
                             JOIN Applications A ON A.ApplicationTypeID = AppType.ApplicationTypeID
                             JOIN LocalDrivingLicenseApplications LDLA ON A.ApplicationID = LDLA.ApplicationID
                             JOIN LicenseClasses LC ON LDLA.LicenseClassID = LC.LicenseClassID
                             JOIN People P ON A.ApplicantPersonID = P.PersonID
                             JOIN Users U ON U.UserID = A.CreatedByUserID
                             WHERE LDLA.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", localDrivingLicenseApplicationId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtApplications); 
            }

            return dtApplications;
        }

        public static int GetLicenseID(int LDLAppID)
        {
            int LicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT L.LicenseID
                From Licenses L JOIN Applications Apps ON Apps.ApplicationID = L.ApplicationID
                                JOIN LocalDrivingLicenseApplications LDLA ON LDLA.ApplicationID = Apps.ApplicationID
                Where LDLA.LocalDrivingLicenseApplicationID = @LDLAppID;";

            SqlCommand cmd = new SqlCommand(@query, connection);

            cmd.Parameters.AddWithValue("@LDLAppID", LDLAppID);

            try
            {
                connection.Open();
                object result = cmd.ExecuteScalar();


                if (result != null)
                {
                    LicenseID = Convert.ToInt32(result);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            finally
            {
                connection.Close();
            }
            return LicenseID;
        }

        public static int GetInternationalLicenseID(int InterLAppID)
        {
            int LicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT [InternationalLicenseID]
                             FROM [dbo].[InternationalLicenses] Inter  JOIN Applications App On App.ApplicationID= Inter.ApplicationID
                             WHERE App.ApplicationID=@InterLAppID;";

            SqlCommand cmd = new SqlCommand(@query, connection);

            cmd.Parameters.AddWithValue("@InterLAppID", InterLAppID);

            try
            {
                connection.Open();
                object result = cmd.ExecuteScalar();


                if (result != null)
                {
                    LicenseID = Convert.ToInt32(result);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            finally
            {
                connection.Close();
            }
            return LicenseID;
        }

        //Manage App Types
        public static DataTable GetApptypes()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT [ApplicationTypeID]
                                   ,[ApplicationTypeTitle]
                                   ,[ApplicationFees]
                               FROM [dbo].[ApplicationTypes]";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool UpdateAppType(int AppTypeID, string ApplicationTypeTitle, float ApplicationFees)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[ApplicationTypes]
                                    SET [ApplicationTypeTitle] = @ApplicationTypeTitle
                                       ,[ApplicationFees] = @ApplicationFees
                                  WHERE [ApplicationTypeID] = @AppTypeID";

            SqlCommand command = new SqlCommand(query,connection);

            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
            command.Parameters.AddWithValue("@AppTypeID", AppTypeID);

            try
            {
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return true;
                }else
                    return false;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }finally { connection.Close(); }
        }
    }

    public class Tests_AppointmentsAccess
    {
        public static DataTable GetAppointments(int LDLAppID,int TestTypeID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT [TestAppointmentID]
                                   ,[AppointmentDate]
                                   ,[PaidFees]
                                   ,[IsLocked]
                               FROM [dbo].[TestAppointments]
                               where
                                   TestTypeID = @TestTypeID and
                                   LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLAppID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static float GetTestTypeFees(int TestTypeID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT [TestTypeFees] FROM [dbo].[TestTypes] WHERE TestTypeID = @TestTypeID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    return (float)Convert.ToDouble(result);
                else
                    return -1;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                connection.Close();
            }
            return -1;
        }

        public static bool AddTestsAppointment(int TestTypeID, int LDLAppID, DateTime AppoiDate, float PaidFees, int createdByUserID, int isLocked)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO [dbo].[TestAppointments]
                                               ([TestTypeID]
                                               ,[LocalDrivingLicenseApplicationID]
                                               ,[AppointmentDate]
                                               ,[PaidFees]
                                               ,[CreatedByUserID]
                                               ,[IsLocked])
                                         VALUES
                                               (@TestTypeID, @LDLAppID, @AppoiDate, @PaidFees, @createdByUserID, @isLocked)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            command.Parameters.AddWithValue("@AppoiDate", AppoiDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@createdByUserID", createdByUserID);
            command.Parameters.AddWithValue("@isLocked", isLocked);

            try
            {
                connection.Open();

                int insertedID = command.ExecuteNonQuery();

                if (insertedID == 1) return true;
                else return false;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }finally { connection.Close(); }

        }

        public static string GetTestAppointmentDate(int TestAppointmentID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT [AppointmentDate] FROM [dbo].[TestAppointments] WHERE [TestAppointmentID] = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    return Convert.ToDateTime(result).ToString();
                else
                    return "";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                connection.Close();
            }
            return "";
        }
        public static int AddTest(int testAppointmentID, byte TestResult, string Notes, int CreatedByUserID)
        {
            int TestInsertedID;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query1 = @"INSERT INTO [dbo].[Tests]
                                           ([TestAppointmentID]
                                           ,[TestResult]
                                           ,[Notes]
                                           ,[CreatedByUserID])
                                     VALUES
                                           (@testAppointmentID, @TestResult, @Notes, @CreatedByUserID);SELECT SCOPE_IDENTITY();";
            string query2 = @"UPDATE [dbo].[TestAppointments]
                                           SET [IsLocked] = @isLocked
                                         WHERE [TestAppointmentID] = @testAppointmentID";

            SqlCommand command1 = new SqlCommand(query1, connection);
            SqlCommand command2 = new SqlCommand(query2, connection);

            command1.Parameters.AddWithValue("@testAppointmentID", testAppointmentID);
            command1.Parameters.AddWithValue("@TestResult", TestResult);
            if(String.IsNullOrEmpty(Notes))
                command1.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                command1.Parameters.AddWithValue("@Notes", Notes);
            command1.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            command2.Parameters.AddWithValue("@isLocked", 1);
            command2.Parameters.AddWithValue("@testAppointmentID", testAppointmentID);

            try
            {
                connection.Open();
                object result = command1.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestInsertedID = insertedID;

                    object result2 = command2.ExecuteScalar();
                    return TestInsertedID;

                }
                else
                    return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return -1;
            }
            finally { connection.Close(); }
        }

        //Manage test Types

        public static DataTable GetTestTypes() {

            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT [TestTypeID]
                                    ,[TestTypeTitle]
                                    ,[TestTypeDescription]
                                    ,[TestTypeFees]
                                FROM [dbo].[TestTypes] ";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool UpdateTestType(int ID, string Title,string Description, float Fees)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[TestTypes]
                             SET [TestTypeTitle] = @Title
                                ,[TestTypeDescription] = @Description
                                ,[TestTypeFees] = @Fees
                           WHERE [TestTypeID] = @ID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@Description", Description);
            command.Parameters.AddWithValue("@Fees", Fees);
            command.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally { connection.Close(); }
        }
    }
    public class LicensesAccess
    {
        public static int AddLocalLicense(int LDLAppID, int CreatedByUserID, string Notes)
        {
            int LicenseID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query1 = @"SELECT [ApplicationID], [ApplicantPersonID]
                            FROM [dbo].[Applications] where ApplicationID = (select ApplicationID from LocalDrivingLicenseApplications where LocalDrivingLicenseApplicationID = @LDLAppID)";

            string query2_checkDriver = @"SELECT [DriverID] FROM [dbo].[Drivers] WHERE [PersonID] = @PersonID";

            string query2_insertDriver = @"INSERT INTO [dbo].[Drivers]
                               ([PersonID], [CreatedByUserID], [CreatedDate])
                               VALUES
                               (@PersonID, @CreatedByUserID, @CreatedDate); 
                               SELECT SCOPE_IDENTITY();";

            string query3 = @"UPDATE [dbo].[Applications]
                         SET [ApplicationStatus] = @CompleteStatus, [LastStatusDate] = @LastStatusDate
                       WHERE  [ApplicationID]= @ApplicationID;";

            string query4 = @"SELECT LC.[LicenseClassID] ,[DefaultValidityLength]
              FROM [dbo].[LicenseClasses] LC JOIN LocalDrivingLicenseApplications LDLA ON LDLA.LicenseClassID = LC.LicenseClassID WHERE LocalDrivingLicenseApplicationID = @LDLAppID;";

            string query5 = @"INSERT INTO [dbo].[Licenses]
                                               ([ApplicationID]
                                               ,[DriverID]
                                               ,[LicenseClass]
                                               ,[IssueDate]
                                               ,[ExpirationDate]
                                               ,[Notes]
                                               ,[PaidFees]
                                               ,[IsActive]
                                               ,[IssueReason]
                                               ,[CreatedByUserID])
                                         VALUES (@ApplicationID, @DriverID, @LicenseClass,
                                                 @IssueDate, @ExpirationDate,@Notes,
                                                 @PaidFees, @IsActive, @IssueReason,@CreatedByUserID);
                                                  SELECT SCOPE_IDENTITY();";

            SqlCommand cmd1 = new SqlCommand(query1, connection);
            SqlCommand cmd2_checkDriver = new SqlCommand(query2_checkDriver, connection);
            SqlCommand cmd2_insertDriver = new SqlCommand(query2_insertDriver, connection);
            SqlCommand cmd3 = new SqlCommand(query3, connection);
            SqlCommand cmd4 = new SqlCommand(query4, connection);
            SqlCommand cmd5 = new SqlCommand(query5, connection);

            try
            {
                int PersonID;
                int DriverID;
                int ApplicationID;
                int LicenseClassID;
                int DefaultValidityLength;

                connection.Open();

                cmd1.Parameters.AddWithValue("@LDLAppID", LDLAppID);

                SqlDataReader reader = cmd1.ExecuteReader();

                if (reader.Read())
                {
                    PersonID = (int)reader["ApplicantPersonID"];
                    ApplicationID = (int)reader["ApplicationID"];

                    reader.Close();

                    cmd2_checkDriver.Parameters.AddWithValue("@PersonID", PersonID);
                    object existingDriver = cmd2_checkDriver.ExecuteScalar();
                    if (existingDriver != null && int.TryParse(existingDriver.ToString(), out int existingDriverID))
                    {
                        DriverID = existingDriverID;
                    }
                    else
                    {
                        cmd2_insertDriver.Parameters.AddWithValue("@PersonID", PersonID);
                        cmd2_insertDriver.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                        cmd2_insertDriver.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                        object result = cmd2_insertDriver.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            DriverID = insertedID;
                        }
                        else
                            return -1;
                    }
                    cmd3.Parameters.AddWithValue("@CompleteStatus", 3);
                    cmd3.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
                    cmd3.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    
                    int rowsAffected = cmd3.ExecuteNonQuery();
                    
                    if (rowsAffected > 0)
                    {
                        cmd4.Parameters.AddWithValue("@LDLAppID", LDLAppID);
                    
                        SqlDataReader reader2 = cmd4.ExecuteReader();
                    
                        if (reader2.Read())
                        {
                            DefaultValidityLength = Convert.ToInt32( reader2["DefaultValidityLength"]);
                            LicenseClassID = (int)reader2["LicenseClassID"];
                    
                            reader2.Close();
                    
                            cmd5.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                            cmd5.Parameters.AddWithValue("@DriverID", DriverID);
                            cmd5.Parameters.AddWithValue("@LicenseClass", LicenseClassID);
                            cmd5.Parameters.AddWithValue("@IssueDate", DateTime.Now);
                            cmd5.Parameters.AddWithValue("@ExpirationDate", DateTime.Now.AddYears(DefaultValidityLength));
                            if(string.IsNullOrEmpty(Notes))
                                cmd5.Parameters.AddWithValue("@Notes", DBNull.Value);
                            else
                                cmd5.Parameters.AddWithValue("@Notes", Notes);
                            cmd5.Parameters.AddWithValue("@PaidFees", 20.00);
                            cmd5.Parameters.AddWithValue("@IsActive", 1);
                            cmd5.Parameters.AddWithValue("@IssueReason", 1);
                            cmd5.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                    
                            object result2 = cmd5.ExecuteScalar();
                    
                            if (result2 != null && int.TryParse(result2.ToString(), out int insertedLicenseID))
                            {
                                LicenseID = insertedLicenseID;
                            }
                        }else
                            reader2.Close();
                        }
                    } else
                        reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                connection.Close();
            }

            return LicenseID;
        }

        public static DataTable GetLicenseInfo(int LicenseID)
        {
            DataTable licenseInfo = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT LC.ClassName as Class,
                       		CONCAT(P.FirstName, ' ', P.SecondName, ' ', P.ThirdName, ' ', P.LastName) AS Name,
                       		L.LicenseID AS LicenseID,
                       		P.NationalNo,
                       		P.Gendor as Gender,
                       		L.IssueDate,
                       		L.IssueReason,
                       		L.Notes,
                       		L.IsActive,
                       		P.DateOfBirth,
                       		L.DriverID,
                       		L.ExpirationDate,
                       		P.ImagePath
                       From Licenses L JOIN LicenseClasses LC ON LC.LicenseClassID = L.LicenseClass
                                       JOIN Drivers D ON D.DriverID = L.DriverID
                       				JOIN People P ON D.PersonID = P.PersonID
                       Where L.LicenseID = @LicenseID;";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)

                {
                    licenseInfo.Load(reader);
                }

                reader.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }

            return licenseInfo;
        }

        public static DataTable GetInternationalLicenseInfo(int InterLicenseID)
        {
            DataTable licenseInfo = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @" SELECT [InternationalLicenseID]
                                     ,[ApplicationID]
                                     ,D.[DriverID]
                                     ,[IssuedUsingLocalLicenseID] as LocalLicenseID
                                     ,[IssueDate]
                                     ,[ExpirationDate]
                                     ,[IsActive]
                             	     ,P.NationalNo
                             	     ,P.Gendor as Gender
                             	     ,P.DateOfBirth
                                     ,P.ImagePath
                             	     ,CONCAT(P.FirstName, ' ', P.SecondName, ' ', P.ThirdName, ' ', P.LastName) AS Name
                               FROM [dbo].[InternationalLicenses] InterL 
                               JOIN Drivers D ON D.DriverID = InterL.DriverID
                               JOIN People P ON P.PersonID = D.PersonID
                               WHERE InternationalLicenseID = @InterLicenseID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@InterLicenseID", InterLicenseID);

            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)

                {
                    licenseInfo.Load(reader);
                }

                reader.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }

            return licenseInfo;
        }

        public static DataTable GetLocalLicenses(int PersonID)
        {
            DataTable LocalLicenses = new DataTable();

            SqlConnection connection = new SqlConnection (clsDataAccessSettings.ConnectionString);

            string query = @"SELECT [LicenseID] as LicID
                          ,[ApplicationID] as AppID
                          ,LC.ClassName
                          ,[IssueDate]
                          ,[ExpirationDate]
                          ,[IsActive]
                      FROM [dbo].[Licenses] L JOIN LicenseClasses LC ON L.LicenseClass= LC.LicenseClassID
                                              JOIN Drivers Dr ON Dr.DriverID = L.DriverID
                    						  JOIN People P ON P.PersonID = Dr.PersonID
                    						  where p.PersonID= @PersonID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    LocalLicenses.Load(reader);
                }

                reader.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }

            return LocalLicenses;

        }

        public static DataTable GetInternationalLicenses(int PersonID)
        {
            DataTable InternationalLicenses = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT IL.InternationalLicenseID as LicID
      ,IL.ApplicationID as AppID
      ,LC.ClassName
      ,IL.IssueDate
      ,IL.ExpirationDate
      ,IL.IsActive
  FROM [dbo].InternationalLicenses IL JOIN Licenses L ON L.LicenseID = IL.IssuedUsingLocalLicenseID JOIN LicenseClasses LC ON L.LicenseClass= LC.LicenseClassID
                          JOIN Drivers Dr ON Dr.DriverID = IL.DriverID
						  JOIN People P ON P.PersonID = Dr.PersonID
						  where p.PersonID= @PersonID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    InternationalLicenses.Load(reader);
                }

                reader.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }

            return InternationalLicenses;

        }

        public static bool IsLicenseExistByID(int LicenseID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT found=1
                            FROM [dbo].[Licenses]
                            WHERE LicenseID = @LicenseID;";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    return true;
                }
                else
                    return false;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }finally { connection.Close(); }

        }

        public static int[] ReplaceLicense(int LicenseID, int ReplacementType, int ApplicantPersonID,
                                               float PaidFees,int CreatedByUserID)
        {
            int[] Result = new int[] { -1, -1 };
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query1 = @"SELECT [LicenseID]
                                    ,[ApplicationID]
                                    ,[DriverID]
                                    ,[LicenseClass]
                                    ,[IssueDate]
                                    ,[ExpirationDate]
                                    ,[Notes]
                                    ,[PaidFees]
                                    ,[IsActive]
                                    ,[IssueReason]
                                    ,[CreatedByUserID]
                                FROM [dbo].[Licenses]
                                where LicenseID = @LicenseID;";

            string query2 = @"INSERT INTO [dbo].[Applications]
                                         ([ApplicantPersonID]
                                          ,[ApplicationDate]
                                          ,[ApplicationTypeID]
                                          ,[ApplicationStatus]
                                          ,[LastStatusDate]
                                          ,[PaidFees]
                                          ,[CreatedByUserID])
                                    VALUES (@ApplicantPersonID,@ApplicationDate,@ApplicationTypeID,
                                             @ApplicationStatus,@LastStatusDate,@PaidFees,@CreatedByUserID);
                                              SELECT SCOPE_IDENTITY();";

            string query3 = @"UPDATE [dbo].[Licenses]
                               SET  [IsActive] = @IsActive
                                WHERE [LicenseID] = @LicenseID";

            string query4 = @"INSERT INTO [dbo].[Licenses]
                                               ([ApplicationID]
                                               ,[DriverID]
                                               ,[LicenseClass]
                                               ,[IssueDate]
                                               ,[ExpirationDate]
                                               ,[Notes]
                                               ,[PaidFees]
                                               ,[IsActive]
                                               ,[IssueReason]
                                               ,[CreatedByUserID])
                                         VALUES(@ApplicationID, @DriverID, @LicenseClass,@IssueDate,@ExpirationDate,
                                                     @Notes,@PaidFees,@IsActive,@IssueReason,@CreatedByUserID);
                                                      SELECT SCOPE_IDENTITY();";


            SqlCommand command1 = new SqlCommand(query1, connection);
            SqlCommand command2 = new SqlCommand(query2, connection);
            SqlCommand command3 = new SqlCommand(query3, connection);
            SqlCommand command4 = new SqlCommand(query4, connection);

            try
            {
                int DriverID;
                int LicenseClass;
                DateTime ExpirationDate;
                string Notes;

                connection.Open();

                command1.Parameters.AddWithValue("@LicenseID", LicenseID);

                SqlDataReader reader = command1.ExecuteReader();

                if (reader.Read())
                {
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    Notes = Convert.ToString(reader["Notes"]);

                    reader.Close();

                    command2.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
                    command2.Parameters.AddWithValue("@ApplicationDate", DateTime.Now);
                    command2.Parameters.AddWithValue("@ApplicationTypeID", ReplacementType);
                    command2.Parameters.AddWithValue("@ApplicationStatus", 3);
                    command2.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
                    command2.Parameters.AddWithValue("@PaidFees", PaidFees);
                    command2.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                    object result1 = command2.ExecuteScalar();

                    if (result1 != null && int.TryParse(result1.ToString(), out int insertedAppID))
                    {
                        Result[0] = insertedAppID;

                        command3.Parameters.AddWithValue("@IsActive", 0);
                        command3.Parameters.AddWithValue("@LicenseID", LicenseID);

                        int rowsAffected = command3.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            command4.Parameters.AddWithValue("@ApplicationID", Result[0]);
                            command4.Parameters.AddWithValue("@DriverID", DriverID);
                            command4.Parameters.AddWithValue("@LicenseClass", LicenseClass);
                            command4.Parameters.AddWithValue("@IssueDate", DateTime.Now);
                            command4.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                            if(string.IsNullOrEmpty(Notes)) 
                                command4.Parameters.AddWithValue("@Notes", DBNull.Value);
                            else
                                command4.Parameters.AddWithValue("@Notes", Notes);

                            command4.Parameters.AddWithValue("@PaidFees", PaidFees);
                            command4.Parameters.AddWithValue("@IsActive", 1);
                            if (ReplacementType == 3)
                                command4.Parameters.AddWithValue("@IssueReason", 4);
                            else
                                command4.Parameters.AddWithValue("@IssueReason", 3);
                            command4.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                            object result2 = command4.ExecuteScalar();

                            if (result2 != null && int.TryParse(result2.ToString(), out int insertedLicenseID))
                            {
                                Result[1] = insertedLicenseID;

                            }

                        }
                    }
                }
                else
                {
                    Console.WriteLine("No license found with the given LicenseID.");
                    reader.Close ();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return Result; 
        }

        public static int getLicenseClass(int licenseID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT  [LicenseClass]  FROM [dbo].[Licenses]  WHERE LicenseID = @LicenseID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", licenseID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    return (int)Convert.ToInt16(result);
                else
                    return -1;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                connection.Close();
            }
            return -1;
        }

        public static int[] IssueInternationalLicense(int LicenseID, int AppTypeID, int ApplicantPersonID,
                                             float PaidFees, int CreatedByUserID)
        {
            int[] Result = new int[] { -1, -1 };
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query1 = @"SELECT   [DriverID]
                                FROM [dbo].[Licenses]
                                where LicenseID = @LicenseID;";

            string query2 = @"INSERT INTO [dbo].[Applications]
                                         ([ApplicantPersonID]
                                          ,[ApplicationDate]
                                          ,[ApplicationTypeID]
                                          ,[ApplicationStatus]
                                          ,[LastStatusDate]
                                          ,[PaidFees]
                                          ,[CreatedByUserID])
                                    VALUES (@ApplicantPersonID,@ApplicationDate,@ApplicationTypeID,
                                             @ApplicationStatus,@LastStatusDate,@PaidFees,@CreatedByUserID);
                                              SELECT SCOPE_IDENTITY();";


            string query3 = @"INSERT INTO [dbo].[InternationalLicenses]
                                                 ([ApplicationID]
                                                 ,[DriverID]
                                                 ,[IssuedUsingLocalLicenseID]
                                                 ,[IssueDate]
                                                 ,[ExpirationDate]
                                                 ,[IsActive]
                                                 ,[CreatedByUserID])
                                           VALUES (@AppID, @DriverID,@IssuedUsingLocalLicenseID,
                                                    @IssueDate,@ExpirationDate,@IsActive,@CreatedByUserID);
                                                           SELECT SCOPE_IDENTITY();";


            SqlCommand command1 = new SqlCommand(query1, connection);
            SqlCommand command2 = new SqlCommand(query2, connection);
            SqlCommand command3 = new SqlCommand(query3, connection);

            try
            {
                int DriverID;

                connection.Open();

                command1.Parameters.AddWithValue("@LicenseID", LicenseID);

                SqlDataReader reader = command1.ExecuteReader();

                if (reader.Read())
                {
                    DriverID = (int)reader["DriverID"];

                    reader.Close();

                    command2.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
                    command2.Parameters.AddWithValue("@ApplicationDate", DateTime.Now);
                    command2.Parameters.AddWithValue("@ApplicationTypeID", AppTypeID);
                    command2.Parameters.AddWithValue("@ApplicationStatus", 3);
                    command2.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
                    command2.Parameters.AddWithValue("@PaidFees", PaidFees);
                    command2.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                    object result1 = command2.ExecuteScalar();

                    if (result1 != null && int.TryParse(result1.ToString(), out int insertedAppID))
                    {
                        Result[0] = insertedAppID;

                        command3.Parameters.AddWithValue("@AppID", Result[0]);
                        command3.Parameters.AddWithValue("@DriverID", DriverID);
                        command3.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", LicenseID);
                        command3.Parameters.AddWithValue("@IssueDate", DateTime.Now);
                        command3.Parameters.AddWithValue("@ExpirationDate", DateTime.Now.AddYears(1));
                        command3.Parameters.AddWithValue("@IsActive", 1);
                        command3.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                        
                        object result2 = command3.ExecuteScalar();
                        
                        if (result2 != null && int.TryParse(result2.ToString(), out int insertedLicenseID))
                        {
                            Result[1] = insertedLicenseID;
                        
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No license found with the given LicenseID.");
                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return Result;
        }

        public static bool IsInternationalLicenseExist(int LocalLID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT found = 1
                            FROM [dbo].[InternationalLicenses]
                            where [IssuedUsingLocalLicenseID] = @LocalLID  and ExpirationDate >  @Now";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalLID", LocalLID);
            command.Parameters.AddWithValue("@Now", DateTime.Now);

            try
            {
                connection.Open();


                object result = command.ExecuteScalar();

                if (result != null)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                connection.Close();
            }
            return false;
        }

        public static DataTable GetDrivers()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT D.[DriverID]
                          ,P.[PersonID]
                     	  ,P.NationalNo
                          ,CONCAT(P.FirstName, ' ', P.SecondName, ' ', P.ThirdName, ' ', P.LastName) AS FullName
                          ,D.[CreatedDate] AS Date
                     	  ,(SELECT COUNT(*) FROM Licenses L WHERE L.IsActive = 1 AND L.DriverID= D.DriverID)+
                           (SELECT COUNT(*) FROM InternationalLicenses IL WHERE IL.IsActive = 1 AND IL.DriverID= D.DriverID) AS ActiveLicenses
                      FROM [dbo].[Drivers] D JOIN People P ON P.PersonID=D.PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static DateTime GetExpirationDate(int LicenseID)
        {
            DateTime ExpirationDate = DateTime.MinValue;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT [ExpirationDate]  FROM [dbo].[Licenses]  WHERE LicenseID = @LicenseID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    ExpirationDate = (DateTime)Convert.ToDateTime(result);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                connection.Close();
            }
            return ExpirationDate;

        }

        public static float GetLicenseClassFees(int LicenseID)
        {
            float LicenseClassFees = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT LC.ClassFees FROM [dbo].[Licenses] L JOIN LicenseClasses LC ON L.LicenseClass= LC.LicenseClassID WHERE L.LicenseID = @LicenseID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    LicenseClassFees = (float)Convert.ToDouble(result);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                connection.Close();
            }
            return LicenseClassFees;
        }
        public static string GetLicenseNotes(int LicenseID)
        {
            string LicenseNotes = string.Empty;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Notes FROM [dbo].[Licenses] WHERE L.LicenseID = @LicenseID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    LicenseNotes = (string)Convert.ToString(result);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                connection.Close();
            }
            return LicenseNotes;
        }
        public static int[] RenewLicense(int LicenseID, int ApplicantPersonID,float PaidFees, int CreatedByUserID)
        {
            int[] Result = new int[] { -1, -1 };
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query1 = @"INSERT INTO [dbo].[Applications]
                                         ([ApplicantPersonID]
                                          ,[ApplicationDate]
                                          ,[ApplicationTypeID]
                                          ,[ApplicationStatus]
                                          ,[LastStatusDate]
                                          ,[PaidFees]
                                          ,[CreatedByUserID])
                                    VALUES (@ApplicantPersonID,@ApplicationDate,@ApplicationTypeID,
                                             @ApplicationStatus,@LastStatusDate,@PaidFees,@CreatedByUserID);
                                              SELECT SCOPE_IDENTITY();";


            string query2 = @"SELECT L.[DriverID]
                                    ,L.[LicenseClass]
	                                ,LC.DefaultValidityLength
                                    ,L.[Notes]
                            FROM [dbo].[Licenses] L JOIN LicenseClasses LC ON LC.LicenseClassID=L.LicenseClass
                            WHERE L.LicenseID = @LicenseID;";

            string query3 = @"INSERT INTO [dbo].[Licenses]
                                               ([ApplicationID]
                                               ,[DriverID]
                                               ,[LicenseClass]
                                               ,[IssueDate]
                                               ,[ExpirationDate]
                                               ,[Notes]
                                               ,[PaidFees]
                                               ,[IsActive]
                                               ,[IssueReason]
                                               ,[CreatedByUserID])
                              VALUES
                                               (@ApplicationID
                                               ,@DriverID
                                               ,@LicenseClass
                                               ,@IssueDate
                                               ,@ExpirationDate
                                               ,@Notes
                                               ,@PaidFees
                                               ,@IsActive
                                               ,@IssueReason
                                               ,@CreatedByUserID);
                              SELECT SCOPE_IDENTITY();";

            string query4 = @"UPDATE [dbo].[Licenses] SET [IsActive] = @IsActive WHERE LicenseID = @LicenseID";

            SqlCommand command1 = new SqlCommand(query1, connection);
            SqlCommand command2 = new SqlCommand(query2, connection);
            SqlCommand command3 = new SqlCommand(query3, connection);
            SqlCommand command4 = new SqlCommand(query4, connection);

            try
            {
                connection.Open();
                
                
                command1.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
                command1.Parameters.AddWithValue("@ApplicationDate", DateTime.Now);
                command1.Parameters.AddWithValue("@ApplicationTypeID", 2);
                command1.Parameters.AddWithValue("@ApplicationStatus", 3);
                command1.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
                command1.Parameters.AddWithValue("@PaidFees", PaidFees);
                command1.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                
                object result1 = command1.ExecuteScalar();

                if (result1 != null && int.TryParse(result1.ToString(), out int insertedAppID))
                {
                    Result[0] = insertedAppID;

                    command2.Parameters.AddWithValue("@LicenseID", LicenseID);
                    SqlDataReader reader = command2.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        int driverID = reader.GetInt32(reader.GetOrdinal("DriverID"));
                        int licenseClass = reader.GetInt32(reader.GetOrdinal("LicenseClass"));
                        byte validityLength = reader.GetByte(reader.GetOrdinal("DefaultValidityLength"));
                        string notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes"));

                        reader.Close();

                        command3.Parameters.AddWithValue("@ApplicationID", Result[0]);
                        command3.Parameters.AddWithValue("@DriverID", driverID);
                        command3.Parameters.AddWithValue("@LicenseClass", licenseClass);
                        command3.Parameters.AddWithValue("@IssueDate", DateTime.Now);
                        command3.Parameters.AddWithValue("@ExpirationDate", DateTime.Now.AddYears(validityLength));
                        command3.Parameters.AddWithValue("@IsActive", 1);
                        command3.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                        if(string.IsNullOrEmpty(notes))
                            command3.Parameters.AddWithValue("@Notes", DBNull.Value);
                        else
                            command3.Parameters.AddWithValue("@Notes", notes);

                        command3.Parameters.AddWithValue("@PaidFees", PaidFees);
                        command3.Parameters.AddWithValue("@IssueReason", 2);

                        object result2 = command3.ExecuteScalar();

                        if (result2 != null && int.TryParse(result2.ToString(), out int insertedLicenseID))
                        {
                            Result[1] = insertedLicenseID;

                            command4.Parameters.AddWithValue("@IsActive", 0);
                            command4.Parameters.AddWithValue("@LicenseID", LicenseID);

                            int rowsAffected= command4.ExecuteNonQuery();
                            if (rowsAffected != 1)
                            {
                                return new int[] { -1, -1 }; 
                            }
                        }
                    }

                }
                
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return Result;
        }

        //Manage Detained Licenses
        public static DataTable GetDetainedLicenses()
        {
            DataTable DetainedLicenses = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT [DetainID]
                                    ,DL.[LicenseID]
                                    ,[DetainDate]
                                    ,[IsReleased]
                                    ,[FineFees]
                                    ,[ReleaseDate]
	                                ,P.NationalNo
	                                ,CONCAT(P.FirstName, ' ', P.SecondName,' ',P.ThirdName,' ',P.LastName) as FullName
                                    ,[ReleaseApplicationID]
                              FROM [dbo].[DetainedLicenses] DL JOIN Licenses L ON L.LicenseID = DL.LicenseID
                              JOIN Drivers D ON D.DriverID = L.DriverID
							  JOIN People P ON P.PersonID = D.PersonID";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    DetainedLicenses.Load(reader);
                }

                reader.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }

            return DetainedLicenses;

        }

        public static int DetainLicense(int LicenseID, float FineFees, int CreatedByUserID)
        {
            int DetainID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO [dbo].[DetainedLicenses]
                                               ([LicenseID]
                                               ,[DetainDate]
                                               ,[FineFees]
                                               ,[CreatedByUserID])
                                    VALUES(@LicenseID, @DetainDate,@FineFees, @CreatedByUserID);
                              SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DateTime.Now);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    DetainID = insertedID;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            finally
            {
                connection.Close();
            }
            return DetainID;
        }

        public static bool IsDetained(int LicenseID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT found = 1
                             FROM [dbo].[DetainedLicenses]
                             WHERE IsReleased = 0 AND LicenseID = @LicenseID;";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                connection.Close();
            }
            return false;
        }

        public static DataRow GetDetainInfo(int LicenseID)
        {
            DataRow DetaineInfo = null;  

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT [DetainID]
                                    ,[LicenseID]
                                    ,[DetainDate]
                                    ,[FineFees]
                        FROM [dbo].[DetainedLicenses]
                        WHERE LicenseID = @LicenseID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                DataTable tempTable = new DataTable();  

                if (reader.HasRows)
                {
                    tempTable.Load(reader);  

                    if (tempTable.Rows.Count > 0)
                    {
                        DetaineInfo = tempTable.Rows[0]; 
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return DetaineInfo;

        }

        public static int ReleaseDetainedLicense(int LicenseID, int UserId,int PersonID,float PaidFees) {

            int ReleaseApplicationID=-1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[DetainedLicenses]
                             SET [IsReleased] = @IsReleased
                                 ,[ReleaseDate] = @ReleaseDate
                                 ,[ReleasedByUserID] = @ReleasedByUserID
                                 ,[ReleaseApplicationID] = @ReleaseApplicationID
                             WHERE  LicenseID = @LicenseID;";

            string query2 = @"INSERT INTO [dbo].[Applications]
                                                ([ApplicantPersonID]
                                                ,[ApplicationDate]
                                                ,[ApplicationTypeID]
                                                ,[ApplicationStatus]
                                                ,[LastStatusDate]
                                                ,[PaidFees]
                                                ,[CreatedByUserID])
                               VALUES  (@ApplicantPersonID,@ApplicationDate,@ApplicationTypeID,
                                         @ApplicationStatus,@LastStatusDate,@PaidFees,@CreatedByUserID);
                                           SELECT SCOPE_IDENTITY();";


            SqlCommand command2 = new SqlCommand(query2, connection);
            command2.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command2.Parameters.AddWithValue("@ApplicationDate", DateTime.Now);
            command2.Parameters.AddWithValue("@ApplicationTypeID", 9);
            command2.Parameters.AddWithValue("@ApplicationStatus", 3);
            command2.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
            command2.Parameters.AddWithValue("@PaidFees", PaidFees);
            command2.Parameters.AddWithValue("@CreatedByUserID", UserId);

            try
            {
                connection.Open();
                object result = command2.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ReleaseApplicationID = insertedID;

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IsReleased", 1);
                    command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);
                    command.Parameters.AddWithValue("@ReleasedByUserID", UserId);
                    command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
                    command.Parameters.AddWithValue("@LicenseID", LicenseID);

                    int rowsAffected = command.ExecuteNonQuery();
                    if(rowsAffected <= 0)
                    {
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
            return ReleaseApplicationID;
        }

    }

   


}
