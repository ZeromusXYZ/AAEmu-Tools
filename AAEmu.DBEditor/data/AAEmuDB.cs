using AAEmu.DBEditor.data.aaemu.game;
using AAEmu.DBEditor.data.aaemu.login;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AAEmu.DBEditor.data
{
    public class AAEmuDB
    {
        public bool IsValid { get; set; }
        public string LastError { get; private set; }

        public aaemu_game_context Game { get; private set; }
        public aaemu_login_context Login { get; private set; }

        public AAEmuDB()
        {
            //
        }

        public bool Initialize()
        {
            IsValid = false;
            LastError = string.Empty;
            try
            {
                Game = new aaemu_game_context();
                Login = new aaemu_login_context();
                _ = Game.Characters.FirstOrDefault();
                _ = Login.Users.FirstOrDefault();
                IsValid = true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
            }

            return IsValid;
        }

    }
}
