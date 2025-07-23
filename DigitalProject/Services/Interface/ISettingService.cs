using DigitalProject.Entitys;
using DigitalProject.Models.Setting;

namespace DigitalProject.Services.Interface
{
    public interface ISettingService
    {

        List<SettingDTO> GetListSetting();
        SettingDTO FindBySettingId(int settingId);
        void CreateSetting(SettingDTO model);
       void EditSetting(SettingDTO dto, int settingId);
        void DeleteSetting(int settingId);

    }
}
