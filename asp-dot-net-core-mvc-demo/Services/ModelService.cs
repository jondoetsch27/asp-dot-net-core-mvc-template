using System;
using System.Threading.Tasks;
using asp.net.core.mvc.demo.Exceptions;
using asp.net.core.mvc.demo.Models;

namespace asp.net.core.mvc.demo.Services
{

    public class ModelService
    {
        private readonly ModelDataContext ModelDB;

        public ModelService() { }

        public ModelService(ModelDataContext ModelDataContext)
        {
            ModelDB = ModelDataContext;
        }

        public virtual async Task<MLModel> createModel(MLModel Model)
        {
            if (ModelDB.MLModels.Find(Model.modelName) != null)
            {
                throw new DuplicateModelException();
            }

            await ModelDB.MLModels.AddAsync(Model);
            await ModelDB.SaveChangesAsync();
            return Model;
        }

        public virtual async Task<MLModel> readModel(string ID)
        {
            MLModel Model = await ModelDB.MLModels.FindAsync(ID);

            if (Model == null)
            {
                throw new ModelNotFoundException();
            }

            return Model;
        }

        public virtual async Task<MLModel> updateModel(MLModel newModel)
        {
            MLModel oldModel = await ModelDB.MLModels.FindAsync(newModel.modelName);

            if (oldModel == null)
            {
                throw new ModelNotFoundException();
            }

            ModelDB.MLModels.Remove(oldModel);
            await ModelDB.MLModels.AddAsync(newModel);
            await ModelDB.SaveChangesAsync();
            return newModel;
        }

        public virtual async Task<MLModel> deleteModel(string ID)
        {
            MLModel Model = await ModelDB.MLModels.FindAsync(ID);

            if (Model == null)
            {
                throw new ModelNotFoundException();
            }

            ModelDB.MLModels.Remove(Model);
            await ModelDB.SaveChangesAsync();
            return Model;
        }
    }
}
