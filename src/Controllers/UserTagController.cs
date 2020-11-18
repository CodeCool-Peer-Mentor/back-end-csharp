namespace Codecool.PeerMentors.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Codecool.PeerMentors.DbContexts;
    using Codecool.PeerMentors.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ITagDTO = Codecool.PeerMentors.DTOs.ITag;

    public abstract class UserTagController<TEntity, TUserTag> : ControllerBase
        where TEntity : Tag
        where TUserTag : UserTag<TEntity>, new()
    {
        private readonly PeerMentorDbContext context;
        private readonly UserManager<User> userManager;

        public UserTagController(PeerMentorDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        protected async Task<IActionResult> AddTag(
            ITagDTO tag,
            Func<PeerMentorDbContext, DbSet<TEntity>> getTable)
        {
            TEntity dbTag = await getTable(context).SingleOrDefaultAsync(t => t.Name == tag.Name);
            if (dbTag == null)
            {
                return BadRequest();
            }

            User user = await userManager.GetUserAsync(User);
            TUserTag userTag = await context.Set<TUserTag>()
                .SingleOrDefaultAsync(ut => ut.User.Id == user.Id && ut.Tag.Name == tag.Name);
            if (userTag == null)
            {
                context.Add(new TUserTag() { User = user, Tag = dbTag });
                await context.SaveChangesAsync();
            }

            return Ok();
        }

        protected async Task<IActionResult> RemoveTag(
            ITagDTO tag,
            Func<PeerMentorDbContext, DbSet<TUserTag>> getTable)
        {
            User user = await userManager.GetUserAsync(User);
            TUserTag userTag = await getTable(context)
                .SingleOrDefaultAsync(ut => ut.User.Id == user.Id && ut.Tag.Name == tag.Name);
            if (userTag == null)
            {
                return BadRequest();
            }

            context.Remove(userTag);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
