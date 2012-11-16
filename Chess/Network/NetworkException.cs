using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Network
{
    public class NetworkException: Exception
    {
        private String msg;
        public String Message {
            get {
                return msg;
            }
        }
        NetworkException()
        {
            msg = "Something wrong";
        }
        NetworkException(String message)
        {
            msg = message;
        }
    }
}
