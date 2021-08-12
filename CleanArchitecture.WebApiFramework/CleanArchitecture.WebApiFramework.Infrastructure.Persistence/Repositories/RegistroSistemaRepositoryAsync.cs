using CleanArchitecture.WebApiFramework.Application.Interfaces.Repositories;
using CleanArchitecture.WebApiFramework.Domain.Entities;
using CleanArchitecture.WebApiFramework.Infrastructure.Persistence.Contexts;
using CleanArchitecture.WebApiFramework.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using LinqKit;
using EntityFramework.Exceptions.SqlServer;
using EntityFramework.Exceptions.Common;

namespace CleanArchitecture.WebApiFramework.Infrastructure.Persistence.Repositories
{
    class RegistroSistemaRepositoryAsync : GenericRepositoryAsync<ClaveRegistro>, IRegistroSistemaRepositoryAsync
    {
        private readonly DbSet<ClaveRegistro> _clavesRegistro;
        const string KEY_SEPARATOR = @"\";


        public RegistroSistemaRepositoryAsync(FrameworkDbContext dbContext) : base(dbContext)
        {
            _clavesRegistro = dbContext.Set<ClaveRegistro>();

        }

        public Task<bool> IsUniqueClaveAsync(ClaveRegistro claveRegistro)
        {
            
            return _clavesRegistro
                .AllAsync(p => (p.Clave1 != claveRegistro.Clave1) && (p.Clave2 != claveRegistro.Clave2) && (p.Clave3 != claveRegistro.Clave3) && (p.Clave4 != claveRegistro.Clave4) && (p.Clave5 != claveRegistro.Clave5));
        }


        public async Task<ClaveRegistro>CreateKeyAsync(ClaveRegistro clave)
        {
            
            {
                try
                {
                    return await this.AddAsync(clave);
                }
                catch (UniqueConstraintException e)
                {
                    throw e;
                }
              
            }
        }



        public async Task<ClaveRegistro> LoadAsync(string key)
        {
          
            ClaveRegistro ClaveLeida;
           
            try
            {


                var result = _clavesRegistro
                            .AsNoTracking()
                            .AsExpandable();


                var predicate = this.MakePredicate(null, key); 
            
                ClaveLeida = await result.Where(predicate).FirstOrDefaultAsync();

              }
            catch (Exception ex)
            {
               throw ex;
            }

            return ClaveLeida;
        }

      
        public async Task<List<ClaveRegistro>> GetKeyValuesAsync(IEnumerable<ClaveRegistro> clavesToRead)
        {

            List<ClaveRegistro> ClavesLeidas;

            try
            {

                var result = _clavesRegistro
                            .AsNoTracking()
                            .AsExpandable();

               var predicate = PredicateBuilder.New<ClaveRegistro>();

                foreach (var key in clavesToRead)
                {
                    predicate = this.MakePredicate(predicate, key.FullPath());
                }

                ClavesLeidas = await result.Where(predicate).ToListAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return  ClavesLeidas;
        }


   
        private ExpressionStarter<ClaveRegistro> MakePredicate(ExpressionStarter<ClaveRegistro> predicate, string key)
        {
            if (predicate == null)
            {
                predicate = PredicateBuilder.New<ClaveRegistro>();
            }

            string[] parameters = key.Split(KEY_SEPARATOR);
            int ix = parameters.GetLength(0);

            switch (ix)
            {
                case 1:

                    if (predicate.IsStarted)
                    {
                        predicate.Or(x => x.Clave1 == parameters[0]);
                    }
                    else
                    {
                        predicate = predicate.And(x => x.Clave1 == parameters[0]);
                    };

                    break;
                case 2:
                    if (predicate.IsStarted)
                    {
                        predicate.Or(x => x.Clave1 == parameters[0] && x.Clave2 == parameters[1]);
                    }
                    else
                    {
                        predicate = predicate.And(x => x.Clave1 == parameters[0]);
                        predicate = predicate.And(x => x.Clave2 == parameters[1]);
                    }
                    break;
                case 3:
                    if (predicate.IsStarted)
                    {
                        predicate.Or(x => x.Clave1 == parameters[0] && x.Clave2 == parameters[1] && x.Clave3 == parameters[2]);
                    }
                    else
                    {
                        predicate = predicate.And(x => x.Clave1 == parameters[0]);
                        predicate = predicate.And(x => x.Clave2 == parameters[1]);
                        predicate = predicate.And(x => x.Clave3 == parameters[2]);
                    };

                    break;
                case 4:

                    if (predicate.IsStarted)
                    {
                        predicate.Or(x => x.Clave1 == parameters[0] && x.Clave2 == parameters[1] && x.Clave3 == parameters[2] && x.Clave4 == parameters[3]);

                    }
                    else
                    {

                        predicate = predicate.And(x => x.Clave1 == parameters[0]);
                        predicate = predicate.And(x => x.Clave2 == parameters[1]);
                        predicate = predicate.And(x => x.Clave3 == parameters[2]);
                        predicate = predicate.And(x => x.Clave4 == parameters[3]);

                    };
                    break;
                case 5:


                    if (predicate.IsStarted)
                    {
                        predicate.Or(x => x.Clave1 == parameters[0] && x.Clave2 == parameters[1] && x.Clave3 == parameters[2] && x.Clave4 == parameters[3] && x.Clave5 == parameters[4]);
                    }
                    else
                    {
                        predicate = predicate.And(x => x.Clave1 == parameters[0]);
                        predicate = predicate.And(x => x.Clave2 == parameters[1]);
                        predicate = predicate.And(x => x.Clave3 == parameters[2]);
                        predicate = predicate.And(x => x.Clave4 == parameters[3]);
                        predicate = predicate.And(x => x.Clave5 == parameters[4]);
                    };

                    break;
            }


            return predicate;
        }


        public async Task UpdateGlobalAsync(IEnumerable<ClaveRegistro> clavesToUpdate)
        {

            List<ClaveRegistro> ClavesLeidas;

            try
            {


                var result = _clavesRegistro
                            .AsExpandable();

                var predicate = PredicateBuilder.New<ClaveRegistro>();

                foreach (var key in clavesToUpdate)
                {

                    predicate = this.MakePredicate(predicate, key.FullPath());

                }


                ClavesLeidas = await result
                                  .Where(predicate)
                                  .OrderBy(x => x.Clave1).ThenBy(x => x.Clave2).ThenBy(x => x.Clave3).ThenBy(x => x.Clave4).ThenBy(x => x.Clave5)
                                  .ToListAsync();

                foreach (var clave in clavesToUpdate)
                {

                  var claveEncontrada = ClavesLeidas.FirstOrDefault(x => x.Clave1 == clave.Clave1 && x.Clave2 == clave.Clave2 && x.Clave3 == clave.Clave3 && x.Clave4 == clave.Clave4 && x.Clave5 == clave.Clave5);
                    if (claveEncontrada == null)
                    {
                        // clave no encontrada -> la creo
                        await this.CreateKeyAsync(clave);
                    }
                    else
                    {
                        await this.UpdateClaveAsync(clave);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
                        
        }


        public async Task<List<ClaveRegistro>> GetKeysAsync(string key)
        {

            List<ClaveRegistro> ClavesLeidas = new List<ClaveRegistro>();
            
            try
            {

                string[] parameters = key.Split(KEY_SEPARATOR);
                int ix = parameters.GetLength(0);

               
                switch (ix)
                {
                    case 1:
                        ClavesLeidas =  await _clavesRegistro.Where(x => x.Clave1 == parameters[0]).ToListAsync();
                        break;
                    case 2:
                        ClavesLeidas = await _clavesRegistro.Where(x => x.Clave1 == parameters[0] && x.Clave2 == parameters[1]).ToListAsync();
                        break;
                    case 3:
                        ClavesLeidas = await _clavesRegistro.Where(x => x.Clave1 == parameters[0] && x.Clave2 == parameters[1] && x.Clave3 == parameters[2]).ToListAsync();
                        break;
                    case 4:
                        ClavesLeidas = await _clavesRegistro.Where(x => x.Clave1 == parameters[0] && x.Clave2 == parameters[1] && x.Clave3 == parameters[2] && x.Clave4 == parameters[3]).ToListAsync();
                        break;
                    case 5:
                        ClavesLeidas = await _clavesRegistro.Where(x => x.Clave1 == parameters[0] && x.Clave2 == parameters[1] && x.Clave3 == parameters[2] && x.Clave4 == parameters[3] && x.Clave5 == parameters[4]).ToListAsync();
                        break;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ClavesLeidas;
        }


        public async Task<List<ClaveRegistro>> GetParametersAsync(string key)
        {

            List<ClaveRegistro> ClavesLeidas = new List<ClaveRegistro>();

            try
            {

                string[] parameters = key.Split(KEY_SEPARATOR);
                int ix = parameters.GetLength(0);


                switch (ix)
                {
                    case 1:
                        ClavesLeidas = await _clavesRegistro.Where(x => x.Clave1 == parameters[0] && x.Clave3 == null && x.Valor != "(NuevoValor)").ToListAsync();
                        break;
                    case 2:
                        ClavesLeidas = await _clavesRegistro.Where(x => x.Clave1 == parameters[0] && x.Clave2 == parameters[1] && x.Clave4 == null && x.Valor != "(NuevoValor)").ToListAsync();
                        break;
                    case 3:
                        ClavesLeidas = await _clavesRegistro.Where(x => x.Clave1 == parameters[0] && x.Clave2 == parameters[1] && x.Clave3 == parameters[2] && x.Clave5 == null && x.Valor != "(NuevoValor)").ToListAsync();
                        break;
                    case 4:
                        ClavesLeidas = await _clavesRegistro.Where(x => x.Clave1 == parameters[0] && x.Clave2 == parameters[1] && x.Clave3 == parameters[2] && x.Clave4 == parameters[3] && x.Valor != "(NuevoValor)").ToListAsync();
                        break;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ClavesLeidas;
        }


        public async Task<List<ClaveRegistro>> GetKeyEnumAsync(string key)
        {

            List<ClaveRegistro> Lista = new List<ClaveRegistro>();

            try
            {

                string[] parameters = key.Split(KEY_SEPARATOR);
                int ix = parameters.GetLength(0);

             

                switch (ix)
                {
                    case 1:

                        {
                            var query = await _clavesRegistro
                                        .Where(x => x.Clave1 == parameters[0])
                                        .Select(t => new { t.Clave1, t.Clave2 }).Distinct().ToListAsync();

                            foreach (var item in query)
                            {
                                Lista.Add(new ClaveRegistro { Clave1 = item.Clave1, Clave2 = item.Clave2 });
                            }

                        }
                       
                        break;
                    case 2:
                        {

                            var query = await _clavesRegistro
                                      .Where(x => x.Clave1 == parameters[0] && x.Clave2 == parameters[1])
                                      .Select(t => new { t.Clave1, t.Clave2, t.Clave3 }).Distinct().ToListAsync();

                            foreach (var item in query)
                            {
                                Lista.Add(new ClaveRegistro { Clave1 = item.Clave1, Clave2 = item.Clave2, Clave3 = item.Clave3 });
                            }


                        }

                        break;
                    case 3:

                        {
                            var query = await _clavesRegistro
                                      .Where(x => x.Clave1 == parameters[0] && x.Clave2 == parameters[1] && x.Clave3 == parameters[2])
                                      .Select(t => new { t.Clave1, t.Clave2, t.Clave3, t.Clave4 }).Distinct().ToListAsync();

                            foreach (var item in query)
                            {
                                Lista.Add(new ClaveRegistro { Clave1 = item.Clave1, Clave2 = item.Clave2, Clave3 = item.Clave3, Clave4 = item.Clave4 });
                            }

                        }

                        break;
                    case 4:

                        {
                            var query = await _clavesRegistro
                                      .Where(x => x.Clave1 == parameters[0] && x.Clave2 == parameters[1] && x.Clave3 == parameters[2])
                                      .Select(t => new { t.Clave1, t.Clave2, t.Clave3, t.Clave4, t.Clave5 }).Distinct().ToListAsync();

                            foreach (var item in query)
                            {
                                Lista.Add(new ClaveRegistro { Clave1 = item.Clave1, Clave2 = item.Clave2, Clave3 = item.Clave3, Clave4 = item.Clave4, Clave5 = item.Clave5 });
                            }

                        }

                        break;

                        
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Lista;
        }


        public async Task<ClaveRegistro> UpdateClaveAsync(ClaveRegistro clave)
        {
           
            try
            {

                // 
                //    existe -> la actualizo
                // 
                await this.UpdateAsync(clave);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return clave;
        }


    }
}
