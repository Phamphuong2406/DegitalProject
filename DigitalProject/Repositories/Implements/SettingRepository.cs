using AutoMapper;
using DigitalProject.Common.Paging;
using DigitalProject.Entitys;
using DigitalProject.Models.Setting;
using DigitalProject.Repositories.Interface;

namespace DigitalProject.Repositories.Implements
{
    public class SettingRepository: ISettingRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;
        public SettingRepository(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<Setting> GetListSetting()
        {
            return _context.settings.ToList();
        }
        public void AddSetting(Setting model)
        {
            _context.settings.Add(model);
            _context.SaveChanges();
        }
      
        public Setting FindById(int id)
        {
            return _context.settings.FirstOrDefault(x => x.Id == id);

        }
        public void EditSetting(Setting model)
        {
            _context.settings.Update(model);
            _context.SaveChanges();
        }

        public void DeleteSetting(Setting model)
        {
            _context.settings.Remove(model);
            _context.SaveChanges();
        }

        public bool FindBykey(string keyName)
        {
            _context.settings.FirstOrDefault(x => x.Key == keyName);
            return true;
        }
    }
}
