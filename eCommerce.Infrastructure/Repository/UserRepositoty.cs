using Dapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContract;
using eCommerce.Infrastructure.DbContext;

namespace eCommerce.Infrastructure.Repository
{
    public class UserRepositoty : IUserRepository
    {
        private readonly DapperDbContext _dbContext;
        public UserRepositoty(DapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApplicationUser?> AddUser(ApplicationUser user)
        {
            user.UserId = Guid.NewGuid();
            string query = "INSERT INTO public.\"Users\"(\"UserId\",\"Email\",\"PersonName\",\"Gender\",\"Password\")" +
                " VALUES(@UserId,@Email,@PersonName,@Gender,@Password)";
            int rows=await _dbContext.dbConnection.ExecuteAsync(query,user);
            if (rows == 0)
            {
                return null;
            }
            return user;
        }

        public async Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password)
        {
            string query = "SELECT * FROM public.\"Users\" WHERE \"Email\"=@Email AND \"Password\"=@Password";
            var parameters=new {Email= email, Password = password};
            ApplicationUser? user=await _dbContext.dbConnection.QueryFirstOrDefaultAsync<ApplicationUser>(query,parameters);
            return user;
        }
    }
}
