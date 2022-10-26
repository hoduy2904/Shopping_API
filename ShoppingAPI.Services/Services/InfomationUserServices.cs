using ShoppingAPI.Data.Models;
using ShoppingAPI.REPO.Repository;
using ShoppingAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Services
{
    public class InfomationUserServices : IInfomationUserServices
    {
        private readonly IRepository<InfomationUser> repository;
        public InfomationUserServices(IRepository<InfomationUser> repository)
        {
            this.repository = repository;
        }
        public async Task DeleteInfomationUser(int id)
        {
            var infomationUser = await repository.GetAsync(id);
           await repository.DeleteAsync(infomationUser);
        }

        public async Task<InfomationUser> GetInfomationUserAsync(int id)
        {
            return await repository.GetAsync(id);
        }

        public async Task<IEnumerable<InfomationUser>> GetInfomationUsersAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task InsertInfomationUser(InfomationUser infomationUser)
        {
            await repository.InsertAsync(infomationUser);
        }

        public async Task UpdateInfomationUser(InfomationUser infomationUser)
        {
            await repository.UpdateAsync(infomationUser);
        }
    }
}
