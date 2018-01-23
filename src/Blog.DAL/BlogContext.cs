using System;
using Blog.Entity;
using Microsoft.EntityFrameworkCore;

namespace Blog.DAL
{
    public class BlogContext:DbContext
    {
        public DbSet<WechatUser> WechatUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseMySql(@"Server=localhost;database=blog;uid=root;pwd=YFsYg2LaMemcNy26;");
    }
}
