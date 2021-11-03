using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedBagerApi.Data;
using RedBagerApi.Models;
using System;

namespace RedBagerApi.Controllers
{
    [ApiController]
    [Route("v1/placas")]
    public class PlacaRBController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<PlacaRB>>> Get([FromServices] DataContext context)
        {
            var placas = await context.PlacaRBs.ToListAsync();
            return placas; 
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<PlacaRB>> GetById([FromServices] DataContext context, int id)
        {
            try
            {
                var placa = await context.PlacaRBs.
                AsNoTracking()
                .FirstAsync(x => x.Id == id);
                return placa; 
            }
            catch(Exception ex)
            {
                var str_ex = ex.ToString();
                return NotFound();
            }
        }

        [HttpGet]
        [Route("status/{status:int}")]
        public async Task<ActionResult<PlacaRB>> GetByStatus([FromServices] DataContext context, int status)
        {
            try
            {
                var placa = await context.PlacaRBs. 
                FirstAsync(x => x.Status == status);
                return placa; 
            }
            catch(Exception ex)
            {
                var str_ex = ex.ToString();
                return NotFound();
            }
        }

        [HttpGet]
        [Route("loja/{cpfCnpj:long}")]
        public async Task<ActionResult<List<PlacaRB>>> GetByLoja([FromServices] DataContext context, long cpfCnpj)
        {
            try
            {
                var placas = await context.PlacaRBs.ToListAsync();
                placas.RemoveAll(r => r.cpfCnpj != cpfCnpj || r.Status != 0);
                return placas;
            }
            catch(Exception ex)
            {
                var str_ex = ex.ToString();
                return NotFound();
            }
        }

        [HttpGet]
        [Route("count")]
        public async Task<ActionResult<int>> GetCount([FromServices] DataContext context)
        {
            var count = await context.PlacaRBs.CountAsync();
            return count; 
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<string>> Post(
            [FromServices] DataContext context,
            [FromBody] PlacaRB model)
        {
            if(ModelState.IsValid)
            {
                context.PlacaRBs.Add(model);
                await context.SaveChangesAsync();
                return "OK";
            }
            else
            {
                return BadRequest(ModelState);
            }
        } 

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<string>> PutById(
            [FromServices] DataContext context, 
            [FromBody] PlacaRB model,
            [FromRoute] int id)
        {
            var placa = await context.PlacaRBs.FirstOrDefaultAsync(x => x.Id == id);

            if(placa == null)
                return NotFound();

            placa.Status = model.Status;
            placa.DataPlaca = model.DataPlaca;
            placa.cpfCnpj = model.cpfCnpj;
            placa.IdChamador = model.IdChamador;

            context.PlacaRBs.Update(placa);
            await context.SaveChangesAsync();
            return "OK"; 
        }       

        [HttpPut]
        [Route("status/{status:int}")]
        public async Task<ActionResult<PlacaRB>> PutByStatus([FromServices] DataContext context, int status)
        {
            try
            {
                var placa = await context.PlacaRBs. 
                FirstAsync(x => x.Status == status);
                placa.Status = 0;
                placa.DataPlaca = Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd"));
                context.PlacaRBs.Update(placa);
                await context.SaveChangesAsync();
                return placa; 
            }
            catch(Exception ex)
            {
                var str_ex = ex.ToString();
                return NotFound();
            }
        }

        [HttpPut]
        [Route("all/{dataplaca:int}")]
        public async Task<ActionResult<string>> PutAll(
            [FromServices] DataContext context, 
            [FromBody] PlacaRB model,
            [FromRoute] int dataplaca)
        {
            var placas = await context.PlacaRBs.ToListAsync();
            foreach (var element in placas)
            {
                if(element.DataPlaca < dataplaca)
                {
                    element.Status = 9;
                    context.PlacaRBs.Update(element);
                    await context.SaveChangesAsync();
                }
            }

            return "OK";
        }       
    }
}