using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp.net.core.mvc.demo.Exceptions;
using asp.net.core.mvc.demo.Models;
using asp.net.core.mvc.demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace asp.net.core.mvc.demo.Controllers
{
    [Produces("application/json")]
    [Route("Models")]
    [ApiController]
    public class ModelController : Controller
    {
        private readonly ModelService ModelService;

        public ModelController(ModelService ModelService)
        {
            this.ModelService = ModelService;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            return new NoContentResult();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(
            [Bind(include: "modelName, modelType, modelVersion, modelStatus")] MLModel Model)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }
            try
            {
                await ModelService.createModel(Model);
                Response.StatusCode = 201;
                return new JsonResult(Model);
            }
            catch (DuplicateModelException)
            {
                return new ConflictResult();
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("read/{id}")]
        public async Task<IActionResult> Read(string modelName)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }
            try
            {
                MLModel Model = await ModelService.readModel(modelName);
                return new JsonResult(Model);
            }
            catch (ModelNotFoundException)
            {
                return new NoContentResult();
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(
            [Bind(include: "modelName, modelType, modelVersion, modelStatus")] MLModel newModel)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }
            try
            {
                await ModelService.updateModel(newModel);
                return new JsonResult(newModel);
            }
            catch (ModelNotFoundException)
            {
                return new NoContentResult();
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string modelName)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }
            try
            {
                MLModel Model = await ModelService.deleteModel(modelName);
                return new JsonResult(Model);
            }
            catch (ModelNotFoundException)
            {
                return new NoContentResult();
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
