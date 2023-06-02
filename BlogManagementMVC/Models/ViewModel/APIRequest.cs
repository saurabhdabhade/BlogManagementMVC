using System.Security.AccessControl;
using static BlogManagementLibrary.SD;

namespace BlogManagementMVC.Models.ViewModel
{
    public class APIRequest
    {
        public string  ApiType { get; set; } = "GET";
        public string Url { get; set; }
        public Object Data { get; set; }
       
    }
}
