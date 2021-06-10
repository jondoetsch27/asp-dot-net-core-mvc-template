using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp.net.core.mvc.demo.Models;
using Microsoft.AspNetCore.Mvc;

namespace asp.net.core.mvc.demo.Controllers
{
    [Route("players")]
    public class PlayerController : Controller
    {
        private readonly PlayerDataContext _db;

        public PlayerController(PlayerDataContext playerDataContext)
        {
            _db = playerDataContext;
        }

        [Route("")]
        public IActionResult Index()
        {
            return new ContentResult{ Content = "/players" };
        }

        [HttpPost, Route("create")]
        public IActionResult Create(Player player)
        {
            if (!ModelState.IsValid)
            {
                return new ContentResult { Content = "Model Error" };
            }
            _db.Players.Add(player);
            _db.SaveChanges();
            return new ContentResult { Content = "/players/create" };
        }

        [HttpGet, Route("read")]
        public IActionResult Read()
        {
            return new ContentResult { Content = "/players/read" };
        }

        [HttpPut, Route("update")]
        public IActionResult Update()
        {
            return new ContentResult { Content = "/players/update" };
        }

        [HttpDelete, Route("delete")]
        public IActionResult Delete()
        {
            return new ContentResult { Content = "/players/delete" };
        }

    }
}
