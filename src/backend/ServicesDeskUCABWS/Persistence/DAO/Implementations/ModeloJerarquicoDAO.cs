using System.ComponentModel;
using System;
using System.Reflection;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.Persistence.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Collections.Generic;
using ServicesDeskUCABWS.Exceptions;


namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    public class ModeloJerarquicoDAO : IModeloJerarquicoDAO
    {
        private readonly IMigrationDbContext _context;
        private readonly IMapper mapper;

        public ModeloJerarquicoDAO( IMigrationDbContext context, IMapper map)
        {
            this._context = context;
            this.mapper = map;
        }
        public ModeloJerarquicoDTO AgregarModeloJerarquicoDAO(ModeloJerarquico modeloJerarquico)
        {
            try
            {
               _context.ModeloAprobacion.Add(modeloJerarquico);
               _context.DbContext.SaveChanges();

                var maper = ModeloJerarquicoMapper.EntityToDto(modeloJerarquico);
              return maper;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException!.Message);
                throw new ServicesDeskUcabWsException("Error al agregar el Modelo Jerarquico: " + ex.Message, ex);
            }

        }
        public List<ModeloJerarquicoDTO> ConsultarModeloJerarquicosDAO()
        {
            try
            {
                var data = _context.ModeloJerarquicos.Select(j => new ModeloJerarquicoDTO()
                                                            {
                                                                id = j.id,
                                                                Nombre = j.nombre,
                                                                CategoriaId = j.categoriaid,
                                                                orden = ModeloJerarquicoMapper.EntityToDtoList(j.Jeraruia!)
                                                            });
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw new ServicesDeskUcabWsException("Error al consultar los Modelos Jerarquicos" + ex.Message, ex);
            }
        }
        public ModeloJerarquicoDTO ObtenerModeloJerarquicoDAO(int id)
        {
            try
            {
                var data = _context.ModeloJerarquicos
                                   .Select(m => new ModeloJerarquicoDTO()
                                   {
                                    id = m.id,
                                    Nombre = m.nombre,
                                    CategoriaId = m.categoriaid,
                                    orden = ModeloJerarquicoMapper.EntityToDtoList(m.Jeraruia!)
                                   }).Where(m => m.id == id);

                return data.First();
            }
            catch (Exception ex)
            {
                throw new ServicesDeskUcabWsException("Error al obtener el Modelo Jerarquico", ex.InnerException!);
            }
        }
        public ModeloJerarquicoDTO ActualizarModeloJerarquicoDAO(ModeloJerarquico modeloJerarquico)
        {
            try
            { 

                _context.ModeloJerarquicos.Update(modeloJerarquico);
                _context.DbContext.SaveChanges();

                var data = _context.ModeloJerarquicos.Where(mj => mj.id == modeloJerarquico.id)
                                                     .Select(mj => new ModeloJerarquicoDTO()
                                                     {
                                                        id = mj.id,
                                                        Nombre = mj.nombre,
                                                        CategoriaId = mj.categoriaid,
                                                        orden = ModeloJerarquicoMapper.EntityToDtoList(mj.Jeraruia!)
                                                     });
                return data.First();
            }
            catch (Exception ex)
            {
                throw new ServicesDeskUcabWsException("Error al actualizar el Modelo Jerarquico, " + ex.Message, ex);
            }
        }

        public ModeloJerarquicoDTO EliminarModeloJerarquicoDAO(int id)
        {
            try
            {
                var data = _context.ModeloJerarquicoCargos
                                    .Where(jc => jc.modelojerarquicoid == id)
                                    .First();
                var modeloJerarquico = _context.ModeloJerarquicos
                                                .Where(mj => mj.id == id)
                                                .First();

                _context.DbContext.Remove(data);
                _context.DbContext.Remove(modeloJerarquico); 

                _context.DbContext.SaveChanges();
                
                return ModeloJerarquicoMapper.EntityToDto(modeloJerarquico);
            }
            catch (Exception ex)
            {
                throw new ServicesDeskUcabWsException(ex.Message, ex.InnerException!);
            }
        }
    }
}