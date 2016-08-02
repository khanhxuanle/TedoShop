using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Model.Models;
using TeduShop.Service;

namespace TeduShop.Web.Infrastructure.Core
{
    public class ApiControllerBase : ApiController
    {
        private readonly IErrorService errorService;

        public ApiControllerBase(IErrorService errorService)
        {
            this.errorService = errorService;
        }

        protected HttpResponseMessage CreatHttpResponseMessage(HttpRequestMessage responseMessage, Func<HttpResponseMessage> func)
        {
            HttpResponseMessage response = null;

            try
            {
                response = func.Invoke();
            }
            catch (DbEntityValidationException dbEntityEx)
            {
                foreach (var eve in dbEntityEx.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:");

                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                LogError(dbEx);
                response = responseMessage.CreateResponse(HttpStatusCode.BadRequest, dbEx.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                response = responseMessage.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return response;
        }

        private void LogError(Exception ex)
        {
            try
            {
                var error = new Error();
                error.CreateDate = DateTime.Now;
                error.Message = ex.Message;
                error.StackTrace = ex.StackTrace;

                errorService.Create(error);
                errorService.Save();
            }
            catch
            {

            }
        }
    }
}