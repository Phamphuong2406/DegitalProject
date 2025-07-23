using AutoMapper;
using DigitalProject.Entitys;

namespace DigitalProject.Repositories.Interface
{
    public interface ISettingRepository
    {
        List<Setting> GetListSetting();
        void AddSetting(Setting model);
        Setting FindById(int id);
        bool FindBykey(string keyName);
        void EditSetting(Setting model);
        void DeleteSetting(Setting model);
    }
}
