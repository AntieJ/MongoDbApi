namespace ChatDAL.CommonModels
{
    public class ChatLog
    {
        public ChatLog(string displayName, string chat, string timestamp)
        {
            DisplayName = displayName;
            Chat = chat;
            TimeStamp = timestamp;
        }
        public string DisplayName { get; set; }
        public string Chat { get; set; }
        public string TimeStamp { get; set; }
    }
}