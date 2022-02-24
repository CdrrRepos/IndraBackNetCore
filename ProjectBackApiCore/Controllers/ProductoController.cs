using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBack.Context;
using ProjectBack.Entities;
using ProjectBackApiCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectBackApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        public AppDbContext Context { get; }

        public ProductoController(AppDbContext context)
        {
            Context = context;
        }

        // GET: api/<ProductoController>
        [HttpGet("GetProducto")]
        public IActionResult GetProducto()
        {
            ModelResponse<ProductoModel> Response = new ModelResponse<ProductoModel>();
            Response.CodigoRespuesta = HttpStatusCode.OK;
            Response.Mensaje = "Operacion exitosa.";
            try
            {
                var producto = Context.obtenerProductos.ToList().Where(p => p.Estado == true);
                //return producto;
                foreach (var item in producto)
                {
                    ProductoModel prodcutoResponse = new ProductoModel();

                    prodcutoResponse.Id_Producto = item.Id_Producto;
                    prodcutoResponse.Nombre = item.Nombre;
                    prodcutoResponse.Descripcion = item.Descripcion;
                    prodcutoResponse.Categoria = item.Categoria;
                    prodcutoResponse.Imagen_Url = item.Imagen_Url;
                    prodcutoResponse.Estado = item.Estado;
                    prodcutoResponse.Fc_reg = item.Fc_reg;

                    Response.Data.Add(prodcutoResponse);
                }
                return Ok(Response);
            }
            catch (Exception ex)
            {
                Response.CodigoRespuesta = HttpStatusCode.BadRequest;
                Response.Mensaje = ex.Message;
                return BadRequest(Response);

            }
        }

        // GET api/<ProductoController>/5
        //[HttpGet("{id}")]
        [HttpGet("GetProducto/{id}")]
        public IActionResult GetProductoId(int id)
        {
            ModelResponse<ProductoModel> Response = new ModelResponse<ProductoModel>();
            Response.CodigoRespuesta = HttpStatusCode.OK;
            Response.Mensaje = "Operacion exitosa.";
            try
            {
                var producto = Context.obtenerProductos.ToList().Where(p => p.Id_Producto == id);
                //return producto;
                foreach (var item in producto)
                {
                    ProductoModel prodcutoResponse = new ProductoModel();

                    prodcutoResponse.Id_Producto = item.Id_Producto;
                    prodcutoResponse.Nombre = item.Nombre;
                    prodcutoResponse.Descripcion = item.Descripcion;
                    prodcutoResponse.Categoria = item.Categoria;
                    prodcutoResponse.Imagen_Url = item.Imagen_Url;
                    prodcutoResponse.Estado = item.Estado;
                    prodcutoResponse.Fc_reg = item.Fc_reg;

                    Response.Data.Add(prodcutoResponse);
                }
                return Ok(Response);
            }
            catch (Exception ex)
            {
                Response.CodigoRespuesta = HttpStatusCode.BadRequest;
                Response.Mensaje = ex.Message;
                return BadRequest(Response);

            }
        }

        // POST api/<ProductoController>
        [HttpPost]
        public IActionResult Post([FromBody] ProductoModel productoData)
        {
            ModelResponse<ProductoModel> Response = new ModelResponse<ProductoModel>();
            Response.CodigoRespuesta = HttpStatusCode.OK;
            try
            {
                productoData.Estado = true;
                productoData.Fc_reg = DateTime.Now;

                if (productoData == null)
                {
                    Response.Mensaje = "Error en los datos.";
                    return BadRequest(Response);
                }
                if (!ModelState.IsValid)
                {
                    Response.Mensaje = "Error en los datos.";
                    return BadRequest(Response);
                }
                Context.registroProductos.Add(productoData);
                var res = Context.SaveChanges();
                Response.Mensaje = "Operacion exitosa se inserto el registro, cantidad insertada: " + res.ToString();

                return Ok(Response);
            }
            catch (Exception ex)
            {
                Response.CodigoRespuesta = HttpStatusCode.BadRequest;
                Response.Mensaje = ex.Message;
                return BadRequest(Response);
            }
        }

        // PUT api/<ProductoController>/5
        [HttpPut]
        public IActionResult Put([FromBody] ProductoModel productoData)
        {
            ModelResponse<ProductoModel> Response = new ModelResponse<ProductoModel>();
            Response.CodigoRespuesta = HttpStatusCode.OK;
            try
            {
                productoData.Fc_reg = DateTime.Now;

                Context.Entry(productoData).State = EntityState.Modified;
                var res = Context.SaveChanges();
                Response.Mensaje = "Operacion exitosa se modifico el registro, cantidad modificada: " + res.ToString();

                return Ok(Response);
            }
            catch (Exception ex)
            {
                Response.CodigoRespuesta = HttpStatusCode.BadRequest;
                Response.Mensaje = ex.Message;
                return BadRequest(Response);
            }
        }

        // DELETE api/<ProductoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ModelResponse<ProductoModel> Response = new ModelResponse<ProductoModel>();
            Response.CodigoRespuesta = HttpStatusCode.OK;
            try
            {
                var producto = Context.obtenerProductos.FirstOrDefault(p => p.Id_Producto == id);

                if (producto != null)
                {
                    Context.obtenerProductos.Remove(producto);
                    var res = Context.SaveChanges();
                    Response.Mensaje = "Operacion exitosa se elimino el registro, cantidad eliminada: " + res.ToString();
                }
                else
                {
                    Response.Mensaje = "No fue posible realizar la eliminacion del registro";
                    return BadRequest(Response);
                }


                return Ok(Response);
            }
            catch (Exception ex)
            {
                Response.CodigoRespuesta = HttpStatusCode.BadRequest;
                Response.Mensaje = ex.Message;
                return BadRequest(Response);
            }
        }
    }
}
