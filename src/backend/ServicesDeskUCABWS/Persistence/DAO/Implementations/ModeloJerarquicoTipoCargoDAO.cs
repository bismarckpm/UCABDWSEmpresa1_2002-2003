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
    public class ModeloJerarquicoTipoCargoDAO : IModeloJerarquicoTipoCargo
    {
        private readonly IMigrationDbContext _context;

        public ModeloJerarquicoTipoCargoDAO( IMigrationDbContext context)
        {
            _context= context;
        }

        public JerarquicoTipoCargoDTO ActualizarJerarquicoTipoCargoDAO(ModeloJerarquicoCargos entity)
        {
            try
            {
                _context.ModeloJerarquicoCargos.Update(entity);
                _context.DbContext.SaveChanges();

                return JerarquicoTipoCargoMapper.EntityToDTO(entity);

            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public JerarquicoTipoCargoDTO CreateJerarquicoTipoCargoDAO(ModeloJerarquicoCargos jc)
        {
            try
            {
                _context.ModeloJerarquicoCargos.Add(jc);
                _context.DbContext.SaveChanges();

                return JerarquicoTipoCargoMapper.EntityToDTO(jc);

            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public JerarquicoTipoCargoDTO EliminarJerarquicoTipoCargoDAO(int id)
        {
            try
            {
                var data = _context.ModeloJerarquicoCargos
                                .Where( jc => jc.Id == id)
                                .First();

                    _context.ModeloJerarquicoCargos.Remove(data);
                    _context.DbContext.SaveChanges();

                    return JerarquicoTipoCargoMapper.EntityToDTO(data);

            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public List<JerarquicoTipoCargoDTO> ListadoJerarquicoTipoCargoDAO()
        {
            try
            {
                var data = _context.ModeloJerarquicoCargos.Select(jc => new JerarquicoTipoCargoDTO()
                {
                    Id = jc.Id,
                    tipoCargoid = jc.TipoCargoid,
                    modelojerarquicoid = jc.modelojerarquicoid,
                    orden = jc.orden
                }).ToList();

                return data;

            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public JerarquicoTipoCargoDTO ObtenerJerarquicoTipoCargoDAO(int id)
        {
           try
           {
                var data = _context.ModeloJerarquicoCargos
                .Where(jc => jc.Id == id)
                .Select(jc => new JerarquicoTipoCargoDTO()
                    {
                        Id = jc.Id,
                        tipoCargoid = jc.TipoCargoid,
                        modelojerarquicoid = jc.modelojerarquicoid,
                        orden = jc.orden
                    }).First();

               return data;     
           }
           catch (Exception ex)
           {
            
            throw new Exception(ex.Message, ex);
           }
        }
    }
}