using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

public class BlogContext : IdentityDbContext<UserModel, IdentityRole<int>,int>
{
    public BlogContext(DbContextOptions<BlogContext> options) : base(options)
    {
    }

    public DbSet<PostModel> postModels { get; set; }
    public DbSet<CommentModel> commentModels { get; set; }
    public DbSet<PostRatingsModel> postRatingsModels { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<PostModel>()
            .HasOne(p => p.Authors)
            .WithMany()
            .HasForeignKey(p => p.authorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<PostModel>()
            .HasMany(p => p.Comments)
            .WithOne(c => c.Post)
            .HasPrincipalKey(p => p.postId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<CommentModel>()
            .HasOne(c => c.Author)
            .WithMany()
            .HasForeignKey(c => c.authorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<PostRatingsModel>()
            .HasOne(pr => pr.Post)
            .WithMany()
            .HasForeignKey(pr => pr.postId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<PostRatingsModel>()
            .HasOne(pr => pr.User)
            .WithMany()
            .HasForeignKey(pr => pr.userId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
