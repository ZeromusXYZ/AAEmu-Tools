using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;

namespace AAEmu.DbEditor.data
{
    static class Data
    {
        static public ServerDB Server;
        static public ClientPak Client;
        static public AAEmuDB MySqlDb;

        static public void Initialize()
        {
            Server = new ServerDB();
            MySqlDb = new AAEmuDB();
            Client = new ClientPak();
            Client.Initialize();
        }
    }
}
