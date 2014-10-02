//using System.Data.Entity;
//using System.Data.Entity.ModelConfiguration.Conventions;

//using Company.Module.Domain;

//namespace Company.Module.Repositories
//{
//    public class MyContext : DbContext, IMyContext
//    {
//        //// ----------------------------------------------------------------------------------------------------------
		
//        public IDbSet<Patient> Patients { get; set; }

//        //// ----------------------------------------------------------------------------------------------------------

//        public IDbSet<TestResult> TestResults { get; set; }

//        //// ----------------------------------------------------------------------------------------------------------

//        static MyContext()
//        {
//            Database.SetInitializer<MyContext>(null);
//        }

//        //// ----------------------------------------------------------------------------------------------------------

//        public MyContext() : base("DefaultConnectionString")
//        {
//        }

//        //// ----------------------------------------------------------------------------------------------------------

//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

//            //modelBuilder.Configurations.Add(new EmployeeConfiguration())
//            //                           .Add(new DepartmentConfiguration());
//        }

//        //// ----------------------------------------------------------------------------------------------------------

//    }
//}
