﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using senai.inlock.webApi.Repositories;
using System.Data;

namespace senai.inlock.webApi.Controllers
{
    //Define que a rota de uma requisição será no seguinte formato
    //domino/api/Nomecontroller
    //ex: http://localhost:5000/api/genero
    [Route("api/[controller]")]

    //Define que é  um controlador de API
    [ApiController]

    //Define que o tipo de resposta da API sera formato JSON
    [Produces("application/json")]
    public class JogoController : ControllerBase
    {
        private IJogoRepository _jogoRepository { get; set; }

        public JogoController()
        {
            _jogoRepository = new JogoRepository();
        }

        /// <summary>
        /// Endpoint que aciona o  metodo ListarTodos no repositorio
        /// </summary>
        /// <returns>retorna a resposta para o usuario(front-end)</returns>
        [HttpGet]
        [Authorize(Roles = "1,2")]
        public IActionResult Get()
        {
            try
            {
                //Cria uma lista que recebe os dados da requisicao
                List<JogoDomain> listaJogos = _jogoRepository.ListarTodos();

                //Retorna a lista no formato JSON com o status code Ok(200)
                return Ok(listaJogos);

            }
            catch (Exception erro)
            {
                //Retorna um status code BadRequest(400) e a mensagem erro
                return BadRequest(erro.Message);
            }
        }
        /// <summary>
        /// Endpoint que aciona o metodo de cadastro de jogo
        /// </summary>
        /// <param name="novoJogo">Objeto recebido na requisição</param>
        /// <returns>status code 201(created)</returns>
        [HttpPost]
        [Authorize(Roles = "2")]
        public IActionResult Post(JogoDomain novoJogo)
        {
            try
            {

                //Fazendo a chamada para o metodo cadastrar passando o objeto novoJogo como parametro
                _jogoRepository.Cadastrar(novoJogo);

                //Retorna o Status Code 201
                return StatusCode(201);

            }
            catch (Exception erro)
            {
                //Retorna o Staus Code 400(BadRequest) e a mensagem do erro
                return BadRequest(erro.Message);
            }

        }
        /// <summary>
        /// Endpoint que aciona o metodo de deletar de Estudio
        /// </summary>
        /// <param name="id">id do objeto recebido</param>
        /// <returns>code 201(deleted)</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "2")]
        public IActionResult Delete(int id)
        {
            try
            {
                _jogoRepository.Deletar(id);

                return StatusCode(201);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}
