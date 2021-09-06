using Microsoft.AspNetCore.Mvc;
using SimpleStore.Application.Models;
using SimpleStore.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleStore.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService itemService;

        public ItemsController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        [HttpGet("{id}")]
        public ActionResult<ItemModel> GetItem(int id)
        {
            return itemService.GetItemById(id);
        }

        [HttpPost]
        public ActionResult<int> CreateItem(ItemModel item)
        {
            return itemService.AddItem(item).ItemId;
        }

        [HttpPut]
        public ActionResult<ItemModel> UpdateItem(ItemModel item)
        {
            itemService.UpdateItem(item);
            return itemService.GetItemById(item.ItemId);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteItem(int id)
        {
            itemService.DeleteItem(new ItemModel() { ItemId = id });
            return Ok();
        }
    }
}
