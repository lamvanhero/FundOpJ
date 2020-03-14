using Newtonsoft.Json.Linq;
using OppJar.Common.Proxy;
using OppJar.Web.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OppJar.Common.Enum;
using OppJar.Dto;
using OppJar.Common.Helpers;
using System.Net.Http.Headers;
using System.IO;
using System;

namespace OppJar.Web.Proxies
{
    public class OppJarProxy : BaseProxy
    {
        private const string LOGIN = "/api/account/login";
        private const string CURRENT_USER_PROFILE = "/api/account/current-user-profile";
        private const string UPDATE_USER_PROFILE = "/api/account/{0}";
        private const string REGISTER = "/api/account/register";
        private const string GET_USER_BY_ID = "/api/account/{0}";
        private const string GET_USER_BY_SLUG = "/api/account/slug/{0}";
        private const string FORGOT_PASSWORD = "/api/account/forgot-password";
        private const string SEND_EMAIL_FORGOT_PASSWORD = "/api/account/send-mail-forgot-password";
        private const string SEND_EMAIL_ACTIVE_ACCOUNT = "/api/account/send-mail-activate-account";
        private const string ACTIVE_ACCOUNT = "/api/account/activate-account";
        private const string SEARCH = "/api/account/search?{0}";
        private const string CHANGE_STATUS = "/api/account/{0}/change-status/{1}";
        private const string CREATE_ADMIN = "/api/account/admin";
        private const string DONATE = "/api/account/{0}/donate";
        private const string IMAGE = "/api/files/image?fileName={0}";
        private const string UPLOAD_FILE = "api/files?userId={0}&type={1}";
        private const string UPLOAD_FILE_MULTIPLE = "/api/files/multiple";
        private const string GET_CHILDREN = "/api/account/children";
        private const string CREATE_CHILD = "/api/account/child";

        public OppJarProxy(HttpClient httpClient) : base(httpClient)
        {
        }

        #region Account

        public async Task<JObject> LoginAsync(LoginDto dto)
        {
            var response = await PostAsJsonAsync(LOGIN, dto);

            var result = JObject.Parse(await response.Content.ReadAsStringAsync());

            return result;
        }

        public async Task<CurrentUserProfileModel> CurrentUserProfileAsync()
        {
            var response = await GetAsync(CURRENT_USER_PROFILE);

            return await response.Content.ReadAsAsync<CurrentUserProfileModel>();
        }

        public async Task<HttpResponseMessage> RegisterAsync(RegisterDto vm)
        {
            var response = await PostAsJsonAsync(REGISTER, vm);

            return response;
        }

        public async Task<HttpResponseMessage> GetUserDetailBySlugAsync(string slug)
        {
            return await GetAsync(string.Format(GET_USER_BY_SLUG, slug));
        }

        public async Task<HttpResponseMessage> GetUserDetailByIdAsync(string id)
        {
            return await GetAsync(string.Format(GET_USER_BY_ID, id));
        }

        public async Task<HttpResponseMessage> UpdateUserDetailByIdAsync(EditProfileDto dto, string id)
        {
            return await PutAsJsonAsync(string.Format(UPDATE_USER_PROFILE, id), dto);
        }

        public async Task<HttpResponseMessage> ActivateAccountAssync(ActivateAccountDto dto)
        {

            return await PostAsJsonAsync(ACTIVE_ACCOUNT, dto);
        }

        public async Task SendMailActivateAccountAsync(ResetPasswordDto dto)
        {
            await PostAsJsonAsync(SEND_EMAIL_ACTIVE_ACCOUNT, dto);
        }

        public async Task<HttpResponseMessage> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            return await PostAsJsonAsync(FORGOT_PASSWORD, dto);
        }

        public async Task<HttpResponseMessage> SendMailForgotPasswordAsync(ResetPasswordDto dto)
        {
            return await PostAsJsonAsync(SEND_EMAIL_FORGOT_PASSWORD, dto);
        }

        public async Task<PageResultDto<UserDetailDto>> SearchAsync(AccountQuerySearch accountQuerySearch)
        {
            var response = await GetAsync(string.Format(SEARCH, accountQuerySearch.GetQueryString()));

            return await response.Content.ReadAsAsync<PageResultDto<UserDetailDto>>();
        }

        public async Task<HttpResponseMessage> ChangeStatusAsync(UserStatus status, string id)
        {
            return await PutAsJsonAsync(string.Format(CHANGE_STATUS, id, status), null);
        }

        public async Task<HttpResponseMessage> CreateAdminAsync(BaseRegisterDto dto)
        {
            return await PostAsJsonAsync(CREATE_ADMIN, dto);
        }

        public async Task<HttpResponseMessage> GetChildrenAsync()
        {
            return await GetAsync(GET_CHILDREN);
        }

        public async Task<HttpResponseMessage> DonateAsync(string id, DonationDto dto)
        {
            return await PutAsJsonAsync(string.Format(DONATE, id), dto);
        }

        public async Task<HttpResponseMessage> CreateChildAsync(ChildInfoDto dto)
        {
            return await PostAsJsonAsync(CREATE_CHILD, dto);
        }

        #endregion

        #region File
        public async Task<FileStreamResult> GetImageAsync(string fileName)
        {
            var response = await GetAsync(string.Format(IMAGE, fileName));

            return await response.Content.ReadAsAsync<FileStreamResult>();
        }

        public async Task<HttpResponseMessage> UploadFileAsync(IFormFile file, string id, UsingForType type = UsingForType.Avatar)
        {
            byte[] data;
            using (var br = new BinaryReader(file.OpenReadStream()))
                data = br.ReadBytes((int)file.OpenReadStream().Length);

            ByteArrayContent bytes = new ByteArrayContent(data);


            MultipartFormDataContent multiContent = new MultipartFormDataContent
            {
                { bytes, "file", file.FileName }
            };

            return await _httpClient.PostAsync(string.Format(UPLOAD_FILE, id, type), multiContent);
        }

        public async Task<HttpResponseMessage> UploadFilesAsync(IFormFileCollection files, UsingForType type = UsingForType.Avatar)
        {
            return await PostAsJsonAsync(UPLOAD_FILE_MULTIPLE, files);
        }

        #endregion

    }
}
