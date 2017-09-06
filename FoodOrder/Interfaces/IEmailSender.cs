using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrder.Interfaces
{
    public interface IEmailSender
    {        
        void SendEmail(string recipient,string title, string message);
    }
}
