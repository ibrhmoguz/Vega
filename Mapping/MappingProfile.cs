using System.Linq;
using AutoMapper;
using Vega.Models;
using Vega.Resources;

namespace Vega.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API Resource
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Contact,
                           opt => opt.MapFrom(v => new ContactResource
                           {
                               Name = v.ContactName,
                               Phone = v.ContactPhone,
                               Mail = v.ContactMail
                           }))
                .ForMember(vr => vr.Features,
                           opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));

            // API Resource to Domain
            CreateMap<VehicleResource, Vehicle>()
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.ContactMail, opt => opt.MapFrom(vr => vr.Contact.Mail))
                .ForMember(v => v.Features,
                        opt => opt.MapFrom(vr => vr.Features.Select(id =>
                                                    new VehicleFeature
                                                    {
                                                        FeatureId = id
                                                    })));
        }
    }
}