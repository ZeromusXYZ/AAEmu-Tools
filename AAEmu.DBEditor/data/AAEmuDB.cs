using System;
using System.Linq;
using AAEmu.DBEditor.data.aaemu.game;
using AAEmu.DBEditor.data.aaemu.login;
using MySqlConnector;

namespace AAEmu.DBEditor.data
{
    public class AAEmuDB
    {
        public bool IsValid { get; set; }
        public string LastError { get; private set; }

        public GameContext Game { get; private set; }
        public LoginContext Login { get; private set; }

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
                Game = new GameContext();
                Login = new LoginContext();
                _ = Game.Characters.FirstOrDefault();
                _ = Login.Users.FirstOrDefault();
                IsValid = true;
            }
            catch (MySqlException mySqlEx)
            {
                switch (mySqlEx.ErrorCode)
                {
                    case MySqlErrorCode.AccessDenied:
                        LastError = $"Access denied, check your username and password";
                        break;
                    default:
                        LastError = $"{mySqlEx.ErrorCode}: {mySqlEx.Message}";
                        break;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
            }

            return IsValid;
        }

    }
}
