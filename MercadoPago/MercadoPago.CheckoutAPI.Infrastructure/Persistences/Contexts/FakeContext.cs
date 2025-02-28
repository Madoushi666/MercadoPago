using MercadoPago.CheckoutAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercadoPago.CheckoutAPI.Infrastructure.Persistences.Contexts
{
    public class FakeContext
    {
        private IEnumerable<Role> _roles { get; set; } 
        private IEnumerable<User> _users { get; set; }

        public FakeContext() 
        {
            _roles = new List<Role>()
            {
                new Role()
                {
                    RoleId = 1,
                    Description = "administrator"
                },
                new Role()
                {
                    RoleId = 2,
                    Description = "supervisor"
                },
                new Role()
                {
                    RoleId = 3,
                    Description = "customer"
                },
            };

            _users = new List<User>()
            {
                new User()
                {
                    // ADMIN
                    UserId = 1,
                    UserName = "Admin",
                    Password = "$argon2id$v=19$m=65536,t=3,p=1$wJzWQr1EhYRpANrf4CN46g$r769BfweDJBt6GW/l2/9+Tf+glooEXl4WWAf18u+1C8", // 1234
                    Email = "test@admin.com.ar",
                    Role = _roles.FirstOrDefault(r => r.RoleId == 1)
                },
                new User()
                {
                    // CUSTOMER
                    UserId = 2,
                    UserName = "Customer",
                    Password = "$argon2id$v=19$m=65536,t=3,p=1$dx/JCx8reoHCnDyv7N30dg$qK//vGto1eBnUUmjwoVOKmVNxNO2rHwIRvIun4eWAxc", // 4321
                    Email = "test@customer.com.ar",
                    Role = _roles.FirstOrDefault(r => r.RoleId == 3)
                }
            };
        }

        public IEnumerable<Role> Roles { get { return _roles; } }
        public IEnumerable<User> Users { get { return _users; } }
    }
}
