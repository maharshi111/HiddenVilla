using Microsoft.AspNetCore.Identity;

namespace DataAccess.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        // the main purpouse of creating ApplicationUser class is , see before doing this the dbo.AspNetUsers table in database doesnot had a column name Name 
        // to add this particular column to our Users table we first created this class and then in ApplicationDbContext we did the below
        // IdentityDbContex ->  IdentityDbContext<ApplicationUser> and than did migration (the migration name is AddAplicationUsertoDb )
        //see vedio 117
    }
}
