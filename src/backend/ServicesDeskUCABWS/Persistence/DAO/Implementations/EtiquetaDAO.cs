using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.Persistence.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Collections.Generic;


namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    public class EtiquetaDAO : IEtiquetaDAO
    {
        private readonly IMigrationDbContext _context;

        private readonly IMapper _mapper;
        public EtiquetaDAO(IMapper mapper, IMigrationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }



        public async Task<ActionResult<EtiquetaDTO>> AgregarEtiquetaDAO(Etiqueta etiqueta)
        {

            _context.Etiquetas.Add(etiqueta);
            await _context.DbContext.SaveChangesAsync();

            return _mapper.Map<EtiquetaDTO>(etiqueta);

        }

        public Task<List<Etiqueta>> ConsultarEtiquetasDAO()
        {

            return _context.Etiquetas.ToListAsync();

        }

        public async Task<ActionResult<Etiqueta>> ObtenerEtiquetaDAO(int id)
        {
            try
            {
                var etiqueta = await _context.Etiquetas.FindAsync(id);
                if (etiqueta == null)
                {
                    return new Etiqueta();
                }
                return etiqueta;
            }
            catch (DbUpdateException ex)
            {
                throw ex.InnerException!;
            }
        }

        public async Task<ActionResult<Etiqueta>> ActualizarEtiquetaDAO(Etiqueta etiqueta, int id)
        {

            try
            {
                var etiquetaOld = await ObtenerEtiquetaDAO(id);
                if (etiquetaOld.Value == null)
                {
                    return new Etiqueta();
                }

                etiquetaOld.Value.nombre = etiqueta.nombre;
                etiquetaOld.Value.descripcion = etiqueta.descripcion;

                await _context.DbContext.SaveChangesAsync();
                return etiquetaOld;
            }
            catch (DbUpdateException ex)
            {

                throw ex.InnerException!;
            }


        }

        public async Task<ActionResult> EliminarEtiquetaDAO(int id)
        {

            var existe = await ObtenerEtiquetaDAO(id);
            if (existe.Value?.id == 0)
            {
                return new NotFoundResult();
            }


            _context.Etiquetas.Remove(existe.Value!);
            await _context.DbContext.SaveChangesAsync();

            return new OkResult();


        }
    }
}