using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TEALINK.GARDEN.Interface.Login;
using TEALINK.GARDEN.Model.Auth;
using TEALINK.GARDEN.Model.Permission;

namespace TEALINK.GARDEN.Repository.LoginRepository
{
    public class RLogin : ILogin
    {
        private readonly Context _context;
        private IConfiguration _config;


        public RLogin(Context context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public IQueryable<LoginModel> AuthenticateUser(string username, string password)
        {
            SqlParameter Type = new SqlParameter("@Type", 2);
            SqlParameter UserName = new SqlParameter("@UserName", username == null ? "" : username);
            SqlParameter Password = new SqlParameter("@Password", password == null ? "" : password);
            return _context.LoginModel.FromSqlRaw("Exec SPGETLOGINUSER @Type,@UserName,@Password", Type, UserName, Password);
        }

        //public string GenerateJsonWebToken()
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        //      _config["Jwt:Issuer"],
        //      null,
        //      expires: DateTime.UtcNow.AddDays(1),
        //      signingCredentials: credentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        public string GenerateJsonWebToken(int enroll, int unitId, int typeId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("Enroll", enroll.ToString()),
                new Claim("UnitId", unitId.ToString()),
                new Claim("TypeId", typeId.ToString())
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,                                     // was null
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // RLogin
        public List<OperatorPermissionListRow> GetOperatorPermissionList()
        {
            return _context.Set<OperatorPermissionListRow>()
                .FromSqlRaw("EXEC SPGETOPERATORPERMISSIONLIST")
                .ToList();
        }
    }
}
