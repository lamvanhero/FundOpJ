using Microsoft.Extensions.Configuration;
using System;

namespace OppJar.Core.Services
{
    public abstract class ServiceBase
    {
        protected readonly IUnitOfWork _unitOfWork;

        public string UserId
        {
            get
            {
                return _unitOfWork.UserId;
            }
        }

        public string UserName
        {
            get
            {
                return _unitOfWork.UserName;
            }
        }

        public string DisplayName
        {
            get
            {
                return _unitOfWork.DisplayName;
            }
        }

        protected string ApiUrl = string.Empty;

        public ServiceBase(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            ApiUrl = configuration.GetSection("JWTSettings").GetValue<string>("Issuer");
        }

        protected int GetTotalPage(int totalRecord, int take)
        {
            if (take > 0)
            {
                return (int)Math.Ceiling(a: totalRecord / (double)take);
            }
            throw new Exception("The take parameter require greater than zero.");
        }
    }
}
