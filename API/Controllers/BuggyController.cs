using System;
using API.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController : BaseApiController
{
    [HttpGet("Unauthorised")]
    public ActionResult GetUnauthorised()
    {
        return Unauthorized("You are not authorized");
    }

    [HttpGet("Badrequest")]
    public ActionResult GetBadRequest()
    {
        return BadRequest("This was a bad request");
    }

    [HttpGet("NotFound")]
    public ActionResult GetNotFound()
    {
        return NotFound("No resource found");
    }

    [HttpGet("internalerror")]
    public ActionResult GetInternalServerError()
    {
        throw new Exception("This is some exception");
    }

    [HttpPost("validationerror")]
    public ActionResult GetValidationError(CreateProductDto product)
    {
        return Ok();
    }
}
