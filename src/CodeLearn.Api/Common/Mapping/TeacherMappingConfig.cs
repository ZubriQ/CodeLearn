//namespace CodeLearn.Api.Common.Mapping;

//public class TeacherMappingConfig : IRegister
//{
//    public void Register(TypeAdapterConfig config)
//    {
//        config.NewConfig<(Guid Id, TeacherRequest Request), UpdateTeacherNameCommand>()
//            .Map(dest => dest.TeacherId, src => src.Id)
//            .Map(dest => dest, src => src.Request);

//        config.NewConfig<Teacher, TeacherResponse>()
//            .Map(dest => dest.TeacherId, src => src.Id.Value);
//    }
//}