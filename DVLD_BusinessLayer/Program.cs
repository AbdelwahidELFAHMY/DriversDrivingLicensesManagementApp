using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DVLD_DataAccessLayer;

namespace DVLD_BusinessLayer
{

    public class clsAuth
    {
        public static string Login(string username, string password)
        {
            return clsDataAccessLayer.Login(username, password); 
        }

        public static DataTable LoadCountries() {

            return clsDataAccessLayer.LoadCountries();
        }
        static void Main(string[] args)
        {
        }
    }

    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int PersonID { get; set; }

        public string CIN { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public byte Gendor { get; set; }
        public string ImagePath { get; set; }
        public int NationalityID { get; set; }

        public clsPerson()
        {
            PersonID = -1;
            CIN = string.Empty;
            FirstName = string.Empty;
            SecondName = string.Empty;
            ThirdName = string.Empty;
            LastName = string.Empty;
            DateOfBirth = DateTime.Now;
            Email = string.Empty;
            Phone = string.Empty;
            Gendor = 0;
            ImagePath = string.Empty;
            NationalityID = -1;

            Mode = enMode.AddNew;
        }

        private clsPerson(int PersonID,string CIN, string FirstName, string LastName,string SecondName, string ThirdName,
            string Email, string Phone, string Address,byte Gendor, DateTime DateOfBirth, int NationalityID, string ImagePath)
        {
            this.PersonID = PersonID;
            this.CIN = CIN;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.SecondName = SecondName;   
            this.ThirdName = ThirdName; 
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.NationalityID = NationalityID;
            this.ImagePath = ImagePath;
            this.Gendor = Gendor;

            Mode = enMode.Update;
        }

        public string GetCountryById(int CountryID)
        {
            return clsDataAccessLayer.GetCountryById(CountryID);
        }

        private int _AddNewPerson()
        {
             return clsDataAccessLayer.AddNewPerson(this.CIN, FirstName, SecondName, ThirdName, LastName,DateOfBirth,
                
                Gendor,Address,Phone,Email,NationalityID,ImagePath);  
        }

        public static bool DeletePerson(int PersonID)
        {
            return clsDataAccessLayer.deletePerson(PersonID);
        }

        private bool _UpdatePerson()
        {
            return clsDataAccessLayer.UpdatePerson(PersonID,CIN,FirstName,LastName,SecondName,ThirdName,
                DateOfBirth,Gendor,Address,Phone,Email,NationalityID,ImagePath);
        }

        public int SavePerson()
        {
            try
            {
                switch (Mode)
                {
                    case enMode.AddNew:
                        this.PersonID = _AddNewPerson();
                        if (this.PersonID != -1)
                        {
                            Mode = enMode.Update;
                            return PersonID;
                        }
                        else
                        {
                            return -1;
                        }

                    case enMode.Update:
                        if (_UpdatePerson())
                        {
                            return PersonID;
                        }
                        else
                        {
                            return -1;
                        }

                    default:
                        throw new InvalidOperationException("Unexpected mode value.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return -1;
            }
        }


        public static clsPerson FindPersonByID(int  PersonID)
        {
            string NationalNo = "", FirstName = "", LastName = "", SecondName = "", ThirdName = "", Address = "", Phone = "", Email = "", ImagePath = "";
            int NationalityID =-1;
            DateTime DateOfBirth=DateTime.Now;
            byte Gendor=0;

            if(clsDataAccessLayer.GetPersonByID(PersonID,ref NationalNo, ref FirstName, ref LastName,ref SecondName,ref ThirdName,
                ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref NationalityID, ref ImagePath))
            {
                return new clsPerson(PersonID,NationalNo,FirstName,LastName,SecondName,ThirdName,Email,
                    Phone,Address,Gendor,DateOfBirth,NationalityID,ImagePath);
            }
            else { return null; }
        }

        public static clsPerson FindPersonByNationalNo(string NationalNo)
        {
            string FirstName = "", LastName = "", SecondName = "", ThirdName = "", Address = "", Phone = "", Email = "", ImagePath = "";
            int NationalityID = -1, PersonID= -1;
            DateTime DateOfBirth = DateTime.Now;
            byte Gendor = 0;

            if (clsDataAccessLayer.GetPersonByNationalNo(NationalNo, ref PersonID, ref FirstName, ref LastName, ref SecondName, ref ThirdName,
                ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref NationalityID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, LastName, SecondName, ThirdName, Email,
                    Phone, Address, Gendor, DateOfBirth, NationalityID, ImagePath);
            }
            else { return null; }
        }

        public static DataTable GetPeople()
        {
            return clsDataAccessLayer.GetPeople();
        }
    }

    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public byte IsActive { get; set; }

        public clsUser()
        {
            this.UserID = -1;
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.IsActive = 0;
            this.PersonID = -1;
            Mode = enMode.AddNew;
        }

        private clsUser(int UserID, int PersonID, string UserName, byte IsActive)
        {
            this.PersonID = PersonID;
            this.UserID = UserID;
            this.UserName = UserName;
            this.IsActive = IsActive;

            Mode = enMode.Update;

        }

        public static clsUser GetUserByPersonID(int PersonID)
        {
            if(IsUserExist(PersonID))
            {
                int UserID = -1;
                string UserName = string.Empty; 
                byte IsActive = 0;
                if (clsUserAccess.GetUserByPersonID(PersonID,ref UserID, ref UserName, ref IsActive))
                {
                    clsUser user = new clsUser(UserID,PersonID,UserName,IsActive);
                    return user;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public static bool IsPasswordCorrect(string Password, int PersonID)
        {
            return clsUserAccess.IsPasswordCorrect(Password, PersonID);
        }

        public static bool changePassword(string Password, int PersonID)
        {
            return clsUserAccess.ChangePassword(Password, PersonID); 
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUserAccess.AddNewUser(PersonID,UserName,Password,IsActive);
            return (UserID != -1);
        }
        public bool SaveUser()
        {

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return false;

            }

            return false;
        }

        public static bool IsUserExist(int PersonId)
        {
            return clsUserAccess.IsUserExist(PersonId);
        }

        public static DataTable GetUsers()
        {
            return clsUserAccess.GetUsers();
        }
    }

    public class clsLicenseClasses
    {
        public static DataTable GetLisenceClasses()
        {
            return LicenseClasseAccess.GetLicenseClasses();
        }
    }

    public class clsApplication
    {
        public static float GetApplicationTypesFees(int AppTypeID)
        {
            return ApplicationsAccess.GetAppTypeFees(AppTypeID);
        }
        public static bool IsPersonHasAlreadyAPP(int PersonID,int AppTyepID, int LicenseClassID)
        {
            return ApplicationsAccess.IsPersonHasAlreadyAPP(PersonID, AppTyepID, LicenseClassID);
        }
        public static int AddNewLocalApp(int PersonID, int UserID, float PaidFees, int LicenseClassID)
        {
            return ApplicationsAccess.AddNewLocalApplication(PersonID, UserID, PaidFees, LicenseClassID);
        }
        public static DataTable GetLocalApplications()
        {
            return ApplicationsAccess.GetLocalApps();
        }
        public static DataTable GetInternationalApplications()
        {
            return ApplicationsAccess.GetInternationalApps();
        }
        public static bool UpdateLocalLicenseApp(int LDLAppID,int CreatedBy,int LicenseClass)
        {
            return ApplicationsAccess.UpdateLocalApplication(LDLAppID, CreatedBy, LicenseClass);
        }
        public static bool DeleteLocalApplication(int LDLAppID)
        {
            return ApplicationsAccess.DeleteLocalApplication(LDLAppID);
        }
        public static bool CancelLocalApplication(int LDLAppID)
        {
            return ApplicationsAccess.CancelLocalApplication(LDLAppID);
        }
        public static string LicenseIssuedReason(int LDLAppID)
        {
            return ApplicationsAccess.LicenseIssuedReason(LDLAppID);
        }
        public static DataTable GetLocalApplicationDetails(int localDrivingLicenseApplicationId)
        {
            return ApplicationsAccess.GetApplicationsByLocalDrivingLicenseApplicationId(localDrivingLicenseApplicationId);
        }
        public static int GetLicenseID(int LDLAppID)
        {
            return ApplicationsAccess.GetLicenseID(LDLAppID);
        }
        public static int GetInternationalLicenseID(int InterAppID)
        {
            return ApplicationsAccess.GetInternationalLicenseID(InterAppID);
        }


        //Manage App Types
        public static DataTable GetAppTypes()
        {
            return ApplicationsAccess.GetApptypes();
        }

        public static bool UpdateAppType(int AppTypeID, string ApplicationTypeTitle, float ApplicationFees)
        {
            return ApplicationsAccess.UpdateAppType(AppTypeID, ApplicationTypeTitle, ApplicationFees); 
        }
    }

    public class clsTestApointments
    {
        public static DataTable GetTestAppointments(int LDLAppID, int TestTypeID)
        {
            return Tests_AppointmentsAccess.GetAppointments(LDLAppID, TestTypeID);
        }

        public static float GetTestTypeFees(int TestTypeID)
        {
            return Tests_AppointmentsAccess.GetTestTypeFees(TestTypeID);
        }

        public static bool AddTestsAppointment(int TestTypeID, int LDLAppID, DateTime AppoiDate, float PaidFees, int createdByUserID, int isLocked)
        {
            return Tests_AppointmentsAccess.AddTestsAppointment(TestTypeID, LDLAppID, AppoiDate, PaidFees, createdByUserID, isLocked);
        }

        public static int AddTest(int testAppointmentID, byte TestResult, string Notes, int CreatedByUserID)
        {
            return Tests_AppointmentsAccess.AddTest(testAppointmentID, TestResult, Notes, CreatedByUserID);
        }

        public static string GetTestAppointmentDate(int TestAppointmentID)
        {
            return Tests_AppointmentsAccess.GetTestAppointmentDate(TestAppointmentID);
        }

        //Manage Test Types
        public static DataTable GetTestTypes()
        {
            return Tests_AppointmentsAccess.GetTestTypes();
        }

        public static bool UpdateTestType(int ID, string Title, string Description, float Fees)
        {
            return Tests_AppointmentsAccess.UpdateTestType(ID, Title, Description, Fees);
        }
    }

    public class clsLicense
    {
        public static int AddLocalLicense(int LDLAppID, int CreatedByUserID, string Notes)
        {
            return LicensesAccess.AddLocalLicense(LDLAppID, CreatedByUserID, Notes);
        }

        public static DataTable GetLicenseInfo(int LicenseID)
        {
            return LicensesAccess.GetLicenseInfo(LicenseID);
        }

        public static DataTable GetInternationalLicenseInfo(int InterLicenseID)
        {
            return LicensesAccess.GetInternationalLicenseInfo(InterLicenseID);
        }

        public static DataTable GetLocalLicenses(int PersonID)
        {
            return LicensesAccess.GetLocalLicenses(PersonID);
        }

        public static DataTable GetInternationalLicenses(int PersonID)
        {
            return LicensesAccess.GetInternationalLicenses(PersonID);
        }

        public static bool IsLicenseExist(int LicenseID)
        {
            return LicensesAccess.IsLicenseExistByID(LicenseID);
        }

        public static int[] ReplaceLicense(int LicenseID, int ReplacementType, int ApplicantPersonID,
                                                float PaidFees, int CreatedByUserID)
        {
            return LicensesAccess.ReplaceLicense(LicenseID, ReplacementType, ApplicantPersonID, PaidFees, CreatedByUserID);
        }

        public static int getLicenseClass(int licenseID)
        {
            return LicensesAccess.getLicenseClass(licenseID);
        }

        public static int[] IssueInternationalLicense(int LicenseID, int AppTypeID, int ApplicantPersonID,
                                            float PaidFees, int CreatedByUserID)
        {
            return LicensesAccess.IssueInternationalLicense(LicenseID, AppTypeID, ApplicantPersonID, PaidFees, CreatedByUserID);
        }

        public static bool IsInternationalLicenseExist(int LocalLID)
        {
            return LicensesAccess.IsInternationalLicenseExist(LocalLID);
        }

        public static DataTable GetDrivers()
        {
            return LicensesAccess.GetDrivers();
        }
        public static int[] RenewLicense(int LicenseID, int ApplicantPersonID, float PaidFees, int CreatedByUserID)
        {
            return LicensesAccess.RenewLicense(LicenseID,ApplicantPersonID, PaidFees, CreatedByUserID);
        }
        public static string GetLicenseNotes(int LicenseID)
        {
            return LicensesAccess.GetLicenseNotes(LicenseID);
        }

        //Manage Detained Licenses
        public static DataTable GetDetainedLicenses()
        {
            return LicensesAccess.GetDetainedLicenses();
        }

        public static int DetainLicense(int LicenseID, float FineFees, int CreatedByUserID)
        {
            return LicensesAccess.DetainLicense(LicenseID, FineFees, CreatedByUserID);
        }
        public static bool IsDetained(int LicenseID)
        {
            return LicensesAccess.IsDetained(LicenseID);
        }
        public static DataRow GetDetainInfo(int LicenseID)
        {
            return LicensesAccess.GetDetainInfo(LicenseID);
        }

        public static int ReleaseDetainedLicense(int LicenseID, int UserId, int PersonID, float PaidFees)
        {
            return LicensesAccess.ReleaseDetainedLicense(LicenseID, UserId, PersonID, PaidFees);
        }
        public static DateTime GetExpirationDate(int LicenseID)
        {
            return LicensesAccess.GetExpirationDate(LicenseID);
        }
        public static float GetLicenseClassFees(int LicenseID)
        {
            return LicensesAccess.GetLicenseClassFees(LicenseID);
        }
    }

}
