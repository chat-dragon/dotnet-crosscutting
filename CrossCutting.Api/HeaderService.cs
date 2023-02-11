using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Api
{
    public class HeaderService
    {
        public const string KEY_CONTA_OWNER = "conta-owner";

        private readonly IHttpContextAccessor _accessor;

        public HeaderService(IHttpContextAccessor accessor)
        {
            this._accessor = accessor;
        }

        public Guid? IdContaOwner
        {
            get
            {
                if (_accessor.HttpContext.Request.Headers.TryGetValue(KEY_CONTA_OWNER, out var idContaOwnerValue))
                {
                    return Guid.Parse(idContaOwnerValue[0]);
                }
                return null;
            }
        }
    }
}
