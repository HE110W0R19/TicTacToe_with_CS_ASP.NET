using FirstWebApp.Models;

namespace FirstWebApp.ServerDatabase
{
    public static class Database
    {
        public static Dictionary<Guid, string> Users = new Dictionary<Guid, string>();
        public static Dictionary<Guid, GameInfo> Games = new Dictionary<Guid, GameInfo>();
        public static Dictionary<Guid, Guid?> Tables = new Dictionary<Guid, Guid?>();
    
        static Database() 
        { 
            for (var i = 0; i < 3; ++i)
            {
                Tables[Guid.NewGuid()] = null;
            }
        }
    }
}
