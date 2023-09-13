public struct UserForAuth
    {
        public int? SchuelerId;
        public int? LehrerId;
        public string LoginId;
        public string password;
        public bool IstAdmin;
        public UserForAuth(object[] vals)
        {
            LoginId = (string)vals[0];
            password = (string)vals[1];
            IstAdmin = (bool)vals[2];
            try
            {
                SchuelerId = (int?)(System.Int64?)vals[3];
            }
            catch
            {
                SchuelerId = null;
            }
            try
            {
                LehrerId = (int?)(System.Int64?)vals[4];
            }
            catch
            {
                LehrerId = null;
            }
        }
    }