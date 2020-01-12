namespace Services.Interfaces
{
    public interface IEntityService<TDto, TDao>
    {
        TDto Add(TDto entityDto);
        TDto[] GetAll();
        TDto GetOne(int id);
        TDto Update(TDto entityDto);
        void Delete(int id);
    }
}