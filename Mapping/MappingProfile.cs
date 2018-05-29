using System.Collections.Generic;
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
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.ContactMail, opt => opt.MapFrom(vr => vr.Contact.Mail))
                .ForMember(v => v.Features, opt => opt.Ignore())
                .AfterMap((vr, v) =>
                {
                    var removedFeatures = new List<VehicleFeature>();
                    foreach (var f in v.Features)
                    {
                        if (!vr.Features.Contains(f.FeatureId))
                        {
                            removedFeatures.Add(f);
                        }
                    }
                    foreach (var f in removedFeatures)
                        v.Features.Remove(f);

                    foreach (var id in vr.Features)
                    {
                        if (!v.Features.Any(x => x.FeatureId == id))
                        {
                            v.Features.Add(new VehicleFeature { FeatureId = id });
                        }
                    }
                });
        }
    }
}