using Microsoft.AspNetCore.Mvc;
using RifatSirProjectAPI5.Models;
using RifatSirProjectAPI5.Repository;

namespace RifatSirProjectAPI5.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminBasicInfoRepository _adminBasicInfoRepository = new AdminBasicInfoRepository();
        private readonly AdminAuthRepository _adminAuthRepository = new AdminAuthRepository();

        [HttpPost("surji/admin/signUp")]
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

        [HttpPost("surji/admin/signUp2")]
        public IActionResult StudentSignUp2([FromBody] AdminBasicInfo adminBasicInfo)
        {

            var addedStudent = _adminBasicInfoRepository.Add(adminBasicInfo);

            return Ok(true);
        }

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

            return Ok(false);
        }

        [HttpPost("surji/admin/signIn2")]
        public IActionResult AdminSignIn2([FromBody] AdminAuth adminAuth)
        {
            if (adminAuth.username == "~admin" && adminAuth.password == "iit123")
            {
                return Ok(true);
            }

            return Ok(false);
        }

        [HttpGet("surji/admin/getAll")]
        public IActionResult getAllStudentBasicInfo()
        {
            return Ok(_adminBasicInfoRepository.GetAll());
        }

        [HttpPost("surji/admin/update")]
        public IActionResult updateStudentBasicInfo([FromBody] AdminBasicInfo admin)
        {
            _adminBasicInfoRepository.Update(admin);
            return Ok(true);

        }

        [HttpPost("surji/admin/updatePassword")]
        public IActionResult updateStudentAuth([FromBody] AdminAuth adminAuth)
        {
            _adminAuthRepository.Update(adminAuth);
            return Ok(true);

        }

        [HttpPost(("surji/admin/delete"))]
        public IActionResult deleteAccount([FromBody] AdminAuth adminAuth)
        {
            _adminAuthRepository.Delete(adminAuth.username);
            _adminBasicInfoRepository.Delete(adminAuth.username);
            return Ok(true);
        }
    }
}
