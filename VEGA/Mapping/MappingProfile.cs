using AutoMapper;
using Vega.Controllers.Resources;
using Vega.Models;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.Metadata;

namespace Vega.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API Resources
            CreateMap<Make, MakeResources>();
            CreateMap<Make, KeyValuePairResources>();

            CreateMap<Model, KeyValuePairResources>();

            CreateMap<Feature, KeyValuePairResources>();

            CreateMap<Vehicle, SaveVehicleResources>()
               .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResources { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
               .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));

            CreateMap<Vehicle, VehicleResources>()
               .ForMember(vr => vr.Make, opt => opt.MapFrom(v =>v.Model.Make))
               .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResources { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
               .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => new KeyValuePairResources{ Id = vf.Feature.Id, Name = vf.Feature.Name })));




            // API Resources to  Domain
            CreateMap<SaveVehicleResources, Vehicle>()
             .ForMember(v => v.Id, opt => opt.Ignore())
             .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
             .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
             .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
             .ForMember(v => v.Features, opt => opt.Ignore())
             .AfterMap((vr, v) =>
             {

                 ////Remove Unselected Features
               
                var removedFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId));
                foreach (var f in removedFeatures)
                     v.Features.Remove(f);

                 //Add New Features

                 var addedFeatures = vr.Features.Where(id => !v.Features.Any(f => f.FeatureId == id)).Select(id => new VehicleFeature { FeatureId = id });
                 foreach (var f in addedFeatures )
                     v.Features.Add(f);
             });
        } 
         
    }
}
