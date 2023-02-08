using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhatsAppDAL.Model;

namespace WhatsAppDAL
{
    public interface IWhatsAppService : IDisposable
    { 
       Task<long> CreateUser (UserViewModel user);

       Task<int> UpdateUser (int id, UserViewModel user);

        Task<int> DeleteUser (int id);

        Task<UserViewModel> GetUser (int id);

      Task<IEnumerable<UserViewModel>> GetUsers ();
    }
}