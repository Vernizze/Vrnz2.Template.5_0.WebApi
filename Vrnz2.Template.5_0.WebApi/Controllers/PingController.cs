using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vrnz2.BaseWebApi.Helpers;

namespace Vrnz2.Template._5_0.WebApi.Controllers
{
    [Route("")]
    public class PingController
        : Controller
    {
        #region Variables

        private readonly ControllerHelper _controllerHelper;

        #endregion 

        #region Constructors

        public PingController(ControllerHelper controllerHelper) 
            => _controllerHelper = controllerHelper;

        #endregion 

        #region Methods

        /// <summary>
        /// Service Heart Beat end point
        /// </summary>
        /// <returns>DateTime.UtcNow + Service Name</returns>
        [HttpGet("ping")]
        [ProducesResponseType(typeof(BaseContracts.DTOs.PingResponse), 200)]
        public async Task<ObjectResult> Ping()
            => await _controllerHelper.ReturnAsync((request) => Task.FromResult(new BaseContracts.DTOs.Ping.Response().Content), new BaseContracts.DTOs.Ping.Request());

        #endregion
    }
}
