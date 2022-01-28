using Abp.Extensions;
using CotacaoAPI.Utility;
using Data;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CotacaoAPI.Controllers
{
    [ApiController]
    [Route("cotacao")]
    public class CotacaoController : Controller
    {
        private readonly CotacaoContext _cotacaoContext;

        public CotacaoController(CotacaoContext cotacaoContext) =>
            _cotacaoContext = cotacaoContext;

        #region ::. LISTAR COTAÇÕES .::

        [HttpGet]
        [Route("buscar")]
        public IActionResult ListarCotacao()
        {
            var cotacao = _cotacaoContext.Cotacao.ToList();

            foreach (var i in cotacao)
            {
                i.CotacaoItem = _cotacaoContext.CotacaoItem.Where(x => x.IdCotacao == i.IdCotacao).ToList();
            }

            return Ok(cotacao);
        }

        #endregion

        #region ::. BUSCAR COTAÇÃO POR ID .::

        [HttpGet]
        [Route("buscar/{id}")]
        public IActionResult BuscarCotacao(Guid Id)
        {
            var cotacao = _cotacaoContext.Cotacao.FirstOrDefault(x => x.IdCotacao == Id);

            cotacao.CotacaoItem = _cotacaoContext.CotacaoItem.Where(x => x.IdCotacao == cotacao.IdCotacao).ToList();

            if (cotacao != null)
                return Ok(cotacao);

            else
                return NotFound("Nenhuma cotação encontrada.");
        }

        #endregion

        #region ::. CADASTRAR NOVA COTAÇÃO .::

        [HttpPost]
        [Route("adicionar")]
        public IActionResult AdicionarCotacao([FromBody] Cotacao cotacao)
        {
            if (cotacao != null)
            {

                // BUSCANDO ENDEREÇO POR CEP
                if (cotacao.Logradouro.IsNullOrEmpty())
                {
                    var dados = new BuscarCEP(cotacao.CEP);
                    cotacao.Logradouro = dados.Lagradouro;
                    cotacao.Bairro = dados.Bairro;
                    cotacao.Cidade = dados.Cidade;
                    cotacao.UF = dados.UF;
                }

                // Validando CNPJ
                if (!ValidaCNPJ.IsCNPJ(cotacao.CNPJComprador)) return BadRequest("Digite um CNPJ do Comprador válido");
                if (!ValidaCNPJ.IsCNPJ(cotacao.CNPJFornecedor)) return BadRequest("Digite um CNPJ do Fornecedor válido");

                // ADICIONANDO COTAÇÃO NO BD
                cotacao.IdCotacao = Guid.NewGuid();
                cotacao.DataCotacao = DateTime.Now;
                _cotacaoContext.Cotacao.Add(cotacao);

                // ADICIONANDO OS ITENS DA COTAÇÃO NA TABELA COTAÇÃO ITEM
                foreach (var i in cotacao.CotacaoItem)
                {
                    i.IdCotacaoItem = Guid.NewGuid();
                    i.IdCotacao = cotacao.IdCotacao;
                    _cotacaoContext.CotacaoItem.Add(i);
                }

                _cotacaoContext.SaveChanges();

                return Ok(cotacao);
            }
            else
            {
                return BadRequest();
            }
        }

        #endregion

        #region ::. ALTERAR COTAÇÃO .::
        [HttpPut]
        [Route("alterar")]
        public IActionResult EditarCotacao([FromBody] Cotacao cotacao)
        {
            // VALIDAÇÃO SE A COTAÇÃO REALMENTE EXISTE.
            if (cotacao.IdCotacao == null) 
                return BadRequest("Informe o Id da cotação.");

            // BUSCANDO ENDEREÇO POR CEP
            if (cotacao.Logradouro.IsNullOrEmpty())
            {
                var dados = new BuscarCEP(cotacao.CEP);
                cotacao.Logradouro = dados.Lagradouro;
                cotacao.Bairro = dados.Bairro;
                cotacao.Cidade = dados.Cidade;
                cotacao.UF = dados.UF;
            }

            // RECUPERANDO A LISTA DE ITENS VINCULADAS AO ID DA COTAÇÃO PARA COMPARAR SE TEVE ITEM NOVO SENDO ADICIONADO, OU SE TEVE ITEM QUE FOI REMOVIDO.
            var itensCotacao = _cotacaoContext.CotacaoItem.Where(x => x.IdCotacao == cotacao.IdCotacao);

            // ADICIONANDO OS ITENS DA COTAÇÃO QUE FORAM INSERIDOS
            foreach (var i in cotacao.CotacaoItem)
            {
                if (i.IdCotacaoItem == Guid.Parse("00000000-0000-0000-0000-000000000000")) { 
                    i.IdCotacao = cotacao.IdCotacao;
                    _cotacaoContext.CotacaoItem.Add(i);
                }
            }

            // REMOVENDO ITENS QUE NÃO ESTÃO MAIS NA COTAÇÃO 
            foreach (var i in itensCotacao)
            {
                var t = cotacao.CotacaoItem.Where(x => x.IdCotacaoItem == i.IdCotacaoItem);
                if (t == null)
                    _cotacaoContext.CotacaoItem.Remove(i);
            }

            _cotacaoContext.Cotacao.Update(cotacao);
            _cotacaoContext.SaveChanges();

            return Ok(cotacao);
        }
        #endregion

        #region ::. EXCLUIR COTAÇÃO .::

        [HttpDelete]
        [Route("excluir/{id}")]
        public IActionResult Excluir (Guid id)
        {
            // BUSCANDO E VALIDANDO COTAÇÃO
            var cotacao = _cotacaoContext.Cotacao.Find(id);

            if (cotacao == null)
                return NotFound("Cotação não encontrada");

            // BUSCANDO E EXCLUINDO ITENS DA COTAÇÃO
            var ItensCotacao = _cotacaoContext.CotacaoItem.Where(x => x.IdCotacao == cotacao.IdCotacao);

            foreach (var i in ItensCotacao)
            {
                _cotacaoContext.CotacaoItem.Remove(i);
            }

            // SALVANDO A REMOÇÃO DOS ITENS DA COTAÇÃO PARA NÃO DAR CONFLITO COM A CHAVE ESTRANGEIRA QUANDO FOR EXCLUIR.
            _cotacaoContext.SaveChanges();

           
            _cotacaoContext.Cotacao.Remove(cotacao);
            _cotacaoContext.SaveChanges();

            return Ok("A cotação Número: " + cotacao.NumeroCotacao + " foi exclída com sucesso.");
        }
        #endregion
    }
}

