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
    public class StudentController : Controller
    {
        private RifatSirProjectAPI5Context _dbContext;
        private readonly RoleManager<RifatSirProjectAPI5Role> _roleManager;
        private readonly UserManager<RifatSirProjectAPI5User> _userManager;
        private readonly SignInManager<RifatSirProjectAPI5User> _signInManager;

        private readonly StudentBasicInfoRepository _studentBasicInfoRepository = new StudentBasicInfoRepository();

        public StudentController(RifatSirProjectAPI5Context _dbContext, RoleManager<RifatSirProjectAPI5Role> _roleManager, UserManager<RifatSirProjectAPI5User> _userManager, SignInManager<RifatSirProjectAPI5User> _signInManager)
        {
            this._dbContext = _dbContext;
            this._roleManager = _roleManager;
            this._userManager = _userManager;
            this._signInManager = _signInManager;
        }

        [AllowAnonymous]
        [HttpPost("surji/student/signIn")]
        public async Task<IActionResult> GetToken([FromBody] StudentAuth studentAuth)
        {
            RifatSirProjectAPI5User user;

            if (studentAuth.BSSEROLL != 0)
            {
                user = _dbContext.rifatSirProjectAPI5User.FirstOrDefault(x => x.UserName == studentAuth.BSSEROLL.ToString());
            }

            else if (studentAuth.Email != null)
            {
                user = _dbContext.rifatSirProjectAPI5User.FirstOrDefault(x => x.Email == studentAuth.Email);
            }
            else {
                return Ok(false);
            }
            var role = await _userManager.GetRolesAsync(user);

            if (user != null)
            {
                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, studentAuth.password, false);
                var tokenString = "";

                if (signInResult.Succeeded)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes("RabiulIslamSujon-01633667872");

                    if (role[0] == "student")
                    {
                        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.Role, "student"),
                        }),
                            Expires = DateTime.UtcNow.AddDays(7),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                        };

                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        tokenString = tokenHandler.WriteToken(token);
                    }

                    return Ok(new { tokenString });
                }
                else
                {
                    return Ok(false);
                }
            }

            return Ok(false);
        }

        [Authorize(Roles = "student")]
        [HttpPost("surji/student/getStudentInfo")]
        public IActionResult GetStudentInfo ([FromBody] StudentAuth studentAuth)
        {
            if (studentAuth.BSSEROLL != 0) 
            {
                var userInfo = _studentBasicInfoRepository.GetByBSSEROLL(studentAuth.BSSEROLL);
                return Ok(userInfo);
            }

            else if (studentAuth.Email != null) 
            {
                var userInfo = _studentBasicInfoRepository.GetByEmail(studentAuth.Email);
                return Ok(userInfo);
            }

            return Ok(false);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("surji/student/signUp")]
        public async Task<ActionResult> Register([FromBody] StudentAuth studentAuth)
        {
            var check = _dbContext.rifatSirProjectAPI5User.FirstOrDefault(x => x.Email == studentAuth.Email);
            if (check != null) return Ok("Already Has An Account");

            RifatSirProjectAPI5User user = new RifatSirProjectAPI5User()
            {
                Email = studentAuth.Email,
                UserName = studentAuth.BSSEROLL.ToString()
            };

            var result = await _userManager.CreateAsync(user, studentAuth.password);


            if (result.Succeeded)
            {
                var role = _dbContext.rifatSirProjectAPI5Role.FirstOrDefault(x => x.Name == "student");

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
        [HttpPost("surji/student/addStudentBasicInfo")]
        public IActionResult addStudentBasicInfo([FromBody] StudentBasicInfo student)
        {
            var addedStudent = _studentBasicInfoRepository.Add(student);

            return Ok(true);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("surji/student/getAll")]
        public IActionResult getAllStudentBasicInfo()
        {
            return Ok(_studentBasicInfoRepository.GetAll());
        }

        [Authorize(Roles = "admin, student")]
        [HttpPost("surji/student/update")]
        public IActionResult updateStudentBasicInfo([FromBody] StudentBasicInfo student)
        {
            var check = _dbContext.rifatSirProjectAPI5User.FirstOrDefault(x => x.UserName == student.BSSEROLL.ToString());
            if (check == null)
            {
                return Ok(false);
            }
            else if (check.Email == student.Email) 
            {
                _studentBasicInfoRepository.Update(student);
                return Ok(true);
            }

            return Ok(false);
        }

        [Authorize(Roles = "student")]
        [HttpPost("surji/student/updatePassword")]
        public async Task<IActionResult> updateStudentAuth([FromBody] ChangePasswordModel model)
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
        [HttpPost(("surji/student/delete"))]
        public async Task<IActionResult> deleteAccount([FromBody] StudentAuth studentAuth) 
        {
            var user = _dbContext.rifatSirProjectAPI5User.FirstOrDefault(x => x.Email == studentAuth.Email);
            await _userManager.DeleteAsync(user);
            _studentBasicInfoRepository.Delete(studentAuth.BSSEROLL);
            return Ok(true);
        }

    }
}
