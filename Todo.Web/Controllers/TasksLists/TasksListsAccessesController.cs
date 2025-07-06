using Microsoft.AspNetCore.Mvc;

namespace Todo.Web.Controllers.TasksLists
{
    public partial class TasksListsController
    {
        [HttpPost("{tasksListId}/access")]
        public Task<IActionResult> CreateAccess(int tasksListId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{tasksListId}/access")]
        public Task<IActionResult> GetAccesses(int tasksListId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{tasksListId}/access")]
        public Task<IActionResult> DeleteAccess(int tasksListId)
        {
            throw new NotImplementedException();
        }
    }
}
