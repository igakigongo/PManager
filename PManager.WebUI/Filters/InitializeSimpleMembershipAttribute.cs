using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PManager.WebUI.Filters
{
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    //public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    //{
    //    private static SimpleMembershipInitializer _initializer;
    //    private static object _initializerLock = new object();
    //    private static bool _isInitialized;

    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        // Ensure ASP.NET Simple Membership is initialized only once per app start
    //        LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
    //    }

    //    private class SimpleMembershipInitializer
    //    {
    //        public SimpleMembershipInitializer()
    //        {
    //            Database.SetInitializer<EFDbContext>(null);
    //            try
    //            {
    //                using (var context = new EFDbContext())
    //                {
    //                    if (!context.Database.Exists())
    //                    {
    //                        // Create the SimpleMembership database without Entity Framework migration schema
    //                        ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
    //                    }
    //                }

    //                // Try to check if membership has been initialized before you can initialize it
    //                if (!WebSecurity.Initialized)
    //                {
    //                    WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
    //                }

    //                //tryinng to setup role based security
    //                if (Roles.GetAllRoles().Length < 1)
    //                {
    //                    Roles.CreateRole("Admin");
    //                    Roles.CreateRole("NormalUser");

    //                    // ship the default admin account into the system
    //                    WebSecurity.CreateUserAndAccount("admin", "pass", propertyValues: null, requireConfirmationToken: false);

    //                    // they have to login to complete their profile, i won't do that for them
    //                    Roles.AddUserToRole("admin", "Admin");
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
    //            }
    //        }
    //    }
    //}
}