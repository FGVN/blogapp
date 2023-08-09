using Microsoft.AspNetCore.Mvc;
using blogapp.Services;
using blogapp.Models;

namespace blogapp.Controllers
{
    /// <summary>
    /// Controller that checks and sends reactions data to the service
    /// </summary>
    public class ReactionController : Controller
    {
        public SQLReactionService ReactionService { get; set; }
        /// <summary>
        /// Configuring service
        /// </summary>
        public ReactionController(SQLReactionService reactionService) => ReactionService = reactionService;
        /// <summary>
        /// Adds reaction to the article by internal logic
        /// </summary>
        /// <param name="toAdd">Reaction to add</param>
        /// <returns>Redirects to Index in all cases</returns>
        public IActionResult React(Reaction toAdd)
        {

            var reacts = ReactionService.GetReactions();
            foreach(var i in reacts)
                Console.WriteLine(i);
            if(reacts.Where(x => x._article_id == toAdd._article_id && x._username == toAdd._username).Any())
            {
                //if user has already reacted in the same way to that article - delete reaction
                if (reacts.Where(x => x._article_id == toAdd._article_id 
                    && x._username == toAdd._username
                    && x._reaction == toAdd._reaction).Any())
                    ReactionService.RemoveReaction(toAdd);
                //if user has already reacted in different way to the same article - change reaction on the new one
                else
                {
                    ReactionService.RemoveReaction(
                        reacts.First(x => x._article_id == toAdd._article_id && x._username == toAdd._username));
                    ReactionService.AddReaction(toAdd);
                }
            }
            //if user has not reacted yet - add reaction
            else
                ReactionService.AddReaction(toAdd);
            return Redirect("/");
        }

        /// <summary>
        /// Returns reactions for a certain article
        /// </summary>
        /// <param name="article_id">Id of an article to get reactions for</param>
        /// <returns>Array of reactions where index in array is reaction and value at index - number of reactions</returns>
        public int[] Display(int article_id)
        {
            var reacts = ReactionService.GetReactions();
            int[] result;

            if (reacts.Select(x => x._reaction).Distinct().Count() > 2)
                result = new int[reacts.Select(x => x._reaction).Distinct().Count()];
            else
                result = new int[2];

            foreach(var i in result)
                result[i] = 0;
            foreach (var i in reacts.Where(x => x._article_id == article_id))
                result[i._reaction - 1]++;
            return result;
        }
    }
}
