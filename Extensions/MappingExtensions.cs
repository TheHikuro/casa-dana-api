using System.Reflection;

namespace CasaDanaAPI.Extensions
{
    public static class MappingExtensions
    {
        public static TTarget MapTo<TTarget>(this object source) where TTarget : class, new()
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var target = new TTarget();
            var sourceProps = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var targetProps = typeof(TTarget).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var sourceProp in sourceProps)
            {
                var targetProp = targetProps.FirstOrDefault(p => p.Name == sourceProp.Name && p.PropertyType == sourceProp.PropertyType);
                if (targetProp != null && targetProp.CanWrite)
                {
                    targetProp.SetValue(target, sourceProp.GetValue(source));
                }
            }
            return target;
        }

        public static void MapTo<TTarget>(this object source, TTarget target)
        {
            if (source == null || target == null) throw new ArgumentNullException();

            var sourceProps = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var targetProps = typeof(TTarget).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var sourceProp in sourceProps)
            {
                var targetProp = targetProps.FirstOrDefault(p => p.Name == sourceProp.Name && p.PropertyType == sourceProp.PropertyType);
                if (targetProp != null && targetProp.CanWrite)
                {
                    targetProp.SetValue(target, sourceProp.GetValue(source));
                }
            }
        }
    }
}