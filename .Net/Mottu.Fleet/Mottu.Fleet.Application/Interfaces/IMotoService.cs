using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mottu.Fleet.Application.DTOs;

public interface IMotoService
{
    Task<IEnumerable<MotoDTO>> ObterTodos();
    Task<MotoDTO> ObterPorId(int id);
    Task Criar(CreateMotoDTO dto);
    Task Atualizar(int id, CreateMotoDTO dto);
    Task Deletar(int id);
}
