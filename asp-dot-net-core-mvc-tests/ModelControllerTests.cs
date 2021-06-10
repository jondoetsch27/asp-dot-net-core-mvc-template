using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using asp.net.core.mvc.demo;
using asp.net.core.mvc.demo.Controllers;
using asp.net.core.mvc.demo.Models;
using asp.net.core.mvc.demo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace asp.net.core.mvc.tests
{
    public class ModelControllerTests
    {
        [Test]
        public async Task givenValidPostMethod_returnActionResult()
        {
            var modelMock = new Mock<MLModel>();
            var modelServiceMock = new Mock<ModelService>();
            modelServiceMock.Setup(m => m.createModel(modelMock.Object));

            var subject = new ModelController(modelServiceMock.Object);
            var actualOutput = await subject.Create(modelMock.Object);
            Assert.IsInstanceOf(typeof(IActionResult), actualOutput);
        }

        [Test]
        public async Task givenValidGetMethod_returnActionResult()
        {
            var modelMock = new Mock<MLModel>();
            var modelServiceMock = new Mock<ModelService>();
            modelServiceMock.Setup(m => m.readModel(modelMock.Object.modelName));

            var subject = new ModelController(modelServiceMock.Object);
            var actualOutput = await subject.Read(modelMock.Object.modelName);
            Assert.IsInstanceOf(typeof(IActionResult), actualOutput);
        }

        [Test]
        public async Task givenValidPutMethod_returnActionResult()
        {
            var modelMock = new Mock<MLModel>();
            var modelServiceMock = new Mock<ModelService>();
            modelServiceMock.Setup(m => m.updateModel(modelMock.Object));

            var subject = new ModelController(modelServiceMock.Object);
            var actualOutput = await subject.Update(modelMock.Object);
            Assert.IsInstanceOf(typeof(IActionResult), actualOutput);
        }

        [Test]
        public async Task givenValidDeleteMethod_returnActionResult()
        {
            var modelMock = new Mock<MLModel>();
            var modelServiceMock = new Mock<ModelService>();
            modelServiceMock.Setup(m => m.deleteModel(modelMock.Object.modelName));

            var subject = new ModelController(modelServiceMock.Object);
            var actualOutput = await subject.Delete(modelMock.Object.modelName);
            Assert.IsInstanceOf(typeof(IActionResult), actualOutput);
        }
    }
}
