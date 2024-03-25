using OEE_dotNET.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OEE_dotNET.API
{
    public class DataController : ApiController
    {
        [Route("Get_machine_runtime")]
        [HttpGet]
        public IHttpActionResult Get_machine_runtime() 
        {
            try
            {
                
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
