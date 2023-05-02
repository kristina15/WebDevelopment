using AutoMapper;
using SpaceApp.CreditApprovalSystem.Entities;
using SpaceApp.CreditApprovalSystem.Entities.Enum;
using SpaceApp.CreditApprovalSystem.HashGenerator;
using SpaceApp.CreditApprovalSystem.HashGeneratorContracts;
using SpaceApp.CreditApprovalSystem.Web.Models;
using SpaceApp.CreditApprovalSystem.Web.Models.Contants;
using SpaceApp.CreditApprovalSystem.Web.Models.LoanVMs;
using SpaceApp.CreditApprovalSystem.Web.Models.PassportVMs;
using SpaceApp.CreditApprovalSystem.Web.Models.ScanVMs;
using SpaceApp.CreditApprovalSystem.Web.Models.UserVMs;

namespace SpaceApp.CreditApprovalSystem.Web.AppStart;

public class AutoMapperConfig
{
    public static IMapper mapper;
    public static IHashing _hashing = new Hashing();

    public static void RegisterMaps()
    {
        var config = new MapperConfiguration(
            cfg =>
            {
                GetDomainModel(ref cfg);

                GetViewModel(ref cfg);

                cfg.CreateMap<IFormFile, EditScanVM>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(d =>
                   GetPassportScan(d)));

                cfg.CreateMap<CreateLoanVM, DisplayLoanVM>()
                .ForMember(dest => dest.DateCreate, opt => opt.MapFrom(d => DateTime.Now))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(d => LoanStatuses.InWaiting));

                cfg.CreateMap<DisplayLoanVM, CreateLoanVM>();
            });

        mapper = config.CreateMapper();
    }

    private static byte[] GetPassportScan(IFormFile item)
    {
        var scan = new byte[item.Length];
        item.OpenReadStream().Read(scan, 0, (int)item.Length);

        return scan;
    }

    private static void GetDomainModel(ref IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<PasswordRecoveryVM, User>()
        .ForMember(dest => dest.HashPassword, opt => opt.MapFrom(d => _hashing.GetHash(d.Password)))
        .ForMember(dest => dest.Passport, opt => opt.MapFrom(d => mapper.Map<Passport>(d.Passport)));

        cfg.CreateMap<EditUserVM, User>()
        .ForMember(dest => dest.AdditionalFile, opt => opt.MapFrom(d => mapper.Map<ScanFile>(d.AdditionalFile)))
        .ForMember(dest => dest.Passport, opt => opt.MapFrom(d => mapper.Map<Passport>(d.Passport)));

        cfg.CreateMap<CreateLoanVM, Loan>()
         .ForMember(dest => dest.DateCreate, opt => opt.MapFrom(d => DateTime.Now))
         .ForMember(dest => dest.Status, opt => opt.MapFrom(d => StatusTypesEnum.InWaiting));

        cfg.CreateMap<DisplayLoanVM, Loan>()
        .ForMember(dest => dest.Status, opt => opt.MapFrom(d => d.Status == LoanStatuses.Approved ? 
                        StatusTypesEnum.Approved : d.Status == LoanStatuses.Denied ?
                        StatusTypesEnum.Denied : StatusTypesEnum.InWaiting));

        cfg.CreateMap<LoginVM, User>()
        .ForMember(dest => dest.HashPassword, opt => opt.MapFrom(order => _hashing.GetHash(order.Password)));

        cfg.CreateMap<RegisterVM, User>()
        .ForMember(dest => dest.HashPassword, opt => opt.MapFrom(order => _hashing.GetHash(order.Password)))
        .ForMember(dest => dest.Passport, opt => opt.MapFrom(user => mapper.Map<CreatePassportVM, Passport>(user.Passport)));

        cfg.CreateMap<DisplayUserVM, User>()
        .ForMember(dest => dest.Passport, opt => opt.MapFrom(d => mapper.Map<Passport>(d.Passport)))
        .ForMember(dest => dest.AdditionalFile, opt => opt.MapFrom(d => mapper.Map<ScanFile>(d.AdditionalFile)));

        cfg.CreateMap<DisplayPassportVM, Passport>()
        .ForMember(dest => dest.Scans, opt => opt.MapFrom(d => mapper.Map<List<ScanFile>>(d.Scans)));

        cfg.CreateMap<EditPassportVM, Passport>()
        .ForMember(dest => dest.Scans, opt => opt.MapFrom(d => mapper.Map<List<ScanFile>>(d.Scans)));

        cfg.CreateMap<CreateScanVM, ScanFile>()
        .ForMember(dest => dest.Title, opt => opt.MapFrom(d => d.Type == ScanTypes.Passport ? TitleTypesEnum.Passport : d.Type == ScanTypes.Snils ? TitleTypesEnum.SNILS : TitleTypesEnum.DriversLicense))
        .ForMember(dest => dest.Link, opt => opt.MapFrom(d => d.Image));

        cfg.CreateMap<CreatePassportVM, Passport>();

        cfg.CreateMap<EditScanVM, ScanFile>()
        .ForMember(dest => dest.Link, opt => opt.MapFrom(d => d.Image))
        .ForMember(dest => dest.Title, opt => opt.MapFrom(d => d.Type == ScanTypes.Passport ? TitleTypesEnum.Passport : d.Type == ScanTypes.Snils ? TitleTypesEnum.SNILS : TitleTypesEnum.DriversLicense));

        cfg.CreateMap<EditScanVM, ScanFile>()
        .ForMember(dest => dest.Link, opt => opt.MapFrom(d => d.Image))
        .ForMember(dest => dest.Title, opt => opt.MapFrom(d => d.Type == ScanTypes.Passport ? TitleTypesEnum.Passport : d.Type == ScanTypes.Snils ? TitleTypesEnum.SNILS : TitleTypesEnum.DriversLicense));
    }

    private static void GetViewModel(ref IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<ScanFile, DisplayScanVM>()
        .ForMember(dest => dest.Type, opt => opt.MapFrom(d => d.Title == TitleTypesEnum.Passport ? ScanTypes.Passport : d.Title == TitleTypesEnum.SNILS ? ScanTypes.Snils : ScanTypes.DriverLicence))
        .ForMember(dest => dest.Image, opt => opt.MapFrom(d => Convert.ToBase64String(d.Link)));

        cfg.CreateMap<ScanFile, EditScanVM>()
        .ForMember(dest => dest.Image, opt => opt.MapFrom(d => d.Link))
        .ForMember(dest => dest.Type, opt => opt.MapFrom(d => d.Title == TitleTypesEnum.Passport ? ScanTypes.Passport : d.Title == TitleTypesEnum.SNILS ? ScanTypes.Snils : ScanTypes.DriverLicence));

        cfg.CreateMap<Passport, DisplayPassportVM>()
        .ForMember(dest => dest.Scans, opt => opt.MapFrom(d => mapper.Map<List<DisplayScanVM>>(d.Scans)));

        cfg.CreateMap<Passport, EditPassportVM>()
        .ForMember(dest => dest.Scans, opt => opt.MapFrom(d => mapper.Map<List<EditScanVM>>(d.Scans)));

        cfg.CreateMap<User, DisplayUserVM>()
        .ForMember(dest => dest.Passport, opt => opt.MapFrom(d => mapper.Map<DisplayPassportVM>(d.Passport)))
        .ForMember(dest => dest.AdditionalFile, opt => opt.MapFrom(d => mapper.Map<DisplayScanVM>(d.AdditionalFile)));

        cfg.CreateMap<Loan, DisplayLoanVM>()
        .ForMember(dest => dest.Status, opt => opt.MapFrom(d => d.Status == StatusTypesEnum.Approved ? LoanStatuses.Approved : d.Status == StatusTypesEnum.Denied ? LoanStatuses.Denied : LoanStatuses.InWaiting));

        cfg.CreateMap<User, EditUserVM>()
        .ForMember(dest => dest.AdditionalFile, opt => opt.MapFrom(d => mapper.Map<EditScanVM>(d.AdditionalFile)))
        .ForMember(dest => dest.Passport, opt => opt.MapFrom(d => mapper.Map<EditPassportVM>(d.Passport)));

        cfg.CreateMap<Loan, CreateLoanVM>();
    }
}