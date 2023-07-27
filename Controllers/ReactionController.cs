using Microsoft.AspNetCore.Mvc;
using blogapp.Services;
using blogapp.Models;


namespace blogapp.Controllers
{
    public class ReactionController : Controller
    {
        public ReactionController(JsonReactionService jsonReactionService) 
        {
            JsonReactionService = jsonReactionService;
        }

        public JsonReactionService JsonReactionService { get; set; }

        public IActionResult React(Reaction toAdd)
        {
            var reacts = JsonReactionService.GetReactions();
            if(reacts.Where(x => x._article_id == toAdd._article_id && x._username == toAdd._username).Any())
            {
                //if the reaction is on the other button or already on the same button
                if (reacts.Where(x => x._article_id == toAdd._article_id 
                    && x._username == toAdd._username
                    && x._reaction == toAdd._reaction).Any())
                {
                    JsonReactionService.RemoveReaction(toAdd);
                }
                else
                {
                    JsonReactionService.RemoveReaction(
                        reacts.First(x => x._article_id == toAdd._article_id && x._username == toAdd._username));
                    JsonReactionService.AddReaction(toAdd);
                }
            }
            else
            {
                JsonReactionService.AddReaction(toAdd);
            }
            return Redirect("/");
        }

        public int[] Display(int article_id)
        {
            Console.WriteLine("art_id: " + article_id);
            var reacts = JsonReactionService.GetReactions();
            int[] result;
            //?

            if (reacts.Select(x => x._reaction).Distinct().Count() > 2)
                result = new int[reacts.Select(x => x._reaction).Distinct().Count()];
            else
                result = new int[2];
            foreach(var i in result)
                result[i] = 0;
            foreach (var i in reacts.Where(x => x._article_id == article_id))
            {
                Console.WriteLine("Increment: " + (i._reaction - 1));
                result[i._reaction - 1]++;
            }
            Console.WriteLine("Res values:");
            foreach(var i in result)
            {
                Console.WriteLine(result[i]);
            }
            return result;
        }
    }
}
