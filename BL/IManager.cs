namespace BL;

public interface IManager<TModel, TCreateModel>
{
    TModel Create(TCreateModel model);
    TModel Update(Guid id, TModel model);
    void Delete(Guid id);
}
