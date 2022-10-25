using ShoppingAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Interfaces
{
    public interface IInfomationUserServices
    {
        Task<InfomationUser> GetInfomationUserAsync(int id);
        Task<IEnumerable<InfomationUser>> GetInfomationUsersAsync();
        void InsertInfomationUser(InfomationUser infomationUser);
        void UpdateInfomationUser(InfomationUser infomationUser);
        void DeleteInfomationUser(int id);
    }
}
