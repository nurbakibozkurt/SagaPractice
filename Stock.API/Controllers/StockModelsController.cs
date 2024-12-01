using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock.API.Data;
using Stock.API.Dtos;
using Stock.API.Models;

namespace Stock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockModelsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StockModelsController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockModel>>> GetAllStocks()
        {
            return await _context.Stocks.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<StockModel>> GetStock(int id)
        {
            var stockModel = await _context.Stocks.FindAsync(id);

            if (stockModel == null)
            {
                return NotFound();
            }

            return stockModel;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockModel(int id, StockModelUpdateDto stockModelUpdateDto)
        {
            try
            {
                var stockToUpdate = await _context.Stocks.FindAsync(id);
                if (stockToUpdate == null)
                {
                    return NotFound();
                }

                stockToUpdate.Quantity = stockModelUpdateDto.quantity;

                _context.Stocks.Update(stockToUpdate);
                await _context.SaveChangesAsync();
                return Ok(stockToUpdate);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpPost]
        public async Task<ActionResult<StockModel>> PostStockModel(StockModelAddDto stockModelAddDto)
        {
            try
            {
                var stockToAdd = new StockModel()
                {
                    ProductId = stockModelAddDto.ProductId,
                    Quantity = stockModelAddDto.Quantity,
                };
                await _context.Stocks.AddAsync(stockToAdd);
                await _context.SaveChangesAsync();

                return Ok(stockToAdd);
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
            
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockModel(int id)
        {
            var stockModel = await _context.Stocks.FindAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
    }
}
