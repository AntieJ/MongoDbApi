using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using ChatDAL.CommonModels;
using DbApi.Models;
using DbApi.Services;

namespace DbApi.Controllers
{
    public class DatabaseController : ApiController
    {
        private DALService _dbService;

        public DatabaseController()
        {
            _dbService = new DALService();
        }

        // GET api/chats/5
        [ActionName("chats")]
        public HttpResponseMessage Getdb(int id)
        {
            return GetHttpResponseMessage(_dbService.Get(id));
        }

        // GET api/chats
        [ActionName("chats")]
        public HttpResponseMessage Getdb()
        {
            return GetHttpResponseMessage(_dbService.Get());
        }

        // POST api/chats
        [HttpPost]
        [ActionName("chats")]
        public string SaveChatLog([FromBody] List<ChatLog> chats)
        {
            _dbService.Create(chats);
            return "done";
        }

        [HttpDelete]
        [ActionName("chats")]
        public string DeleteChatLog([FromUri] string id)
        {
            return _dbService.Delete(id);
        }

        [HttpDelete]
        [ActionName("chats")]
        public string DeleteChatLog()
        {
            return _dbService.Delete();
        }
        //GET api/insertfakes
        [HttpGet]
        public string InsertFakeChat()
        {
            var fakeChatLog = new List<ChatLog>()
            {
                new ChatLog("aj", "hello", DateTime.Now.ToString()),
                new ChatLog("anand", "hi", DateTime.Now.AddMinutes(1).ToString()),
                new ChatLog("anton", "how are you", DateTime.Now.AddMinutes(2).ToString()),
                new ChatLog("anand", "good", DateTime.Now.AddMinutes(3).ToString())
            };

            _dbService.Create(fakeChatLog);

            return "done";
        }

        private HttpResponseMessage GetHttpResponseMessage(string returnString)
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent(returnString, System.Text.Encoding.UTF8, "application/json")
                
            };

        }

        /*Example Save
         * 
            POST http://localhost:5749/api/chats HTTP/1.1
            User-Agent: Fiddler
            Host: localhost:5749
            Content-Type: application/json
            Content-Length: 568

            [
                {
                    "DisplayName": "aj",
                    "Chat": "howdy",
                    "TimeStamp": "26/08/2016 10:50:48"
                },
                {
                    "DisplayName": "anand",
                    "Chat": "hidyho",
                    "TimeStamp": "26/08/2016 10:51:48"
                },
                {
                    "DisplayName": "anton",
                    "Chat": "how are things",
                    "TimeStamp": "26/08/2016 10:52:48"
                },
                {
                    "DisplayName": "anand",
                    "Chat": "great!",
                    "TimeStamp": "26/08/2016 10:53:48"
                }
            ]
         * 
         * */
    }
}
