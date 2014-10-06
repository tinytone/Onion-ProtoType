using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using System.Web.Http.Description;

namespace Company.Module.Web.Host.Controllers.WebApi
{
    public class BootsController : ApiController
    {
        //// ----------------------------------------------------------------------------------------------------------

        // GET: api/boots
        [ResponseType(typeof(List<KeyValuePair<String, String>>))]
        public IHttpActionResult Get()
        {
            return GetBootStyleOptions();
        }

        //// ----------------------------------------------------------------------------------------------------------

        [ResponseType(typeof(List<KeyValuePair<String, String>>))]
        [Route("api/boots/GetBootStyleOptions")]
        public IHttpActionResult GetBootStyleOptions()
        {
            var bootStyles = new List<KeyValuePair<String, String>>
                                 {
                                     new KeyValuePair<string, string>("", "&mdash; choose a style &mdash;"),
                                     new KeyValuePair<string, string>("7177382", "Caterpillar Tradesman Work Boot"),
                                     new KeyValuePair<string, string>("7269643", "Caterpillar Logger Boots"),
                                     new KeyValuePair<string, string>("7332058", "Chippewa 9\" Briar Waterproof Bison Boot"),
                                     new KeyValuePair<string, string>("7141832", "Chippewa 17\" Engineer Boot"),
                                     new KeyValuePair<string, string>("7141833", "Chippewa 17\" Snakeproof Boot"),
                                     new KeyValuePair<string, string>("7173656", "Chippewa 11\" Engineer Boot"),
                                     new KeyValuePair<string, string>("7141922", "Chippewa Harness Boot"),
                                     new KeyValuePair<string, string>("7141730", "Danner Foreman Pro Work Boot"),
                                     new KeyValuePair<string, string>("7257914", "Danner Grouse GTX Boot")
                                 };
        
            return Ok(bootStyles);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
