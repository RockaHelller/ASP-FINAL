using ASP_FINAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASP_FINAL.Data
{
        public class AppDbContext : IdentityDbContext<AppUser>
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

            public DbSet<Basket> Baskets { get; set; }
            public DbSet<Brand> Brands { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Discount> Discounts { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<ProductBasket> ProductBaskets { get; set; }
            public DbSet<ProductImage> ProductImages { get; set; }
            public DbSet<ProductTag> ProductTags { get; set; }
            public DbSet<ProductWishList> ProductWishlists { get; set; }
            public DbSet<Rating> Ratings { get; set; }
            public DbSet<Review> Reviews { get; set; }
            public DbSet<Setting> Settings { get; set; }
            public DbSet<Subcategory> SubCategories { get; set; }
            public DbSet<Tag> Tags { get; set; }
            public DbSet<WishList> Wishlists { get; set; }
            public DbSet<Slider> Sliders { get; set; }
            public DbSet<SliderInfo> SliderInfos { get; set; }
            public DbSet<Blog> Blogs { get; set; }
            public DbSet<Branch> Branches { get; set; }
            public DbSet<Location> Locations { get; set; }
            public DbSet<Member> Members { get; set; }
            public DbSet<Reason> Reasons { get; set; }



            









        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Subcategory>()
            .HasMany(e => e.Products)
            .WithOne(e => e.SubCategory)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rating>()
            .HasMany(e => e.Reviews)
            .WithOne(e => e.Rating)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Basket>().HasQueryFilter(m => !m.SoftDelete);
                modelBuilder.Entity<Brand>().HasQueryFilter(m => !m.SoftDelete);
                modelBuilder.Entity<Category>().HasQueryFilter(m => !m.SoftDelete);
                modelBuilder.Entity<Discount>().HasQueryFilter(m => !m.SoftDelete);
                modelBuilder.Entity<Product>().HasQueryFilter(m => !m.SoftDelete);
                modelBuilder.Entity<ProductImage>().HasQueryFilter(m => !m.SoftDelete);
                modelBuilder.Entity<Rating>().HasQueryFilter(m => !m.SoftDelete);
                modelBuilder.Entity<Review>().HasQueryFilter(m => !m.SoftDelete);
                modelBuilder.Entity<Subcategory>().HasQueryFilter(m => !m.SoftDelete);
                modelBuilder.Entity<Tag>().HasQueryFilter(m => !m.SoftDelete);
                modelBuilder.Entity<WishList>().HasQueryFilter(m => !m.SoftDelete);

        }
        }


}
