using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using FNHMVC.CommandProcessor.Dispatcher;
using FNHMVC.Data.Repositories;
using AutoMapper;
using FNHMVC.Model;
using FNHMVC.Web.Core.Models;

namespace FNHMVC.Web.Core.Authentication
{
    public class DefaultUserRoleStore : IUserRoleStore<FNHMVCUser, int> 
    {
        private readonly IUserRepository userRepository;

        public DefaultUserRoleStore(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public Task CreateAsync(FNHMVCUser user)
        {
            throw new NotImplementedException();
        }
        public Task DeleteAsync(FNHMVCUser user)
        {
            throw new NotImplementedException();
        }
        public Task<FNHMVCUser> FindByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
        public Task<FNHMVCUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }
        public Task UpdateAsync(FNHMVCUser user)
        {
            throw new NotImplementedException();
        }

        public Task AddToRoleAsync(FNHMVCUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(FNHMVCUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(FNHMVCUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(FNHMVCUser user, string roleName)
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
        }
    }
}
