namespace Services.Interfaces
{
    public interface IRepository<TDto>
    {
        TDto Add(TDto entityDto);
        TDto[] GetAll();
        TDto GetOne(int id);
        TDto Update(TDto entityDto);
        void Delete(int id);
    }
}