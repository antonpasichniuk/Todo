using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Models.Requests;
using Todo.Application.Models.Requests.Page;
using Todo.Application.Models.Requests.TasksList.Page;
using Todo.Application.Services.Interfaces;
using Todo.Web.Infrastructure;
using Todo.Web.Models.Requests;

namespace Todo.Web.Controllers.TasksLists
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public partial class TasksListsController(ITasksListService tasksListService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateTasksList([FromBody]CreateTaskListRequest createTaskListRequests, CancellationToken cancellationToken)
        {
            var result = await tasksListService.CreateTasksListAsync(new CreateTasksList
            {
                Name = createTaskListRequests.Name
            },
            cancellationToken);

            return result.ToHttpResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTasksList(int id, [FromBody] UpdateTaskListRequest updateTaskListRequest, CancellationToken cancellationToken) 
        {
            var result = await tasksListService.UpdateTasksListAsync(new UpdateTasksList { Id = id, Name = updateTaskListRequest.Name }, cancellationToken);

            return result.ToHttpResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasksList(int id)
        {
            var result = await tasksListService.DeleteTasksListAsync(id);

            return result.ToHttpResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTasksList(int id)
        {
            var result = await tasksListService.GetTasksListByIdAsync(id);

            return result.ToHttpResult();
        }

        [HttpGet]
        public Task<IActionResult> GetTasksLists([FromQuery] TasksListFiltering filtering, [FromQuery] PagePagination pagination)
        {
            
        }
    }
}
