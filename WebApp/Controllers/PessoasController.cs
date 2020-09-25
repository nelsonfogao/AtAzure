using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.EntityFrameworkCore;
using WebApiPessoas.Context;
using WebApiPessoas.Models;
using WebApp.ApiServices;
using WebApp.Models;
using WebApp.Models.Pessoas;

namespace WebApp.Controllers
{
    public class PessoasController : Controller
    {
        private readonly WebApiPessoaContext _context;
        private readonly IPessoaApi _pessoaApi;
        private readonly IEstadoApi _estadoApi;
        private readonly IPaisApi _paisApi;

        public PessoasController(WebApiPessoaContext context, IPaisApi paisApi, IEstadoApi estadoApi, IPessoaApi pessoaApi)
        {
            _context = context;
            _estadoApi = estadoApi;
            _paisApi = paisApi;
            _pessoaApi = pessoaApi;
        }

        // GET: Pessoas
        public async Task<IActionResult> Index()
        {
            var viewModel = await _pessoaApi.GetPessoas();

            return View(viewModel);
        }

        // GET: Pessoas/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await _pessoaApi.GetPessoa(id);

            return View(viewModel);
        }

        // GET: Pessoas/Create
        public async Task<ActionResult> Create()
        {
            var viewModel = new CriarPessoaViewModel();

            viewModel.Paises = await _paisApi.GetPaises();

            viewModel.Estados = await _estadoApi.GetEstados();

            return View(viewModel);
        }

        // POST: Pessoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CriarPessoaViewModel pessoaViewModel)
        {
            var foto = UploadFotoPessoa(pessoaViewModel.ImgFoto);

            pessoaViewModel.Foto = foto;

            await _pessoaApi.PostPessoaAsync(pessoaViewModel);
            try
            {

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Pessoas/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var viewModel = await _pessoaApi.GetPessoa(id);
            return View(viewModel);
        }

        // POST: Pessoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Sobrenome,Email,Telefone,DataDeNascimento,Foto,EstadoId,PaisId")] Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _context.Update(pessoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.Id))
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
            return View(pessoa);
        }

        // GET: Pessoas/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var viewModel = await _pessoaApi.GetPessoa(id);
            return View(viewModel);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var pessoa = await _context.Pessoa.FindAsync(id);
                _context.Pessoa.Remove(pessoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Amigos(int id)
        {
            var viewModel = new ListarAmigosViewModel();
            viewModel.Amigos = await _pessoaApi.GetAmigos(id);

            var pessoas = await _pessoaApi.GetPessoas();
            viewModel.Pessoas = pessoas;
            viewModel.Pessoa = pessoas.First(x => x.Id == id);

            pessoas.Remove(viewModel.Pessoa);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Amigos([FromForm] CadastrarAmigoViewModel viewModel)
        {
            await _pessoaApi.PostAmigos(viewModel.PessoaId, viewModel.Ids);

            return RedirectToAction(nameof(Amigos), new {id= viewModel.PessoaId});
        }



        private string UploadFotoPessoa(IFormFile foto)
        {
            var reader = foto.OpenReadStream();
            var cloudStorageAccount = CloudStorageAccount.Parse(@"BlobEndpoint=https://atnelson.blob.core.windows.net/;QueueEndpoint=https://atnelson.queue.core.windows.net/;FileEndpoint=https://atnelson.file.core.windows.net/;TableEndpoint=https://atnelson.table.core.windows.net/;SharedAccessSignature=sv=2019-12-12&ss=bfqt&srt=sco&sp=rwdlacupx&se=2020-09-25T07:01:05Z&st=2020-09-24T23:01:05Z&spr=https,http&sig=tDtPlxQRFgZOWBWjly3qUtYM1QjNfNQlJEMOX5nwruw%3D");
            var blobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("fotos-pessoas");
            container.CreateIfNotExists();
            var blob = container.GetBlockBlobReference(Guid.NewGuid().ToString());
            blob.UploadFromStream(reader);
            var destinoDaImagemNaNuvem = blob.Uri.ToString();

            return destinoDaImagemNaNuvem;
        }
        private bool PessoaExists(int id)
        {
            return _context.Pessoa.Any(e => e.Id == id);
        }
    }

}
