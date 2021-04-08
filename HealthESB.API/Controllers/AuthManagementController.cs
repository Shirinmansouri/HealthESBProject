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
using HealthESB.Framework.Logger;

namespace HealthESB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [UserAccess]
    public class AuthManagementController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtConfig _jwtConfig;
        private readonly IAspNetUserRolesService _aspNetUserRolesService;
        private readonly IClaimsService _claimsService;
        private readonly ILogService _logService;
        public AuthManagementController(ILogService logService, UserManager<IdentityUser> userManager, IOptionsMonitor<JwtConfig> optionsMonitor, RoleManager<IdentityRole> roleManager, IAspNetUserRolesService aspNetUserRolesService, IClaimsService claimsService)
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
            _roleManager = roleManager;
            _aspNetUserRolesService = aspNetUserRolesService;
            _claimsService = claimsService;
            _logService = logService;
        }

        [HttpPost]
        [Route("CreateUser")]
        [Authorize]
        public async Task<AutResponse> CreateUser([FromBody] UserRegistrationRequest user)
        {
            AutResponse baseResponse = new AutResponse();
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByNameAsync(user.UserName);
                if (existingUser != null)
                    return baseResponse.ToDulicateUserName<AutResponse>();

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
                    baseResponse.Token = jwtToken;
                    return baseResponse.ToSuccess<AutResponse>();
                }

                return baseResponse.ToApiError<AutResponse>();
            }
            return baseResponse.ToIncompleteInput<AutResponse>();

        }
        [HttpPost]
        [Route("UpdateUser")]
        [Authorize]
        public async Task<BaseResponse> UpdateUser([FromBody] UserRegistrationRequest user)
        {
            BaseResponse baseResponse = new BaseResponse();
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByIdAsync(user.Id);
                if (existingUser == null)
                    return baseResponse.ToInvalidUserNameOrPassword<BaseResponse>();
                var existingUserName = await _userManager.FindByNameAsync(user.UserName);
                if (existingUserName != null)
                {
                    if (existingUserName.Id != user.Id)
                        return baseResponse.ToDulicateUserName<BaseResponse>();
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

                    return baseResponse.ToSuccess<BaseResponse>();
                }

                return baseResponse.ToApiError<BaseResponse>();
            }

            return baseResponse.ToIncompleteInput<BaseResponse>();
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
                    return userListResponse.ToInvalidUserNameOrPassword<UserListResponse>();
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
                    return userListResponse.ToInvalidUserNameOrPassword<UserListResponse>();
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
                userRow.UserName = user.UserName;
                userRow.Id = existingUser.Id;
                userRow.Email = existingUser.Email;
                userListResponse.Users.Add(userRow);
                userListResponse.ToSuccess<UserListResponse>();


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
        public async Task<BaseResponse> CreateRoles([FromBody] RoleRequest role)
        {
            BaseResponse baseResponse = new BaseResponse();
            if (ModelState.IsValid)
            {

                if (await _roleManager.RoleExistsAsync(role.Name))
                    return baseResponse.ToDuplicateRole<BaseResponse>();
                var newRole = new IdentityRole() { Name = role.Name };
                var isCreated = await _roleManager.CreateAsync(newRole);
                if (isCreated.Succeeded)
                {
                    return baseResponse.ToSuccess<BaseResponse>();
                }
            }
            return baseResponse.ToIncompleteInput<BaseResponse>();
        }
        [HttpPost]
        [Route("UpdateRoles")]
        [Authorize]
        public async Task<BaseResponse> UpdateRoles([FromBody] RoleRequest role)
        {
            BaseResponse baseResponse = new BaseResponse();
            if (ModelState.IsValid)
            {
                var existedRole = await _roleManager.FindByIdAsync(role.Id);
                if (existedRole == null)
                    return baseResponse.ToDuplicateRole<BaseResponse>();
                existedRole.Name = role.Name;

                var isUpdated = await _roleManager.UpdateAsync(existedRole);
                if (isUpdated.Succeeded)
                    return baseResponse.ToSuccess<BaseResponse>();
            }
            return baseResponse.ToIncompleteInput<BaseResponse>();
        }
        [HttpPost]
        [Route("GetRoles")]
        [Authorize]
        public RoleListResponse GetRoles([FromBody] RoleRequest role)
        {
            RoleListResponse roleListResponse = new RoleListResponse();
            try
            {
                roleListResponse.Roles = new List<RoleRow>();
                var existedRole = _roleManager.Roles.Where((a => (a.Name == role.Name || string.IsNullOrEmpty(role.Name)) &&
              (a.Id == role.Id || string.IsNullOrEmpty(role.Id)))).ToList();
                foreach (var item in existedRole)
                {
                    roleListResponse.Roles.Add(new RoleRow() { Id = item.Id, Name = item.Name });
                }
                roleListResponse.ToSuccess<RoleListResponse>();
                roleListResponse.LstCount = existedRole.Count();
                return roleListResponse;
            }
            catch (Exception ex)
            {
                return roleListResponse.ToApiError<RoleListResponse>();
            }

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

            //SearchFilter searchFilter = new SearchFilter();
            //listDTO.IsRequestCount = false;
            //listDTO.PageNum = 1;
            //listDTO.PageSize = 1000;
            return await _aspNetUserRolesService.getUsersAsync(listDTO);
        }
        [HttpPost]
        [Route("GetClaimList")]
        [Authorize]
        public async Task<ClaimsResponse> GetClaimList([FromBody] ListDTO listDTO)
        {
            //SearchFilter searchFilter = new SearchFilter();
            //listDTO.IsRequestCount = false;
            //listDTO.PageNum = 1;
            //listDTO.PageSize = 1000;
            return await _claimsService.GetListAsync(listDTO);
        }
        [HttpPost]
        [Route("AssignRoleToClaims")]
       [Authorize]
        public async Task<BaseResponse> AssignRoleToClaims([FromBody] AssignRoleToClaimsRequest assignRoleToClaimsRequest)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    var role = _roleManager.Roles.Where(a => a.Id == assignRoleToClaimsRequest.RoleId).FirstOrDefault();

                    if (role != null)
                    {
                        var OldClaims = await _roleManager.GetClaimsAsync(role);
                        foreach (var item in OldClaims)
                        {
                            await _roleManager.RemoveClaimAsync(role, item);
                        }

                        foreach (var item in assignRoleToClaimsRequest.ClaimId)
                        {
                            var claimResult = _claimsService.GetById(item).Result.Claims.FirstOrDefault();
                            await _roleManager.AddClaimAsync(role, new Claim(claimResult.ActionName, claimResult.Id.ToString()));
                        }
                        return baseResponse.ToSuccess<BaseResponse>();

                    }
                }
                return baseResponse.ToIncompleteInput<BaseResponse>();
            }
            catch (Exception ex)
            {
                _logService.LogText(ex.Message);
                return baseResponse.ToApiError<BaseResponse>();
            }
        }
        //[HttpPost]
        //[Route("RemoveClaimsFromRole")]
        //[Authorize]
        //public async Task<BaseResponse> RemoveClaimsFromRole([FromBody] AssignRoleToClaimsRequest assignRoleToClaimsRequest)
        //{
        //    BaseResponse baseResponse = new BaseResponse();
        //    var role = _roleManager.Roles.Where(a => a.Id == assignRoleToClaimsRequest.RoleId).FirstOrDefault();
        //    var claimResult = _claimsService.GetById(assignRoleToClaimsRequest.ClaimId).Result.Claims.FirstOrDefault();
        //    var OldClaims = await _roleManager.GetClaimsAsync(role);
        //    if (claimResult != null && role != null)
        //    {
        //        var IsRemoved = await _roleManager.RemoveClaimAsync(role, new Claim(claimResult.ActionName, claimResult.Id.ToString()));
        //        if (IsRemoved.Succeeded)
        //            return baseResponse.ToSuccess<BaseResponse>();
        //        return baseResponse.ToApiError<BaseResponse>();
        //    }
        //    return baseResponse.ToIncompleteInput<BaseResponse>();
        //}
        [HttpPost]
        [Route("GetUserClaims")]
        //[Authorize]
        public async Task<ClaimsResponse> GetUserClaims([FromBody] UserRow User)
        {
            return await _aspNetUserRolesService.getUserClaimsByUserIdAsync(User.Id);
        }
        [HttpPost]
        [Route("GetRoleClaims")]
        [Authorize]
        public async Task<ClaimsResponse> GetRoleClaims([FromBody] RoleRequest role)
        {
            return await _aspNetUserRolesService.GetClaimsByRole(role.Id);
        }

    }
}
