using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Models.Domain;

namespace WallerAPI.Data
{
    public class Seeder
    {
        private readonly WallerDbContext _ctx;
        private readonly UserManager<User> _userMgr;
        private readonly RoleManager<IdentityRole> _roleMgr;

        public Seeder(WallerDbContext ctx, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _ctx = ctx;
            _userMgr = userManager;
            _roleMgr = roleManager;
        }

        public async Task Seed()
        {
            _ctx.Database.EnsureCreated();

            try
            {
                var roles = new string[] { "Admin", "Regular" };
                if (!_roleMgr.Roles.Any())
                {
                    foreach(var role in roles)
                    {
                        await _roleMgr.CreateAsync(new IdentityRole(role));
                    }
                    _ctx.SaveChanges();
                }

                var currencies = new List<Currency>();
                var currencyDetails = new string[,] { { "Naira", "NGN" }, { "Dollars", "USD" }, { "Pounds", "GBP" } };
                if (!_ctx.Currencies.Any())
                {
                    for(int i=0; i<3; i++)
                    {
                        var currency = new Currency
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = currencyDetails[i, 0],
                            Abbreviation = currencyDetails[i, 1]
                        };
                        _ctx.Currencies.Add(currency);
                        currencies.Add(currency);
                    }
                }
                else
                {
                    currencies = _ctx.Currencies.ToList();
                }

                var data = File.ReadAllText(@"C:\Users\Avatar\Documents\AB\Holiday\WallerAPI\WallerAPI.Data\SeedData.json");
                var ListOfUsers = JsonConvert.DeserializeObject<List<User>>(data);

                if (!_userMgr.Users.Any())
                {
                    var counter = 0;
                    var role = "";
                    Currency currency = null;
                    foreach(var user in ListOfUsers)
                    {
                        user.UserName = user.Email;

                        currency = counter < 1 ? currencies[2] : (counter % 2 == 0 ? currencies[0] : currencies[1]);
                        user.Wallets[0].Currency = currency;

                        user.Wallets[0].Id = Guid.NewGuid().ToString();
                        _ctx.Wallets.Add(user.Wallets[0]);

                        role = counter < 1 ? roles[0] : (counter % 2 == 0 ? roles[1] : roles[2]);

                        var res = await _userMgr.CreateAsync(user, "P@55word");
                        if (res.Succeeded) await _userMgr.AddToRoleAsync(user, role);

                        _ctx.SaveChanges();
                        counter++;
                    }
                }
            }
            catch (DbException)
            {
                // Log Error
            }
        }

    }
}
