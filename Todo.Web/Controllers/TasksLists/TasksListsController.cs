using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Models.Projections;
using Todo.Application.Models.Requests.Page;
using Todo.Application.Models.Requests.TasksList;
using Todo.Application.Models.Requests.TasksList.Page;
using Todo.Application.Services.Interfaces;
using Todo.Web.Infrastructure;
using Todo.Web.Models.Requests;
using Todo.Web.Models.Responses.Common;

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
        public async Task<IActionResult> DeleteTasksList(int id, CancellationToken cancellationToken)
        {
            var result = await tasksListService.DeleteTasksListAsync(id, cancellationToken);

            return result.ToHttpResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTasksList(int id, CancellationToken cancellationToken)
        {
            var result = await tasksListService.GetTasksListByIdAsync(id, cancellationToken);

            return result.ToHttpResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetTasksLists([FromQuery] TasksListFiltering filtering, [FromQuery] PagePagination pagination, CancellationToken cancellationToken)
        {
            var result = await tasksListService.GetTasksListPageAsync(filtering, pagination, cancellationToken);

            if (result.IsFailure)
            {
                return result.ToHttpResult();
            }

            return new OkObjectResult(new PagedList<TasksListProjection>
            {
                Items = result.Value.Items,
                Metadata = new PagedMetadata
                {
                    CurrentPage = pagination.CurrentPage,
                    PageSize = pagination.PageSize,
                    TotalItems = result.Value.TotalCount,
                }
            });
        }
    }
}
