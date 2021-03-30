using HealthESB.API.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthESB.Domain.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using HealthESB.Framework.Utility;
using Microsoft.AspNetCore.Authorization;
using HealthESB.Domain.IService;
using HealthESB.EF.DynamicFilter;

namespace HealthESB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtConfig _jwtConfig;
        private readonly IAspNetUserRolesService _aspNetUserRolesService;
        private readonly IClaimsService _claimsService;
        public AuthManagementController(UserManager<IdentityUser> userManager, IOptionsMonitor<JwtConfig> optionsMonitor, RoleManager<IdentityRole> roleManager, IAspNetUserRolesService aspNetUserRolesService, IClaimsService claimsService)
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
            _roleManager = roleManager;
            _aspNetUserRolesService = aspNetUserRolesService;
            _claimsService = claimsService;
        }

        [HttpPost]
        [Route("CreateUser")]
        [Authorize]
        public async Task<IActionResult> CreateUser([FromBody] UserRegistrationRequest user)
        {
            // Check if the incoming request is valid
            if (ModelState.IsValid)
            {
                // check i the user with the same email exist
                var existingUser = await _userManager.FindByNameAsync(user.UserName);

                if (existingUser != null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Result = false,
                        Errors = new List<string>(){
                                            "UserName already exist"
                                        }
                    });
                }

                var newUser = new IdentityUser() { Email = user.Email, UserName = user.UserName };
                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                if (isCreated.Succeeded)
                {
                    List<string> LstRoleName = new List<string>();
                    foreach (var item in user.LstRoleId)
                    {
                        var result = _roleManager.Roles.Where(a => a.Id == item.Trim()).FirstOrDefault();
                        LstRoleName.Add(result.Name);
                    }

                    await _userManager.AddToRolesAsync(newUser, LstRoleName);

                    var jwtToken = GenerateJwtToken(newUser.Id);
                    return Ok(new RegistrationResponse()
                    {
                        Result = true,
                        Token = jwtToken
                    });
                }

                return new JsonResult(new RegistrationResponse()
                {
                    Result = false,
                    Errors = isCreated.Errors.Select(x => x.Description).ToList()
                }
                        )
                { StatusCode = 500 };
            }

            return BadRequest(new RegistrationResponse()
            {
                Result = false,
                Errors = new List<string>(){
                                            "Invalid User"
                                        }
            });
        }
        [HttpPost]
        [Route("UpdateUser")]
        //[Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] UserRegistrationRequest user)
        {
            // Check if the incoming request is valid
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByIdAsync(user.Id);
                if (existingUser == null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Result = false,
                        Errors = new List<string>(){
                                            "User does not exist"
                                        }
                    });
                }
                var existingUserName = await _userManager.FindByNameAsync(user.UserName);
                if (existingUserName != null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Result = false,
                        Errors = new List<string>(){
                                            "Duplicate UserName "
                                        }
                    });
                }
                existingUser.UserName = user.UserName;
                existingUser.Email = user.Email;
                existingUser.PasswordHash = _userManager.PasswordHasher.HashPassword(existingUser, user.Password);
                var isUpdated = await _userManager.UpdateAsync(existingUser);
                if (isUpdated.Succeeded)
                {
                    List<string> LstRoleName = new List<string>();
                    foreach (var item in user.LstRoleId)
                    {
                        var result = _roleManager.Roles.Where(a => a.Id == item.Trim()).FirstOrDefault();
                        LstRoleName.Add(result.Name);
                    }
                    var oldRules = await _aspNetUserRolesService.getUserRolesByUserIdAsync(user.Id);
                    foreach (var item in oldRules.Roles)
                    {
                        await _userManager.RemoveFromRoleAsync(existingUser, item.Name);
                    }

                    await _userManager.AddToRolesAsync(existingUser, LstRoleName);

                    return Ok(new RegistrationResponse()
                    {
                        Result = true
                    });
                }

                return new JsonResult(new RegistrationResponse()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Error in update user"
                    }
                }
                        )
                { StatusCode = 500 };
            }

            return BadRequest(new RegistrationResponse()
            {
                Result = false,
                Errors = new List<string>(){
                                            "Invalid User"
                                        }
            });
        }
        [HttpPost]
        [Route("Login")]
        public async Task<UserListResponse> Login([FromBody] UserLoginRequest user)
        {
            UserListResponse userListResponse = new UserListResponse();
            if (ModelState.IsValid)
            {
               
                userListResponse.Users = new List<UserRow>();
                UserRow userRow = new UserRow();
                var existingUser = await _userManager.FindByNameAsync(user.UserName);
                if (existingUser == null)
                {
                    return userListResponse.ToFailedAuthentication<UserListResponse>();
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                if (isCorrect)
                {
                    Constants.LoginUserName = user.UserName;
                    var jwtToken = GenerateJwtToken(existingUser.Id);
                    userRow.Token = jwtToken;
                }
                else
                {
                    return userListResponse.ToFailedAuthentication<UserListResponse>();
                }

                var roles = _aspNetUserRolesService.getUserRolesByUserIdAsync(existingUser.Id);
                if (roles != null)
                {
                    userRow.Roles = new List<RoleRow>();
                    userRow.Roles = roles.Result.Roles;
                }
                var claim = _aspNetUserRolesService.getUserClaimsByUserIdAsync(existingUser.Id);
                if (claim != null)
                {
                    userRow.Claims = new List<ClaimsRow>();
                    userRow.Claims = claim.Result.Claims;
                }
                userListResponse.Users.Add(userRow);
                userListResponse.ToSuccess<UserListResponse>();
                userListResponse.HasError = false;
                return userListResponse;
            }
            return userListResponse.ToIncompleteInput<UserListResponse>();

        }
        private string GenerateJwtToken(string UserId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("UserId", UserId) }),
                Expires = DateTime.UtcNow.AddHours(Constants.TokenExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        [HttpPost]
        [Route("CreateRoles")]
        [Authorize]
        public async Task<IActionResult> CreateRoles([FromBody] RoleRequest role)
        {
            if (ModelState.IsValid)
            {
                if (await _roleManager.RoleExistsAsync(role.Name))
                    return BadRequest(new RegistrationResponse()
                    {
                        Result = false,
                        Errors = new List<string>(){
                                            "RoleName already exist"
                                        }
                    });
                var newRole = new IdentityRole() { Name = role.Name };
                var isCreated = await _roleManager.CreateAsync(newRole);
                if (isCreated.Succeeded)
                {
                    return Ok(new RoleResponse()
                    {
                        Result = true
                    });
                }
            }
            return BadRequest(new RoleResponse()
            {
                Result = false,
                Errors = new List<string>(){
                                            "Invalid Role"
                                        }
            });
        }
        [HttpPost]
        [Route("UpdateRoles")]
        [Authorize]
        public async Task<IActionResult> UpdateRoles([FromBody] RoleRequest role)
        {
            if (ModelState.IsValid)
            {
                var existedRole = await _roleManager.FindByIdAsync(role.Id);
                if (existedRole == null)
                    return BadRequest(new RegistrationResponse()
                    {
                        Result = false,
                        Errors = new List<string>(){
                                            "RoleName does not exist"
                                        }
                    });
                existedRole.Name = role.Name;

                var isUpdated = await _roleManager.UpdateAsync(existedRole);
                if (isUpdated.Succeeded)
                {
                    return Ok(new RoleResponse()
                    {
                        Result = true
                    });
                }
            }
            return BadRequest(new RoleResponse()
            {
                Result = false,
                Errors = new List<string>(){
                                            "Error In Update Role"
                                        }
            });
        }
        [HttpPost]
        [Route("GetRoles")]
        [Authorize]
        public RoleListResponse GetRoles([FromBody] RoleRequest role)
        {
            RoleListResponse roleListResponse = new RoleListResponse();
            roleListResponse.Roles = new List<RoleRow>();
            var existedRole = _roleManager.Roles.Where((a => (a.Name == role.Name || string.IsNullOrEmpty(role.Name)) &&
          (a.Id == role.Id || string.IsNullOrEmpty(role.Id)))).ToList();
            foreach (var item in existedRole)
            {
                roleListResponse.Roles.Add(new RoleRow() { Id = item.Id, Name = item.Name });
            }
            roleListResponse.LstCount = existedRole.Count();
            return roleListResponse;
        }

        [HttpPost]
        [Route("getUserRolesByUserIdAsync")]
        [Authorize]
        public async Task<RoleListResponse> getUserRolesByUserIdAsync([FromBody] UserRow User)
        {

            return await _aspNetUserRolesService.getUserRolesByUserIdAsync(User.Id);
        }
        [HttpPost]
        [Route("getUsersAsync")]
        [Authorize]
        public async Task<UserListResponse> getUsersAsync([FromBody] ListDTO listDTO)
        {

            SearchFilter searchFilter = new SearchFilter();
            listDTO.IsRequestCount = false;
            listDTO.PageNum = 1;
            listDTO.PageSize = 1000;
            return await _aspNetUserRolesService.getUsersAsync(listDTO);
        }
        [HttpPost]
        [Route("GetClaimList")]
        [Authorize]
        public async Task<ClaimsResponse> GetClaimList([FromBody] ListDTO listDTO)
        {
            SearchFilter searchFilter = new SearchFilter();
            listDTO.IsRequestCount = false;
            listDTO.PageNum = 1;
            listDTO.PageSize = 1000;
            return await _claimsService.GetListAsync(listDTO);
        }
        [HttpPost]
        [Route("AssignRoleToClaims")]
        [Authorize]
        public async Task<IActionResult> AssignRoleToClaims([FromBody] AssignRoleToClaimsRequest assignRoleToClaimsRequest)
        {
            var role = _roleManager.Roles.Where(a => a.Id == assignRoleToClaimsRequest.RoleId).FirstOrDefault();
            var claimResult = _claimsService.GetById(assignRoleToClaimsRequest.ClaimId).Result.Claims.FirstOrDefault();
            if (claimResult != null && role != null)
            {
                var OldClaims = await _roleManager.GetClaimsAsync(role);

                if (OldClaims.Where(a => a.Value == claimResult.Id.ToString()).Count() == 0)
                {
                    var IsCreated = await _roleManager.AddClaimAsync(role, new Claim(claimResult.ActionName, claimResult.Id.ToString()));
                    if (IsCreated.Succeeded)
                    {
                        return Ok(new RoleResponse()
                        {
                            Result = true
                        });
                    }
                }
                return BadRequest(new RoleResponse()
                {
                    Result = false,
                    Errors = new List<string>(){
                                            "Error In Update RoleClaim"
                                        }
                });
            }
            return BadRequest(new RoleResponse()
            {
                Result = false,
                Errors = new List<string>(){
                                            "Invalid input Data"
                                        }
            });
        }
        [HttpPost]
        [Route("RemoveClaimsFromRole")]
        [Authorize]
        public async Task<IActionResult> RemoveClaimsFromRole([FromBody] AssignRoleToClaimsRequest assignRoleToClaimsRequest)
        {
            var role = _roleManager.Roles.Where(a => a.Id == assignRoleToClaimsRequest.RoleId).FirstOrDefault();
            var claimResult = _claimsService.GetById(assignRoleToClaimsRequest.ClaimId).Result.Claims.FirstOrDefault();
            var OldClaims = await _roleManager.GetClaimsAsync(role);
            if (claimResult != null && role != null)
            {
                var IsRemoved = await _roleManager.RemoveClaimAsync(role, new Claim(claimResult.ActionName, claimResult.Id.ToString()));
                if (IsRemoved.Succeeded)
                {
                    return Ok(new RoleResponse()
                    {
                        Result = true
                    });
                }
                return BadRequest(new RoleResponse()
                {
                    Result = false,
                    Errors = new List<string>(){
                                            "Error In Update RoleClaim"
                                        }
                });
            }
            return BadRequest(new RoleResponse()
            {
                Result = false,
                Errors = new List<string>(){
                                            "Invalid input Data"
                                        }
            });
        }
        [HttpPost]
        [Route("GetUserClaims")]
        [Authorize]
        public async Task<ClaimsResponse> GetUserClaims([FromBody] UserRow User)
        {
            return await _aspNetUserRolesService.getUserClaimsByUserIdAsync(User.Id);
        }
    }
}
