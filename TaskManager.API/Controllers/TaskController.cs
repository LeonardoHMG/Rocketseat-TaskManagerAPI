using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.UseCases.Tasks.Delete;
using TaskManager.Application.UseCases.Tasks.GetAll;
using TaskManager.Application.UseCases.Tasks.GetById;
using TaskManager.Application.UseCases.Tasks.Register;
using TaskManager.Application.UseCases.Tasks.Update;
using TaskManager.Communication.Requests;
using TaskManager.Communication.Responses;

namespace TaskManager.API.Controllers;
[Route("api/taks")]
[ApiController]
public class TaskController : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredTaskJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public IActionResult CreateTask([FromBody] RequestTaskJson request)
    {
        var response = new RegisterTaskUseCase().Execute(request);
        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseAllTaskJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public IActionResult GetAll()
    {
        var useCase = new GetAllTasksUseCase();

        var response = useCase.Execute();

        if (response.Tasks.Any())
        {
            return Ok(response);
        }

        return NoContent();
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseTaskJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public IActionResult Get([FromRoute] Guid id)
    {
        var useCase = new GetTaskByIdUseCase();
        var response = useCase.Execute(id);
        return Ok(response);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public IActionResult Upadate([FromRoute] Guid id, [FromBody] RequestTaskJson request)
    {
        var useCase = new UpdateTaskUseCase();

        useCase.Execute(id, request);

        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public IActionResult Delete([FromRoute] Guid id)
    {
        var useCase = new DeleteTaskByIdUseCase();

        useCase.Execute(id);

        return NoContent();
    }
}