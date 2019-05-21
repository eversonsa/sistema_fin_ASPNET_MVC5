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
        // GET: Produto
        public ActionResult Index()
        {
            ProdutosDAO dao = new ProdutosDAO();
            IList<Produto> produtos = dao.Lista();
            ViewBag.Produtos = produtos;
            return View();
        }

        public ActionResult Form()
        {
            CategoriasDAO categorias = new CategoriasDAO();
            IList<CategoriaDoProduto> categoria = categorias.Lista();
            ViewBag.Categorias = categoria;
            return View();
        }

        [HttpPost]
        public ActionResult Adiciona(Produto produto)
        {
            int idCategoriaInformatica = 1;
            if (produto.CategoriaId.Equals(idCategoriaInformatica) && produto.Preco < 100)
            {
                ModelState.AddModelError("produto.precoInferior", "Produto invalido");
            }
            ProdutosDAO dao = new ProdutosDAO();
            if (ModelState.IsValid)
            {
                dao.Adiciona(produto);
                return RedirectToAction("Index");
            }
            else
            {
                ProdutosDAO produtos = new ProdutosDAO();
                CategoriasDAO categorias = new CategoriasDAO();
                ViewBag.Categorias = categorias.Lista();
                ViewBag.Produtos = produtos;
                return View("Form");
            }
        }
    }
}