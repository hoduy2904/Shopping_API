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
        public async void DeleteInfomationUser(int id)
        {
            var infomationUser = await repository.GetAsync(id);
            repository.DeleteAsync(infomationUser);
        }

        public async Task<InfomationUser> GetInfomationUserAsync(int id)
        {
            return await repository.GetAsync(id);
        }

        public async Task<IEnumerable<InfomationUser>> GetInfomationUsersAsync()
        {
            return await repository.GetAllAsync();
        }

        public void InsertInfomationUser(InfomationUser infomationUser)
        {
            repository.InsertAsync(infomationUser);
        }

        public void UpdateInfomationUser(InfomationUser infomationUser)
        {
            repository.UpdateAsync(infomationUser);
        }
    }
}
