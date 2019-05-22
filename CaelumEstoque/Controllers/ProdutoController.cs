using CaelumEstoque.DAO;
using CaelumEstoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaelumEstoque.Controllers
{
    public class ProdutoController : Controller
    {
        
        public ActionResult Index()
        {
            ProdutosDAO dao = new ProdutosDAO();
            IList<Produto> produtos = dao.Lista();
            return View(produtos);
        }

        public ActionResult Form()
        {
            CategoriasDAO categorias = new CategoriasDAO();
            IList<CategoriaDoProduto> categoria = categorias.Lista();
            ViewBag.Categorias = categoria;
            ViewBag.Produto = new Produto();
            return View();
        }

        [HttpPost]
        public ActionResult Adiciona(Produto produto)
        {
            ProdutosDAO dao = new ProdutosDAO();
            int idCategoriaInformatica = 1;
            if (produto.CategoriaId.Equals(idCategoriaInformatica) && produto.Preco < 100)
            {
                ModelState.AddModelError("produto.precoInferior", "Produto invalido");
            }
            
            if (ModelState.IsValid)
            {
                dao.Adiciona(produto);
                return RedirectToAction("Index");
            }
            else
            {
                CategoriasDAO categorias = new CategoriasDAO();
                ViewBag.Categorias = categorias.Lista();
                ViewBag.Produto = produto;
                return View("Form");
            }
        }

        public ActionResult Visualiza(int id)
        {
            ProdutosDAO produto = new ProdutosDAO();
            ViewBag.Produto = produto.BuscaPorId(id);
            return View();
        }
    }
}