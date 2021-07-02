using desafioTDSA.Action;
using desafioTDSA.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace desafioTDSA.Controllers
{
    [RoutePrefix("api/Usuario")]
    public class UsuarioController : ApiController
    {
        [HttpGet]
        [Route("Select")]
        public IHttpActionResult SelectUsuario()
        {
            UsuarioAction usuarioAction = new UsuarioAction();
            List<UsuarioModel> usuarioModel = usuarioAction.Select();

            return Json(usuarioModel);
        }

        [HttpPost]
        [Route("Insert")]
        public IHttpActionResult InsertUsuario(UsuarioModel Usuario)
        {
            UsuarioAction usuarioAction = new UsuarioAction();
            return Json(usuarioAction.Insert(Usuario.CLI_NOME, Usuario.CLI_DATANASCIMENTO, Usuario.CLI_ATIVO == "Ativo" ? 1 : 0));
        }

        [HttpPost]
        [Route("Update")]
        public IHttpActionResult UpdateUsuario(UsuarioModel Usuario)
        {
            UsuarioAction usuarioAction = new UsuarioAction();
            return Json(usuarioAction.Update(Usuario.CLI_ID, Usuario.CLI_NOME, Usuario.CLI_DATANASCIMENTO, Usuario.CLI_ATIVO == "Ativo" ? 1 : 0));
        }

        [HttpPost]
        [Route("Delete")]
        public IHttpActionResult DeleteUsuario(UsuarioModel Usuario)
        {
            UsuarioAction usuarioAction = new UsuarioAction();
            return Json(usuarioAction.Delete(Usuario.CLI_ID));
        }
    }


}
