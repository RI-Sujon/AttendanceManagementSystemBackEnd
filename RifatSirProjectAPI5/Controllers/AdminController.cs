using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RifatSirProjectAPI5.Areas.Identity.Data;
using RifatSirProjectAPI5.Data;
using RifatSirProjectAPI5.Models;
using RifatSirProjectAPI5.Repository;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RifatSirProjectAPI5.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AdminController : Controller
    {
        private RifatSirProjectAPI5Context _dbContext;
        private readonly RoleManager<RifatSirProjectAPI5Role> _roleManager;
        private readonly UserManager<RifatSirProjectAPI5User> _userManager;
        private readonly SignInManager<RifatSirProjectAPI5User> _signInManager;

        private readonly AdminBasicInfoRepository _adminBasicInfoRepository = new AdminBasicInfoRepository();

        public AdminController(RifatSirProjectAPI5Context _dbContext, RoleManager<RifatSirProjectAPI5Role> _roleManager, UserManager<RifatSirProjectAPI5User> _userManager, SignInManager<RifatSirProjectAPI5User> _signInManager)
        {
            this._dbContext = _dbContext;
            this._roleManager = _roleManager;
            this._userManager = _userManager;
            this._signInManager = _signInManager;
        }


        /*
        [AllowAnonymous]
        [HttpPost("surji/admin/createInitialAdmin")]
        public async Task<ActionResult> AddAdminAccount([FromBody] AdminAuth adminAuth)
        {
            RifatSirProjectAPI5User user = new RifatSirProjectAPI5User()
            {
                UserName = adminAuth.username,
            };

            var result = await _userManager.CreateAsync(user, adminAuth.password);


            if (result.Succeeded)
            {
                var role = _dbContext.rifatSirProjectAPI5Role.FirstOrDefault(x => x.Name == "admin");

                var result1 = await _userManager.AddToRoleAsync(user, role.Name);

                if (result1.Succeeded)
                {
                    return Ok(true);
                }

                return Ok(false);
            }

            return Ok(false);
        }*/


        [AllowAnonymous]
        [HttpPost("surji/admin/signIn")]
        public async Task<IActionResult> GetToken([FromBody] AdminAuth adminAuth)
        {
            var user = _dbContext.rifatSirProjectAPI5User.FirstOrDefault(x => x.UserName == adminAuth.username);
            var role = await _userManager.GetRolesAsync(user);

            if (user != null)
            {
                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, adminAuth.password, false);
                var tokenString = "";

                if (signInResult.Succeeded)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes("RabiulIslamSujon-01633667872");
                     
                    if (role[0] == "admin")
                    {
                        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, adminAuth.username),
                            new Claim(ClaimTypes.Role, "admin"),
                        }),
                            Expires = DateTime.UtcNow.AddDays(7),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                        };

                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        tokenString = tokenHandler.WriteToken(token);
                    }
                    else if (role[0] == "teacher")
                    {
                        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                                new Claim(ClaimTypes.Name, adminAuth.username),
                                new Claim(ClaimTypes.Role, "teacher"),
                            }),

                            Expires = DateTime.UtcNow.AddDays(7),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                        };

                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        tokenString = tokenHandler.WriteToken(token);
                    }

                    return Ok(new { tokenString });
                }
                else {
                    return Ok(false);
                }
            }

            return Ok(false);
        }

        [Authorize(Roles = "teacher")]
        [HttpPost("surji/admin/getTeacherInfo")]
        public IActionResult GetStudentInfo([FromBody] AdminAuth adminAuth)
        {
            var userInfo = _adminBasicInfoRepository.GetByUserName(adminAuth.username);
            return Ok(userInfo);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("surji/admin/signUp")]
        public async Task<ActionResult> Register([FromBody] AdminAuth adminAuth)
        {
            RifatSirProjectAPI5User user = new RifatSirProjectAPI5User()
            {
                UserName = adminAuth.username,
            };

            var result = await _userManager.CreateAsync(user, adminAuth.password);
            

            if (result.Succeeded)
            {
                var role = _dbContext.rifatSirProjectAPI5Role.FirstOrDefault(x => x.Name == "teacher");

                var result1 = await _userManager.AddToRoleAsync(user, role.Name);

                if (result1.Succeeded)
                {
                    return Ok(true);
                }

                return Ok(false);
            }

            return Ok(false);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("surji/admin/addTeacherInfo")]
        public IActionResult AddTeacherInfo([FromBody] AdminBasicInfo adminBasicInfo)
        {

            _adminBasicInfoRepository.Add(adminBasicInfo);

            return Ok(true);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("surji/admin/getAll")]
        public IActionResult getAllTeacherBasicInfo()
        {
            return Ok(_adminBasicInfoRepository.GetAll());
        }

        [Authorize(Roles = "admin, teacher")]
        [HttpPost("surji/admin/update")]
        public IActionResult updateTeacherBasicInfo([FromBody] AdminBasicInfo admin)
        {
            var check = _dbContext.rifatSirProjectAPI5User.FirstOrDefault(x => x.UserName == admin.username);
            if (check == null)
            {
                return Ok(false);
            }

            _adminBasicInfoRepository.Update(admin);
            return Ok(true);

        }

        [Authorize(Roles = "teacher, admin")]
        [HttpPost("surji/admin/updatePassword")]
        public async Task<IActionResult> updateTeacherAuth([FromBody] ChangePasswordModel model)
        {
            var user = _dbContext.rifatSirProjectAPI5User.FirstOrDefault(x => x.UserName == model.usernameOrRoll);

            if (user != null)
            {
                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, model.oldPassword, false);

                if (signInResult.Succeeded)
                {
                    var userToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, userToken, model.newPassword);
                    if (result.Succeeded)
                    {
                        return Ok(true);
                    }
                }
            }

            return Ok(false);
        }

        [Authorize(Roles = "admin")]
        [HttpPost(("surji/admin/delete"))]
        public async Task<IActionResult> deleteAccount([FromBody] AdminAuth adminAuth)
        {
            var user = _dbContext.rifatSirProjectAPI5User.FirstOrDefault(x => x.UserName == adminAuth.username);
            await _userManager.DeleteAsync(user);
            _adminBasicInfoRepository.Delete(adminAuth.username);
            return Ok(true);
        }
    }
}


/*
        //[AllowAnonymous]
        //[HttpPost("createRole")]
        public async Task<ActionResult> assignUserRole (){
            var user = _dbContext.rifatSirProjectAPI5User.FirstOrDefault(x => x.UserName == "admin");
            var role = _dbContext.rifatSirProjectAPI5Role.FirstOrDefault(x => x.Name == "admin");

            var result = await _userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                return Ok(true);
            }

            return Ok(false);
        }

        //[AllowAnonymous]
        //[HttpPost("createRole")]
        public async Task<ActionResult> createRole() {
            RifatSirProjectAPI5Role role = new RifatSirProjectAPI5Role()
            {
                Name = "student"
            };

            var result = await _roleManager.CreateAsync(role);
            
            if (result.Succeeded)
            {
                return Ok(true);
            }

            return Ok(false);
        }*/

/*
 * 
        [AllowAnonymous]
        [HttpPost("surji/admin/signIn")]
        public IActionResult AdminSignIn([FromBody] AdminAuth adminAuth)
        {
            var checkAccount = _adminAuthRepository.GetByUserName(adminAuth.username);
            if (checkAccount != null)
            {
                if (checkAccount.password == adminAuth.password)
                {
                    return Ok(_adminBasicInfoRepository.GetByUserName(adminAuth.username));
                }
            }

            //admin
            //iit12345

            return Ok(false);
        } 

        [AllowAnonymous]
        [HttpPost("surji/admin/signIn2")]
        public IActionResult AdminSignIn2([FromBody] AdminAuth adminAuth)
        {
            if (adminAuth.username == "~admin" && adminAuth.password == "iit123")
            {
                return Ok(true);
            }

            return Ok(false);
        }
 
  [HttpPost("ll")]
        public IActionResult StudentSignUp([FromBody] AdminAuth adminAuth)
        {
            var checkAccount = _adminAuthRepository.GetByUserName(adminAuth.username);

            if (checkAccount == null)
            {
                var addedStudent = _adminAuthRepository.Add(adminAuth);

                return Ok(true);
            }
            else
            {
                return Ok("already exists");
            }
        }
 */