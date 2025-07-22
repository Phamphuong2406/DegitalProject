using DigitalProject.Entitys;

namespace DigitalProject.Repositories.Interface
{
    public interface IContactRequestRepository
    {
        List<Gallery> getListGallery();
        void AddGallery(Gallery model);
    }
}
