using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.EntityFrameworkCore;
using WebApiPaises.Context;
using WebApiPaises.Models;
using WebApp.ApiServices;
using WebApp.Models.Paises;

namespace WebApp.Controllers
{
    public class EstadoController : Controller
    {
        private readonly IEstadoApi _estadoApi;
        private readonly IPaisApi _paisApi;
        private readonly WebApiPaisesContext _context;

        public EstadoController(IEstadoApi estadoApi, IPaisApi paisApi,WebApiPaisesContext context)
        {
            this._estadoApi = estadoApi;
            this._paisApi = paisApi;
            _context = context;
        }

        // GET: EstadoController
        public async Task<ActionResult> Index()
        {
            var viewModel = await _estadoApi.GetEstados();

            return View(viewModel);
        }

        // GET: EstadoController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var viewModel = await _estadoApi.GetEstado(id);

            return View(viewModel);
        }

        // GET: EstadoController/Create
        public async Task<ActionResult> Create()
        {
            var viewModel = new CriarEstadoViewModel();

            viewModel.Paises = await _paisApi.GetPaises();

            return View(viewModel);
        }

        // POST: EstadoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CriarEstadoViewModel estadoViewModel)
        {
            var foto = UploadFotoEstado(estadoViewModel.ImgFoto);

            estadoViewModel.Foto = foto;

            await _estadoApi.PostEstadoAsync(estadoViewModel);
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EstadoController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var viewModel = await _estadoApi.GetEstado(id);
            return View(viewModel);
        }

        // POST: EstadoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Foto,PaisId")] Estado estado)
        {
            if (id != estado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _context.Update(estado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadoExists(estado.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(estado);
        }

        // GET: EstadoController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var viewModel = await _estadoApi.GetEstado(id);
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var estado = await _context.Estados.FindAsync(id);
                _context.Estados.Remove(estado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {
                throw;
            }
        }
        private string UploadFotoEstado(IFormFile foto)
        {
            var reader = foto.OpenReadStream();
            var cloudStorageAccount = CloudStorageAccount.Parse(@"BlobEndpoint=https://atnelson.blob.core.windows.net/;QueueEndpoint=https://atnelson.queue.core.windows.net/;FileEndpoint=https://atnelson.file.core.windows.net/;TableEndpoint=https://atnelson.table.core.windows.net/;SharedAccessSignature=sv=2019-12-12&ss=bfqt&srt=sco&sp=rwdlacupx&se=2020-09-25T07:01:05Z&st=2020-09-24T23:01:05Z&spr=https,http&sig=tDtPlxQRFgZOWBWjly3qUtYM1QjNfNQlJEMOX5nwruw%3D");
            var blobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("fotos-estados");
            container.CreateIfNotExists();
            var blob = container.GetBlockBlobReference(Guid.NewGuid().ToString());
            blob.UploadFromStream(reader);
            var destinoDaImagemNaNuvem = blob.Uri.ToString();

            return destinoDaImagemNaNuvem;
        }

        private bool EstadoExists(int id)
        {
            return _context.Estados.Any(e => e.Id == id);
        }

    }
}
