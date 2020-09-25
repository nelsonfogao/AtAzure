using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.EntityFrameworkCore;
using WebApiPaises.Context;
using WebApiPaises.Models;
using WebApp.ApiServices;
using WebApp.Models.Paises;

namespace WebApp.Controllers
{
    public class PaisesController : Controller
    {
        private readonly IPaisApi _paisApi;
        private readonly WebApiPaisesContext _context;

        public PaisesController(WebApiPaisesContext context,IPaisApi paisApi)
        {
            _paisApi = paisApi;
            _context = context;
        }

        // GET: Paises
        public async Task<IActionResult> Index()
        {
            var pais = await _paisApi.GetPaises();

            return View(pais);
        }

        // GET: Paises/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var viewModel = await _paisApi.GetPais(id);

            return View(viewModel);
        }

        // GET: Paises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Paises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CriarPaisViewModel criarpaisViewModel)
        {
            var foto = UploadFotoPais(criarpaisViewModel.ImgFoto);

            criarpaisViewModel.Foto = foto;

            await _paisApi.PostAsync(criarpaisViewModel);
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Paises/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var viewModel = await _paisApi.GetPais(id);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Foto")] Pais pais)
        {
            if (id != pais.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pais);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaisExists(pais.Id))
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
            return View(pais);
        }

        // GET: Paises/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var viewModel = await _paisApi.GetPais(id);
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var pais = await _context.Pais.FindAsync(id);
                _context.Pais.Remove(pais);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {
                throw;
            }

        }
        private string UploadFotoPais(IFormFile foto)
        {
            var reader = foto.OpenReadStream();
            var cloudStorageAccount = CloudStorageAccount.Parse(@"DefaultEndpointsProtocol=https;AccountName=atnelson;AccountKey=0r7JESt9kWCq4GawzCn5GuzCSHppEnoq3arb/Kp0v+Eo5vM94Slsc74L5JNnQBNQnpEuaLyN2Z0SwZl9VPoPzg==;EndpointSuffix=core.windows.net");
            var blobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("fotos-paises");
            container.CreateIfNotExists();
            var blob = container.GetBlockBlobReference(Guid.NewGuid().ToString());
            blob.UploadFromStream(reader);
            var destinoDaImagemNaNuvem = blob.Uri.ToString();

            return destinoDaImagemNaNuvem;
        }
        private bool PaisExists(int id)
        {
            return _context.Pais.Any(e => e.Id == id);
        }
    }
}
