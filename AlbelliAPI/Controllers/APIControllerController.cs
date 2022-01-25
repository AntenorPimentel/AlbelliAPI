using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;

namespace AlbelliAPI.Controllers
{
    public class APIControllerController : Controller
    {
        protected readonly ILogger _logger = Log.Logger;

        protected async Task<ActionResult<T>> ExecuteGet<T>(Func<Task<T>> action, string customErrorMessage)
        {
            try
            {
                return Ok(await action());
            }
            catch (ArgumentException ex)
            {
                LogError(ex, customErrorMessage);
                return StatusCode(StatusCodes.Status204NoContent, ex.Message);
            }
            catch (Exception ex)
            {
                LogError(ex, customErrorMessage);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        protected async Task<ActionResult> ExecutePost(Func<Task> action, string customErrorMessage)
        {
            try
            {
                await action();
                return Ok();
            }
            catch (ArgumentException ex)
            {
                LogError(ex, customErrorMessage);
                return StatusCode(StatusCodes.Status204NoContent, ex.Message);
            }
            catch (Exception ex)
            {
                LogError(ex, customErrorMessage);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        protected void LogError(Exception ex, string customErrorMessage)
        {
            var controllerName = ControllerContext.ActionDescriptor.ControllerName;
            var actionName = ControllerContext.ActionDescriptor.ActionName;

            _logger.Error(ex, "{CustomErrorMessage}, Controller: {ControllerName}, Action: {ActionName}", customErrorMessage, controllerName, actionName);
        }
    }
}
