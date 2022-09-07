using EstateManagement.WebService.Implementations;
using EstateManagement.WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace EstateManagement.WebService
{
    /// <summary>
    /// Summary description for EstateWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class EstateWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public CreateEstateResponse CreateEstate(CreateEstateRequest request)
        {
            EstateManager manager = new EstateManager();
            CreateEstateResponse response = manager.CreateEstate(request);
            return response;
        }

        [WebMethod]
        public string GetAllStates() 
        {
            EstateManager manager = new EstateManager();
            List<StateModel> stateList = manager.GetAllStates();
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(stateList);
        }

        [WebMethod]
        public string GetAllLgasByStateId(int stateId)
        {
            EstateManager manager = new EstateManager();
            List<LgaModel> lgaModels= manager.GetAllLgaByStateId(stateId);
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(lgaModels);
        }

        
    }
}
